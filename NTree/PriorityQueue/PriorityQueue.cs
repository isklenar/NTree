using System;
using System.Collections.Generic;
using NTree.Common;

namespace NTree.PriorityQueue
{
    public class PriorityQueue<V> where V : IComparable
    {
        private int _heapSize = 0;
        private V [] _heap = new V[10];

        public int HeapSize { get { return _heapSize; } }

        public void Add(V value)
        {
            _heapSize++;
            int current = _heapSize;
            if (_heap.Length <= _heapSize)
            {
                ResizeHeap();
            }
            while (current > 1 && _heap[current / 2].CompareTo(value) > 0)
            {
                _heap[current] = _heap[current / 2];
                current = current / 2;
            }
            _heap[current] = value;
        }

        public V PeekTop()
        {
            if (_heapSize == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            return _heap[1];
        }

        public V ExtractTop()
        {
            if (_heapSize == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            V max = _heap[1];
            _heap[1] = _heap[_heapSize];
            _heapSize--;
            Heapify(1);

            return max;
        }

        private void Heapify(int i)
        {
            int left = 2 * i;
            int right = 2 * i + 1;

            int smallest = i;

            if (left <= _heapSize && _heap[left].CompareTo(_heap[i]) < 0)
            {
                smallest = left;
            }

            if (right <= _heapSize && _heap[right].CompareTo(_heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                V tmp = _heap[i];
                _heap[i] = _heap[smallest];
                _heap[smallest] = tmp;
                Heapify(smallest);
            }
        }

        private void ResizeHeap()
        {
            V [] newHeap = new V[_heap.Length * 2];
            Array.Copy(_heap, newHeap, _heap.Length);
            _heap = newHeap;
        }
    }

    public class PriorityQueue<K,V> where K : IComparable
    {
        private PriorityQueue<KeyValueNode<K, V>> _priorityQueue;

        public int HeapSize { get { return _priorityQueue.HeapSize; } }

        public PriorityQueue()
        {
            _priorityQueue = new PriorityQueue<KeyValueNode<K, V>>();
        }

        public void Add(K priority, V value)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(priority, value);
            _priorityQueue.Add(item);
        }

        public V PeekTop()
        {
            return _priorityQueue.PeekTop().Value;
        }

        public V ExtractTop()
        {
            return _priorityQueue.ExtractTop().Value;
        }

    }
}
