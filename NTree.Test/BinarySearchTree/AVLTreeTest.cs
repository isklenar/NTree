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
    public class AVLTreeTest : BinaryTreeTestBase
    {

        [SetUp]
        public void Initialize()
        {
            _tree = new AVLTree<TestElement>();    
        }
    }
}
