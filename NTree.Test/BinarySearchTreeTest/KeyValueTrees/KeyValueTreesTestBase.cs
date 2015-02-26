using System;
using System.Linq;
using NTree.Common;
using NTree.Test.BinarySearchTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTreeTest.KeyValueTrees
{
    public abstract class KeyValueTreesTestBase
    {
        protected Tree<int, TestElement> _tree;

        [Test]
        public void InsertAndRetrieve([Range(0, 100000, 20000)] int n)
        {
            int [] keys = new int[n];
            TestElement[] values = new TestElement[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                keys[i] = random.Next();
                values[i] = new TestElement(keys[i] + 10);
                _tree.Add(keys[i], values[i]);
            }
            int[] distinctKeys = keys.Distinct().ToArray();

            Assert.IsTrue(distinctKeys.Count() == _tree.Count);

            foreach (var distinctKey in distinctKeys)
            {
                Assert.IsTrue(_tree.Contains(distinctKey));
                Assert.IsTrue(_tree.GetValue(distinctKey).Id == distinctKey + 10);
            }
        }

        [Test]
        public void InsertAndRemove([Range(0, 100000, 20000)] int n)
        {
            int[] keys = new int[n];
            TestElement[] values = new TestElement[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                keys[i] = random.Next();
                values[i] = new TestElement(keys[i] + 10);
                _tree.Add(keys[i], values[i]);
            }
            int[] distinctKeys = keys.OrderBy(u => random.Next()).Distinct().ToArray();
            int[] half = distinctKeys.Take(distinctKeys.Length/2).ToArray();
            int[] remaining = distinctKeys.Except(half).ToArray();
            foreach (var i in half)
            {
                _tree.Remove(i);
            }

            Assert.IsTrue(remaining.Count() == _tree.Count);

            foreach (var i in remaining)
            {
                Assert.IsTrue(_tree.Contains(i));
                Assert.IsTrue(_tree.GetValue(i).Id == i + 10);
            }
        }
    }
}
