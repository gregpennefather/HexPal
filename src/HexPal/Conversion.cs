using System;
using System.Numerics;

namespace HexPal
{
    public static class Conversion
    {
        public static (float x, float y) HexToPosition(Hex hex, LayoutOrientation orientation, (float x, float y) size, (float x, float y) origin)
        {
            float x = (float)(orientation.f0 * hex.Q + orientation.f1 * hex.R) * size.x;
            float y = (float)(orientation.f2 * hex.Q + orientation.f3 * hex.R) * size.y;
            return (x + origin.x, y + origin.y);
        }

        public static Hex PositionToHex((float x, float y) position, LayoutOrientation orientation, (float x, float y) size, (float x, float y) origin)
        {
            (float x, float y) pt = ((position.x - origin.x) / size.x,
                            (position.y - origin.y) / size.y);
            double pq = orientation.b0 * pt.x + orientation.b1 * pt.y;
            double pr = orientation.b2 * pt.x + orientation.b3 * pt.y;

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
    }
}