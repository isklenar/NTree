using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTree.Common;

namespace NTree.BinaryTree.BinarySearchTree
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        public override void Add(T item)
        {
            InnerAdd(item);
        }

        public override bool Remove(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            return RemoveNode(item) != null;           
        }

        internal IComparable GetItem(T item)
        {
            return FindElement(item).Element;
        }

    }

    public class BinarySearchTree<K, V> : BinaryTree<K, V>, IEnumerable where K : IComparable
    {
        private BinarySearchTree<KeyValueNode<K, V>> _tree;

        public BinarySearchTree()
        {
            _tree = new BinarySearchTree<KeyValueNode<K, V>>();
        }

        public override IEnumerator<V> GetEnumerator()
        {
            KeyValueNode<K, V>[] items = new KeyValueNode<K, V>[_tree.Count];
            _tree.CopyTo(items, 0);

            var values = items.Select(u => u.Value).ToList();
            return values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Add(K key, V value)
        {
            var item = new KeyValueNode<K, V>(key, value);
            _tree.Add(item);
        }

        public override void Clear()
        {
            _tree.Clear();
        }

        public override bool Contains(K key)
        {
            var item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Contains(item);
        }

        public override V GetValue(K key)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, default(V));
            var ret = _tree.GetItem(item) as KeyValueNode<K, V>;
            return ret.Value;
        }

        public override void CopyTo(V[] array, int arrayIndex)
        {
            KeyValueNode<K, V>[] items = new KeyValueNode<K, V>[_tree.Count];
            _tree.CopyTo(items, 0);

            var values = items.Select(u => u.Value).ToList();
            foreach (var value in values)
            {
                array[arrayIndex++] = value;
            }
        }

        public override bool Remove(K key)
        {
            var item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Remove(item);
        }

        public override int Count { get { return _tree.Count; } }
        public override bool IsReadOnly { get { return _tree.IsReadOnly; } }
    }
}
