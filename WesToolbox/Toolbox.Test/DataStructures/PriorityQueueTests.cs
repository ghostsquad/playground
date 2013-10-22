namespace WesToolbox.Test.DataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using WesToolbox.DataStructures;
    using WesToolbox.HelperClasses;

    [TestClass]
    public class PriorityQueueTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GivenEmptyQueueWhenGetEnumeratorExpectNullOnFirstNext()
        {
            var priorityQueue = new PriorityQueue<object>();
            var theEnumerator = priorityQueue.GetEnumerator();
            Assert.IsFalse(theEnumerator.MoveNext());
        }

        [TestMethod]
        public void GivenPrioritizedQueueWhenGetEnumeratorExpectCorrectOrder()
        {
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<string>();
            List<Tuple<byte, string>> orderedPriorityTuples;
            EnqueuePrioritizedObjects(priorityQueue, fixture, out orderedPriorityTuples);
            var theEnumerator = priorityQueue.GetEnumerator();

            // assert that the objects are dequeue in the order
            var objectIndex = 0;
            while (theEnumerator.MoveNext())
            {
                Assert.AreSame(orderedPriorityTuples[objectIndex].Item2, theEnumerator.Current);
                objectIndex++;
            }

            Assert.AreEqual(objectIndex, orderedPriorityTuples.Count);
        }

        [TestMethod]
        public void GivenDefaultPriorityWhenDequeueExpectCorrectOrder()
        {
            // arrange
            var fixture = new Fixture();
            const byte DefaultPriority = 2;
            var priorityQueue = new PriorityQueue<string>(DefaultPriority);
            var defaultPriEnqueue = fixture.Create<string>();
            var higherPriorityEnqueue = fixture.Create<string>();
            priorityQueue.Enqueue(defaultPriEnqueue);
            priorityQueue.Enqueue(0, higherPriorityEnqueue);

            // act
            var dequeue1 = priorityQueue.Dequeue();
            var dequeue2 = priorityQueue.Dequeue();

            // assert
            Assert.AreEqual(DefaultPriority, priorityQueue.DefaultPriority);
            Assert.AreSame(higherPriorityEnqueue, dequeue1);
            Assert.AreSame(defaultPriEnqueue, dequeue2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenEmptyQueueWhenDequeueExpectException()
        {
            // arrange
            var priorityQueue = new PriorityQueue<object>();

            // act
            priorityQueue.Dequeue();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenEmptyQueueWhenPeekExpectException()
        {
            // arrange
            var priorityQueue = new PriorityQueue<object>();

            // act
            priorityQueue.Peek();
        }

        [TestMethod]
        public void GivenEnqueueDequeueWhenCountExpectNumberOfObjects()
        {
            // enqueue count test
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<string>();
            var objectToEnqueue = fixture.Create<string>();
            priorityQueue.Enqueue(objectToEnqueue);

            // act
            int queueCount = priorityQueue.Count;

            // assert
            Assert.AreEqual(1, queueCount);

            // dequeue count test
            // act
            var dequeuedObject = priorityQueue.Dequeue();
            queueCount = priorityQueue.Count;

            // assert
            Assert.AreEqual(0, queueCount);
            Assert.AreSame(objectToEnqueue, dequeuedObject);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenHighIndexNotEnoughRoomWhenCopyToExpectArgumentException()
        {
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<object>();
            var stringsToEnqueue = fixture.CreateMany<string>();
            var stringCount = 0;
            foreach (var fixtureString in stringsToEnqueue)
            {
                stringCount++;
                priorityQueue.Enqueue(fixtureString);
            }
            var theArray = new object[stringCount];
            priorityQueue.CopyTo(theArray, 1);
        }

        [TestMethod]
        public void GivenLargerDestinationArrayWhenCopyToExpectSuccess()
        {
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<string>();
            priorityQueue.Enqueue(fixture.Create<string>());
            var theArray = new string[2];
            priorityQueue.CopyTo(theArray, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GivenNegativeIndexWhenCopyToArgumentOutOfRangeException()
        {
            var priorityQueue = new PriorityQueue<object>();
            var theArray = new object[0];
            priorityQueue.CopyTo(theArray, -1);
        }

        [TestMethod]
        public void GivenNewWhenCountExpectZero()
        {
            // arrange
            var priorityQueue = new PriorityQueue<object>();

            // act
            int queueCount = priorityQueue.Count;

            // assert
            Assert.AreEqual(0, queueCount);
        }

        [TestMethod]
        public void GivenNonPrioritizedEnqueuesWhenDequeuedExpectEnqueueOrder()
        {
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<string>();
            var obj1 = fixture.Create<string>();
            var obj2 = fixture.Create<string>();
            priorityQueue.Enqueue(obj1);
            priorityQueue.Enqueue(obj2);

            // act
            object dequeue1 = priorityQueue.Dequeue();
            object dequeue2 = priorityQueue.Dequeue();

            // assert
            Assert.AreSame(obj1, dequeue1);
            Assert.AreSame(obj2, dequeue2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenNullArrayWhenCopyToArgumentNullException()
        {
            var priorityQueue = new PriorityQueue<object>();
            priorityQueue.CopyTo(null, 0);
        }

        [TestMethod]
        public void GivenPrioritizedEnqueuesWhenDequeuedExpectOrderedByPriority()
        {
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<string>();
            List<Tuple<byte, string>> orderedPriorityTuples;
            EnqueuePrioritizedObjects(priorityQueue, fixture, out orderedPriorityTuples);

            // assert that the objects are dequeue in the order
            foreach (var sortedObject in orderedPriorityTuples)
            {
                object theDequeued = priorityQueue.Dequeue();
                Assert.AreSame(sortedObject.Item2, theDequeued);
            }
        }

        [TestMethod]
        public void GivenPrioritizedEnqueuesWhenPeekExpectOrderedByPriority()
        {
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<string>();
            List<Tuple<byte, string>> orderedPriorityTuples;
            EnqueuePrioritizedObjects(priorityQueue, fixture, out orderedPriorityTuples);

            Assert.AreSame(orderedPriorityTuples[0].Item2, priorityQueue.Peek());
        }

        [TestMethod]
        public void GivenSameSizeDestinationArrayWhenCopyToExpectSuccess()
        {
            var priorityQueue = new PriorityQueue<object>();
            priorityQueue.Enqueue(new object());
            var theArray = new object[1];
            priorityQueue.CopyTo(theArray, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenSmallerDestinationArrayWhenCopyToArgumentException()
        {
            var priorityQueue = new PriorityQueue<object>();
            priorityQueue.Enqueue(new object());
            var theArray = new object[0];
            priorityQueue.CopyTo(theArray, 0);
        }

        #endregion

        #region Methods

        private static void EnqueuePrioritizedObjects(
            IPriorityQueue<string> priorityQueue, 
            ISpecimenBuilder fixture, 
            out List<Tuple<byte, string>> priorityTuples)
        {
            priorityTuples = fixture.CreateMany<Tuple<byte, string>>().ToList();

            // first lets shuffle them up 
            while (priorityTuples.ToList().IsSorted())
            {
                priorityTuples.Sort((tuple, tuple1) => Guid.NewGuid().CompareTo(Guid.NewGuid()));
            }

            // add to the priority queue in a shuffled state
            foreach (var tuple in priorityTuples)
            {
                priorityQueue.Enqueue(tuple.Item1, tuple.Item2);
            }

            // sort the objects
            priorityTuples.Sort((tuple, tuple1) => tuple.Item1.CompareTo(tuple1.Item1));
        }

        #endregion
    }
}