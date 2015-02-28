using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BinaryTree.BinarySearchTree;
using NTree.BTree;
using NTree.Test.BinarySearchTreeTest.KeyValueTrees;
using NUnit.Framework;

namespace NTree.Test.BTreeTest
{
    [TestFixture(3)]
    [TestFixture(10)]
    [TestFixture(50)]
    [TestFixture(100)]
    public class KeyValueBTreeTests : KeyValueTreesTestBase
    {
        private int _order;

        public KeyValueBTreeTests(int order)
        {
            _order = order;
        }

        [SetUp]
        public void Initialize()
        {
            _tree = new BTree<int, TestElement>(_order);
        }
    }
}
