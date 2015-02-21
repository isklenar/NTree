using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTree.BinaryTree.RBTree
{
    public class RBNode<T> : BTNode<T> where T : IComparable
    {
        public RBNode(IComparable item) : base(item)
        {
            Colour = Colour.Red;  
        }

        public Colour Colour { get; set; }


    }

    public enum Colour
    {
        Red,
        Black
    }
}
