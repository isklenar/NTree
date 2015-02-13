using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.RBTree
{
    public class RBTree<T> : BinaryTree<T> where T : IComparable
    {
        public override void Add(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }

            if (Root == null)
            {
                Root = new RBNode<T>(item);
                _count++;
                return;
            }

            RBNode<T> currentNode = (RBNode<T>)Root;
            RBNode<T> prevNode = null;
            bool left = false;

            while (currentNode != null)
            {
                prevNode = (RBNode<T>)currentNode;

                int comparison = item.CompareTo(currentNode.Element);
                if (comparison == 0) //element exists
                {
                    return;
                }
                if (comparison > 0)
                {
                    currentNode = (RBNode<T>)currentNode.Right;
                    left = false;
                }

                if (comparison < 0)
                {
                    currentNode = (RBNode<T>)currentNode.Left;
                    left = true;
                }

            }

            //currentNode is null at this point
            currentNode = new RBNode<T>(item) { Parent = prevNode };
            _count++;

            if (left)
            {
                prevNode.Left = currentNode;
            }
            else
            {
                prevNode.Right = currentNode;
            }

            InsertCheckCase1(currentNode);
        }

        /// <summary>
        /// Checks first RB case - current node is root -> change colour to black.
        /// 
        /// Continues with InsertCheckCase2
        /// </summary>
        /// <param name="node">node to check</param>
        private void InsertCheckCase1(RBNode<T> node)
        {
            if (node.Parent == null)
            {
                node.Colour = RBColour.Black;
                return;
            }
            InsertCheckCase2(node);
        }

        /// <summary>
        /// Checks second RB case -> node.Parent is black.
        /// 
        /// Continues with InsertCheckCase3
        /// </summary>
        /// <param name="node">node to check</param>
        private void InsertCheckCase2(RBNode<T> node)
        {
            RBNode<T> parent = (RBNode<T>)node.Parent;
            if (parent.Colour == RBColour.Black)
            {
                return; //RB tree property is still OK
            }
            InsertCheckCase3(node);
        }

        /// <summary>
        /// Checks third RB case -> both parent and uncle are red, then both can be
        /// repainted black and grandparent red. Does check1 on grandparent afterwards.
        /// 
        /// Othewise continues with check4
        /// </summary>
        /// <param name="node">node to check</param>
        private void InsertCheckCase3(RBNode<T> node)
        {
            RBNode<T> uncle = Uncle(node);
            if (uncle != null && uncle.Colour == RBColour.Red) //parent is red, since it passed check2
            {
                RBNode<T> parent = (RBNode<T>)node.Parent;
                parent.Colour = RBColour.Black;
                uncle.Colour = RBColour.Black;
                RBNode<T> grandparent = GrandParent(node);
                grandparent.Colour = RBColour.Red;
                InsertCheckCase1(grandparent);
            }
            else
            {
                InsertCheckCase4(node);
            }
        }
        /// <summary>
        /// Checks fourth RB case -> parent is red but uncle black and current node is the right child of parent
        /// and parent left child of it's parent. Performs rotaton
        /// </summary>
        /// <param name="node"></param>
        private void InsertCheckCase4(RBNode<T> node)
        {
            RBNode<T> nextCheck = null;
            RBNode<T> grandparent = GrandParent(node);
            if (grandparent!= null && ReferenceEquals(node, node.Parent.Right) && ReferenceEquals(node.Parent, grandparent.Left))
            {
                RotateLeft(node);
                
                nextCheck = (RBNode<T>)node.Left;
            }
            else if (grandparent != null && ReferenceEquals(node, node.Parent.Left) && ReferenceEquals(node.Parent, grandparent.Right))
            {
                RotateRight(node);
                
                nextCheck = (RBNode<T>)node.Right;
            }

            InsertCheckCase5(nextCheck);
        }

        /// <summary>
        /// Last case, parent is red, but uncle black, current node is left child and parent of also left child
        /// </summary>
        /// <param name="node"></param>
        private void InsertCheckCase5(RBNode<T> node)
        {
            RBNode<T> grandparent = GrandParent(node);
            if (grandparent == null)
            {
                return;
            }
            RBNode<T> parent = (RBNode<T>)node.Parent;
            parent.Colour = RBColour.Black;

            if (ReferenceEquals(node, node.Parent.Left))
            {
                RotateRight(grandparent);
            }
            else
            {
                RotateLeft(grandparent);
            }

        }

        private void RotateRight(RBNode<T> node)
        {
            RBNode<T> grandparent = GrandParent(node);
            RBNode<T> parent = (RBNode<T>)grandparent.Left;

            //rotate right
            grandparent.Right = node;
            node.Parent = grandparent;

            parent.Left = node.Right;
            if (node.Right != null)
            {
                node.Right.Parent = parent;
            }

            node.Right = parent;
            parent.Parent = node; ;
        }

        private void RotateLeft(RBNode<T> node)
        {
            RBNode<T> grandparent = GrandParent(node);
            RBNode<T> parent = (RBNode<T>)grandparent.Left;

            grandparent.Left = node;
            node.Parent = grandparent;

            parent.Right = node.Left;
            if (node.Left != null)
            {
                node.Left.Parent = parent;
            }

            node.Left = parent;
            parent.Parent = node;
        }

        public override bool Remove(T item)
        {
            throw new NotImplementedException();
        }


        private RBNode<T> GrandParent(RBNode<T> node)
        {
            if (node != null && node.Parent != null)
            {
                return (RBNode<T>)node.Parent.Parent;
            }

            return null;
        }

        private RBNode<T> Uncle(RBNode<T> node)
        {
            RBNode<T> grandparent = GrandParent(node);
            if (grandparent == null)
            {
                return null;
            }

            if (ReferenceEquals(grandparent.Left, node.Parent))
            {
                return (RBNode<T>)grandparent.Right;
            }
            else
            {
                return (RBNode<T>)grandparent.Left;
            }
        }
    }
}
