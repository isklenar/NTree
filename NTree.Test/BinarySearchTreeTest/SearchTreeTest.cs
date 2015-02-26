using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NTree.BinaryTree.BinarySearchTree;
using NUnit.Framework;

namespace NTree.Test.BinarySearchTree
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
