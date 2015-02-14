using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.AVLTree
{
    class AVLNode<T> : TreeNode<T> where T : IComparable
    {
        public AVLNode(IComparable item) : base(item)
        {
        }

        public int Balance { get; set; }
        public int Height { get; set; }
    }
}
