using System;

namespace NTree.BinaryTree.BinarySearchTree
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
            return RemoveNode(item) != null;           
        }   

    }
}
