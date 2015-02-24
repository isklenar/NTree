using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.Common
{
    public class KeyValueNode<K, V> : IComparable where K : IComparable
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public KeyValueNode(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(object obj)
        {
            KeyValueNode<K, V> other = obj as KeyValueNode<K, V>;

            if (other == null)
            {
                throw new ArgumentException("Comparison argument is null");
            }

            return Key.CompareTo(other.Key);
        }
    }
}
