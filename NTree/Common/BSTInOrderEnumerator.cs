using System;
using System.Collections;
using System.Collections.Generic;

namespace NTree.Common
{
    class BSTInOrderEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private TreeNode<T> _root;
        private TreeNode<T> _current;

        private bool _first = true;

        public BSTInOrderEnumerator(TreeNode<T> root)
        {
            _root = root;
            _current = _root;

            while (_current.Left != null)
            {
                _current = _current.Left;
            }
        }

        public void Dispose()
        {
            //no need to do anything
        }

        public bool MoveNext()
        {
            if (_first)
            {
                _first = false;
                return true;
            }

            if (_current == null)
            {
                return false;
            }

            if (_current.Right != null)
            {
                _current = _current.Right;
                while (_current.Left != null)
                {
                    _current = _current.Left;
                }
            }
            else
            {
                while (true)
                {
                    if (_current.Parent == null)
                    {
                        _current = null;
                        return false;
                    }

                    if (ReferenceEquals(_current.Parent.Left, _current))
                    {
                        _current = _current.Parent;
                        return true;
                    }

                    _current = _current.Parent;
                }
            }

            return true;
        }

        public void Reset()
        {
            _current = _root;
        }

        public T Current
        {
            get { return (T)(_current == null ? null : _current.Element); }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }

}