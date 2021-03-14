using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HexPal
{
    public class TerrainMap
    {
        public IPathableMapLayer Terrain { get; }

        public LayoutOrientation Orientation { get; }

        public (float x, float y) Size { get; }

        public (float x, float y) Origin { get; }

        public (float x, float y) HexToPosition(Hex hex) => Conversion.HexToPosition(hex, Orientation, Size, Origin);

        public Hex PositionToHex((float x, float y) position) => Conversion.PositionToHex(position, Orientation, Size, Origin);

        public TerrainMap(LayoutOrientation orientation, (float x, float y) size, (float x, float y) origin, IPathableMapLayer terrtainLayer)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
            Terrain = terrtainLayer;
        }

        public IList<Hex> Navigate(Hex start, Hex goal, IList<Hex> unpathableHexes = null)
        {

            return Navigate(start, new List<Hex>() { goal }, unpathableHexes);
        }

        public IList<Hex> Navigate(Hex start, IList<Hex> goals, IList<Hex> unpathableHexes = null)
        {
            unpathableHexes = unpathableHexes ?? new List<Hex>();

            var pathingInfo = new Dictionary<Hex, float>();

            foreach (var pathSnapshot in Terrain.PathingSnapshot)
            {
                if (!unpathableHexes.Contains(pathSnapshot.Key))
                {
                    pathingInfo[pathSnapshot.Key] = pathSnapshot.Value;
                }
            }

            return Pathfinder.AStar(start, goals, pathingInfo);
        }

        public IList<Hex> Range(Hex start, float range, IEnumerable<Hex> unpathableHexes = null) {
            unpathableHexes = unpathableHexes ?? new List<Hex>();

            var pathingInfo = new Dictionary<Hex, float>();

            foreach (var pathSnapshot in Terrain.PathingSnapshot)
            {
                if (!unpathableHexes.Contains(pathSnapshot.Key))
                {
                    pathingInfo[pathSnapshot.Key] = pathSnapshot.Value;
                }
            }

            return Pathfinder.Range(start, range, pathingInfo);
        }
    }
}