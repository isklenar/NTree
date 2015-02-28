using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.Common
{
    public interface ITree<T> : ICollection<T> where T : IComparable
    {
        ReadOnlyTree<T> AsReadOnly();
        ConcurrentTree<T> AsConcurrentTree();
    }

    public interface ITree<K, V> : IEnumerable where K : IComparable
    {
        IEnumerator<V> GetEnumerator();

        void Add(K key, V value);
        void Clear();
        bool Contains(K key);
        void CopyTo(V[] array, int arrayIndex);
        bool Remove(K key);
        int Count { get; }
        bool IsReadOnly { get; }
        V GetValue(K key);

        ReadOnlyTree<K, V> AsReadOnly();
        ConcurrentTree<K, V> AsConcurrentTree();
    }
}
