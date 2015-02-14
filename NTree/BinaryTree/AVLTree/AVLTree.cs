using System;

namespace NTree.BinaryTree.AVLTree
{
    public class AVLTree<T> : BinaryTree<T> where T : IComparable
    {

        public int MaxDepth
        {
            get { return Root == null ? 0 : ((AVLNode<T>) Root).Height; }
        }

        public override void Add(T item)
        {
            if (Contains(item))
            {
                return;
            }
            AVLNode<T> node = new AVLNode<T>(item);

            Root = AVLInsert((AVLNode<T>) Root, node, null);
            _count++;
        }

        public override bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        private AVLNode<T> AVLInsert(AVLNode<T> root, AVLNode<T> node, AVLNode<T> parent)
        {
            if (root == null)
            {
                root = new AVLNode<T>(node.Element) {Parent = parent};
            }
            else
            {
                int comparison = node.Element.CompareTo(root.Element);
                if (comparison < 0)
                {
                    root.Left = AVLInsert((AVLNode<T>) root.Left, node, root);

                    AVLNode<T> leftNode = (AVLNode<T>)root.Left;
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

            root.Height = Math.Max(NodeHeight(root.Left), NodeHeight(root.Right)) + 1;
            return root;
        }

        private int NodeHeight(BTNode<T> node)
        {

            if (node == null)
            {
                return -1;
            }
            AVLNode<T> tmp = (AVLNode<T>) node;
            return tmp.Height;
        }

        private AVLNode<T> RotateLeft(AVLNode<T> node)
        {
            AVLNode<T> tmp = (AVLNode<T>) node.Right;
            node.Right = tmp.Left;
            if (node.Right != null)
            {
                node.Right.Parent = node;
            }
            tmp.Parent = node.Parent;
            tmp.Left = node;
            node.Parent = tmp;

            node.Height = Math.Max(NodeHeight(node.Left), NodeHeight(node.Right)) + 1;
            tmp.Height = Math.Max(NodeHeight(tmp.Right), node.Height) + 1;

            return tmp;
        }

        private AVLNode<T> RotateRight(AVLNode<T> node)
        {
            AVLNode<T> tmp = (AVLNode<T>) node.Left;
            node.Left = tmp.Right;
            if (node.Left != null)
            {
                node.Left.Parent = node;
            }

            tmp.Parent = node.Parent;
            tmp.Right = node;
            node.Parent = tmp;

            node.Height = Math.Max(NodeHeight(node.Left), NodeHeight(node.Right)) + 1;
            tmp.Height = Math.Max(NodeHeight(tmp.Left), node.Height) + 1;

            return tmp;
        }

        private AVLNode<T> RotateLeftRight(AVLNode<T> node)
        {
            AVLNode<T> tmp = (AVLNode<T>) node.Left;
            node.Left = RotateLeft(tmp);
            return RotateRight(node);
        }

        private AVLNode<T> RotateRightLeft(AVLNode<T> node)
        {
            AVLNode<T> tmp = (AVLNode<T>) node.Right;
            node.Right = RotateRight(tmp);
            return RotateLeft(node);
        }

    }
}
