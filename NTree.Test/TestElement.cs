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


﻿using System;

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
