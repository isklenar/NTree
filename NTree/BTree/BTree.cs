using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.BTree
{
    public class BTree<T> : ITree<T> where T : IComparable
    {
        private BTreeNode<T> _root;
        private int _count;
        private bool _readOnly;

        private int _order;

        private const int DefaultOrder = 4;

        public BTree(int order)
        {
            _order = order;
        }

        /// <summary>
        /// Creates new BTree with default order = 4.
        /// </summary>
        public BTree()
            :this(DefaultOrder)
        {
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return _readOnly; }
        }

        public ReadOnlyTree<T> AsReadOnly()
        {
            throw new NotImplementedException();
        }

        public ConcurrentTree<T> AsConcurrentTree()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
