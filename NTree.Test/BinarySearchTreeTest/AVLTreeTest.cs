// The MIT License (MIT)

// Copyright (c) 2015 Ivo Sklenar

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


﻿namespace NTree.Test.BinarySearchTreeTest
{
    /*
    class AVLTreeTest : TreeTestBase
    {
        [SetUp]
        public void Initialize()
        {
            //_tree = new AVLTree<TestElement>();
        }
    
        [Test]
        public void MaxDepthTest()
        {
            int n = 10000;
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

        [Test]
        public void RotationTestDebugOnly()
        {
            _tree.Add(new TestElement(1));
            _tree.Add(new TestElement(2));
            _tree.Add(new TestElement(3));
            _tree.Add(new TestElement(4));
            _tree.Add(new TestElement(5));
        }
    }*/
}
