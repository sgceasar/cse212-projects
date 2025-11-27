using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomLinkedList
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Data;
            public Node Next;
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node head;
        private Node tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }
        public bool IsEmpty()
        {
            return head == null;
        }

        // Problem 1: Insert the Tail
        public void AddTail(T item)
        {
            Node newNode = new Node(item);
            if (IsEmpty())
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
        }

        // Problem 2: Remove the Tail
        public bool RemoveTail()
        {
            if (IsEmpty())
            {
                return false;
            }

            if (head == tail) // Only 1 node
            {
                head = null;
                tail = null;
                return true;
            }

            // more than 1 node: need to traverse!!
            Node current = head;
            while (current.Next != tail)
            {
                current = current.Next;
            }
            // current is now the node before tail
            current.Next = null;
            tail = current;
            return true;
        }

        // Problem 3: Remove (1th occurrence of item)
        public bool Remove(T item)
        {
            if (IsEmpty())
                return false;

            Node current = head;
            Node previous = null;

            while (current != null && !current.Data.Equals(item))
            {
                previous = current;
                current = current.Next;
            }

            if (current == null)
            {
                return false; // not found :)
            }

            // found
            if (previous == null)
            {
                // removing head
                head = current.Next;
                if (head == null)
                {
                    tail = null;
                }
            }
            else
            {
                previous.Next = current.Next;
                if (previous.Next == null)
                {
                    tail = previous;
                }
            }

            return true;
        }

        // Problem 4: Replace (replace 1th occurrence of oldValue with newValue)
        public bool Replace(T oldValue, T newValue)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.Equals(oldValue))
                {
                    current.Data = newValue;
                    return true;
                }
                current = current.Next;
            }
            return false; // not found
        }

        // Problem 5: Reversed Iterator
        public IEnumerable<T> GetReversed()
        {
            // Colect and enumerate
            Stack<T> stack = new Stack<T>();
            Node current = head;
            while (current != null)
            {
                stack.Push(current.Data);
                current = current.Next;
            }

            while (stack.Count > 0)
            {
                yield return stack.Pop();
            }
        }

        // IEnumerable<T> implementation to allow foreach
        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // For testing/debug
        public override string ToString()
        {
            List<string> items = new List<string>();
            foreach (var x in this)
            {
                items.Add(x?.ToString());
            }
            return "[" + string.Join(" -> ", items) + "]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();

            // Test Insert Tail
            list.AddTail(1);
            list.AddTail(2);
            list.AddTail(3);
            Console.WriteLine("After AddTail: " + list);

            // Test Remove
            list.Remove(2);
            Console.WriteLine("After Remove(2): " + list);

            // Test Replace
            list.Replace(3, 42);
            Console.WriteLine("After Replace(3 -> 42): " + list);

            // Test Remove Tail
            list.RemoveTail();
            Console.WriteLine("After RemoveTail: " + list);

            // Add moree
            list.AddTail(5);
            list.AddTail(6);
            Console.WriteLine("Add more tail: " + list);

            // Test reversed Iterator
            Console.Write("Reversed iteration: ");
            foreach (var x in list.GetReversed())
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();
        }
    }
}
