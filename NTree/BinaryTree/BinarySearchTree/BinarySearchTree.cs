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
            if (Root == null)
            {
                Root = new BSTNode<T>(item);
                _count++;
                return;
            }

            var currentNode = Root;

            while (currentNode != null)
            {
                var prevNode = (BSTNode<T>)currentNode;

                int comparison = item.CompareTo(currentNode.Element);
                if (comparison == 0) //element exists
                {
                    return;
                }
                if (comparison > 0)
                {
                    currentNode = (BSTNode<T>)currentNode.Right;
                    if (currentNode == null)
                    {
                        currentNode = new BSTNode<T>(item) { Parent = prevNode };
                        prevNode.Right = currentNode;
                        break;
                    }
                }

                if (comparison < 0)
                {
                    currentNode = (BSTNode<T>)currentNode.Left;

                    if (currentNode == null)
                    {
                        currentNode = new BSTNode<T>(item) { Parent = prevNode };
                        prevNode.Left = currentNode;
                        break;
                    }
                }

            }

            _count++;
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

    public class BinarySearchTree<K, V> : IEnumerable<V> where K : IComparable
    {
        private BinarySearchTree<KeyValueNode<K, V>> _tree;

        public BinarySearchTree()
        {
            _tree = new BinarySearchTree<KeyValueNode<K, V>>();
        }

        public IEnumerator<V> GetEnumerator()
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

        public void Add(K key, V value)
        {
            var item = new KeyValueNode<K, V>(key, value);
            _tree.Add(item);
        }

        public void Clear()
        {
            _tree.Clear();
        }

        public bool Contains(K key)
        {
            var item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Contains(item);
        }

        public V GetValue(K key)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, default(V));
            var ret = _tree.GetItem(item) as KeyValueNode<K, V>;
            return ret.Value;
        }

        public void CopyTo(V[] array, int arrayIndex)
        {
            KeyValueNode<K, V>[] items = new KeyValueNode<K, V>[_tree.Count];
            _tree.CopyTo(items, 0);

            var values = items.Select(u => u.Value).ToList();
            foreach (var value in values)
            {
                array[arrayIndex++] = value;
            }
        }

        public bool Remove(K key)
        {
            var item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Remove(item);
        }

        public int Count { get { return _tree.Count; } }
        public bool IsReadOnly { get { return _tree.IsReadOnly; } }
    }
}
