namespace WesToolbox.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        #region Fields

        private readonly Dictionary<byte, Queue<T>> objectStorage;

        private readonly List<byte> priorityIndex;

        private bool isSorted = true;

        #endregion

        #region Constructors and Destructors

        public PriorityQueue(byte defaultPriority)
        {
            this.priorityIndex = new List<byte>();
            this.objectStorage = new Dictionary<byte, Queue<T>>();
            this.DefaultPriority = defaultPriority;
        }

        public PriorityQueue()
            : this(0)
        {
        }

        #endregion

        #region Public Properties

        public int Count { get; private set; }

        public byte DefaultPriority { get; set; }

        public bool IsSynchronized { get; private set; }

        public object SyncRoot { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array is null.");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index is less than 0.");
            }

            int spaceRemaining = array.Length - index;

            if (spaceRemaining < this.Count)
            {
                throw new ArgumentException("not enough space in array.");
            }

            this.GetAllQueueObjectsInOrder().ToArray().CopyTo(array, index);
        }

        public T Dequeue()
        {
            Queue<T> prioritizedQueue = this.GetHighestPrioritizedQueue();
            T theDequeued = prioritizedQueue.Dequeue();

            this.Count--;

            if (prioritizedQueue.Count == 0)
            {
                this.objectStorage.Remove(this.priorityIndex[0]);
                this.priorityIndex.RemoveAt(0);
            }

            return theDequeued;
        }

        public void Enqueue(T obj)
        {
            this.Enqueue(this.DefaultPriority, obj);
        }

        public void Enqueue(byte priority, T obj)
        {
            this.Count++;
            Queue<T> thePrioritizedQueue;
            if (this.objectStorage.TryGetValue(priority, out thePrioritizedQueue))
            {
                thePrioritizedQueue.Enqueue(obj);
            }
            else
            {
                this.priorityIndex.Add(priority);
                thePrioritizedQueue = new Queue<T>();
                thePrioritizedQueue.Enqueue(obj);
                this.objectStorage.Add(priority, thePrioritizedQueue);
            }

            this.isSorted = false;
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetAllQueueObjectsInOrder().GetEnumerator();
        }

        public T Peek()
        {
            Queue<T> prioritizedQueue = this.GetHighestPrioritizedQueue();
            return prioritizedQueue.Peek();
        }

        #endregion

        #region Methods

        private List<T> GetAllQueueObjectsInOrder()
        {
            var queuedObjects = new List<T>();
            this.SortPriorityIndexIfNecessary();
            foreach (byte priority in this.priorityIndex)
            {
                queuedObjects.AddRange(this.objectStorage[priority]);
            }

            return queuedObjects;
        }

        private Queue<T> GetHighestPrioritizedQueue()
        {
            if (this.priorityIndex.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            this.SortPriorityIndexIfNecessary();

            byte highestPriority = this.priorityIndex[0];
            return this.objectStorage[highestPriority];
        }

        private void SortPriorityIndexIfNecessary()
        {
            if (!this.isSorted)
            {
                this.priorityIndex.Sort();
                this.isSorted = true;
            }
        }

        #endregion
    }
}