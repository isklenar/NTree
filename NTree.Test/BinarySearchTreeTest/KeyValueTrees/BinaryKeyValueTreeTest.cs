using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BinaryTree.BinarySearchTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree.KeyValueTrees
{
    [TestFixture]
    public class BinaryKeyValueTreeTest : KeyValueTreesTestBase
    {
        [SetUp]
        public void Initialize()
        {
            _tree = new BinarySearchTree<int, TestElement>();
        }
    }
}
