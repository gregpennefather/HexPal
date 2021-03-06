using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HexPalTest")]
namespace HexPal
{
    public class Hex : IEquatable<Hex>, IEqualityComparer<Hex>
    {
        public static Hex[] Directions = new[] { new Hex(1, 0), new Hex(0, 1), new Hex(-1, 1), new Hex(-1, 0), new Hex(0, -1), new Hex(1, -1) };

        private int[] vector;

        public int Q => vector[0];
        public int R => vector[1];
        public int S => vector[2];
        public int Length => (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;

        public Hex(int q, int r, int s)
        {
            if (q + r + s != 0)
            {
                throw new ArgumentException($"Invalid coords ({q},{r},{s})");
            }

            vector = new[] { q, r, s };
        }

        public Hex(int q, int r) : this(q, r, -q - r)
        {
        }

        public override string ToString()
        {
            return $"({Q},{R})";
        }

        public override bool Equals(object other) => other is Hex otherHex && this.Equals(otherHex);

        public bool Equals(Hex other)
        {
            return !object.ReferenceEquals(other, null) && (Q, R, S) == (other.Q, other.R, other.S);
        }

        public bool Equals(Hex x, Hex y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return Q ^ R ^ S; ;
        }

        public int GetHashCode([DisallowNull] Hex obj)
        {
            return obj.GetHashCode();
        }

        public static bool operator ==(Hex a, Hex b)
        {
            if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
            {
                return object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null);
            }
            return a.Equals(b);
        }

        public static bool operator !=(Hex a, Hex b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return !object.ReferenceEquals(b, null);
            }

            if (object.ReferenceEquals(b, null))
            {
                return !object.ReferenceEquals(a, null);
            }
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