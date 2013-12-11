namespace Zillow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TrinaryTree
    {
        public void Insert(int value)
        {
            this.addNode(value, this.RootNode);
        }

        private void addNode(int data, TrinaryTreeNode root)
        {            
            if (root.Data == data)
            {
                if (root.Middle == null)
                {
                    root.Middle = new TrinaryTreeNode(data);
                    return;
                }

                this.addNode(data, root.Middle);
            }
            else if (data < root.Data)
            {
                if (root.Left == null)
                {
                    root.Left = new TrinaryTreeNode(data);
                    return;
                }

                this.addNode(data, root.Left);
            }
            else
            {
                if (root.Right == null)
                {
                    root.Right = new TrinaryTreeNode(data);
                    return;
                }

                this.addNode(data, root.Right);
            }
        }

        public void Delete(TrinaryTreeNode value)
        {
            throw new NotImplementedException();
        }

        public TrinaryTree(int value)
        {
            this.RootNode = new TrinaryTreeNode(value);
        }

        public TrinaryTreeNode RootNode { get; private set; }
    }
}
