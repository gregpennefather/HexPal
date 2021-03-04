using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HexPal
{
    public struct FractionalHex : IEquatable<FractionalHex>, IEqualityComparer<FractionalHex>
    {

        private double[] vector;

        public double Q => vector[0];
        public double R => vector[1];
        public double S => vector[2];
        public double Length => (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;

        public FractionalHex(double q, double r, double s)
        {
            if (q+r+s != 0) {
                throw new ArgumentException($"Invalid coords ({q},{r},{s})");
            }

            vector = new[] { q, r, s };
        }

        public FractionalHex(double q, double r) : this(q, r, -q-r)
        {
        }


        public override bool Equals(object other) => other is FractionalHex otherHex && this.Equals(otherHex);

        public bool Equals(FractionalHex other) {
            return (Q,R,S) == (other.Q, other.R, other.S);
        }

        public bool Equals(FractionalHex x, FractionalHex y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return (int)Math.Pow(Math.Pow(Q, R), S);
        }

        public int GetHashCode([DisallowNull] FractionalHex obj)
        {
            return obj.GetHashCode();
        }

        public static bool operator ==(FractionalHex a, FractionalHex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(FractionalHex a, FractionalHex b)
        {
            return !a.Equals(b);
        }
        public static FractionalHex operator +(FractionalHex a, FractionalHex b)
        {
            return new FractionalHex(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static FractionalHex operator -(FractionalHex a, FractionalHex b)
        {
            return new FractionalHex(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        public static FractionalHex operator *(FractionalHex a, double m)
        {
            return new FractionalHex(a.Q * m, a.R * m, a.S * m);
        }
    }
}