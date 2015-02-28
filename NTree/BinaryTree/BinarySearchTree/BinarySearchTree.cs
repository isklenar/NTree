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
            InnerAdd(new BTNode<T>(item));
        }

        public override bool Remove(T item)
        {
            if (ReadOnly)
            {
                throw new NotSupportedException("Tree is read only");
            }
            if (RemoveNode(FindElement(item)) != null)
            {
                _count--;
                return true;
            }
            return false;         
        }

        public override ReadOnlyTree<T> AsReadOnly()
        {
            return new ReadOnlyTree<T>(this);
        }

        public override ConcurrentTree<T> AsConcurrentTree()
        {
            return new ConcurrentTree<T>(this);
        }
    }

    public class BinarySearchTree<K, V> : BinaryTree<K, V> where K : IComparable
    {
        public BinarySearchTree()
        {
            _tree = new BinarySearchTree<KeyValueNode<K, V>>();
        }

        public override ReadOnlyTree<K, V> AsReadOnly()
        {
            return new ReadOnlyTree<K, V>(this);
        }

        public override ConcurrentTree<K, V> AsConcurrentTree()
        {
            return new ConcurrentTree<K, V>(this);
        }
    }
}
