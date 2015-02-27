using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.BTree;
using NUnit.Framework;

namespace NTree.Test.BTreeTest
{
    [TestFixture(3)]
    [TestFixture(10)]
    [TestFixture(50)]
    [TestFixture(100)]
    public class BTreeTests : TreeTestBase
    {
        private int _order;

        public BTreeTests(int order)
        {
            _order = order;
        }

        [SetUp]
        public void Initialize()
        {
            _tree = new BTree<TestElement>(_order);
        }
    }
}
