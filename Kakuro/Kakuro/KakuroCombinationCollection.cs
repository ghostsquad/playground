namespace Kakuro
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class KakuroCombinationCollection : IMetaCollection<short[]>
    {
        #region Fields

        private readonly List<short[]> combinations = new List<short[]>();

        #endregion

        #region Constructors and Destructors

        public KakuroCombinationCollection(short sum, short spaces)
            : this(sum, spaces, null)
        {
        }

        public KakuroCombinationCollection(short sum, short spaces, IEnumerable<int> exclusions)
        {
            if (sum < 3)
            {
                throw new ArgumentOutOfRangeException("sum");
            }

            if (spaces < 2)
            {
                throw new ArgumentOutOfRangeException("spaces");
            }

            this.Exclusions = new BitArray(9, false);

            if (exclusions != null)
            {
                int[] myExclusions = exclusions.Where(i => i > 0 && i < 10).Distinct().ToArray();

                foreach (int exclusion in myExclusions)
                {
                    this.Exclusions[exclusion - 1] = true;
                }
            }

            this.Sum = sum;
            this.Spaces = spaces;
            this.Initialize();
            this.LowerIndex = 0;
            this.UpperIndex = Math.Max(this.combinations.Count - 1, 0);
        }

        #endregion

        #region Public Properties

        public int Count { get; private set; }

        public BitArray Exclusions { get; private set; }

        public int LowerIndex { get; private set; }

        public short Spaces { get; private set; }

        public short Sum { get; private set; }

        public int UpperIndex { get; private set; }

        #endregion

        #region Public Indexers

        public short[] this[int index]
        {
            get
            {
                return this.combinations[index];
            }
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerator<short[]> GetEnumerator()
        {
            return this.combinations.GetEnumerator();
        }

        #endregion

        #region Explicit Interface Methods

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Methods

        private bool GetCombinations(int desiredSum, int spaces, int? maxNextAddend = null, short[] currentCombo = null)
        {
            if (currentCombo == null)
            {
                currentCombo = new short[spaces];
            }

            for (int addend = Math.Min(desiredSum - 1, maxNextAddend ?? 9); addend > 1; addend--)
            {
                if (this.Exclusions[addend - 1])
                {
                    continue;
                }

                int comboMaxIndex = currentCombo.GetUpperBound(0);

                if (spaces < 3)
                {
                    int secondAddend = desiredSum - addend;
                    if (secondAddend >= addend || secondAddend < 1)
                    {
                        return false;
                    }

                    if (this.Exclusions[secondAddend - 1])
                    {
                        continue;
                    }

                    currentCombo[comboMaxIndex - 1] = (short)addend;
                    currentCombo[comboMaxIndex] = (short)secondAddend;
                    this.combinations.Add((short[])currentCombo.Clone());

                    int dif = addend - secondAddend;
                    if (dif <= 2)
                    {
                        return true;
                    }
                }
                else
                {
                    currentCombo[comboMaxIndex - (spaces - 1)] = (short)addend;
                    if (!this.GetCombinations(desiredSum - addend, spaces - 1, addend - 1, currentCombo))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Initialize()
        {
            this.GetCombinations(this.Sum, this.Spaces);
            this.Count = this.combinations.Count;
        }

        #endregion
    }
}