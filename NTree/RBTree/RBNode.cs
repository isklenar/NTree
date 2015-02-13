using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.RBTree
{
    class RBNode<T> : TreeNode<T> where T : IComparable
    {
        public RBNode(IComparable item) : base(item)
        {
        }
    }
}
