using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zillow.Test
{
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TrinaryTreeTests
    {
        private int[] valuesToAdd = new int[] { 4, 9, 5, 7, 2, 2 };

        // adding nodes to the tree in this order 5,4,9,5,7,2,2
        // results in a tree that looks like this:
        //             5
        //         /   |   \
        //        4    5    9
        //       /         /
        //       2        7
        //       |
        //       2
        [TestMethod]
        public void GivenZillowExampleWhenInsert()
        {
            // arrange
            var tree = new TrinaryTree(5);            

            // act
            foreach (var value in this.valuesToAdd)
            {
                tree.Insert(value);
            }

            // assert
            Assert.AreEqual(5, tree.RootNode.Data);
            Assert.AreEqual(4, tree.RootNode.Left.Data);
            Assert.AreEqual(2, tree.RootNode.Left.Left.Data);
            Assert.AreEqual(2, tree.RootNode.Left.Left.Middle.Data);

            Assert.AreEqual(5, tree.RootNode.Middle.Data);

            Assert.AreEqual(9, tree.RootNode.Right.Data);
            Assert.AreEqual(7, tree.RootNode.Right.Left.Data);
        }

        [TestMethod]
        public void GivenZillowExampleWhenDeleteRoot()
        {
            // arrange
            //             5
            //         /   |   \
            //        4    5    9
            //       /         /
            //       2        7
            //       |
            //       2
            var startTree = GetBasicTree();

            // create expected tree
            //             5
            //         /      \
            //        4        9
            //       /        /
            //       2       7
            //       |
            //       2
            var expectedTree = new TrinaryTree(5);
            expectedTree.RootNode.Left = new TrinaryTreeNode(4);
            expectedTree.RootNode.Left.Left = new TrinaryTreeNode(2);
            expectedTree.RootNode.Left.Left.Middle = new TrinaryTreeNode(2);
            expectedTree.RootNode.Right = new TrinaryTreeNode(9);
            expectedTree.RootNode.Right.Left = new TrinaryTreeNode(7);

            // act
            expectedTree.Delete(expectedTree.RootNode);

            // assert

        }

        [TestMethod]
        public void GivenZillowExampleWhenDeleteLeftFirst()
        {
            // arrange
            //             5
            //         /   |   \
            //        4    5    9
            //       /         /
            //       2        7
            //       |
            //       2
            var startTree = GetBasicTree();

            // create expected tree
            //             5
            //         /   |   \
            //        2    5    9
            //        |        /
            //        2       7            
            var expectedTree = new TrinaryTree(5);
            expectedTree.RootNode.Left = new TrinaryTreeNode(4);
            expectedTree.RootNode.Left.Left = new TrinaryTreeNode(2);
            expectedTree.RootNode.Left.Left.Middle = new TrinaryTreeNode(2);
            expectedTree.RootNode.Middle = new TrinaryTreeNode(5);
            expectedTree.RootNode.Right = new TrinaryTreeNode(9);
            expectedTree.RootNode.Right.Left = new TrinaryTreeNode(7);

            // act
            startTree.Delete(startTree.RootNode);

            // assert

        }

        private TrinaryTree GetBasicTree()
        {
            var tree = new TrinaryTree(5);
            tree.RootNode.Left = new TrinaryTreeNode(4);
            tree.RootNode.Left.Left = new TrinaryTreeNode(2);
            tree.RootNode.Left.Left.Middle = new TrinaryTreeNode(2);
            tree.RootNode.Middle = new TrinaryTreeNode(5);
            tree.RootNode.Right = new TrinaryTreeNode(9);
            tree.RootNode.Right.Left = new TrinaryTreeNode(7);

            return tree;
        }
    }
}
