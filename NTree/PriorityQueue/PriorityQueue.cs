using System;
using System.Collections.Generic;
using NTree.Common;

namespace NTree.PriorityQueue
{
    /// <summary>
    /// PriorityQueue implemented as min binary heap, meaning that the smallest element is at the top.
    /// </summary>
    /// <typeparam name="V">type implementing IComparable</typeparam>
    public class PriorityQueue<V> where V : IComparable
    {
        private int _heapSize = 0;
        private V [] _heap = new V[10];
        
        /// <summary>
        /// Returns number of elements in heap.
        /// </summary>
        public int HeapSize { get { return _heapSize; } }

        /// <summary>
        /// Adds element to heap.
        /// </summary>
        /// <param name="value">value to add</param>
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

        /// <summary>
        /// Returns, but does not remove, top element from heap.
        /// </summary>
        /// <returns>top element, default(V) if heap is empty</returns>
        public V PeekTop()
        {
            if (_heapSize == 0)
            {
                return default(V);
            }

            return _heap[1];
        }

        /// <summary>
        /// Returns and replaces top element in heap.
        /// </summary>
        /// <returns>top element, default(V) if heap is empty</returns>
        public V ExtractTop()
        {
            if (_heapSize == 0)
            {
                return default(V);
            }

            V min = _heap[1];
            _heap[1] = _heap[_heapSize];
            _heapSize--;
            Heapify(1);

            return min;
        }

        /// <summary>
        /// Heapifies heap.
        /// </summary>
        /// <param name="i">index of node to heapify</param>
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

        /// <summary>
        /// Resizes heap.
        /// </summary>
        private void ResizeHeap()
        {
            V [] newHeap = new V[_heap.Length * 2];
            Array.Copy(_heap, newHeap, _heap.Length);
            _heap = newHeap;
        }
    }

    /// <summary>
    /// Priority key-value queue, implemented as min binary heap.
    /// </summary>
    /// <typeparam name="K">Key, implementing IComparable interface</typeparam>
    /// <typeparam name="V">Value</typeparam>
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
