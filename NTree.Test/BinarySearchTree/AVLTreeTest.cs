using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BinaryTree.AVLTree;
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
        public void MaxDepthTest([Range(0, 1000000, 200000)] int n)
        {
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
    }
}
