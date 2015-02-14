using System;
using System.Collections;
using System.Collections.Generic;
using NTree.Common;

namespace NTree.BinaryTree
{
    public abstract class BinaryTree<T> : Tree<T> where T : IComparable
    {
        protected int _count;
        protected bool ReadOnly;
        protected BTNode<T> Root { get; set; } 

        /// <summary>
        /// Return in-order enumerator
        /// </summary>
        /// <returns>in order IEnumerator</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            return new BSTInOrderEnumerator<T>(Root);
        }

        public override void Clear()
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            Root = null;
            _count = 0;
        }

        public override bool Contains(T item)
        {
            return FindElement(item) != null;
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;
            foreach (var element in this)
            {
                array[currentIndex++] = element;
            }
        }

        public override int Count
        {
            get { return _count; }
        }

        public override  bool IsReadOnly
        {
            get { return ReadOnly; }
        }

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
        /// From BST definition, min node is the left most node
        /// </summary>
        /// <param name="subBtNode">sub tree root</param>
        /// <returns>node with min value</returns>
        protected BTNode<T> FindMinInSubtree(BTNode<T> subBtNode)
        {
            var currentNode = subBtNode;
            while (currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }

            return currentNode;
        }

    }
}
