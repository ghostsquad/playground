namespace WesToolbox.DataStructures
{
    using System.Collections.Generic;

    public interface IMap<T1, T2>
    {
        #region Public Properties

        ICollection<T1> Keys { get; }

        ICollection<T2> Values { get; }

        int Count { get; }

        #endregion

        #region Public Methods and Operators

        bool ContainsKey(T1 t1);

        bool ContainsValue(T2 t2);

        void ForwardAdd(T1 t1, T2 t2);

        T2 ForwardItem(T1 t1);

        void ReverseAdd(T2 t2, T1 t1);

        T1 ReverseItem(T2 t2);

        void ForwardRemove(T1 t1);

        void ReverseRemove(T2 t2);

        bool TryForwardLookup(T1 t1, out T2 t2);

        bool TryReverseLookup(T2 t2, out T1 t1);

        #endregion
    }
}