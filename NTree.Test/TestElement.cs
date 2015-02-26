using System;

namespace NTree.Test
{
    public class TestElement : IComparable
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

        protected bool Equals(TestElement other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestElement) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
