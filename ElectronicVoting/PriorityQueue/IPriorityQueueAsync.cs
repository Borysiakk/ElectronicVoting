using System.Threading.Tasks;

namespace ElectronicVoting.PriorityQueue
{
    public interface IPriorityQueueAsync<T>
    {
        public Task<T> Pop();
        public bool IsEmpty();
        public void Push(T node, Priority priority = Priority.Normal);

        public delegate void ReadingNodesDelegate();
    }
}