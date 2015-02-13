using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.RBTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree
{
    [TestFixture]
    public class RBTreeTest : BinaryTreeTestBase
    {

        [SetUp]
        public void Initialize()
        {
            _tree = new RBTree<TestElement>();
        }
    }
}
