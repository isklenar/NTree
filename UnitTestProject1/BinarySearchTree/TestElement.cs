using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.Test.BinarySearchTree
{
    class TestElement : IComparable
    {
        public TestElement(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public int CompareTo(object obj)
        {
            TestElement other = obj as TestElement;
            if (other == null)
            {
                throw new ArgumentException("Argument obj is not of type " + typeof(TestElement));
            }

            if (this.Id < other.Id)
            {
                return -1;
            }
            if (this.Id > other.Id)
            {
                return 1;
            }

            //equal
            return 0;
        }

    }
}
