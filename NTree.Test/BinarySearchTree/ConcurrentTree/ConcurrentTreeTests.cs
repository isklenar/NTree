using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BinaryTree.BinarySearchTree;
using NTree.BinaryTree.RBTree;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree.ConcurrentTree
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
        [Test]
        public void InsertAndContainsThreaded([Range(1000, 15000, 3000)] int n, [Range(2,16,4)] int threads)
        {
            Task [] tasks = new Task[threads];
            for (int i = 0; i < threads; i++)
            {
                var i1 = i;
                tasks[i] = Task.Run(() =>
                {
                    TaskFunctionInsertContains(i1, n);
                });
            }

            Task.WaitAll(tasks);

            Assert.AreEqual(threads*n, _tree.Count);
            for (int i = 0; i < n* threads; i++)
            {
                Assert.IsTrue(_tree.Contains(new TestElement(i)));
            }
        }

        private void TaskFunctionInsertContains(int threadNum, int n)
        {
            for (int i = 0; i < n; i++)
            {
                _tree.Add(new TestElement(n*threadNum + i));
            }
            
        }
    }
}
