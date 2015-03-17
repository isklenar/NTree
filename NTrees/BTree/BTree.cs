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
﻿using NTrees.Common;

namespace NTrees.BTree
{
    internal class BTree<T> : ITree<T> where T : IComparable
    {
        private BTreeNode<T> _root;
        private int _count;

        private int _order;

        private const int DefaultOrder = 4;

        public BTree(int order)
        {
            _order = order;
        }

        /// <summary>
        /// Creates new BTree with default order = 4.
        /// </summary>
        public BTree()
            :this(DefaultOrder)
        {
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            // this tree is never read only - use AsReadOnly(this)
            get { return false; }
        }

        public ReadOnlyTree<T> AsReadOnly()
        {
            throw new NotImplementedException();
        }

        public ConcurrentTree<T> AsConcurrentTree()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class BTree<K, V> : ITree<K, V> where K : IComparable
    {
        private BTree<KeyValueNode<K, V>> _tree;
        private int _order;

        public BTree(int order)
        {
            _order = order;
            _tree = new BTree<KeyValueNode<K, V>>(_order);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(K key, V value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(K key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(V[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public V GetValue(K key)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyTree<K, V> AsReadOnly()
        {
            throw new NotImplementedException();
        }

        public ConcurrentTree<K, V> AsConcurrentTree()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<V> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
