using System;
using System.ComponentModel;
using NTree.Common;

namespace NTree.BinaryTree
{
    public abstract class BTNode<T>
    {
        protected BTNode(IComparable item)
        {
            Element = item;
        }

        public IComparable Element { get; set; }

        public BTNode<T> Parent { get; set; }
        public BTNode<T> Right { get; set; }
        public BTNode<T> Left { get; set; } 
  

    }

}
