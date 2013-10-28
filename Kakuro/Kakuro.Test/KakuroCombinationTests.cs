namespace Kakuro.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class KakuroCombinationTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GivenOneSpaceExpectArgumentOutOfRangeException()
        {
            // arrange
            const short Number = 8;
            const short Spaces = 1;

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GivenZeroNumberExpectArgumentOutOfRangeException()
        {
            // arrange
            const short Number = 0;
            const short Spaces = 2;

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces);
        }

        [TestMethod]
        public void GivenExclusionsOutsideNormExpectLeftOut()
        {
            var expectedExclusions = new[] { -1, 2, 50 };

            // arrange
            const short Number = 5;
            const short Spaces = 2;

            var actualCombos = new KakuroCombinationCollection(Number, Spaces, expectedExclusions);

            var actualExclusions = new bool[9];
            actualCombos.Exclusions.CopyTo(actualExclusions, 0);

            Assert.AreEqual(9, actualCombos.Exclusions.Count);
            Assert.AreEqual(1, actualExclusions.Where(e => e).Count());
            Assert.IsTrue(actualCombos.Exclusions[1]);
        }        

        [TestMethod]
        public void MoreThanTwoSpacesUniqueCombination()
        {
            // arrange
            const short Number = 24;
            const short Spaces = 3;
            var expectedCombos = new List<short[]> { new short[] { 9, 8, 7 } };

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces);

            // assert
            AssertCombinations(Spaces, expectedCombos, actualCombos.ToList());
            CollectionAssert.AreEquivalent(expectedCombos[0], actualCombos[0]);
        }

        [TestMethod]
        public void GivenUniqueCombinationWhenEnumeratedExpectOneItem()
        {
            // arrange
            const short Number = 4;
            const short Spaces = 2;

            var actualCombos = new KakuroCombinationCollection(Number, Spaces).AsEnumerable();

            var kakuroEnumerator = actualCombos.GetEnumerator();
            var moved = 0;
            while (kakuroEnumerator.MoveNext())
            {
                Assert.IsNotNull(kakuroEnumerator.Current);
                moved++;
            }

            Assert.AreEqual(1, moved);        
        }

        [TestMethod]
        public void MoreThanTwoSpacesNonUniqueCombination()
        {
            // arrange
            const short Number = 8;
            const short Spaces = 3;
            var expectedCombos = new List<short[]> { new short[] { 5, 2, 1 }, new short[] { 4, 3, 1 } };

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces);

            // assert
            AssertCombinations(Spaces, expectedCombos, actualCombos.ToList());
        }

        [TestMethod]
        public void NumberWithMultipleCombinations()
        {
            // arrange
            const short Number = 5;
            const short Spaces = 2;
            var expectedCombos = new List<short[]> { new short[] { 4, 1 }, new short[] { 3, 2 } };

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces);

            // assert
            AssertCombinations(Spaces, expectedCombos, actualCombos.ToList());
            Assert.AreEqual(0, actualCombos.LowerIndex);
            Assert.AreEqual(1, actualCombos.UpperIndex);
        }

        [TestMethod]
        public void NumberWithMultipleCombinationsWithExclusions()
        {
            // arrange
            const short Number = 5;
            const short Spaces = 2;
            var expectedCombos = new List<short[]> { new short[] { 4, 1 } };
            var exclusions = new[] { 3 };

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces, exclusions);

            // assert
            AssertCombinations(Spaces, expectedCombos, actualCombos.ToList());
        }

        [TestMethod]
        public void NumberWithMultipleCombinationsWithExclusionsForSecondAddend()
        {
            // arrange
            const short Number = 5;
            const short Spaces = 2;
            var expectedCombos = new List<short[]> { new short[] { 3, 2 } };
            var exclusions = new[] { 1 };

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces, exclusions);

            // assert
            AssertCombinations(Spaces, expectedCombos, actualCombos.ToList());
        }

        [TestMethod]
        public void NumberWithUniqueCombination()
        {
            // arrange
            const short Number = 4;
            const short Spaces = 2;
            var expectedCombos = new List<short[]> { new short[] { 3, 1 } };

            // act
            var actualCombos = new KakuroCombinationCollection(Number, Spaces);

            // assert
            AssertCombinations(Spaces, expectedCombos, actualCombos.ToList());
        }

        #endregion

        #region Methods

        private static void AssertCombinations(
            short spaces, 
            IReadOnlyList<short[]> expected, 
            IReadOnlyList<short[]> actual)
        {
            // assert
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(spaces, actual[i].Length);
                CollectionAssert.AreEqual(expected[i], actual[i]);
            }
        }

        #endregion
    }
}