using System;
using Microsoft.Xna.Framework;

namespace HexPal.Monogame
{
    public static class TerrainMapExtensions
    {
        public static Vector2 SizeVector(this TerrainMap terrainMap)
        {
            return new Vector2(terrainMap.Size.x, terrainMap.Size.y);
        }

        public static Vector2 OriginVector(this TerrainMap terrainMap)
        {
            return new Vector2(terrainMap.Origin.x, terrainMap.Origin.y);
        }

        public static Vector2 HexToPositionVector(this TerrainMap terrainMap, Hex hex)
        {
            (float x, float y) position = Conversion.HexToPosition(hex, terrainMap.Orientation, terrainMap.Size, terrainMap.Origin);
            return new Vector2(position.x, position.y);
        }

        public static Hex PositionVectorToHex(this TerrainMap terrainMap, Vector2 positionVector)
        {
            (float x, float y) position = (positionVector.X, positionVector.Y);
            return Conversion.PositionToHex(position, terrainMap.Orientation, terrainMap.Size, terrainMap.Origin);
        }
    }
}
