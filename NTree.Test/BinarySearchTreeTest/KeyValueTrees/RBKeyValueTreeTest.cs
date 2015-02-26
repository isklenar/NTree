using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BinaryTree.RBTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree.KeyValueTrees
{
    [TestFixture]
    public class RBKeyValueTreeTest : KeyValueTreesTestBase
    {
        [SetUp]
        public void Initialize()
        {
            _tree = new RBTree<int, TestElement>();
        }
    }
}
