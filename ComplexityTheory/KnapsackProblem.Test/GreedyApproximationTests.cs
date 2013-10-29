namespace KnapsackProblem.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;
    using KnapsackProblem;

    [TestClass]
    public class GreedyApproximationTests
    {
        private KnapsackSolver knapsackSolver = new KnapsackSolver();

        /// <summary>
        /// http://en.wikipedia.org/wiki/Knapsack_problem
        /// </summary>
        [TestMethod]
        public void GreedyApproximationUsingWikipediaAnswer()
        {
            // arrange
            const int MaxWeight = 15;
            var possibleSupplies = new List<Supply>()
                                       {
                                           new Supply(12, 4),
                                           new Supply(2, 2),
                                           new Supply(1, 2),
                                           new Supply(4, 10),
                                           new Supply(1, 1)
                                       };
            const int ExpectedContentValue = 36;
            const int ExpectedWeight = MaxWeight;
            var expectedSupplies = new List<Supply>
                                       {
                                           new Supply(4, 10),
                                           new Supply(4, 10),
                                           new Supply(4, 10),
                                           new Supply(1, 2),
                                           new Supply(1, 2),
                                           new Supply(1, 2)
                                       };

            // act
            var knapsack = this.knapsackSolver.SolveProblemUsing(
                KnapsackAlgorithmType.GreedyApproximation,
                possibleSupplies,
                MaxWeight);

            // assert
            Assert.AreEqual(ExpectedContentValue, knapsack.ContentValue);
            Assert.AreEqual(ExpectedWeight, knapsack.CurrentWeight);            
        }
    }
}
