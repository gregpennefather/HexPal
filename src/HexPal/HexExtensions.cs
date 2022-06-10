using System;

namespace HexPal
{
    public static class HexExtensions
    {
        public static int DistanceTo(this Hex hex, Hex other)
        {
            return (hex - other).Length;
        }

        public static Hex DirectionTo(this Hex hex, Hex other)
        {
            var diff = other - hex;
            return diff.Normalize();
        }

        public static Hex Normalize(this Hex hex)
        {
            var q = Math.Max(Math.Min(hex.Q, 1), -1);
            var s = Math.Max(Math.Min(hex.S, 1), -1);
            return new Hex(q, -q - s, s);
        }

        public static Hex Neighbour(this Hex hex, int position)
        {
            return hex + Hex.Directions[position];
        }

        public static Hex[] Neighbours(this Hex hex)
        {
            var arr = new Hex[6];
            for (int i = 0; i < 6; i++)
            {
                arr[i] = hex.Neighbour(i);
            }
            return arr;
        }
    }
}