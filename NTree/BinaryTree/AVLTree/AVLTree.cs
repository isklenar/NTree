using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTree.Common;

namespace NTree.BinaryTree.AVLTree
{
   /* /// <summary>
    /// Self-balancing binary search tree.
    /// Guarantees O(log(n)) insert, remove and search by performing balancing operations.
    /// </summary>
    /// <typeparam name="T">Type implementing IComparable interface</typeparam>
    public class AVLTree<T> : BinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// Returns maximal depth of tree.
        /// </summary>
        public int MaxDepth
        {
            get { return NodeHeight(Root); }
        }

        /// <summary>
        /// Adds item into the tree
        /// </summary>
        /// <param name="item">item to add</param>
        public override void Add(T item)
        {
            var node = InnerAdd(new BTNode<T>(item));
            if (node == null)
            {
                return;
            }

            var parent = node.Parent;
            while (parent != null)
            {
                int balance = NodeBalance(parent);
                if (balance == 2 || balance == -2)
                {
                    AVLBalanceNode(parent, balance);
                }
                parent = parent.Parent;
            }
            
        }

        /// <summary>
        /// Balances AVL node by performing rotations.
        /// </summary>
        /// <param name="node">node to balance</param>
        /// <param name="balance">balance factor of node</param>
        private void AVLBalanceNode(BTNode<T> node, int balance)
        {
            if (balance == 2)
            {
                int rightBalance = NodeBalance(node.Right);
                if (rightBalance == 1 || rightBalance == 0)
                {
                    RotateLeftLeft(node);
                }
                if (rightBalance == -1)
                {
                    RotateRightLeft(node);
                }
            }
            if (balance == -2)
            {
                int leftBalance = NodeBalance(node.Left);
                if (leftBalance == 1)
                {
                    RotateLeftRight(node);
                }
                if (leftBalance == -1 || leftBalance == 0)
                {
                    RotateRightRight(node);
                }
            }
        }

        /// <summary>
        /// Determines balance of node.
        /// Null node returns 0.
        /// </summary>
        /// <param name="node">node for which we want to know balance</param>
        /// <returns>balance of node</returns>
        private int NodeBalance(BTNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return NodeHeight(node.Left) - NodeHeight(node.Right);
        }

        public override bool Remove(T item)
        {
            var removedNode = RemoveNode(item);
            if (removedNode == null)
            {
                return false;
            }

            var currentNode = removedNode.Parent;
            while (currentNode != null)
            {
                int balance = NodeHeight(currentNode.Left) - NodeHeight(currentNode.Right);
                if (balance == -2)
                {
                    int leftBalance = NodeHeight(currentNode.Left.Left) - NodeHeight(currentNode.Left.Right);
                    if (leftBalance == 1)
                    {
                        RotateLeftRight(currentNode);
                    }
                    else if (leftBalance == -1 || leftBalance == 0)
                    {
                        RotateRightRight(currentNode);
                    }
                }

                if (balance == 2)
                {
                    int rightBalance = NodeHeight(currentNode.Right.Left) - NodeHeight(currentNode.Right.Right);
                    if (rightBalance == 1 || rightBalance == 0)
                    {
                        RotateLeftLeft(currentNode);
                    }
                    else if (rightBalance == -1)
                    {
                        RotateRightLeft(currentNode);
                    }
                }

                currentNode = currentNode.Parent;
            }

            return true;
        }

        /*private BTNode<T> AVLInsert(BTNode<T> root, BTNode<T> node, BTNode<T> parent)
        {
            if (root == null)
            {
                root = new BTNode<T>(node.Element) { Parent = parent };
            }
            else
            {
                int comparison = node.Element.CompareTo(root.Element);
                if (comparison < 0)
                {
                    root.Left = AVLInsert(root.Left, node, root);

                    var leftNode = root.Left;
                    if (leftNode.Height - NodeHeight(root.Right) == 2)
                    {
                        if (node.Element.CompareTo(leftNode.Element) < 0)
                        {
                            root = RotateRight(root);
                        }
                        else
                        {
                            root = RotateLeftRight(root);
                        }
                    }
                }
                else if (comparison > 0)
                {
                    root.Right = AVLInsert((AVLNode<T>) root.Right, node, root);

                    AVLNode<T> rightNode = (AVLNode<T>) root.Right;
                    if (rightNode.Height - NodeHeight(root.Left) == 2)
                    {
                        if (node.Element.CompareTo(rightNode.Element) > 0)
                        {
                            root = RotateLeft(root);
                        }
                        else
                        {
                            root = RotateRightLeft(root);
                        }
                    }
                }
                else
                {
                    return root;
                }
            }

            root.Height = NodeHeight(root);
            return root;
        }

        /// <summary>
        /// Determines height of node.
        /// </summary>
        /// <param name="node">node for which we want to know height</param>
        /// <returns>height of node</returns>
        private int NodeHeight(BTNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + Math.Max(NodeHeight(node.Left), NodeHeight(node.Right));
        }

        /// <summary>
        /// Performs single rotation to the left over a specified node.
        /// </summary>
        /// <param name="node">node to rotate</param>
        private void RotateLeftLeft(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            var tmp = node.Left;
            if (tmp == null)
            {
                return;
            }

            bool isRoot = ReferenceEquals(Root, node);
            BTNode<T> oldParent = node.Parent;

            node.Left = tmp.Right;
            if (node.Left != null)
            {
                node.Left.Parent = node;
            }
            tmp.Parent = node.Parent;
            tmp.Right = node;
            node.Parent = tmp;
            if (isRoot)
            {
                Root = tmp;
            }
            else
            {
                if (ReferenceEquals(oldParent.Left, node))
                {
                    oldParent.Left = tmp;
                }
                else if (ReferenceEquals(oldParent.Right, node))
                {
                    oldParent.Right = tmp;
                }
            }
        }

        /// <summary>
        /// Performs single right rotation over a node.
        /// </summary>
        /// <param name="node">node to rotate</param>
        private void RotateRightRight(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            var tmp = node.Right;
            if (tmp == null)
            {
                return;
            }

            bool isRoot = ReferenceEquals(Root, node);
            BTNode<T> oldParent = node.Parent;

            node.Right = tmp.Left;
            if (node.Right != null)
            {
                node.Right.Parent = node;
            }

            tmp.Parent = node.Parent;
            tmp.Left = node;
            node.Parent = tmp;
            if (isRoot)
            {
                Root = tmp;
            }
            else
            {
                if (ReferenceEquals(oldParent.Left, node))
                {
                    oldParent.Left = tmp;
                }
                else if (ReferenceEquals(oldParent.Right, node))
                {
                    oldParent.Right = tmp;
                }
            }
        }

        /// <summary>
        /// Performs left-right rotation on node.
        /// This is accomplished by first rotating left node.Left and then rotating right node.
        /// </summary>
        /// <param name="node">node to rotate</param>
        private void RotateLeftRight(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            RotateLeftLeft(node.Left);
            RotateRightRight(node);
        }

        /// <summary>
        /// Performs right-left rotation on node.
        /// This is accomplished by first rotation right node.Right and then rotating left node
        /// </summary>
        /// <param name="node"></param>
        private void RotateRightLeft(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            RotateRightRight(node.Right);
            RotateLeftLeft(node);
        }

        /// <summary>
        /// Returns the same elements that is passed as argument.
        /// Should only be used from within key-value tree.
        /// 
        /// For key-value trees, this allows you to pass in a new item with just key and retrieve an element with both key and value.
        /// </summary>
        /// <param name="item">item to retrieve</param>
        /// <returns>item from tree</returns>
        internal IComparable GetItem(T item)
        {
            return FindElement(item).Element;
        }
    }

    /// <summary>
    /// Key-Value version of self-balancing AVL tree.
    /// </summary>
    /// <typeparam name="K">Type implementing IComparable used as a key</typeparam>
    /// <typeparam name="V">Any type used as a value</typeparam>
    public class AVLTree<K, V> : BinaryTree<K, V>, IEnumerable where K : IComparable
    {
        private AVLTree<KeyValueNode<K, V>> _tree;

        public AVLTree()
        {
            _tree = new AVLTree<KeyValueNode<K, V>>();
        }

        public override void Add(K key, V value)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, value);
            _tree.Add(item);
        }

        public override bool Remove(K key)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Remove(item);
        }

        public override bool Contains(K key)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, default(V));
            return _tree.Contains(item);  
        }

        public override V GetValue(K key)
        {
            KeyValueNode<K, V> item = new KeyValueNode<K, V>(key, default(V));
            var ret = _tree.GetItem(item) as KeyValueNode<K, V>;
            return ret.Value;
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

        public override void Clear()
        {
            _tree.Clear();
        }

        public override int Count { get { return _tree.Count; } }
        public override bool IsReadOnly { get { return _tree.IsReadOnly; } }
    }*/
}
