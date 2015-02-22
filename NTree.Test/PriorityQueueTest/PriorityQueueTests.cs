using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Test.BinarySearchTree;
using NUnit.Framework;
using NTree.PriorityQueue;

namespace NTree.Test.PriorityQueueTest
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
            int j = 0;
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
