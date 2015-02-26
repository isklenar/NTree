using NTree.BinaryTree.BinarySearchTree;
using NTree.Test.BinarySearchTree;
using NTree.Test.BinarySearchTree.KeyValueTrees;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTreeTest.KeyValueTrees
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
