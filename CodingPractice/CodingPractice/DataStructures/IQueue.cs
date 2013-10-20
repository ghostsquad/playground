namespace CodingPractice.DataStructures
{
    using System.Collections;

    public interface IQueue<T> : ICollection
    {
        void Enqueue(T obj);

        T Dequeue();

        T Peek();
    }
}