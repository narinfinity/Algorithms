using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardPermuts
{
    public class Permutation :
        ICloneable,
        IComparable,
        System.Collections.IEnumerable,
        IComparable<Permutation>,
        IEquatable<Permutation>,
        IEnumerable<int>
    {
        private int[] data;   // The arrangement for the current rank.
        private long rank;    // Row index of the table of permutations.

        #region Constructors

        public Permutation()
        {
            this.data = new int[0];
            this.rank = 0;
        }


        public Permutation(Permutation source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            this.data = new int[source.data.Length];
            source.data.CopyTo(this.data, 0);

            this.rank = source.rank;
        }


        public Permutation(int width)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException("width", "Value is less than zero.");

            if (width > MaxWidth)
                throw new ArgumentOutOfRangeException("width", "Value is greater than maximum allowed.");

            this.data = new int[width];
            for (int ei = 0; ei < width; ++ei)
                this.data[ei] = ei;

            this.rank = 0;
        }


        public Permutation(int width, long rank)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException("width", "Value is less than zero.");

            if (width > MaxWidth)
                throw new ArgumentOutOfRangeException("width", "Value is greater than maximum allowed.");

            this.data = new int[width];
            Rank = rank;
        }


        public Permutation(int[] source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Length > MaxWidth)
                throw new ArgumentOutOfRangeException("source", "Too many values.");

            this.data = new int[source.Length];
            source.CopyTo(this.data, 0);

            bool[] isUsed = new bool[Width];
            for (int ei = 0; ei < Width; ++ei)
            {
                if (this.data[ei] < 0 || this.data[ei] >= Width)
                    throw new ArgumentOutOfRangeException("source", "Value is out of range.");

                if (isUsed[this.data[ei]])
                    throw new ArgumentException("Value is repeated.", "source");
                isUsed[this.data[ei]] = true;
            }

            this.rank = CalcRank(this.data);
        }


        private static long CalcRank(int[] elements)
        {
            //
            // Perform ranking:
            //

            long newRank = 0;
            bool[] isUsed = new bool[elements.Length];

            for (int ei1 = 0; ei1 < elements.Length; ++ei1)
            {
                isUsed[elements[ei1]] = true;

                long digit = 0;
                for (int ei2 = 0; ei2 < elements[ei1]; ++ei2)
                    if (!isUsed[ei2])
                        ++digit;

                newRank += digit * Factorial(elements.Length - ei1 - 1);
            }
            return newRank;
        }

        #endregion

        #region Properties

        public long Height
        {
            get
            {
                return data.Length == 0
                ? 0
                : Factorial(data.Length);
            }
        }


        public long Rank
        {
            get
            {
                return rank;
            }
            set
            {
                if (Height == 0)
                    return;

                // Normalize the new rank.
                if (value < 0)
                {
                    value = value % Height;
                    if (value < 0)
                        value += Height;
                }
                else
                    if (value >= Height)
                    value = value % Height;

                rank = value;

                //
                // Perform unranking:
                //

                bool[] isUsed = new bool[data.Length];
                int[] factoradic = new int[data.Length];

                // Build the factoradic from the diminishing rank.
                for (int fi = data.Length - 1; fi >= 0; --fi)
                    factoradic[fi] = (int)Math.DivRem(value, Factorial(fi), out value);

                // Build the permutation from the diminishing factoradic.
                for (int fi = data.Length - 1; fi >= 0; --fi)
                    for (int newAtom = 0; ; ++newAtom)
                        if (!isUsed[newAtom])
                            if (factoradic[fi] > 0)
                                --factoradic[fi];
                            else
                            {
                                data[data.Length - fi - 1] = newAtom;
                                isUsed[newAtom] = true;
                                break;
                            }
            }
        }


        public int Width
        {
            get { return data.Length; }
        }


        public int this[int index]
        { get { return data[index]; } }

        #endregion

        #region Member methods

        public int Backtrack(int nodeIndex)
        {
            if (nodeIndex < 0 || nodeIndex >= Width)
                throw new ArgumentOutOfRangeException("nodeIndex", "Value is out of range.");

            Array.Sort(this.data, nodeIndex + 1, Width - nodeIndex - 1);
            for (var tailIndex = nodeIndex + 1; tailIndex < Width; ++tailIndex)
            {
                int swap = this.data[tailIndex];
                if (swap > this.data[nodeIndex])
                {
                    this.data[tailIndex] = this.data[nodeIndex];
                    this.data[nodeIndex] = swap;
                    this.rank = CalcRank(this.data);
                    return nodeIndex;
                }
            }

            for (; ; )
            {
                if (--nodeIndex < 0)
                    return nodeIndex;

                int tailIndex = nodeIndex + 1;
                int tail = this.data[tailIndex];

                for (; ; )
                {
                    if (++tailIndex == Width)
                        if (tail < this.data[nodeIndex])
                        {
                            this.data[tailIndex - 1] = tail;
                            break;
                        }
                        else
                        {
                            this.data[tailIndex - 1] = this.data[nodeIndex];
                            this.data[nodeIndex] = tail;
                            this.rank = CalcRank(this.data);
                            return nodeIndex;
                        }
                    if (this.data[tailIndex] < this.data[nodeIndex])
                        this.data[tailIndex - 1] = this.data[tailIndex];
                    else
                    {
                        this.data[tailIndex - 1] = this.data[nodeIndex];
                        this.data[nodeIndex] = this.data[tailIndex];
                        while (++tailIndex < Width)
                            this.data[tailIndex - 1] = this.data[tailIndex];
                        this.data[tailIndex - 1] = tail;
                        this.rank = CalcRank(this.data);
                        return nodeIndex;
                    }
                }
            }
        }


        public object Clone()
        { return new Permutation(this); }


        public int CompareTo(object obj)
        { return CompareTo(obj as Permutation); }


        public int CompareTo(Permutation other)
        {
            if ((object)other == null)
                return 1;

            int result = this.Width - other.Width;

            if (result == 0)
                if (this.Rank > other.Rank)
                    result = 1;
                else if (this.Rank < other.Rank)
                    result = -1;

            return result;
        }


        public void CopyTo(int[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (array.Length < this.data.Length)
                throw new ArgumentException("Destination array is not long enough.");

            this.data.CopyTo(array, 0);
        }

        public override bool Equals(object obj)
        { return Equals(obj as Permutation); }


        public bool Equals(Permutation other)
        {
            return (object)other != null && other.Rank == Rank && other.Width == Width;
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return GetEnumerator(); }


        public IEnumerator<int> GetEnumerator()
        {
            for (int ei = 0; ei < Width; ++ei)
                yield return this[ei];
        }


        public override int GetHashCode()
        { return unchecked((int)Rank); }


        public IEnumerable<Permutation> GetRows()
        {
            if (Height > 0)
            {
                long startRank = Rank;

                for (Permutation current = (Permutation)MemberwiseClone(); ;)
                {
                    yield return current;

                    current.Rank = current.Rank + 1;
                    if (current.Rank == startRank)
                        break;
                }
            }
        }


        public IEnumerable<Permutation> GetRowsForAllWidths()
        {
            for (int w = 1; w <= Width; ++w)
            {
                Permutation current = (Permutation)MemberwiseClone();

                current.data = new int[w];
                for (int ei = 0; ei < current.data.Length; ++ei)
                    current.data[ei] = ei;
                current.rank = 0;

                for (; ; )
                {
                    yield return current;

                    current.Rank = current.Rank + 1;
                    if (current.Rank == 0)
                        break;
                }
            }
        }


        public override string ToString()
        {
            if (Height == 0)
                return ("{ }");

            StringBuilder result = new StringBuilder("{ ");

            for (int ei = 0; ;)
            {
                result.Append(this[ei]);

                ++ei;
                if (ei >= Width)
                    break;

                result.Append(", ");
            }

            result.Append(" }");

            return result.ToString();
        }

        #endregion

        #region Static methods

        public static List<T> Permute<T>(Permutation arrangement, IList<T> source)
        {
            if (arrangement == null)
                throw new ArgumentNullException("arrangement");

            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Count < arrangement.Width)
                throw new ArgumentException("Not enough supplied values.", "source");

            List<T> result = new List<T>(arrangement.Width);

            for (int ai = 0; ai < arrangement.Width; ++ai)
                result.Add(source[arrangement[ai]]);

            return result;
        }


        public static bool operator ==(Permutation param1, Permutation param2)
        {
            if ((object)param1 == null)
                return (object)param2 == null;
            else
                return param1.Equals(param2);
        }


        public static bool operator !=(Permutation param1, Permutation param2)
        { return !(param1 == param2); }


        public static bool operator <(Permutation param1, Permutation param2)
        {
            if ((object)param1 == null)
                return (object)param2 != null;
            else
                return param1.CompareTo(param2) < 0;
        }


        public static bool operator >=(Permutation param1, Permutation param2)
        { return !(param1 < param2); }


        public static bool operator >(Permutation param1, Permutation param2)
        {
            if ((object)param1 == null)
                return false;
            else
                return param1.CompareTo(param2) > 0;
        }

        public static bool operator <=(Permutation param1, Permutation param2)
        { return !(param1 > param2); }

        static long Factorial(int n)
        {
            return factorials[n];
            // long f = 1;
            // while (n > 1)
            // {
            //     f *= n;
            //     --n;
            // }
            // return f;
        }

        static public int MaxWidth
        { get { return factorials.Length - 1; } }

        #endregion
        static long[] factorials = new long[] {
            1,
            1,
            2,
            6,
            24,
            120,
            720,
            5040,
            40320,
            362880,
            3628800,
            39916800,
            479001600,
            6227020800,
            87178291200,
            1307674368000,
            20922789888000,
            355687428096000,
            6402373705728000,
            121645100408832000,
            2432902008176640000
        };
    }
}
