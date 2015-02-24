using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.Common
{
    public class ConcurrentTree<T> : Tree<T> where T : IComparable
    {
        private readonly Tree<T> _tree;
        private readonly object _lock = new object();

        internal ConcurrentTree(Tree<T> tree)
        {
            _tree = tree;
        }   

        public override IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _tree.GetEnumerator();
            }
            
        }

        public override void Add(T item)
        {
            lock (_lock)
            {
                _tree.Add(item);
            }
        }

        public override void Clear()
        {
            lock (_lock)
            {
                _tree.Clear();
            }
        }

        public override bool Contains(T item)
        {
            lock (_lock)
            {
                return _tree.Contains(item);
            }
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock)
            {
                _tree.CopyTo(array, arrayIndex);
            }
        }

        public override bool Remove(T item)
        {
            lock (_lock)
            {
                return _tree.Remove(item);
            }
        }

        public override int Count
        {
            get {
                    lock (_lock)
                    {
                        return _tree.Count;
                    }
                }
        }

        public override bool IsReadOnly
        {
            get
            {
                lock (_lock)
                {
                    return _tree.IsReadOnly;
                }
            }
        }
    }
}
