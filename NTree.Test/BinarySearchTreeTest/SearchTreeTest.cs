using NTree.BinaryTree.BinarySearchTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTreeTest
{
    [TestFixture]
    public class SearchTreeTest : TreeTestBase
    {
        [SetUp]
        public void Initialize()
        {
            _tree = new BinarySearchTree<TestElement>();
        }        
    }
}
