namespace CodingPractice.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        #region Fields

        private readonly Dictionary<byte, Queue<T>> objectStorage;

        private readonly List<byte> priorityIndex;

        #endregion

        #region Constructors and Destructors

        public PriorityQueue(byte defaultPriority)
        {
            this.priorityIndex = new List<byte>();
            this.objectStorage = new Dictionary<byte, Queue<T>>();
            this.DefaultPriority = defaultPriority;
        }

        public PriorityQueue() : this(0)
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
            throw new NotImplementedException();
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
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();            
        }

        public T Peek()
        {
            Queue<T> prioritizedQueue = this.GetHighestPrioritizedQueue();
            return prioritizedQueue.Peek();
        }

        #endregion

        #region Methods

        private Queue<T> GetHighestPrioritizedQueue()
        {
            if (this.priorityIndex.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            this.priorityIndex.Sort();
            byte highestPriority = this.priorityIndex[0];
            return this.objectStorage[highestPriority];
        }

        #endregion
    }
}