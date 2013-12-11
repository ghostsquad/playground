namespace Zillow
{
    /// <summary>
    ///     The trinary tree node.
    /// </summary>
    public class TrinaryTreeNode
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrinaryTreeNode"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public TrinaryTreeNode(int data)
        {
            this.Data = data;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public int Data { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public TrinaryTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets the middle.
        /// </summary>
        public TrinaryTreeNode Middle { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public TrinaryTreeNode Right { get; set; }

        #endregion
    }
}