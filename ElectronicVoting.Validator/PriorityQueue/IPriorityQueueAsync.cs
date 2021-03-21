using System.Threading.Tasks;

namespace ElectronicVoting.Validator.PriorityQueue
{
    public interface IPriorityQueueAsync<T>
    {
        public T Pop();
        public bool IsEmpty();
        public void Push(T node, PriorityMessage priority = PriorityMessage.Normal);

        public delegate void ReadingNodesDelegate();
    }
}