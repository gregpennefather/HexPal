using System;
using System.Collections.Generic;
using System.Linq;

namespace HexPal
{
    public interface IMapLayer
    {
        Dictionary<Hex, float> PathingSnapshot { get; }

        IList<Hex> Tiles { get; }
    }

    public interface IUnpathedMapLayer : IMapLayer
    {

    }

    public interface IPathableMapLayer : IMapLayer
    {
    }

    public struct PathedTile<T>
    {
        public bool Pathable { get; }

        public int Weight { get; }

        public T Value { get; }

        public PathedTile(bool pathable, int weight, T value)
        {
            Pathable = pathable;
            Weight = weight;
            Value = value;
        }
    }

    public class MapLayer<T> : IUnpathedMapLayer
    {
        private Dictionary<Hex, float> _pathingSnapshot = new Dictionary<Hex, float>();
        public Dictionary<Hex, T> Store { get; set; } = new Dictionary<Hex, T>();

        public Dictionary<Hex, float> PathingSnapshot => _pathingSnapshot;

        public IList<Hex> Tiles => Store.Keys.ToList();

        public T this[Hex key]
        {
            get => Store[key];
            set
            {
                if (value != null)
                {
                    Store[key] = value;
                    _pathingSnapshot[key] = 1;
                }
                else if (Store.ContainsKey(key))
                {
                    Store.Remove(key);
                    _pathingSnapshot.Remove(key);
                }
            }
        }
    }

    public class PathedMapLayer<T> : IPathableMapLayer
    {
        private Dictionary<Hex, float> _pathingSnapshot = new Dictionary<Hex, float>();

        public Dictionary<Hex, PathedTile<T>> Store { get; set; } = new Dictionary<Hex, PathedTile<T>>();

        public Dictionary<Hex, float> PathingSnapshot => _pathingSnapshot;

        public IList<Hex> Tiles => Store.Keys.ToList();

        public Nullable<PathedTile<T>> this[Hex key]
        {
            get => Store[key];
            set
            {
                if (value.HasValue)
                {
                    var pathedTile = value.Value;
                    Store[key] = pathedTile;
                    if (pathedTile.Pathable)
                    {
                        _pathingSnapshot[key] = pathedTile.Weight;
                    }
                    else
                    {
                        _pathingSnapshot.Remove(key);
                    }
                }
                else if (Store.ContainsKey(key))
                {
                    Store.Remove(key);
                    _pathingSnapshot.Remove(key);
                }
            }
        }
    }

}