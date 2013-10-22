namespace WesToolbox.DataStructures
{
    public interface IPriorityQueue<T> : IQueue<T>
    {
        byte DefaultPriority { get; set; }

        void Enqueue(byte priority, T obj);
    }
}