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
    class PriorityQueueTests
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
    }
}
