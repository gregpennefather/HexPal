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

        public static Hex[] Neighbours(this Hex hex) {
            var arr = new Hex[6];
            for(int i = 0; i < 6; i++) {
                arr[i] = hex.Neighbour(i);
            }
            return arr;
        }
    }
}