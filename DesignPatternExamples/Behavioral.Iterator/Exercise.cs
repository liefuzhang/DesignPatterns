using System;
using System.Collections.Generic;

namespace Coding.Exercise
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder => TravesePreOrder(this);

        IEnumerable<T> TravesePreOrder(Node<T> node)
        {
            yield return node.Value;

            if (node.Left != null)
                foreach (var n in TravesePreOrder(node.Left))
                {
                    yield return n;
                }

            if (node.Right != null)
                foreach (var n in TravesePreOrder(node.Right))
                {
                    yield return n;
                }
        }
    }
}
