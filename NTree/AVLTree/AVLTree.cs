using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.AVLTree
{
    public class AVLTree<T> : BinaryTree<T> where T : IComparable
    {
        public override IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Add(T item)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
    }
}
