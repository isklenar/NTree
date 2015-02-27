using NTree.BinaryTree.BinarySearchTree;
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
