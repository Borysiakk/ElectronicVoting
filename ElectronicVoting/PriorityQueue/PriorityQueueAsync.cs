
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicVoting.PriorityQueue
{
    public sealed class PriorityQueueAsync<T> :IPriorityQueueAsync<T> where T:ItemPriorityQueue
    {
        private Task _autoReadTask;
        private CancellationToken _cancellationToken;
        private readonly List<ItemPriorityQueue> _heap;

        public Action ActionAutoReadNode { get;set; }
        
        public PriorityQueueAsync()
        {
            _heap = new List<ItemPriorityQueue>();
            _cancellationToken = new CancellationToken();
        }

        public async Task<T> Pop()
        {
            return await Task.Run(() =>
            {
                ItemPriorityQueue item = _heap[0];
                _heap[0] = _heap[^1];
                _heap.RemoveAt(_heap.Count - 1);
                Repair();
                return (T) item;
            }, _cancellationToken);

        }

        public void Push(T node, Priority priority = Priority.Normal)
        {
            int count = _heap.Count;
            node.Priority = priority;
            _heap.Add(node);

            while (count != 0 && _heap[Parent(count)].Priority <_heap[count].Priority)
            {
                ItemPriorityQueue item = _heap[count];
                _heap[count] = _heap[Parent(count)];
                _heap[Parent(count)] = item;
                count = Parent(count);
            }
            
            if (_autoReadTask == null || _autoReadTask.Status == TaskStatus.RanToCompletion)
            {
                _autoReadTask = new Task(ActionAutoReadNode,_cancellationToken);
            }

            if (_autoReadTask.Status == TaskStatus.Created)
            {
                _autoReadTask.Start();
            }

            if (_autoReadTask.Status != TaskStatus.Running && _autoReadTask.Status != TaskStatus.WaitingToRun)
            {
                _autoReadTask.Wait(_cancellationToken);
            }
        }

        private void Repair(int i = 0)
        {
            int temp = i;
            int left = Left(i);
            int right = Right(i);

            if (left < _heap.Count && _heap[left].Priority > _heap[temp].Priority)
            {
                temp = left;
            }
            
            if (right < _heap.Count && _heap[right].Priority > _heap[temp].Priority)
            {
                temp = right;
            }

            if (temp != i)
            {
                ItemPriorityQueue item = _heap[temp];
                _heap[temp] = _heap[i];
                _heap[i] = item;
                
                Repair(temp);
            }
        }

        public bool IsEmpty()
        {
            return _heap.Count == 0 ? true : false;
        }
        
        private int Parent(int i)
        {
            return (i - 1) / 2;
        }

        private int Left(int i)
        {
            return (2 * i) + 1;
        }

        private int Right(int i)
        {
            return (2 * i) + 2;
        }
    }
}