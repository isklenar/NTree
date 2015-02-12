using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.Common
{
    public abstract class TreeNode<T> where T : IComparable
    {
        public TreeNode(IComparable item)
        {
            Element = item;
        }

        public IComparable Element { get; set; }

        public TreeNode<T> Parent { get; set; }
        public TreeNode<T> Right { get; set; }
        public TreeNode<T> Left { get; set; } 
  

    }

}
