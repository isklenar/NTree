using System;

namespace NTree.BinaryTree.BinarySearchTree
{
    public class BSTNode<T>: BTNode<T> where T : IComparable
    {
        public BSTNode(IComparable item) : base(item)
        {
        }
    }
}
