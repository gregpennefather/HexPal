using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HexPal
{
    public struct Hex : IEquatable<Hex>, IEqualityComparer<Hex>
    {
        private int[] vector;

        public int Q => vector[0];
        public int R => vector[1];
        public int S => vector[2];

        public Hex(int q, int r, int s)
        {
            if (q+r+s != 0) {
                throw new ArgumentException($"Invalid coords ({q},{r},{s})");
            }

            vector = new[] { q, r, s };
        }

        public Hex(int q, int r) : this(q, r, -q-r)
        {
        }

        public override bool Equals(object other) => other is Hex otherHex && this.Equals(otherHex);

        public bool Equals(Hex other) {
            return (Q,R,S) == (other.Q, other.R, other.S);
        }

        public bool Equals(Hex x, Hex y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return Q ^ R ^ S;;
        }

        public int GetHashCode([DisallowNull] Hex obj)
        {
            return obj.GetHashCode();
        }

        public static bool operator ==(Hex a, Hex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Hex a, Hex b)
        {
            return !a.Equals(b);
        }

        public static Hex operator +(Hex a, Hex b)
        {
            return new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static Hex operator -(Hex a, Hex b)
        {
            return new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        public static Hex operator *(Hex a, int m)
        {
            return new Hex(a.Q * m, a.R * m, a.S * m);
        }
    }
}