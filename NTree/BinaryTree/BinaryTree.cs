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

        protected BTNode<T> RemoveNode(T item)
        {
            var nodeToRemove = FindElement(item);
            if (nodeToRemove == null)
            {
                //not found, can't remove
                return null;
            }
            //does not have children
            if (nodeToRemove.Left == null && nodeToRemove.Right == null)
            {
                RemoveChildlessNode(nodeToRemove);
            }
            else if (nodeToRemove.Left == null ^ nodeToRemove.Right == null) // has one child
            {
                RemoveOneChildNode(nodeToRemove);
            }
            else //has both children
            {
                RemoveTwoChildrenNode(nodeToRemove);
            }


            _count--;

            return nodeToRemove;
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
        /// <param name="nodeToRemove">node to remove</param>
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
        /// Removes node with two children
        /// </summary>
        /// <param name="node">node to remove</param>
        private void RemoveTwoChildrenNode(BTNode<T> node)
        {
            //find min node in right subtree, which will be replacement for current one
            var replacement = FindMinInSubtree(node.Right);

            //replacement is right child of node we are removing
            //since it's min value, it only has right child
            //with elements larger than itself
            if (replacement.Parent.Element.CompareTo(node.Element) == 0)
            {
                //just "push" replacement one layer up
                node.Element = replacement.Element;
                node.Right = replacement.Right;
                if (replacement.Right != null)
                {
                    replacement.Right.Parent = node;
                }
            }
            else
            // replacement is nested deeper, there is atleast one node between replacement and node <=> replacement.Parent != node
            {
                node.Element = replacement.Element;
                if (replacement.Left == null && replacement.Right == null)
                {
                    //replacement is min node, has to be left child
                    replacement.Parent.Left = null;
                }
                else //can only have right child
                {
                    //add right child as a left child of parent
                    replacement.Parent.Left = replacement.Right;
                    replacement.Right.Parent = replacement.Parent;
                }
            }
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
