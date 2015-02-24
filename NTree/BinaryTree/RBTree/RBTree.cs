using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
            throw new NotImplementedException();
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
        /// Performs single right rotation over a node.
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

        private RBNode<T> Sibling(RBNode<T> node)
        {
            if (node.Parent == null)
            {
                return null;
            }

            if (ReferenceEquals(node.Parent.Left, node))
            {
                return (RBNode<T>) node.Parent.Right;
            }
            else
            {
                return (RBNode<T>) node.Parent.Left;
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
    }
}
