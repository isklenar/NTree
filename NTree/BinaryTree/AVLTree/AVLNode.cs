using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.BinaryTree.AVLTree
{
    public class AVLNode<T> : BTNode<T> where T : IComparable
    {
        public AVLNode(IComparable item) : base(item)
        {
        }

        public int Height { get; set; }
    }
}
