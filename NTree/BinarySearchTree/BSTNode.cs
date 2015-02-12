﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTree.Common;

namespace NTree.BinarySearchTree
{
    public class BSTNode<T>: TreeNode<T> where T : IComparable
    {
        public BSTNode(IComparable item) : base(item)
        {
        }
    }
}
