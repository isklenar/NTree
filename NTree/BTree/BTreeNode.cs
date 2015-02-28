using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.BTree
{
    internal class BTreeNode<T> where T : IComparable
    {
        public BTreeNode<T> Parent { get; set; }
        public BTreeNode<T>[] Children { get; set; }

        public T Element { get; set; }

        public BTreeNode(T element, int order)
        {
            Element = element;
            Children = new BTreeNode<T>[order];
        }
    }
}
