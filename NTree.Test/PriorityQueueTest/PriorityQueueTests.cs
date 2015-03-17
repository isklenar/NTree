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
using NUnit.Framework;
using NTrees.PriorityQueue;

namespace NTrees.Test.PriorityQueueTest
{
    [TestFixture]
    class KeyValuePriorityQueueTests
    {
        private PriorityQueue<int, TestElement> _queue;

        [SetUp]
        public void Initaliaze()
        {
            _queue = new PriorityQueue<int, TestElement>();
        }

        [Test]
        public void InsertAndPeekTest()
        {
            _queue.Add(1, new TestElement(100));
            _queue.Add(4, new TestElement(123));
            _queue.Add(2, new TestElement(20));
            _queue.Add(3, new TestElement(111));

            Assert.AreEqual(100, _queue.PeekTop().Id);
        }

        [Test]
        public void InsertAndExtractTest()
        {
            _queue.Add(1, new TestElement(100));
            _queue.Add(4, new TestElement(123));
            _queue.Add(2, new TestElement(20));
            _queue.Add(3, new TestElement(111));

            Assert.AreEqual(100, _queue.ExtractTop().Id);
            Assert.AreEqual(20, _queue.ExtractTop().Id);
            Assert.AreEqual(111, _queue.ExtractTop().Id);
            Assert.AreEqual(123, _queue.ExtractTop().Id);
        }

        [Test]
        public void InsertAndExtractTest([Range(0, 100000, 20000)] int n)
        {
            int [] priorities = new int[n];
            for (int i = 0; i < n; i++)
            {
                priorities[i] = i;
            }
           
            TestElement[] elements = new TestElement[n];
            priorities = priorities.OrderBy(u => (new Random()).Next()).ToArray();
            int j = 0;
            foreach (var priority in priorities)
            {
                elements[j] = new TestElement(priority * 2); 
                _queue.Add(priority, elements[j++]);
            }

            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(_queue.PeekTop().Id == (i * 2));
                _queue.ExtractTop();
            }
        }
    }

    [TestFixture]
    class ValuePriorityQueueTests
    {
        private PriorityQueue<TestElement> _queue;

        [SetUp]
        public void Initaliaze()
        {
            _queue = new PriorityQueue<TestElement>();
        }

        [Test]
        public void InsertAndPeekTest()
        {
            _queue.Add(new TestElement(100));
            _queue.Add(new TestElement(123));
            _queue.Add(new TestElement(20));
            _queue.Add(new TestElement(111));

            Assert.AreEqual(20, _queue.PeekTop().Id);
        }

        [Test]
        public void InsertAndExtractTest()
        {
            _queue.Add(new TestElement(100));
            _queue.Add(new TestElement(123));
            _queue.Add(new TestElement(20));
            _queue.Add(new TestElement(111));

            Assert.AreEqual(20, _queue.ExtractTop().Id);
            Assert.AreEqual(100, _queue.ExtractTop().Id);
            Assert.AreEqual(111, _queue.ExtractTop().Id);
            Assert.AreEqual(123, _queue.ExtractTop().Id);
        }

        [Test]
        public void InsertAndExtractTest([Range(0, 100000, 20000)] int n)
        {
            int[] priorities = new int[n];
            for (int i = 0; i < n; i++)
            {
                priorities[i] = i;
            }

            TestElement[] elements = new TestElement[n];
            for (int i = 0; i < n; i++)
            {
                elements[i] = new TestElement(i * 2);
            }
            elements = elements.OrderBy(u => (new Random()).Next()).ToArray();
            
            foreach (var element in elements)
            {
                _queue.Add(element);
            }

            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(_queue.PeekTop().Id == (i * 2));
                _queue.ExtractTop();
            }
        }
    }
}
