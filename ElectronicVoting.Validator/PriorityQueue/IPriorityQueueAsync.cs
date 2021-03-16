using System.Threading.Tasks;

namespace ElectronicVoting.PriorityQueue
{
    public interface IPriorityQueueAsync<T>
    {
        public Task<T> Pop();
        public bool IsEmpty();
        public void Push(T node, PriorityMessage priority = PriorityMessage.Normal);

        public delegate void ReadingNodesDelegate();
    }
}