using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.BinaryTree.RBTree
{
    public class RBTree<T> : BinaryTree<T> where T : IComparable
    {
        public override void Add(T item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
