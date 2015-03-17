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
using System.Collections.Generic;
using System.Linq;
using NTrees.Common;
using NUnit.Framework;

namespace NTrees.Test
{
    public abstract class TreeTestBase{

        protected ITree<TestElement> _tree;

        [Test]
        public void InsertAndContainsTest1Element()
        {
            var testElement = new TestElement(1);
            _tree.Add(testElement);

            Assert.IsTrue(_tree.Contains(testElement));
            Assert.AreEqual(1, _tree.Count);
        }

        [Test]
        public void InsertAndContainsTwoElements()
        {
            var testElement1 = new TestElement(1);
            var testElement2 = new TestElement(2);
            _tree.Add(testElement1);
            _tree.Add(testElement2);

            Assert.IsTrue(_tree.Contains(testElement1));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.AreEqual(2, _tree.Count);

        }


        [Test]
        public void InsertAndContainsSmallScale()
        {
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(5));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(9));
            _tree.Add(new TestElement(10));


            Assert.IsTrue(_tree.Contains(new TestElement(1)));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.IsTrue(_tree.Contains(new TestElement(3)));
            Assert.IsTrue(_tree.Contains(new TestElement(4)));
            Assert.IsTrue(_tree.Contains(new TestElement(5)));
            Assert.IsTrue(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(7)));
            Assert.IsTrue(_tree.Contains(new TestElement(8)));
            Assert.IsTrue(_tree.Contains(new TestElement(9)));
            Assert.IsTrue(_tree.Contains(new TestElement(10)));

            Assert.AreEqual(10, _tree.Count);
        }

        [Test]
        public void InsertAndContainsLargeScale([Range (0,500000,100000)] int n)
        {
            Random random = new Random();
            TestElement[] numbers = new TestElement[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(random.Next());
                _tree.Add(numbers[i]);
            }

            List<TestElement> list = numbers.OrderBy(u => u.Id).Distinct().ToList();

            foreach (var item in list)
            {
                Assert.IsTrue(_tree.Contains(item));
            }

        }

        [Test]
        public void InsertAndRemoveLargeScale()
        {
            int n = 100000;

            Random random = new Random();
            TestElement[] numbers = new TestElement[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(random.Next());
                _tree.Add(numbers[i]);
            }

            var removeOrder = numbers.OrderBy(x => random.Next()).ToList();

            foreach (var item in removeOrder)
            {
                _tree.Remove(item);
                Assert.IsFalse(_tree.Contains(item));
            }

            Assert.AreEqual(0, _tree.Count);
        }

        /*
         *     2
         *   /   \
         *  1      4 <----
         *        /  \
         *       3    8
         *          / \
         *         6  100
         *          \
         *            7
         **/
        [Test]
        public void InsertAndRemoveSmallScale()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));
            _tree.Add(new TestElement(5));

            _tree.Remove(new TestElement(6));
            Assert.IsFalse(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(4)));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.IsTrue(_tree.Contains(new TestElement(7)));
            Assert.IsTrue(_tree.Contains(new TestElement(8)));
            Assert.IsTrue(_tree.Contains(new TestElement(100)));
            Assert.AreEqual(8, _tree.Count);
        }

        /*
         *     2
         *   /   \
         *  1      4 <----
         *        /  \
         *       3    8
         *          / \
         *         6  100
         *          \
         *           7
         **/

        [Test]
        public void RemoveElementWithTwoChildrenTest()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));

            _tree.Remove(new TestElement(4));
            _tree.Remove(new TestElement(1));
            Assert.IsFalse(_tree.Contains(new TestElement(4)));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.IsFalse(_tree.Contains(new TestElement(1)));
            Assert.IsTrue(_tree.Contains(new TestElement(7)));
            Assert.IsTrue(_tree.Contains(new TestElement(8)));
            Assert.IsTrue(_tree.Contains(new TestElement(100)));
            Assert.AreEqual(6, _tree.Count);
        }

        /*
         *     2
         *   /   \
         *  1      4
         *        /  \
         *       3    8
         *          / \
         *   --->  6  100
         *          \
         *           7
         **/

        [Test]
        public void RemoveElementWithOneChildTest()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));

            _tree.Remove(new TestElement(6));
            Assert.IsFalse(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.IsTrue(_tree.Contains(new TestElement(1)));
            Assert.IsTrue(_tree.Contains(new TestElement(7)));
            Assert.IsTrue(_tree.Contains(new TestElement(8)));
            Assert.IsTrue(_tree.Contains(new TestElement(100)));
            Assert.AreEqual(7, _tree.Count);
        }

        /*
        *     2 <---
        *   /   \
        *  1      4
        *        /  \
        *       3    8
        *          / \
        *         6  100
        *          \
        *           7
        **/

        [Test]
        public void RemoveElementRootTest()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));

            _tree.Remove(new TestElement(2));
            Assert.IsFalse(_tree.Contains(new TestElement(2)));
            Assert.IsTrue(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(1)));
            Assert.IsTrue(_tree.Contains(new TestElement(7)));
            Assert.IsTrue(_tree.Contains(new TestElement(8)));
            Assert.IsTrue(_tree.Contains(new TestElement(100)));
            Assert.IsTrue(_tree.Contains(new TestElement(3)));
            Assert.AreEqual(7, _tree.Count);
        }


        /*
        *     2 <---
        *   /   \
        *  1      4 <---
        *        /  \
        *       3    8
        *          / \
        *         6  100 <---
        *          \
        *           7 <---
        **/
        [Test]
        public void RemoveMultipleElementsTest()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));

            _tree.Remove(new TestElement(2));
            _tree.Remove(new TestElement(4));
            _tree.Remove(new TestElement(100));
            _tree.Remove(new TestElement(7));
            
            Assert.IsFalse(_tree.Contains(new TestElement(2)));
            Assert.IsFalse(_tree.Contains(new TestElement(4)));
            Assert.IsFalse(_tree.Contains(new TestElement(7)));
            Assert.IsFalse(_tree.Contains(new TestElement(100)));

            Assert.IsTrue(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(1)));           
            Assert.IsTrue(_tree.Contains(new TestElement(8)));        
            Assert.IsTrue(_tree.Contains(new TestElement(3)));

            Assert.AreEqual(4, _tree.Count);
        }

        [Test]
        public void LinqTest()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));
 
            Assert.IsTrue(_tree.Any(x => x.Id == 1));
            Assert.AreEqual(100, _tree.Max().Id);
            Assert.AreEqual(6 , _tree.ElementAt(4).Id);
        }

        [Test]
        public void EnumeratorTestSmallScale()
        {
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(8));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(7));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(100));

            List<TestElement> expected = new List<TestElement>
            {
                new TestElement(2),
                new TestElement(4),
                new TestElement(1),
                new TestElement(8),
                new TestElement(6),
                new TestElement(7),
                new TestElement(3),
                new TestElement(100)
            };



            expected = expected.OrderBy(u => u.Id).ToList();
            var returned = new List<TestElement>(_tree);

            CollectionAssert.AreEqual(expected, returned);
        }


        [Test]
        public void EnumeratorTestLargeScale([Range(0, 500000, 100000)] int n)
        {
            Random random = new Random();
            TestElement[] numbers = new TestElement[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(random.Next());
            }

            for (int i = 0; i < n; i++)
            {
                _tree.Add(numbers[i]);
            }

            List<TestElement> list1 = numbers.OrderBy(u => u.Id).Distinct().ToList();
            List<TestElement> list2 = new List<TestElement>(_tree); //create from enumerator

            CollectionAssert.AreEqual(list1, list2);
        }

        [Test]
        public void InsertAndContainsGrowingSequence([Range(0, 15000, 5000)] int n)
        {
            TestElement[] numbers = new TestElement[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(i);
                _tree.Add(numbers[i]);
            }

            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(_tree.Contains(numbers[i]));
            }
        }

        [Test]
        public void RemoveThenAddTest([Range(40000, 200000, 40000)] int n)
        {
            TestElement[] numbers = new TestElement[n];
            var random = new Random();
            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(random.Next());
                _tree.Add(numbers[i]);
            }

            numbers = numbers.Distinct().ToArray();

            var toRemove = numbers.Take(n/2);
            foreach (var item in toRemove)
            {
                _tree.Remove(item);
            }

            var addedItems = new TestElement[n/2];
            
            for (int i = 0; i < n/2; i++)
            {
                addedItems[i] = new TestElement(random.Next());
                _tree.Add(addedItems[i]);
            }
            var shouldContain = numbers.Except(toRemove).Union(addedItems).OrderBy(u => u.Id).ToList();
            Assert.AreEqual(shouldContain.Count, _tree.Count);
            CollectionAssert.AreEqual(shouldContain, _tree);

        }

        [Test]
        public void ReadOnlyTest()
        {
            Assert.IsFalse(_tree.IsReadOnly);
        }

        [Test]
        public void ClearTreeTest()
        {
            _tree.Add(new TestElement(1));
            Assert.AreEqual(1, _tree.Count);
            _tree.Clear();
            Assert.AreEqual(0, _tree.Count);
            Assert.IsFalse(_tree.Contains(new TestElement(1)));
        }
    }
}
