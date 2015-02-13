using System;
using System.Collections;
using System.Collections.Generic;
using NTree.BinarySearchTree;


namespace NTree.Common
{
    public abstract class BinaryTree<T> : ICollection<T> where T : IComparable
    {
        protected TreeNode<T> Root;
        protected int _count;
        protected bool ReadOnly;

        /// <summary>
        /// Return in-order enumerator
        /// </summary>
        /// <returns>in order IEnumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new BSTInOrderEnumerator<T>(Root);
        }

        /// <summary>
        /// Return in-order enumerator
        /// </summary>
        /// <returns>in order IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract void Add(T item);

        public void Clear()
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            Root = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            return FindElement(item) != null;
        }
        
        public void CopyTo(T[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;
            foreach (var element in this)
            {
                array[currentIndex++] = element;
            }
        }
        
        public abstract bool Remove(T item);

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return ReadOnly; }
        }

        protected TreeNode<T> FindElement(T item)
        {
            TreeNode<T> currentNode = Root;

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
        /// From BST definition, min node is the left most node
        /// </summary>
        /// <param name="subTreeNode">sub tree root</param>
        /// <returns>node with min value</returns>
        protected TreeNode<T> FindMinInSubtree(TreeNode<T> subTreeNode)
        {
            var currentNode = subTreeNode;
            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            return currentNode;
        }

    }
}
