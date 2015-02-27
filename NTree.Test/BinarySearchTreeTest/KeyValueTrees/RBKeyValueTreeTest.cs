using NTree.BinaryTree.RBTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTreeTest.KeyValueTrees
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
