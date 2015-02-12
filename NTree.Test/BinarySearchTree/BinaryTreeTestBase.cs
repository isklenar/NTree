using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BinarySearchTree;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree
{
    public abstract class BinaryTreeTestBase{

        protected BinaryTree<TestElement> _tree;

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
            Assert.IsTrue(_tree.Contains(new TestElement(5)));
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
            Assert.IsFalse(_tree.Contains(new TestElement(4)));
            Assert.IsTrue(_tree.Contains(new TestElement(2)));
            Assert.IsTrue(_tree.Contains(new TestElement(1)));
            Assert.IsTrue(_tree.Contains(new TestElement(7)));
            Assert.IsTrue(_tree.Contains(new TestElement(8)));
            Assert.IsTrue(_tree.Contains(new TestElement(100)));
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

            List<TestElement> list1 = numbers.OrderBy(u => u.Id).ToList();
            List<TestElement> list2 = new List<TestElement>(_tree);

            CollectionAssert.AreEqual(list1, list2);
        }
    }
}
