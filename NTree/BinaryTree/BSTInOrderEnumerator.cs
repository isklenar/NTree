// The MIT License (MIT)

// Copyright (c) 2015 Ivo Sklenar

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace NTree.BinaryTree
{
    public class BSTInOrderEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private BTNode<T> _root;
        private BTNode<T> _current;

        private bool _first = true;

        internal BSTInOrderEnumerator(BTNode<T> root)
        {
            _root = root;
            _current = _root;

            while (_current != null &&_current.Left != null)
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
                return _current != null;
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
            _first = true;
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