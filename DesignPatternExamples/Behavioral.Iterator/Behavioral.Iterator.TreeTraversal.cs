using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DotNetDesignPatternDemos.Structural.Iterator.TreeTraversal
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            this.Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {
        public Node<T> Current { get; set; }
        private readonly Node<T> root;
        private bool yieldedStart;

        public InOrderIterator(Node<T> root)
        {
            this.root = Current = root;
            while (Current.Left != null)
                Current = Current.Left;
        }



        public void Reset()
        {
            Current = root;
            yieldedStart = true;
        }

        public bool MoveNext()
        {
            if (!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else
            {
                var p = Current.Parent;
                while (p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }
                Current = p;
                return Current != null;
            }
        }
    }

    public class BinaryTree1<T>
    {
        private Node<T> root;

        public BinaryTree1(Node<T> root)
        {
            this.root = root;
        }

        public IEnumerable<Node<T>> NaturalInOrder // more readable using IEnumerable and yield return
            => TraverseInOrder(root);

        IEnumerable<Node<T>> TraverseInOrder(Node<T> current) // C# 7 inner method
        {
            if (current.Left != null)
            {
                foreach (var left in TraverseInOrder(current.Left))
                    yield return left;
            }
            yield return current;
            if (current.Right != null)
            {
                foreach (var right in TraverseInOrder(current.Right))
                    yield return right;
            }
        }
    }

    public class BinaryTree2<T>
    {
        private Node<T> root;

        public BinaryTree2(Node<T> root)
        {
            this.root = root;
        }

        public IEnumerator<Node<T>> GetEnumerator() // use IEnumerator interface
        {
            return TraverseInOrder(root).GetEnumerator();
        }

        IEnumerable<Node<T>> TraverseInOrder(Node<T> current)
        {
            if (current.Left != null)
            {
                foreach (var left in TraverseInOrder(current.Left))
                    yield return left;
            }
            yield return current;
            if (current.Right != null)
            {
                foreach (var right in TraverseInOrder(current.Right))
                    yield return right;
            }
        }
    }

    public class BinaryTree3<T>
    {
        private Node<T> root;

        public BinaryTree3(Node<T> root)
        {
            this.root = root;
        }

        public InOrderIterator<T> GetEnumerator() // without using IEnumerator interface
        {
            return new InOrderIterator<T>(root);
        }
    }

    public class Demo
    {
        public static void Main()
        {
            //   1
            //  / \
            // 2   3

            // in-order:  213
            // preorder:  123
            // postorder: 231

            var root = new Node<int>(1,
              new Node<int>(2), new Node<int>(3));

            // C++ style - clumsy
            var it = new InOrderIterator<int>(root);

            while (it.MoveNext())
            {
                Write(it.Current.Value);
                Write(',');
            }
            WriteLine();

            // C# style - more natural
            var tree = new BinaryTree1<int>(root);

            WriteLine(string.Join(",", tree.NaturalInOrder.Select(x => x.Value)));

            // duck typing! tree is enumerable when it has GetEnumerator() that returns an iterator
            // an iterator is something that has Current and MoveNext()
            foreach (var node in new BinaryTree3<int>(root))
                WriteLine(node.Value);

            // duck typing, BinaryTree2 doesn't implement IEnumerable
            foreach (var node in new BinaryTree2<int>(root))
            {
                WriteLine(node.Value);
            }
        }
    }
}