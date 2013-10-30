namespace Sorting.Test
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InPlaceQuickSorterTests
    {
        [TestMethod]
        public void GivenSinglePopulationListExpectNoChange()
        {
            var expectedList = new List<int>() { 1 };
            var actualList = new List<int>(expectedList);            

            actualList.InPlaceQuickSort();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GivenReversedList()
        {
            var expectedList = new List<int>() { 1, 2 };
            var unsortedList = new List<int>() { 2, 1 };
            var actualList = new List<int>(unsortedList);             

            actualList.InPlaceQuickSort();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GivenEndPointsReversedList()
        {
            var expectedList = new List<int>() { 1, 2, 3, 5 };
            var unsortedList = new List<int>() { 5, 2, 3, 1 };
            var actualList = new List<int>(unsortedList);           

            actualList.InPlaceQuickSort();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GivenUnSortedList()
        {
            var expectedList = new List<int>() { 1, 2, 3, 5 };
            var unsortedList = new List<int>() { 5, 1, 3, 2 };
            var actualList = new List<int>(unsortedList);           

            actualList.InPlaceQuickSort();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GivenSortedList()
        {
            var expectedList = new List<int>() { 1, 2, 3, 5 };
            var actualList = new List<int>(expectedList); 

            actualList.InPlaceQuickSort();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GivenUnSortedListWithDuplicates()
        {
            var expectedList = new List<int>() { 1, 2, 2, 3, 5, 10 };
            var unsortedList = new List<int>() { 5, 1, 10, 2, 3, 2 };
            var actualList = new List<int>(unsortedList);

            actualList.InPlaceQuickSort();

            CollectionAssert.AreEqual(expectedList, actualList);
        }
    }
}
