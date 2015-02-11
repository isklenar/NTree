using System;
using System.Linq;
using NTree.BinarySearchTree;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree
{
    [TestFixture]
    public class BinarySearchTreePrimitivesTest
    {
        private BinaryTree<int> _tree;

        [SetUp]
        public void Initialize()
        {
            _tree = new BinarySearchTree<int>();
        }

        [Test]
        public void InsertAndContainsTest1Element()
        {
            _tree.Add(1);

            Assert.IsTrue(_tree.Contains(1));
            Assert.AreEqual(1, _tree.Count);
        }

        [Test]
        public void InsertAndContainsTwoElements()
        {
            _tree.Add(1);
            _tree.Add(2);

            Assert.IsTrue(_tree.Contains(1));
            Assert.IsTrue(_tree.Contains(2));
            Assert.AreEqual(2, _tree.Count);

        }

        /*
         *     4
         *  /     \
         *  2      6
         *  \      / \
         *   3    5  100
         *   
         **/

        [Test]
        public void InsertAndContainsLayered()
        {
            _tree.Add(4);
            _tree.Add(2);
            _tree.Add(3);
            _tree.Add(6);
            _tree.Add(5);
            _tree.Add(100);

            Assert.IsTrue(_tree.Contains(4));
            Assert.IsTrue(_tree.Contains(2));
            Assert.IsTrue(_tree.Contains(3));
            Assert.IsTrue(_tree.Contains(6));
            Assert.IsTrue(_tree.Contains(5));
            Assert.IsTrue(_tree.Contains(100));

            Assert.AreEqual(6, _tree.Count);
        }

        [Test]
        public void InsertAndContainsLargeScale([Range(0, 500000, 100000)] int n)
        {
            Random random = new Random();
            int[] numbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = random.Next();
            }

            for (int i = 0; i < n; i++)
            {
                _tree.Add(numbers[i]);
            }

            int[] permutation = numbers.OrderBy(x => random.Next()).ToArray();

            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(_tree.Contains(permutation[i]));
            }

            Assert.AreEqual(n, _tree.Count);
        }

        [Test]
        public void ClearTreeTest()
        {
            int n = 100;
            Random random = new Random();
            int[] numbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = random.Next();
            }

            for (int i = 0; i < n; i++)
            {
                _tree.Add(numbers[i]);
            }

            Assert.AreEqual(n, _tree.Count);
            _tree.Clear();
            Assert.AreEqual(0, _tree.Count);
            Assert.IsFalse(_tree.Contains(1));
        }
    }
}
