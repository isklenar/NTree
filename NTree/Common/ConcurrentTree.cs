// The MIT License (MIT)

// Copyright (c) 2015 Ivo Sklenar

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTree.Common
{
    public class ConcurrentTree<T> : ITree<T> where T : IComparable
    {
        private readonly ITree<T> _tree;
        //private readonly object _lock = new object();

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        internal ConcurrentTree(ITree<T> tree)
        {
            _tree = tree;
        }   

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _tree.GetEnumerator();
            }
            
        }

        public void Add(T item)
        {
            _lock.EnterWriteLock();
            _tree.Add(item);
            _lock.ExitWriteLock();
        }

        public void Clear()
        {
            _lock.EnterWriteLock();
            _tree.Clear();
            _lock.ExitWriteLock();
        }

        public bool Contains(T item)
        {
            _lock.EnterReadLock();
            bool result = _tree.Contains(item);
            _lock.ExitReadLock();
            return result;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            _tree.CopyTo(array, arrayIndex);
            _lock.ExitReadLock();
        }

        public bool Remove(T item)
        {
            _lock.EnterWriteLock();
            bool result = _tree.Remove(item);
            _lock.ExitWriteLock();

            return result;
        }

        public int Count
        {
            get { return _tree.Count; }
        }

        public bool IsReadOnly
        {
            get { return _tree.IsReadOnly; }
        }

        public ReadOnlyTree<T> AsReadOnly()
        {
            return new ReadOnlyTree<T>(this);
        }

        public ConcurrentTree<T> AsConcurrentTree()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ConcurrentTree<K, V> : ITree<K, V> where K : IComparable
    {
        private readonly ITree<K, V> _tree;
        //private readonly object _lock = new object();

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        internal ConcurrentTree(ITree<K, V> tree)
        {
            _tree = tree;
        }

        public IEnumerator<V> GetEnumerator()
        {
            lock (_lock)
            {
                return _tree.GetEnumerator();
            }
            
        }

        public void Add(K key, V value)
        {
            _lock.EnterWriteLock();
            _tree.Add(key, value);
            _lock.ExitWriteLock();
        }

        public void Clear()
        {
            _lock.EnterWriteLock();
            _tree.Clear();
            _lock.ExitWriteLock();
        }

        public bool Contains(K key)
        {
            _lock.EnterReadLock();
            bool result = _tree.Contains(key);
            _lock.ExitReadLock();
            return result;
        }

        public void CopyTo(V[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            _tree.CopyTo(array, arrayIndex);
            _lock.ExitReadLock();
        }

        public bool Remove(K key)
        {
            _lock.EnterWriteLock();
            bool result = _tree.Remove(key);
            _lock.ExitWriteLock();

            return result;
        }

        public int Count
        {
            get { return _tree.Count; }
        }

        public bool IsReadOnly
        {
            get { return _tree.IsReadOnly; }
        }

        public V GetValue(K key)
        {
            _lock.EnterReadLock();
            V value = _tree.GetValue(key);
            _lock.ExitReadLock();

            return value;
        }

        public ReadOnlyTree<K, V> AsReadOnly()
        {
            return new ReadOnlyTree<K, V>(this);
        }

        public ConcurrentTree<K, V> AsConcurrentTree()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
