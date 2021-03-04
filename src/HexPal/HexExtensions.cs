namespace HexPal
{
    public static class HexExtensions
    {
        public static int DistanceTo(this Hex hex, Hex other) {
            return (hex - other).Length;
        }

        public static Hex Neighbour(this Hex hex, int position) {
            return hex + Hex.Directions[position];
        }
    }
}