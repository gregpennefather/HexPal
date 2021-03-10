using System.Collections.Generic;
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

        public IList<Hex> Navigate(Hex start, Hex goal)
        {
            return Pathfinder.AStar(start, goal, Terrain.PathingSnapshot);
        }

        public IList<Hex> Navigate(Hex start, IList<Hex> goals)
        {
            return Pathfinder.AStar(start, goals, Terrain.PathingSnapshot);
        }
    }
}