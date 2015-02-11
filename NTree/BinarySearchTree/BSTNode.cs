using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.BinarySearchTree
{
    public class BSTNode<T> where T : IComparable
    {
        public BSTNode(IComparable item)
        {
            Element = item;
        }

        public IComparable Element { get; set; }

        public BSTNode<T> Parent { get; set; }
        public BSTNode<T> Right { get; set; }
        public BSTNode<T> Left { get; set; } 
  

    }
}
