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
using System.Linq;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test
{
    public abstract class KeyValueTreesTestBase
    {
        protected ITree<int, TestElement> _tree;

        [Test]
        public void InsertAndRetrieve([Range(0, 100000, 20000)] int n)
        {
            Random random = new Random();
            var keys = Enumerable.Range(0, n).OrderBy(r => random.Next());

            foreach (var key in keys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            Assert.AreEqual(n, _tree.Count);

            foreach (var key in keys)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }
        }

        [Test]
        public void InsertAndRemove([Range(0, 100000, 20000)] int n)
        {
            Random random = new Random();
            var keys = Enumerable.Range(0, n).OrderBy(r => random.Next());

            foreach (var key in keys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            var firstHalf = keys.Take(n / 2).ToList();
            var secondHalf = keys.Except(firstHalf);
            foreach (var i in firstHalf)
            {
                _tree.Remove(i);
            }

            Assert.AreEqual(n / 2, _tree.Count);

            foreach (var key in secondHalf)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }
        }

        [Test]
        public void InsertRemoveThenAdd([Range(0, 100000, 20000)] int n)
        {
            Random random = new Random();
            var keys = Enumerable.Range(0, n).OrderBy(r => random.Next());

            foreach (var key in keys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            var firstHalf = keys.Take(n / 2).ToList();
            var secondHalf = keys.Except(firstHalf);
            foreach (var i in firstHalf)
            {
                _tree.Remove(i);
            }

            Assert.AreEqual(n / 2, _tree.Count);       

            var addedKeys = Enumerable.Range(n, n/2).OrderBy(r => random.Next());
            foreach(var key in addedKeys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            Assert.AreEqual(n, _tree.Count);

            foreach (var key in addedKeys)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }

            foreach (var key in secondHalf)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }
        }

        [Test]
        public void ClearTreeTest()
        {
            _tree.Add(1, new TestElement(1));
            Assert.AreEqual(1, _tree.Count);
            Assert.IsTrue(_tree.Contains(1));

            _tree.Clear();

            Assert.AreEqual(0, _tree.Count);
            Assert.IsFalse(_tree.Contains(1));
        }
    }
}
