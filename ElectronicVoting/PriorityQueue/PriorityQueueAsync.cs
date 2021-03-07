using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KolejkaPriorytetowa
{
    public class PriorityQueueAsync<T> where T:ItemPriorityQueue
    {
        private Task _task;
        public List<ItemPriorityQueue> Heap;
        private CancellationToken _cancellationToken;
        
        public PriorityQueueAsync()
        {
            Heap = new List<ItemPriorityQueue>();
            _cancellationToken = new CancellationToken();
            
            // _task = new Task(async() =>
            // {
            //     while (!IsEmpty())
            //     {
            //         var t = Pop();
            //         await Console.Out.WriteLineAsync(t.Priority.ToString());
            //     }
            // },_cancellationToken);
        }

        public T Pop()
        {
            ItemPriorityQueue item = Heap[0];
            Heap[0] = Heap[^1];
            Heap.RemoveAt(Heap.Count - 1);
            Repair();
            return (T) item;
        }

        public  void Push(T node, Priority priority = Priority.Normal)
        {
            int count = Heap.Count;
            node.Priority = priority;
            Heap.Add(node);

            while (count != 0 && Heap[Parent(count)].Priority <Heap[count].Priority)
            {
                ItemPriorityQueue item = Heap[count];
                Heap[count] = Heap[Parent(count)];
                Heap[Parent(count)] = item;
                count = Parent(count);
            }

            // if (_task.Status == TaskStatus.Created)
            // {
            //     _task.Start();
            // }
            //
            // if (_task.Status != TaskStatus.Running && _task.Status != TaskStatus.WaitingToRun)
            // {
            //     _task.Wait(_cancellationToken);
            // }
        }

        private void Repair(int i = 0)
        {
            int temp = i;
            int left = Left(i);
            int right = Right(i);

            if (left < Heap.Count && Heap[left].Priority > Heap[temp].Priority)
            {
                temp = left;
            }
            
            if (right < Heap.Count && Heap[right].Priority > Heap[temp].Priority)
            {
                temp = right;
            }

            if (temp != i)
            {
                ItemPriorityQueue item = Heap[temp];
                Heap[temp] = Heap[i];
                Heap[i] = item;
                
                Repair(temp);
            }
        }

        public bool IsEmpty()
        {
            return Heap.Count == 0 ? true : false;
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