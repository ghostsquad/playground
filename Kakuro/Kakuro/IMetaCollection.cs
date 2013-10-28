namespace Kakuro
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for Permutations, Combinations and any other classes that present
    ///     a collection of collections based on an input collection.  The enumerators that
    ///     this class inherits defines the mechanism for enumerating through the collections.
    /// </summary>
    /// <typeparam name="T">The of the elements in the collection, not the type of the collection.</typeparam>
    public interface IMetaCollection<T> : IEnumerable<T>
    {
        #region Public Properties

        /// <summary>
        ///     The count of items in the collection.  This is not inherited from
        ///     ICollection since this meta-collection cannot be extended by users.
        /// </summary>
        int Count { get; }

        int LowerIndex { get; }

        int UpperIndex { get; }

        #endregion

        // Indexer declaration: 
        #region Public Indexers

        T this[int index] { get; }

        #endregion        
    }
}