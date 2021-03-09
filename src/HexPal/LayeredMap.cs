using System;
using System.Collections.Generic;
using System.Numerics;

namespace HexPal
{
    public class LayeredMap
    {
        private IList<IMapLayer> _layers = new List<IMapLayer>();

        public LayoutOrientation Orientation { get; }

        public Vector2 Size { get; }
        public Vector2 Origin { get; }

        public IMapLayer this[int key]
        {
            get => _layers[key];
        }

        public LayeredMap(LayoutOrientation orientation, Vector2 size, Vector2 origin, IPathableMapLayer mapLayer = null)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
            if (mapLayer != null)
            {
                 _layers.Add(mapLayer);
            }
        }

        public void AddLayer(IUnpathedMapLayer layer)
        {
            _layers.Add(layer);
        }

        public IList<Hex> Navigate(Hex start, Hex goal)
        {
            return Pathfinder.AStar(start, goal, CalculatedTileWeights());
        }

        public IList<Hex> Navigate(Hex start, IList<Hex> goals)
        {
            return Pathfinder.AStar(start, goals, CalculatedTileWeights());
        }

        public Vector2 HexToPosition(Hex hex)
        {
            float x = (float)(Orientation.f0 * hex.Q + Orientation.f1 * hex.R) * Size.X;
            float y = (float)(Orientation.f2 * hex.Q + Orientation.f3 * hex.R) * Size.Y;
            return new Vector2(x + Origin.X, y + Origin.Y);
        }

        public Hex PositionToHex(Vector2 position)
        {
            var pt = new Vector2((position.X - Origin.X) / Size.X,
                            (position.Y - Origin.Y) / Size.Y);
            double pq = Orientation.b0 * pt.X + Orientation.b1 * pt.Y;
            double pr = Orientation.b2 * pt.X + Orientation.b3 * pt.Y;

            var fHex = new FractionalHex(pq, pr);

            int q = (int)(Math.Round(fHex.Q));
            int r = (int)(Math.Round(fHex.R));
            int s = (int)(Math.Round(fHex.S));
            double q_diff = Math.Abs(q - fHex.Q);
            double r_diff = Math.Abs(r - fHex.R);
            double s_diff = Math.Abs(s - fHex.S);
            if (q_diff > r_diff && q_diff > s_diff)
            {
                q = -r - s;
            }
            else if (r_diff > s_diff)
            {
                r = -q - s;
            }
            else
            {
                s = -q - r;
            }
            return new Hex(q, r, s);
        }

        internal Dictionary<Hex, float> CalculatedTileWeights()
        {
            var pathInfo = new Dictionary<Hex, float>();

            var pathingLayer = (IPathableMapLayer)_layers[0];

            foreach (var tile in pathingLayer.PathingSnapshot)
            {
                pathInfo[tile.Key] = tile.Value;
            }

            for (int i = 1; i < _layers.Count; i++)
            {
                foreach (var tile in _layers[i].PathingSnapshot)
                {
                    pathInfo.Remove(tile.Key);
                }
            }

            return pathInfo;
        }
    }
}