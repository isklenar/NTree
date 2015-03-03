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
using System.Threading.Tasks;
using NTree.BinaryTree.RBTree;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTreeTest.ConcurrentTree
{
    [TestFixture]
    public class ConcurrentTreeTests
    {
        private ConcurrentTree<TestElement> _tree;

        [SetUp]
        public void Initialize()
        {
            _tree = (new RBTree<TestElement>()).AsConcurrentTree();
        }

        /// <summary>
        /// Concurently inserts and queries data from tree.
        /// 
        /// Creates threads/2 write threads and threads/2 read threads. Write threads write n items each, while read threads query n items each.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="threads"></param>
        [Test]
        public void InsertAndContainsThreaded([Range(0, 10000, 2000)] int n, [Range(2,34,8)] int threads)
        {
            Task [] tasks = new Task[threads];
            for (int i = 0; i < threads/2; i++) //create write threads
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionInsert(i1, n);
                });
            }


            for (int i = threads / 2; i < threads; i++) //contains threads
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionContains(i1 - (threads/2), n);
                });
            }            
            Task.WaitAll(tasks);
            Assert.AreEqual((threads/2) * n, _tree.Count);
        }

        [Test]
        public void InsertAndDeleteThreaded([Range(0, 10000, 2000)] int n, [Range(2, 34, 8)] int threads)
        {
            Task[] tasks = new Task[threads];
            for (int i = 0; i < threads; i++) //create write threads
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionInsert(i1, n);
                });
            }

            Task.WaitAll(tasks);

            for (int i = 0; i < threads; i++) //delete threads
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionDelete(i1, n);
                });
            }
            Task.WaitAll(tasks);
            Assert.AreEqual(0, _tree.Count);
        }

        /// <summary>
        /// Each threads inserts n values. After that, threads delete the first n values while remaining threads insert new values at the same time.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="threads"></param>
        [Test]
        public void InsertDeleteThenInsert([Range(0, 10000, 2000)] int n, [Range(2, 34, 8)] int threads)
        {
            Task[] tasks = new Task[threads];

            Random random = new Random();
            
            var secondInsert = Enumerable
                .Range(threads * n + 1, 2 * threads * n)
                .OrderBy(r => random.Next())
                .Select(u => new TestElement(u))
                .ToList();

            for (int i = 0; i < threads; i++) //create write threads
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionInsert(i1, n);
                });
            }

            Task.WaitAll(tasks);
            tasks = new Task[threads * 2];
            for (int i = 0; i < threads; i++) //create write threads
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionDelete(i1, n);
                });
            }

            for (int i = threads; i < threads * 2; i++)
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionInsert(i1, n);
                });
            }

            Task.WaitAll(tasks);

            Assert.AreEqual(n * threads, _tree.Count);
        }

        private void TaskFunctionDelete(int threadNum, int n)
        {
            for (int i = 0; i < n; i++)
            {
                _tree.Remove(new TestElement(threadNum*n + i));
            }
        }

        private void TaskFunctionInsert(int threadNum, int n)
        {
            for (int i = 0; i < n; i++)
            {
                _tree.Add(new TestElement(n*threadNum + i));
            }
            
        }

        private void TaskFunctionContains(int threadNum, int n)
        {
            for (int i = 0; i < n; i++)
            {
                _tree.Contains(new TestElement(n * threadNum + i));
            }
        }
    }
}
