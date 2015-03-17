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


using System;
using NTrees.Common;

namespace NTrees.BinaryTree.BinarySearchTree
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        public override void Add(T item)
        {
            InnerAdd(new BTNode<T>(item));
        }

        public override bool Remove(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            if (RemoveNode(FindElement(item)) != null)
            {
                _count--;
                return true;
            }
            return false;         
        }

        public override ReadOnlyTree<T> AsReadOnly()
        {
            return new ReadOnlyTree<T>(this);
        }

        public override ConcurrentTree<T> AsConcurrentTree()
        {
            return new ConcurrentTree<T>(this);
        }

        /// <summary>
        /// Removes node from tree and returns it.
        /// </summary>
        /// <param name="node">element to remove</param>
        /// <returns>Removed node detached from tree, null if not found</returns>
        private BTNode<T> RemoveNode(BTNode<T> node)
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
    }

    public class BinarySearchTree<K, V> : BinaryTree<K, V> where K : IComparable
    {
        public BinarySearchTree()
        {
            _tree = new BinarySearchTree<KeyValueNode<K, V>>();
        }

        public override ReadOnlyTree<K, V> AsReadOnly()
        {
            return new ReadOnlyTree<K, V>(this);
        }

        public override ConcurrentTree<K, V> AsConcurrentTree()
        {
            return new ConcurrentTree<K, V>(this);
        }
    }
}
