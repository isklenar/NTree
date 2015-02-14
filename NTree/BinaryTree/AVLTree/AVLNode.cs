using System;

namespace NTree.BinaryTree.AVLTree
{
    class AVLNode<T> : BTNode<T> where T : IComparable
    {
        public AVLNode(IComparable item) : base(item)
        {
        }

        public int Height { get; set; }
    }
}
