using System;
using System.ComponentModel;
using NTree.Common;

namespace NTree.BinaryTree
{
    /// <summary>
    /// Node in binary tree.
    /// </summary>
    /// <typeparam name="T">type implementing IComparable</typeparam>
    public class BTNode<T> where T : IComparable
    {
        public BTNode(IComparable item)
        {
            Element = item;
        }

        public IComparable Element { get; set; }

        public BTNode<T> Parent { get; set; }
        public BTNode<T> Right { get; set; }
        public BTNode<T> Left { get; set; } 
 
    }

}
