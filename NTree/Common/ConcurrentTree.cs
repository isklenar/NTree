using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTree.Common
{
    public class ConcurrentTree<T> : Tree<T> where T : IComparable
    {
        private readonly Tree<T> _tree;
        //private readonly object _lock = new object();

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

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
            _lock.EnterWriteLock();
            _tree.Add(item);
            _lock.ExitWriteLock();
        }

        public override void Clear()
        {
            _lock.EnterWriteLock();
            _tree.Clear();
            _lock.ExitWriteLock();
        }

        public override bool Contains(T item)
        {
            _lock.EnterReadLock();
            bool result = _tree.Contains(item);
            _lock.ExitReadLock();
            return result;
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            _tree.CopyTo(array, arrayIndex);
            _lock.ExitReadLock();
        }

        public override bool Remove(T item)
        {
            _lock.EnterWriteLock();
            bool result = _tree.Remove(item);
            _lock.ExitWriteLock();

            return result;
        }

        public override int Count
        {
            get { return _tree.Count; }
        }

        public override bool IsReadOnly
        {
            get { return _tree.IsReadOnly; }
        }
    }
}
