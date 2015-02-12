using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.BinarySearchTree
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        private int _count;
        protected bool ReadOnly;

        private BSTNode<T> _root; 
        
        public override IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Add(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            BSTNode<T> currentNode = _root;
            if (_root == null)
            {
                _root = new BSTNode<T>(item);
                _count++;
                return;
            }

            BSTNode<T> prevNode = currentNode;
            bool left = false;
            while (currentNode != null)
            {
                prevNode = currentNode;

                int comparison = item.CompareTo(currentNode.Element);
                if (comparison >= 0)
                {
                    currentNode = currentNode.Right;
                    left = false;
                }

                if (comparison < 0)
                {
                    currentNode = currentNode.Left;
                    left = true;
                }
                
            }

            currentNode = new BSTNode<T>(item) {Parent = prevNode};
            _count++;
            //currentNode is root -> does not have parent
            if (prevNode != null)
            {
                if (left)
                {
                    prevNode.Left = currentNode;
                }
                else
                {
                    prevNode.Right = currentNode;
                }
            }            
        }

        public override void Clear()
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            _root = null;
            _count = 0;
        }

        public override bool Contains(T item)
        {
            return FindElement(item) != null;
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            var nodeToRemove = FindElement(item);
            if (nodeToRemove == null)
            {
                //not found, can't remove
                return false;
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
            return true;
        }

        public override int Count
        {
            get { return _count; }
        }

        public override bool IsReadOnly
        {
            get { return ReadOnly; }
        }

        private BSTNode<T> FindElement(T item)
        {
            BSTNode<T> currentNode = _root;

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
        /// Removes a node that has no children
        /// </summary>
        /// <param name="node">node to remove</param>
        private void RemoveChildlessNode(BSTNode<T> node)
        {
            var parent = node.Parent;

            //we're removing root
            if (parent == null)
            {
                _root = null;
                return;
            }

            if (parent.Left.Element.CompareTo(node.Element) == 0)
            {
                parent.Left = null;
            }
            else
            {
                parent.Right = null;
            }
        }
        /// <summary>
        /// Removes a node with one child
        /// </summary>
        /// <param name="nodeToRemove">node to remove</param>
        private void RemoveOneChildNode(BSTNode<T> node)
        {
            var leftChild = node.Left;
            if (leftChild != null)
            {
                var parent = node.Parent;

                //root
                if (parent == null)
                {
                    leftChild.Parent = null;
                    _root = leftChild;
                    return;
                }
                
                //otherwise move child
                parent.Left = leftChild;
                leftChild.Parent = parent;               
            }
            else
            {
                var rightChild = node.Right;
                var parent = node.Parent;
                //root
                if (parent == null)
                {
                    rightChild.Parent = null;
                    _root = rightChild;
                    return;
                }

                //otherwise move child
                parent.Left = rightChild;
                rightChild.Parent = parent;
            }
        }

        /// <summary>
        /// Removes node with two children
        /// </summary>
        /// <param name="node">node to remove</param>
        private void RemoveTwoChildrenNode(BSTNode<T> node)
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
            // replacement is nested deeper, there is atleast one ndoe between replacement and node, i.e. replacement.Parent != node
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

        /// <summary>
        /// Finds min value in subtree.
        /// 
        /// From BST definition, min node is the left most node
        /// </summary>
        /// <param name="subTreeNode">sub tree root</param>
        /// <returns>node with min value</returns>
        private BSTNode<T> FindMinInSubtree(BSTNode<T> subTreeNode)
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
