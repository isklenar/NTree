using System;
using NTree.Common;

namespace NTree.RBTree
{
    class RBNode<T> : TreeNode<T> where T : IComparable
    {
        public RBNode(IComparable item) : base(item)
        {
            Colour = RBColour.Red;
        }

        public RBColour Colour { get; set; }
    }

    public enum RBColour
    {
        Red,
        Black
    }
}
