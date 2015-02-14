using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.AVLTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree
{
    [TestFixture]
    class AVLTreeTest : BinaryTreeTestBase
    {
        [SetUp]
        public void Initialize()
        {
            _tree = new AVLTree<TestElement>();
        }
    
        [Test]
        public void MaxDepthTest()
        {
            int n = 1000000;
            TestElement[] numbers = new TestElement[n];

            for (int i = 0; i < n; i++)
            {
                numbers[i] = new TestElement(i);
                _tree.Add(numbers[i]);
            }

            int maxAllowedDepth = (int) (Math.Log(n, 2) * 2);

            AVLTree<TestElement> tree = (AVLTree<TestElement>) _tree;
            Assert.Greater(maxAllowedDepth, tree.MaxDepth);
        }
    }
}
