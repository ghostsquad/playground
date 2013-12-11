namespace Zillow.Test
{
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ploeh.AutoFixture;

    /// <summary>
    /// The trinary tree tests.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TrinaryTreeTests
    {
        #region Fields

        /// <summary>
        /// The values to add.
        /// </summary>
        private readonly int[] valuesToAdd = { 4, 9, 5, 7, 2, 2 };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The given empty tree when delete expect no change.
        /// </summary>
        [TestMethod]
        public void GivenEmptyTreeWhenDeleteExpectNoChange()
        {
            // arrange
            var fixture = new Fixture();
            var nonExistantValue = fixture.Create<int>();
            var tree = new TrinaryTree();

            // act
            tree.Delete(nonExistantValue);

            // assert
            Assert.IsNull(tree.RootNode);
        }

        /// <summary>
        /// The given identical numbers when delete expect last one removed.
        /// </summary>
        [TestMethod]
        public void GivenIdenticalNumbersWhenDeleteExpectLastOneRemoved()
        {
            // arrange            
            var fixture = new Fixture();
            var valueToAdd = fixture.Create<int>();
            var tree = new TrinaryTree(valueToAdd);
            tree.RootNode.Middle = new TrinaryTree.TrinaryTreeNode(valueToAdd);
            tree.RootNode.Middle.Middle = new TrinaryTree.TrinaryTreeNode(valueToAdd);

            // act
            tree.Delete(valueToAdd);

            // assert
            Assert.AreEqual(valueToAdd, tree.RootNode.Data);
            tree.RootNode.AssertLeftIsNull().AssertRightIsNull();

            Assert.AreEqual(valueToAdd, tree.RootNode.Middle.Data);
            AssertNoChildren(tree.RootNode.Middle);
        }

        /// <summary>
        /// The given identical numbers when insert expect all put in middle.
        /// </summary>
        [TestMethod]
        public void GivenIdenticalNumbersWhenInsertExpectAllPutInMiddle()
        {
            // arrange
            var fixture = new Fixture();
            var valueToAdd = fixture.Create<int>();
            var dups = new[] { valueToAdd, valueToAdd, valueToAdd };
            var tree = new TrinaryTree();

            // act
            foreach (int value in dups)
            {
                tree.Insert(value);
            }

            // assert
            Assert.AreEqual(dups[0], tree.RootNode.Data);
            tree.RootNode.AssertLeftIsNull().AssertRightIsNull();

            Assert.AreEqual(dups[0], tree.RootNode.Middle.Data);
            tree.RootNode.Middle.AssertLeftIsNull().AssertRightIsNull();

            Assert.AreEqual(dups[0], tree.RootNode.Middle.Middle.Data);
            AssertNoChildren(tree.RootNode.Middle.Middle);
        }

        /// <summary>
        /// The given left only children when delete expect left reassigned to parent.
        /// </summary>
        [TestMethod]
        public void GivenLeftOnlyChildrenWhenDeleteExpectLeftReassignedToParent()
        {
            // arrange
            // 5
            // /   |   \
            // 4    5    9
            // /         /
            // 2        7
            // |
            // 2
            TrinaryTree startTree = this.GetBasicTree();

            // create expected tree
            // 5
            // /   |   \
            // 2    5    9
            // |        /
            // 2       7            
            var expectedTree = new TrinaryTree(5);
            expectedTree.RootNode.Left = new TrinaryTree.TrinaryTreeNode(4);
            expectedTree.RootNode.Left.Left = new TrinaryTree.TrinaryTreeNode(2);
            expectedTree.RootNode.Left.Left.Middle = new TrinaryTree.TrinaryTreeNode(2);
            expectedTree.RootNode.Middle = new TrinaryTree.TrinaryTreeNode(5);
            expectedTree.RootNode.Right = new TrinaryTree.TrinaryTreeNode(9);
            expectedTree.RootNode.Right.Left = new TrinaryTree.TrinaryTreeNode(7);

            // act
            startTree.Delete(4);

            // assert
            Assert.AreEqual(5, startTree.RootNode.Data);
            Assert.AreEqual(2, startTree.RootNode.Left.Data);
            Assert.AreEqual(2, startTree.RootNode.Left.Middle.Data);

            Assert.AreEqual(5, startTree.RootNode.Middle.Data);

            Assert.AreEqual(9, startTree.RootNode.Right.Data);
            Assert.AreEqual(7, startTree.RootNode.Right.Left.Data);
        }

        /// <summary>
        /// The given node in left spot when is left expect true.
        /// </summary>
        [TestMethod]
        public void GivenNodeInLeftSpotWhenIsLeftExpectTrue()
        {
            // arrange            
            var tree = new TrinaryTree(1);
            tree.RootNode.Left = new TrinaryTree.TrinaryTreeNode(0);

            // act
            bool actual = tree.RootNode.Left.IsLeft(tree.RootNode);

            // assert
            Assert.IsTrue(actual);
            Assert.IsFalse(tree.RootNode.Left.IsRight(tree.RootNode));
            Assert.IsFalse(tree.RootNode.Left.IsMiddle(tree.RootNode));
        }

        /// <summary>
        /// The given node in middle spot when is middle expect true.
        /// </summary>
        [TestMethod]
        public void GivenNodeInMiddleSpotWhenIsMiddleExpectTrue()
        {
            // arrange            
            var tree = new TrinaryTree(1);
            tree.RootNode.Middle = new TrinaryTree.TrinaryTreeNode(1);

            // act
            bool actual = tree.RootNode.Middle.IsMiddle(tree.RootNode);

            // assert
            Assert.IsTrue(actual);
            Assert.IsFalse(tree.RootNode.Middle.IsLeft(tree.RootNode));
            Assert.IsFalse(tree.RootNode.Middle.IsRight(tree.RootNode));
        }

        /// <summary>
        /// The given node in right spot when is right expect true.
        /// </summary>
        [TestMethod]
        public void GivenNodeInRightSpotWhenIsRightExpectTrue()
        {
            // arrange            
            var tree = new TrinaryTree(1);
            tree.RootNode.Right = new TrinaryTree.TrinaryTreeNode(2);

            // act
            bool actual = tree.RootNode.Right.IsRight(tree.RootNode);

            // assert
            Assert.IsTrue(actual);
            Assert.IsFalse(tree.RootNode.Right.IsLeft(tree.RootNode));
            Assert.IsFalse(tree.RootNode.Right.IsMiddle(tree.RootNode));
        }

        /// <summary>
        /// The given right only children when delete expect min from right subtree as replacement 2.
        /// </summary>
        [TestMethod]
        public void GivenRightOnlyChildrenWhenDeleteExpectMinFromRightSubtreeAsReplacement2()
        {
            // arrange
            // create start tree
            // 5 
            // \
            // 7 <-- to remove
            // \
            // 12
            // /  |  \
            // replacement -->  9   12  14
            // \
            // 10
            var startTree = new TrinaryTree(5);
            startTree.RootNode.Right = new TrinaryTree.TrinaryTreeNode(7);
            startTree.RootNode.Right.Right = new TrinaryTree.TrinaryTreeNode(12);
            startTree.RootNode.Right.Right.Left = new TrinaryTree.TrinaryTreeNode(9);
            startTree.RootNode.Right.Right.Left.Right = new TrinaryTree.TrinaryTreeNode(10);
            startTree.RootNode.Right.Right.Middle = new TrinaryTree.TrinaryTreeNode(12);
            startTree.RootNode.Right.Right.Right = new TrinaryTree.TrinaryTreeNode(14);

            // act
            startTree.Delete(7);

            // expected tree
            // 5 
            // \
            // 12
            // /  |  \
            // 9   12  14
            // \
            // 10        

            // assert
            Assert.AreEqual(5, startTree.RootNode.Data);
            startTree.RootNode.AssertLeftIsNull().AssertMiddleIsNull();

            Assert.AreEqual(12, startTree.RootNode.Right.Data);

            Assert.AreEqual(14, startTree.RootNode.Right.Right.Data);
            AssertNoChildren(startTree.RootNode.Right.Right);

            Assert.AreEqual(12, startTree.RootNode.Right.Middle.Data);
            AssertNoChildren(startTree.RootNode.Right.Middle);

            Assert.AreEqual(9, startTree.RootNode.Right.Left.Data);
            startTree.RootNode.Right.Left.AssertLeftIsNull().AssertMiddleIsNull();

            Assert.AreEqual(10, startTree.RootNode.Right.Left.Right.Data);
            AssertNoChildren(startTree.RootNode.Right.Left.Right);
        }

        /// <summary>
        /// The given zillow example when delete root expect middle replacement.
        /// </summary>
        [TestMethod]
        public void GivenZillowExampleWhenDeleteRootExpectMiddleReplacement()
        {
            // arrange
            // 5
            // /   |   \
            // 4    5    9
            // /         /
            // 2        7
            // |
            // 2
            TrinaryTree startTree = this.GetBasicTree();

            // create expected tree
            // 5
            // /      \
            // 4        9
            // /        /
            // 2       7
            // |
            // 2
            var expectedTree = new TrinaryTree(5);
            expectedTree.RootNode.Left = new TrinaryTree.TrinaryTreeNode(4);
            expectedTree.RootNode.Left.Left = new TrinaryTree.TrinaryTreeNode(2);
            expectedTree.RootNode.Left.Left.Middle = new TrinaryTree.TrinaryTreeNode(2);
            expectedTree.RootNode.Right = new TrinaryTree.TrinaryTreeNode(9);
            expectedTree.RootNode.Right.Left = new TrinaryTree.TrinaryTreeNode(7);

            // act
            expectedTree.Delete(5);

            // assert
        }

        /// <summary>
        /// The given zillow example when insert expect match example.
        /// </summary>
        [TestMethod]
        public void GivenZillowExampleWhenInsertExpectMatchExample()
        {
            // arrange
            var tree = new TrinaryTree(5);

            // adding nodes to the tree in this order 5,4,9,5,7,2,2
            // results in a tree that looks like this:
            // 5
            // /   |   \
            // 4    5    9
            // /         /
            // 2        7
            // |
            // 2

            // act
            foreach (int value in this.valuesToAdd)
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

        #endregion

        #region Methods

        /// <summary>
        /// The assert no children.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        private static void AssertNoChildren(TrinaryTree.TrinaryTreeNode node)
        {
            node.AssertLeftIsNull().AssertMiddleIsNull().AssertRightIsNull();
        }

        /// <summary>
        /// The get basic tree.
        /// </summary>
        /// <returns>
        /// The <see cref="TrinaryTree"/>.
        /// </returns>
        private TrinaryTree GetBasicTree()
        {
            var tree = new TrinaryTree(5);
            tree.RootNode.Left = new TrinaryTree.TrinaryTreeNode(4);
            tree.RootNode.Left.Left = new TrinaryTree.TrinaryTreeNode(2);
            tree.RootNode.Left.Left.Middle = new TrinaryTree.TrinaryTreeNode(2);
            tree.RootNode.Middle = new TrinaryTree.TrinaryTreeNode(5);
            tree.RootNode.Right = new TrinaryTree.TrinaryTreeNode(9);
            tree.RootNode.Right.Left = new TrinaryTree.TrinaryTreeNode(7);

            return tree;
        }

        #endregion
    }
}