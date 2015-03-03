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
