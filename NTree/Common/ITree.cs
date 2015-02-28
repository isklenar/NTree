using System;
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
