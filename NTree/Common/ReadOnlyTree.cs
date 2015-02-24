using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.Common
{
    public class ReadOnlyTree<T> : IReadOnlyCollection<T> where T : IComparable
    {
        private Tree<T> _tree;

        public ReadOnlyTree(Tree<T> tree)
        {
            _tree = tree;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _tree.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _tree.Count; }
        }
    }
}
