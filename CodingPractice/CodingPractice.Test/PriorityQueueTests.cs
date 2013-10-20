namespace CodingPractice.Test
{
    using System;

    using CodingPractice.DataStructures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ploeh.AutoFixture;

    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void GivenNewWhenCountExpectZero()
        {
            // arrange
            var priorityQueue = new PriorityQueue<object>();

            // act
            var queueCount = priorityQueue.Count;

            // assert
            Assert.AreEqual<int>(0, queueCount);
        }

        [TestMethod]
        public void GivenEnqueueDequeueWhenCountExpectNumberOfObjects()
        {
            // enqueue count test
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<object>();
            var objectToEnqueue = fixture.Create<object>();
            priorityQueue.Enqueue(objectToEnqueue);

            // act
            var queueCount = priorityQueue.Count;

            // assert
            Assert.AreEqual<int>(1, queueCount);

            // dequeue count test
            // act
            var dequeuedObject = priorityQueue.Dequeue();
            queueCount = priorityQueue.Count;

            // assert
            Assert.AreEqual<int>(0, queueCount);
            Assert.AreSame(objectToEnqueue, dequeuedObject);
        }

        [TestMethod]
        public void GivenNonPrioritizedEnqueuesWhenDequeuedExpectEnqueueOrder()
        {
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<object>();
            var obj1 = fixture.Create<object>();
            var obj2 = fixture.Create<object>();
            priorityQueue.Enqueue(obj1);
            priorityQueue.Enqueue(obj2);

            // act
            var dequeue1 = priorityQueue.Dequeue();
            var dequeue2 = priorityQueue.Dequeue();

            // assert
            Assert.AreSame(obj1, dequeue1);
            Assert.AreSame(obj2, dequeue2);
        }

        [TestMethod]
        public void GivenPrioritizedEnqueuesWhenDequeuedExpectOrderedByPriority()
        {
            // arrange
            var fixture = new Fixture();
            var priorityQueue = new PriorityQueue<object>();
            var objPri4 = fixture.Create<object>();
            var objPri1 = fixture.Create<object>();
            var objPri1_1 = fixture.Create<object>();
            var objPri2 = fixture.Create<object>();
            priorityQueue.Enqueue(4, objPri4);
            priorityQueue.Enqueue(1, objPri1);            
            priorityQueue.Enqueue(2, objPri2);
            priorityQueue.Enqueue(1, objPri1_1);

            // act
            var dequeue1 = priorityQueue.Dequeue();
            var dequeue2 = priorityQueue.Dequeue();
            var dequeue3 = priorityQueue.Dequeue();
            var dequeue4 = priorityQueue.Dequeue();

            // assert
            Assert.AreSame(objPri1, dequeue1);
            Assert.AreSame(objPri1_1, dequeue2);
            Assert.AreSame(objPri2, dequeue3);
            Assert.AreSame(objPri4, dequeue4);
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenEmptyQueueWhenDequeueExpectException()
        {
            // arrange
            var priorityQueue = new PriorityQueue<object>();

            // act
            priorityQueue.Dequeue();
        }

        [TestMethod]
        public void GivenDefaultPriorityWhenDequeueExpectCorrectOrder()
        {
            // arrange
            var fixture = new Fixture();
            const byte DefaultPriority = 2;
            var priorityQueue = new PriorityQueue<object>(DefaultPriority);
            var defaultPriEnqueue = fixture.Create<object>();
            var higherPriorityEnqueue = fixture.Create<object>();
            priorityQueue.Enqueue(defaultPriEnqueue);
            priorityQueue.Enqueue(0, higherPriorityEnqueue);

            // act
            var dequeue1 = priorityQueue.Dequeue();
            var dequeue2 = priorityQueue.Dequeue();

            // assert
            Assert.AreEqual<byte>(DefaultPriority, priorityQueue.DefaultPriority);
            Assert.AreSame(higherPriorityEnqueue, dequeue1);
            Assert.AreSame(defaultPriEnqueue, dequeue2);
        }
    }
}
