using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTree.Common;

namespace NTree.BinaryTree
{
    public abstract class BinaryTree<T> : ITree<T> where T : IComparable
    {
        protected int _count;
        protected bool ReadOnly;
        internal BTNode<T> Root { get; set; }

        /// <summary>
        /// Adds node to tree, using standard BST insert algorithm.
        /// 
        /// Returns null if element in node already exists in tree.
        /// </summary>
        /// <param name="node">node to add</param>
        /// <returns>added node, null if element exists</returns>
        protected BTNode<T> InnerAdd(BTNode<T> node)
        {
            var currentNode = Root;
            BTNode<T> prevNode = null;
            bool left = false;

            while (currentNode != null)
            {
                prevNode = currentNode;
                int comparison = node.Element.CompareTo(currentNode.Element);
                if (comparison < 0)
                {
                    currentNode = currentNode.Left;
                    left = true;
                }
                if (comparison > 0)
                {
                    currentNode = currentNode.Right;
                    left = false;
                }
                if (comparison == 0)
                {
                    return null;
                }
            }
            currentNode = node;
            if (Root == null)
            {
                Root = currentNode;
            }
            else
            {
                if (left)
                {
                    prevNode.Left = currentNode;
                    currentNode.Parent = prevNode;
                }
                else
                {
                    prevNode.Right = currentNode;
                    currentNode.Parent = prevNode;
                }
            }
            _count++;

            return currentNode;
        }

        /// <summary>
        /// Returns in-order enumerator.
        /// </summary>
        /// <returns>in order IEnumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new BSTInOrderEnumerator<T>(Root);
        }

        public abstract void Add(T item);

        /// <summary>
        /// Clears tree of all data. 
        /// </summary>
        /// <exception cref="NotSupportedException">Throws NotSupportedException if tree is read-only.</exception>
        public void Clear()
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            Root = null;
            _count = 0;
        }
        /// <summary>
        /// Determines if tree contains element.
        /// </summary>
        /// <param name="item">element to find</param>
        /// <returns>true if found, false if not</returns>
        public bool Contains(T item)
        {
            return FindElement(item) != null;
        }

        /// <summary>
        /// Copies data in-order to an array, starting at index specified at arrayIndex.
        /// </summary>
        /// <param name="array">array to copy to</param>
        /// <param name="arrayIndex">starting index of array</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;
            foreach (var element in this)
            {
                array[currentIndex++] = element;
            }
        }

        public abstract bool Remove(T item);

        /// <summary>
        /// Returns number of elements in tree.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return ReadOnly; }
        }

        public abstract ReadOnlyTree<T> AsReadOnly();

        public abstract ConcurrentTree<T> AsConcurrentTree();

        /// <summary>
        /// Removes node from tree and returns it.
        /// </summary>
        /// <param name="node">element to remove</param>
        /// <returns>Removed node detached from tree, null if not found</returns>
        protected BTNode<T> RemoveNode(BTNode<T> node)
        {
            if (node == null)
            {
                //not found, can't remove
                return null;
            }
            //does not have children
            if (node.Left == null && node.Right == null)
            {
                RemoveChildlessNode(node);
            }
            else if (node.Left == null ^ node.Right == null) // has one child
            {
                RemoveOneChildNode(node);
            }
            else //has both children
            {
                RemoveTwoChildrenNode(node);
            }

            return node;
        }

        /// <summary>
        /// Removes a node that has no children
        /// </summary>
        /// <param name="node">node to remove</param>
        private void RemoveChildlessNode(BTNode<T> node)
        {
            var parent = node.Parent;

            //we're removing root
            if (parent == null)
            {
                Root = null;
                return;
            }

            //removing left child
            if (parent.Left != null && parent.Left.Element.CompareTo(node.Element) == 0)
            {
                parent.Left = null;
            }
            else //it's a right child
            {
                parent.Right = null;
            }
        }
        /// <summary>
        /// Removes a node with one child
        /// </summary>
        /// <param name="node">node to remove</param>
        private void RemoveOneChildNode(BTNode<T> node)
        {
            var child = node.Left;
            if (child != null) //only child is left one
            {
                var parent = node.Parent;

                //root
                if (parent == null)
                {
                    child.Parent = null;
                    Root = child;
                    return;
                }

                if (ReferenceEquals(parent.Left, node)) //node is left child of parent
                {
                    parent.Left = child;
                    child.Parent = parent;
                }
                else
                {
                    parent.Right = child;
                    child.Parent = parent;
                }
            }
            else
            {
                child = node.Right;
                var parent = node.Parent;
                //root
                if (parent == null)
                {
                    child.Parent = null;
                    Root = child;
                    return;
                }

                if (ReferenceEquals(parent.Left, node)) //node is left child of parent
                {
                    parent.Left = child;
                    child.Parent = parent;
                }
                else
                {
                    parent.Right = child;
                    child.Parent = parent;
                }
            }
        }

        /// <summary>
        /// Removes node with two children.
        /// Detaches it from tree and replaces it with in-order successor.
        /// </summary>
        /// <param name="node">node to remove</param>
        private void RemoveTwoChildrenNode(BTNode<T> node)
        {
            //find min node in right subtree, which will be replacement for current one
            var replacement = FindMinInSubtree(node.Right);
            node.Element = replacement.Element;
            RemoveNode(replacement);
        }

        /// <summary>
        /// Finds element in tree.
        /// </summary>
        /// <param name="item">node to find</param>
        /// <returns>node containing element, null if not found</returns>
        protected BTNode<T> FindElement(T item)
        {
            BTNode<T> currentNode = Root;

            while (currentNode != null)
            {
                int comparison = item.CompareTo(currentNode.Element);
                if (comparison == 0)
                {
                    return currentNode;
                }
                if (comparison > 0)
                {
                    currentNode = currentNode.Right;
                }

                if (comparison < 0)
                {
                    currentNode = currentNode.Left;
                }

            }

            return null;
        }

        /// <summary>
        /// Finds min value in subtree.
        /// 
        /// From BST definition, min node is the left most node of tree.
        /// </summary>
        /// <param name="node">sub tree root</param>
        /// <returns>node with min value</returns>
        protected BTNode<T> FindMinInSubtree(BTNode<T> node)
        {
            var currentNode = node;
            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            return currentNode;
        }

        internal IComparable GetItem(T item)
        {
            return FindElement(item).Element;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class BinaryTree<K, V> : ITree<K, V>, IEnumerable where K : IComparable
    {
        protected BinaryTree<KeyValueNode<K, V>> _tree;

        public IEnumerator<V> GetEnumerator()
        {
            KeyValueNode<K, V>[] items = new KeyValueNode<K, V>[_tree.Count];
            _tree.CopyTo(items, 0);

            var values = items.Select(u => u.Value).ToList(); //select just values
            return values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(K key, V value)
        {
            var item = new KeyValueNode<K, V>(key, value);
            _tree.Add(item);
        }

        public void Clear()
        {
            _tree.Clear();
        }

        public bool Contains(K key)
        {
            var item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Contains(item);
        }

        public V GetValue(K key)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, default(V));
            var ret = _tree.GetItem(item) as KeyValueNode<K, V>;
            return ret.Value;
        }

        public abstract ReadOnlyTree<K, V> AsReadOnly();

        public abstract ConcurrentTree<K, V> AsConcurrentTree();

        public void CopyTo(V[] array, int arrayIndex)
        {
            KeyValueNode<K, V>[] items = new KeyValueNode<K, V>[_tree.Count];
            _tree.CopyTo(items, 0);

            var values = items.Select(u => u.Value).ToList();
            foreach (var value in values)
            {
                array[arrayIndex++] = value;
            }
        }

        public bool Remove(K key)
        {
            var item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Remove(item);
        }

        public int Count { get { return _tree.Count; } }
        public bool IsReadOnly { get { return _tree.IsReadOnly; } }
    }
}
