namespace NTree.Test.BinarySearchTreeTest
{
    /*
    class AVLTreeTest : TreeTestBase
    {
        [SetUp]
        public void Initialize()
        {
            //_tree = new AVLTree<TestElement>();
        }
    
        [Test]
        public void MaxDepthTest()
        {
            int n = 10000;
            TestElement[] numbers = new TestElement[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(i);
                _tree.Add(numbers[i]);
            }

            int maxAllowedDepth = n == 0 ? 1 : (int)(Math.Log(n, 2) * 1.5); //AVL has depth at most ~1.44*log(n)
            AVLTree<TestElement> tree = (AVLTree<TestElement>) _tree;
            Assert.Greater(maxAllowedDepth, tree.MaxDepth);
        }

        [Test]
        public void RotationTestDebugOnly()
        {
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(5));
        }
    }*/
}
