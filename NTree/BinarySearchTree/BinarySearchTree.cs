using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.BinarySearchTree
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        public override void Add(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }

            if (Root == null)
            {
                Root = new BSTNode<T>(item);
                _count++;
                return;
            }

            var currentNode = Root;
            BSTNode<T> prevNode = null;
            bool left = false;

            while (currentNode != null)
            {
                prevNode = (BSTNode<T>)currentNode;

                int comparison = item.CompareTo(currentNode.Element);
                if (comparison == 0) //element exists
                {
                    return;
                }
                if (comparison > 0)
                {
                    currentNode = (BSTNode<T>)currentNode.Right;
                    left = false;
                }

                if (comparison < 0)
                {
                    currentNode = (BSTNode<T>)currentNode.Left;
                    left = true;
                }

            }

            //currentNode is null at this point
            currentNode = new BSTNode<T>(item) { Parent = prevNode };
            _count++;

            if (left)
            {
                prevNode.Left = currentNode;
            }
            else
            {
                prevNode.Right = currentNode;
            }

        }

        public override bool Remove(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            BSTNode<T> nodeToRemove = (BSTNode<T>)FindElement(item);
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
        private void RemoveOneChildNode(BSTNode<T> node)
        {
            var child = node.Left;
            if (child != null) //only child is left one
            {
                var parent = node.Parent;

                //root
                if (parent == null)
                {
                    child.Parent = null;
                    Root = (BSTNode<T>)child;
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
                    Root = (BSTNode<T>)child;
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

    }
}
