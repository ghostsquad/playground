namespace WesToolbox.DataStructures
{
    using System;
    using System.Collections.Generic;

    public class Map<T1, T2> : IMap<T1, T2>
    {
        #region Fields

        private readonly Dictionary<T1, T2> forwardDictionary;

        private readonly Dictionary<T2, T1> reverseDictionary;

        #endregion

        #region Constructors and Destructors

        public Map()
        {
            this.forwardDictionary = new Dictionary<T1, T2>();
            this.reverseDictionary = new Dictionary<T2, T1>();
        }

        #endregion

        #region Public Properties

        public int Count { get; private set; }

        public ICollection<T1> Keys
        {
            get
            {
                return this.forwardDictionary.Keys;
            }            
        }

        public ICollection<T2> Values
        {
            get
            {
                return this.reverseDictionary.Keys;
            }
        }

        #endregion

        #region Public Methods and Operators

        public bool ContainsKey(T1 t1)
        {
            return this.forwardDictionary.ContainsKey(t1);
        }

        public bool ContainsValue(T2 t2)
        {
            return this.reverseDictionary.ContainsKey(t2);
        }

        public void ForwardAdd(T1 t1, T2 t2)
        {
            this.AddItem(t1, t2);
        }

        public T2 ForwardItem(T1 t1)
        {
            return this.forwardDictionary[t1];
        }

        public void ForwardRemove(T1 t1)
        {
            T2 t2 = this.forwardDictionary[t1];
            this.forwardDictionary.Remove(t1);
            this.reverseDictionary.Remove(t2);
        }

        public void ReverseAdd(T2 t2, T1 t1)
        {
            this.AddItem(t1, t2);
        }

        public T1 ReverseItem(T2 t2)
        {
            return this.reverseDictionary[t2];
        }

        public void ReverseRemove(T2 t2)
        {
            T1 t1 = this.reverseDictionary[t2];
            this.forwardDictionary.Remove(t1);
            this.reverseDictionary.Remove(t2);
        }

        public bool TryForwardLookup(T1 t1, out T2 t2)
        {
            bool keyFound = this.forwardDictionary.ContainsKey(t1);

            t2 = keyFound ? this.forwardDictionary[t1] : default(T2);

            return keyFound;
        }

        public bool TryReverseLookup(T2 t2, out T1 t1)
        {
            bool keyFound = this.reverseDictionary.ContainsKey(t2);

            t1 = keyFound ? this.reverseDictionary[t2] : default(T1);

            return keyFound;
        }

        #endregion

        #region Methods

        private void AddItem(T1 t1, T2 t2)
        {
            if (this.forwardDictionary.ContainsKey(t1))
            {
                throw new ArgumentException("Key already exists.");
            }

            if (this.reverseDictionary.ContainsKey(t2))
            {
                throw new ArgumentException("Value already exists.");
            }

            this.forwardDictionary.Add(t1, t2);
            this.reverseDictionary.Add(t2, t1);
        }

        #endregion
    }
}