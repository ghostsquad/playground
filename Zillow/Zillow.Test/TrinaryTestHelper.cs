namespace Zillow.Test
{
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [ExcludeFromCodeCoverage]
    public static class TrinaryTestHelper
    {
        public static TrinaryTree.TrinaryTreeNode AssertLeftIsNull(this TrinaryTree.TrinaryTreeNode node)
        {
            Assert.IsNull(node.Left);
            return node;
        }

        public static TrinaryTree.TrinaryTreeNode AssertMiddleIsNull(this TrinaryTree.TrinaryTreeNode node)
        {
            Assert.IsNull(node.Middle);
            return node;
        }

        public static TrinaryTree.TrinaryTreeNode AssertRightIsNull(this TrinaryTree.TrinaryTreeNode node)
        {
            Assert.IsNull(node.Right);
            return node;
        }
    }
}