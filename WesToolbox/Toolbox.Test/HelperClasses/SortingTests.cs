namespace WesToolbox.Test.HelperClasses
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WesToolbox.HelperClasses;

    [TestClass]
    public class SortingTests
    {
        [TestMethod]
        public void GivenSortedListWhenIsSortedExpectTrue()
        {
            var sortedList = new List<int>();
            for (var i = 0; i < 3; i++)
            {
                sortedList.Add(i);
            }

            Assert.IsTrue(sortedList.IsSorted());
        }

        [TestMethod]
        public void GivenEmptyListWhenIsSortedExpectTrue()
        {
            var emptyList = new List<int>();
            Assert.IsTrue(emptyList.IsSorted());
        }

        [TestMethod]
        public void GivenUnSortedListWhenIsSortedExpectFalse()
        {
            var unSortedList = new List<int>();
            for (var i = 3; i >= 0; i--)
            {
                unSortedList.Add(i);
            }

            Assert.IsFalse(unSortedList.IsSorted());
        }
    }
}
