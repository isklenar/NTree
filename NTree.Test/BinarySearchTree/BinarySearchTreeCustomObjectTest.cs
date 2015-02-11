using System;
using System.Linq;
using NTree.BinarySearchTree;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree
{
    [TestFixture]
    public class BinarySearchTreeCustomObjectTest
    {
        private BinaryTree<TestElement> _tree;

        [SetUp]
        public void Initialize()
        {
            _tree = new BinarySearchTree<TestElement>();
        }

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
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(6));
            _tree.Add(new TestElement(5));
            _tree.Add(new TestElement(100));

            Assert.IsTrue(_tree.Contains(new TestElement(4)));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.IsTrue(_tree.Contains(new TestElement(3)));
            Assert.IsTrue(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(6)));
            Assert.IsTrue(_tree.Contains(new TestElement(100)));

            Assert.AreEqual(6, _tree.Count);
        }

        [Test]
        public void InsertAndContainsLargeScale([Range (0,500000,100000)] int n)
        {
            Random random = new Random();
            int[] numbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = random.Next();
            }
           
            for (int i = 0; i < n; i++)
            {
                _tree.Add(new TestElement(numbers[i]));
            }

            int[] permutation = numbers.OrderBy(x => random.Next()).ToArray();

            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(_tree.Contains(new TestElement(permutation[i])));
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
                _tree.Add(new TestElement(numbers[i]));
            }

            Assert.AreEqual(n, _tree.Count);
            _tree.Clear();
            Assert.AreEqual(0, _tree.Count);
            Assert.IsFalse(_tree.Contains(new TestElement(1)));
        }
    }
}
