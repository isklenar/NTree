using System;
using System.Linq;
using NTree.Common;
using NUnit.Framework;

namespace NTree.Test
{
    public abstract class KeyValueTreesTestBase
    {
        protected ITree<int, TestElement> _tree;

        [Test]
        public void InsertAndRetrieve([Range(0, 100000, 20000)] int n)
        {
            Random random = new Random();
            var keys = Enumerable.Range(0, n).OrderBy(r => random.Next());

            foreach (var key in keys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            Assert.AreEqual(n, _tree.Count);

            foreach (var key in keys)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }
        }

        [Test]
        public void InsertAndRemove([Range(0, 100000, 20000)] int n)
        {
            Random random = new Random();
            var keys = Enumerable.Range(0, n).OrderBy(r => random.Next());

            foreach (var key in keys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            var firstHalf = keys.Take(n / 2).ToList();
            var secondHalf = keys.Except(firstHalf);
            foreach (var i in firstHalf)
            {
                _tree.Remove(i);
            }

            Assert.AreEqual(n / 2, _tree.Count);

            foreach (var key in secondHalf)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }
        }

        [Test]
        public void InsertRemoveThenAdd([Range(0, 100000, 20000)] int n)
        {
            Random random = new Random();
            var keys = Enumerable.Range(0, n).OrderBy(r => random.Next());

            foreach (var key in keys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            var firstHalf = keys.Take(n / 2).ToList();
            var secondHalf = keys.Except(firstHalf);
            foreach (var i in firstHalf)
            {
                _tree.Remove(i);
            }

            Assert.AreEqual(n / 2, _tree.Count);       

            var addedKeys = Enumerable.Range(n, n/2).OrderBy(r => random.Next());
            foreach(var key in addedKeys)
            {
                _tree.Add(key, new TestElement(key + 10));
            }

            Assert.AreEqual(n, _tree.Count);

            foreach (var key in addedKeys)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }

            foreach (var key in secondHalf)
            {
                Assert.IsTrue(_tree.Contains(key));
                Assert.IsTrue(_tree.GetValue(key).Id == key + 10);
            }
        }
    }
}
