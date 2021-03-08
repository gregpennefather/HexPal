namespace HexPal
{
    public struct WeightedHex
    {
        public float Weight { get; }

        public Hex Hex { get; }

        public WeightedHex(float weight, Hex hex) {
            Weight = weight;
            Hex = hex;
        }
    }
}