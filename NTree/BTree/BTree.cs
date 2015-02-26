using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.BTree
{
    public class BTree<T> : Tree<T> where T : IComparable
    {
        private BTreeNode<T> _root;
        private int _count;
        private bool _readOnly;

        private int _order;

        public BTree(int order)
        {
            _order = order;
        }

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
            _root = null;
            _count = 0;
        }

        public override bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get { return _count; }
        }

        public override bool IsReadOnly
        {
            get { return _readOnly; }
        }
    }
}
