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

namespace NTree.Common
{
    public interface ITree<T> : ICollection<T> where T : IComparable
    {
        /// <summary>
        /// Returns this tree as read only tree.
        /// </summary>
        /// <returns>read only version of this tree</returns>
        ReadOnlyTree<T> AsReadOnly();

        /// <summary>
        /// Returns this tree as thread safe version.
        /// 
        /// Allows multiple threads to read the tree, but only one at time can modify it.
        /// </summary>
        /// <returns>thread safe version of this tree</returns>
        ConcurrentTree<T> AsConcurrentTree();
    }

    public interface ITree<K, V> : IEnumerable where K : IComparable
    {
        IEnumerator<V> GetEnumerator();

        /// <summary>
        /// Adds item to the tree.
        /// 
        /// If item is already present, the tree is not modified.
        /// </summary>
        /// <param name="key">items key</param>
        /// <param name="value">items value</param>
        void Add(K key, V value);

        /// <summary>
        /// Clears the tree of all elements.
        /// </summary>
        void Clear();

        /// <summary>
        /// Checks whether key is already present in the tree.
        /// </summary>
        /// <param name="key">key to check</param>
        /// <returns>true if found; false if not</returns>
        bool Contains(K key);

        /// <summary>
        /// Does in-order copy of the tree to array.
        /// </summary>
        /// <param name="array">array to copy to</param>
        /// <param name="arrayIndex">starting index</param>
        void CopyTo(V[] array, int arrayIndex);

        /// <summary>
        /// Removes item with specified key from the tree.
        /// </summary>
        /// <param name="key">key to remove</param>
        /// <returns>true if item was found and removed; false otherwise</returns>
        bool Remove(K key);

        /// <summary>
        /// Returns number of elements in the tree.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns whether the tree is read only or not.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Return value associated with key.
        /// </summary>
        /// <param name="key">key to find</param>
        /// <returns>value; default(V) if key is not found</returns>
        V GetValue(K key);

        /// <summary>
        /// Returns this tree as read only tree.
        /// </summary>
        /// <returns>read only version of this tree</returns>
        ReadOnlyTree<K, V> AsReadOnly();

        /// <summary>
        /// Returns this tree as thread safe version.
        /// 
        /// Allows multiple threads to read the tree, but only one at time can modify it.
        /// </summary>
        /// <returns>thread safe version of this tree</returns>
        ConcurrentTree<K, V> AsConcurrentTree();
    }
}
