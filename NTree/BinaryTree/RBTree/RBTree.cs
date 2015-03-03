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
using NTree.Common;

namespace NTree.BinaryTree.RBTree
{
    public class RBTree<T> : BinaryTree<T> where T : IComparable
    {
        public override void Add(T item)
        {
            RBNode<T> node = (RBNode<T>) InnerAdd(new RBNode<T>(item));
            if (node == null)
            {
                return;
            }
            InsertCase1(node);
        }

        public override bool Remove(T item)
        {
            RBNode<T> node = (RBNode<T>) FindElement(item);
            if (node == null)
            {
                return false;
            }

            RBDelete(node);
            _count--;
            return true;
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
        /// Deletes node from RB tree
        /// </summary>
        /// <param name="node"></param>
        private void RBDelete(RBNode<T> node)
        {
            RBNode<T> currentNode;
            if (node.Left == null || node.Right == null)
            {
                currentNode = node;
            }
            else
            {
                currentNode = (RBNode<T>) FindMinInSubtree(node.Right); //successor
            }
            RBNode<T> tmp;
            if (currentNode.Left != null)
            {
                tmp = (RBNode<T>) currentNode.Left;
            }
            else
            {
                tmp = (RBNode<T>) currentNode.Right;
            }
            if (tmp != null)
            {
                tmp.Parent = currentNode.Parent;
            }

            RBNode<T> tmpParent = (RBNode<T>) currentNode.Parent;
            bool left = false;
            if (currentNode.Parent == null)
            {
                Root = tmp;
            }
            else if (ReferenceEquals(currentNode, currentNode.Parent.Left))
            {
                currentNode.Parent.Left = tmp;
                left = true;
            }
            else
            {
                currentNode.Parent.Right = tmp;
                left = false;
            }

            if (!ReferenceEquals(currentNode, node))
            {
                node.Element = currentNode.Element;
            }

            if (!IsRed(currentNode))
            {
                RBDeleteFixUp(tmp, tmpParent, left);
            }
        }

        /// <summary>
        /// Fixes tree after deleting node.
        /// 
        /// Thanks to Trog from stackoverflow for this algorithm.
        /// 
        /// Original found at: http://stackoverflow.com/questions/6723488/red-black-tree-deletion-algorithm
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <param name="left"></param>
        private void RBDeleteFixUp(RBNode<T> node, RBNode<T> parent, bool left)
        {
            while (!ReferenceEquals(node, Root) && IsBlack(node))
            {
                RBNode<T> tmp;
                if (left)
                {
                    tmp = (RBNode<T>) parent.Right;
                    if (IsRed(tmp))
                    {
                        tmp.Colour = Colour.Black;
                        parent.Colour = Colour.Red;
                        RotateLeft(parent);
                        tmp = (RBNode<T>) parent.Right;
                    }

                    if (IsBlack(tmp.Left) && IsBlack(tmp.Right))
                    {
                        tmp.Colour = Colour.Red;
                        node = parent;
                        parent = (RBNode<T>) node.Parent;
                        left = ReferenceEquals(node, parent == null ? null : parent.Left);
                    }
                    else
                    {
                        if (IsBlack(tmp.Right))
                        {
                            ((RBNode<T>) tmp.Left).Colour = Colour.Black;
                            tmp.Colour = Colour.Red;
                            RotateRight(tmp);
                            tmp = (RBNode<T>) parent.Right;
                        }

                        tmp.Colour = parent.Colour;
                        parent.Colour = Colour.Black;
                        if (tmp.Right != null)
                        {
                            ((RBNode<T>) tmp.Right).Colour = Colour.Black;
                        }
                        RotateLeft(parent);
                        node = (RBNode<T>) Root;
                        parent = null;
                    }
                }
                else
                {
                    tmp = (RBNode<T>) parent.Left;
                    if (IsRed(tmp))
                    {
                        tmp.Colour = Colour.Black;
                        parent.Colour = Colour.Red;
                        RotateRight(parent);
                        tmp = (RBNode<T>) parent.Left;
                    }

                    if (IsBlack(tmp.Right) && IsBlack(tmp.Left))
                    {
                        tmp.Colour = Colour.Red;
                        node = parent;
                        parent = (RBNode<T>) node.Parent;
                        left = (ReferenceEquals(node, parent == null ? null : parent.Left));
                    }
                    else
                    {
                        if (IsBlack(tmp.Left))
                        {
                            ((RBNode<T>) tmp.Right).Colour = Colour.Black;
                            tmp.Colour = Colour.Red;
                            RotateLeft(tmp);
                            tmp = (RBNode<T>) parent.Left;
                        }

                        tmp.Colour = parent.Colour;
                        parent.Colour = Colour.Black;
                        if (tmp.Left != null)
                        {
                            ((RBNode<T>)tmp.Left).Colour = Colour.Black;
                        }
                        RotateRight(parent);
                        node = (RBNode<T>) Root;
                        parent = null;
                    }
                }
            }
            if (node != null)
            {
                node.Colour = Colour.Black;
            }
            
        }


        private void InsertCase1(RBNode<T> node)
        {
            if (node.Parent == null)
            {
                node.Colour = Colour.Black;
            }
            else
            {
                InsertCase2(node);
            }
        }

        private void InsertCase2(RBNode<T> node)
        {
            RBNode<T> parent = (RBNode<T>) node.Parent;
            if (parent.Colour == Colour.Black)
            {
                return;
            }

            InsertCase3(node);
        }

        private void InsertCase3(RBNode<T> node)
        {
            RBNode<T> uncle = Uncle(node);
            if (uncle != null && uncle.Colour == Colour.Red)
            {
                RBNode<T> parent = (RBNode<T>) node.Parent;
                parent.Colour = Colour.Black;
                uncle.Colour = Colour.Black;
                RBNode<T> grandparent = Grandparent(node);
                grandparent.Colour = Colour.Red;
                InsertCase1(grandparent);
            }
            else
            {
                InsertCase4(node);
            }
        }

        private void InsertCase4(RBNode<T> node)
        {
            RBNode<T> grandparent = Grandparent(node);
            if (ReferenceEquals(node, node.Parent.Right) && ReferenceEquals(node.Parent, grandparent.Left))
            {
                RotateLeft(node.Parent);
                node = (RBNode<T>) node.Left;
            }
            else if (ReferenceEquals(node, node.Parent.Left) && ReferenceEquals(node.Parent, grandparent.Right))
            {
                RotateRight(node.Parent);
                node = (RBNode<T>) node.Right;
            }

            InsertCase5(node);
        }

        private void InsertCase5(RBNode<T> node)
        {
            RBNode<T> grandparent = Grandparent(node);
            if (grandparent == null)
            {
                return;
            }
            RBNode<T> parent = (RBNode<T>) node.Parent;
            parent.Colour = Colour.Black;
            grandparent.Colour = Colour.Red;
            if (ReferenceEquals(node, node.Parent.Left))
            {
                RotateRight(grandparent);
            }
            else
            {
                RotateLeft(grandparent);
            }
        }

        /// <summary>
        /// Performs single left rotation over a node.
        /// </summary>
        /// <param name="node">node to rotate</param>
        private void RotateLeft(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            var tmp = node.Right;
            if (tmp == null)
            {
                return;
            }

            BTNode<T> oldParent = node.Parent;
            bool wasRoot = ReferenceEquals(node, Root);
            node.Right = tmp.Left;
            if (node.Right != null)
            {
                node.Right.Parent = node;
            }

            tmp.Parent = node.Parent;
            tmp.Left = node;
            node.Parent = tmp;

            if (wasRoot)
            {
                Root = tmp;
            }
            else
            {
                if (ReferenceEquals(oldParent.Left, node))
                {
                    oldParent.Left = tmp;
                }
                else if (ReferenceEquals(oldParent.Right, node))
                {
                    oldParent.Right = tmp;
                }
            }

        }

        /// <summary>
        /// Performs single right rotation over a node.
        /// </summary>
        /// <param name="node">node to rotate</param>
        private void RotateRight(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            var tmp = node.Left;
            if (tmp == null)
            {
                return;
            }

            BTNode<T> oldParent = node.Parent;
            bool wasRoot = ReferenceEquals(node, Root);

            node.Left = tmp.Right;
            if (node.Left != null)
            {
                node.Left.Parent = node;
            }
            tmp.Parent = node.Parent;
            tmp.Right = node;
            node.Parent = tmp;

            if (wasRoot)
            {
                Root = tmp;
            }
            else
            {
                if (ReferenceEquals(oldParent.Left, node))
                {
                    oldParent.Left = tmp;
                }
                else if (ReferenceEquals(oldParent.Right, node))
                {
                    oldParent.Right = tmp;
                }
            }
        }

        private RBNode<T> Grandparent(RBNode<T> node)
        {
            if (node != null && node.Parent != null)
            {
                return (RBNode<T>) node.Parent.Parent;
            }

            return null;
        }

        private RBNode<T> Uncle(RBNode<T> node)
        {
            RBNode<T> grandparent = Grandparent(node);
            if (grandparent != null)
            {
                if (ReferenceEquals(grandparent.Left, node.Parent))
                {
                    return (RBNode<T>) grandparent.Right;
                }

                return (RBNode<T>) grandparent.Left;
            }

            return null;
        }

        private bool IsRed(BTNode<T> node)
        {
            if (node == null)
            {
                return false;
            }
            RBNode<T> tmp = (RBNode<T>)node;
            return tmp.Colour == Colour.Red;
        }

        private bool IsBlack(BTNode<T> node)
        {
            if (node == null)
            {
                return true;
            }
            RBNode<T> tmp = (RBNode<T>) node;
            return tmp.Colour == Colour.Black;
        }
    }

    public class RBTree<K, V> : BinaryTree<K, V> where K : IComparable
    {
        public RBTree()
        {
            _tree = new RBTree<KeyValueNode<K, V>>();
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
