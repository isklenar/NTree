using NTree.BinaryTree.RBTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTreeTest
{
    [TestFixture]
    public class RBTreeTest : TreeTestBase
    {
        [SetUp]
        public void Initialize()
        {
            _tree = new RBTree<TestElement>();
        }
    }
}
