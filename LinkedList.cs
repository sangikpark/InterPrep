using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace InterPrep
{
    // Node
    class Node<T>
    {
        public T Data;
        public Node<T> Next;

        public Node(T data)
        {
            Data = data;
        }
    }

    // Singly Linke List
    class SinglyLinkeList<T> where T : IComparable<T>
    {
        public Node<T> Head;

        public int NodeCount
        {
            get
            {
                Node<T> node = Head;

                int count = 0;

                while (node != null)
                {
                    count++;
                    node = node.Next;
                }

                return count;
            }
        }

        /// <summary>
        /// Adds a new node containing the specified value at the start of the LinkedList. 
        /// </summary>
        public void AddFirst(Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (Head == null)
            {
                Head = node;
                return;
            }

            node.Next = Head;
            Head = node;

            return;
        }

        /// <summary>
        /// Adds a new node containing the specified value at the end of the LinkedList.
        /// </summary>
        public void AddLast(Node<T> node)
        {
            if (node == null)
                return;

            if (Head == null)
            {
                Head = node;
                return;
            }

            Node<T> current = Head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = node;

            return;
        }

        /// <summary>
        /// Removes the first occurrence of the specified value from the LinkedList. (in a linear time)
        /// Time: O(n), Space: O(1)
        /// </summary>
        public void Remove(Node<T> node)
        {
            if (node == null || Head == null)
                return;

            // Special case for head.
            if (Head == node)
            {
                Head = Head.Next;
                return;
            }

            Node<T> current = Head;
            while (current.Next != null)
            {
                if (current.Next == node)
                {
                    current.Next = current.Next.Next;
                    break;
                }
                current = current.Next;
            }
        }

        public Node<T> Random()
        {
            if (Head == null)
            {
                return null;
            }

            Node<T> current = Head;
            int nodeCount = 0;

            while (current != null)
            {
                nodeCount++;
                current = current.Next;
            }

            Node<T> result = null;
            current = Head;
            Random random = new Random();
            // Random.Next(the inclusive lower bound, the exclusive upper bound)
            int randomIndex = random.Next(0, nodeCount);

            for (int i = 0; i < nodeCount; i++)
            {
                if (i == randomIndex)
                {
                    result = current;
                    break;
                }
                current = current.Next;
            }

            return result;
        }

        /// <summary>
        /// Reverse (w/o recursion)
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>        
        public Node<T> Reverse(Node<T> current)
        {
            Node<T> result = null;

            while (current != null)
            {
                Node<T> temp = current.Next;

                current.Next = result;
                result = current;

                current = temp;
            }

            return result;
        }

        /// <summary>
        /// Reverse (w/ recursion)
        /// </summary>
        public static Node<T> Reverse(Node<T> current, Node<T> result)
        {
            if (current == null)
                return result;

            Node<T> temp = current.Next;

            current.Next = result;
            result = current;

            return Reverse(temp, result);
        }
        
        /// <summary>
        ///  HasCycle (one step one pointer and two steps the other pointer)
        /// </summary>
        public static bool HasCycle(Node<T> head)
        {
            Node<T> fast = head;
            Node<T> slow = head;

            while (fast != null && fast.Next != null && fast.Next.Next != null)
            {
                fast = fast.Next.Next;
                slow = slow.Next;

                if (fast == slow || fast.Next == slow)
                    return true;
            }

            return false;
        }

        /// <summary>
        ///  Given a singly-linked list, devise a time- and space-efficient algorithm to find the 
        ///  mth-to-last element of the list. Time: O(n), Space: O(1).
        /// </summary>        
        public Node<T> FindMthLastNode(int m)
        {
            if (Head == null || m < 0)
            {
                return null;
            }

            // Advance current m elements from beginning, checking for the end of the list
            Node<T> current = Head;
            for (int i = 0; i < m; i++)
            {
                if (current.Next != null)
                {
                    current = current.Next;
                }
                else
                {
                    return null;
                }
            }

            // Start mBehind at beginning and advance pointers together until current hits last element
            Node<T> mBehind = Head;
            while (current.Next != null)
            {
                current = current.Next;
                mBehind = mBehind.Next;
            }

            return mBehind;
        }

        /// <summary>
        /// This algorithm keeps a queue of the (n) last elements. Time: O(n), Space: O(1).
        /// In some scenario, a little tweak allows you to calculate a moving average of (n) last elements.
        /// </summary>
        public Node<T> FindMthLastNodeWithQueue(int m)
        {
            if (Head == null || m < 0)
            {
                return null;
            }

            Node<T> current = Head;
            Queue<Node<T>> q = new Queue<Node<T>>();

            while (current != null)
            {
                if (q.Count > m)
                {
                    q.Dequeue();
                }
                q.Enqueue(current);
                current = current.Next;
            }

            return q.Peek();
        }


        /// <summary>
        /// Summation: 9182 + 517 = 9699. Each digit is stored in a linked list node.
        /// Ex. given two operands: 9 -> 1 -> 8 -> 2 and 5 -> 1 -> 7; the sum becomes 9 -> 6 -> 9 -> 9.
        /// </summary>
        public static Node<int> Sum(Node<int> lhs, Node<int> rhs)
        {
            int leftInt = 0;
            while (lhs != null)
            {
                leftInt *= 10;
                leftInt += lhs.Data;

                lhs = lhs.Next;
            }

            int rightInt = 0;
            while (rhs != null)
            {
                rightInt *= 10;
                rightInt += rhs.Data;

                rhs = rhs.Next;
            }

            int sumInt = leftInt + rightInt;

            string sumString = sumInt.ToString();

            SinglyLinkeList<int> sum = new SinglyLinkeList<int>();
            for (int i = 0; i < sumString.Length; i++)
            {
                Node<int> s = new Node<int>(Int32.Parse(sumString.Substring(i, 1)));
                sum.AddLast(s);
            }

            return sum.Head;
        }


        /// <summary>
        /// Swap every two nodes. Ex. 1 -> 2 -> 3 -> 4 -> 5 becomes 2 -> 1 -> 4 -> 3 -> 5.
        /// </summary>
        public static SinglyLinkeList<T> SwapEveryTwoNodes(SinglyLinkeList<T> linkedList)
        {
            if (linkedList == null || linkedList.Head == null)
                return null;

            Node<T> current = linkedList.Head;
            
            while (current != null && current.Next != null)
            {                
                var temp = current.Next.Data;
                current.Next.Data = current.Data;    
                current.Data = temp;

                current = current.Next.Next;
            }

            return linkedList;
        }

        // Q: Insert a node in a sorted singly linked list
        public void Insert(Node<T> node)
        {
            if (Head == null)
            {
                Head = node;
                return;
            }

            if (Head.Data.CompareTo(node.Data) > 0)
            {
                node.Next = Head;
                Head = node;
                return;
            }

            Node<T> current = Head;

            while (current.Next != null && current.Next.Data.CompareTo(node.Data) < 0)
                current = current.Next;

            node.Next = current.Next;
            current.Next = node;
        }

        // Q: Remove duplicates from an unsorted linked list.
        public static void RemoveDuplicates(Node<T> node)
        {
            Hashtable ht = new Hashtable();
            Node<T> prev = null;

            while (node != null)
            {
                if (ht.Contains(node))
                {
                    prev.Next = node.Next;
                }
                else
                {
                    ht.Add(node, null);
                    prev = node;
                }
                node = node.Next;
            }
        }

        public static void Print(Node<T> node)
        {
            while (node != null)
            {
                Console.Write("{0}", node.Data);
                node = node.Next;

                if (node != null)
                {
                    Console.Write(" -> ");
                }
            }
            Console.WriteLine();
        }
    }

    // Q: Implement AllNodes() with yield.
    class Node
    {
        public int Data;
        public Node Next;
        public IEnumerable<Node> AllNodes()
        {
            Node current = this;

            while (current != null)
            {
                yield return current;
                current = current.Next;
            }

            yield break;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Node head = new Node() { Data = 1, Next = new Node() { Data = 2, Next = new Node() { Data = 3, Next = null } } };
            foreach (Node node in head.AllNodes())
                Console.WriteLine(node.Data);

            SinglyLinkeList<int> linkedList = new SinglyLinkeList<int>();

            Node<int>[] nodes = new Node<int>[10];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new Node<int>(i+1);                
            }
            linkedList.Head = nodes[1];
            linkedList.AddFirst(nodes[0]);
            linkedList.AddLast(nodes[2]);
            linkedList.AddLast(nodes[3]);
            linkedList.AddLast(nodes[4]);
            linkedList.AddLast(nodes[5]);
            SinglyLinkeList<int>.Print(linkedList.Head);
            int count = linkedList.NodeCount;

            linkedList.Remove(nodes[5]);
            SinglyLinkeList<int>.Print(linkedList.Head);

            Node<int> reverseNode = linkedList.Reverse(linkedList.Head);
            SinglyLinkeList<int>.Print(reverseNode);

            reverseNode = linkedList.Reverse(reverseNode);
            SinglyLinkeList<int>.Print(reverseNode);

            bool hasCycle = SinglyLinkeList<int>.HasCycle(reverseNode);

            SinglyLinkeList<int> swapLinkedList = SinglyLinkeList<int>.SwapEveryTwoNodes(linkedList);
            SinglyLinkeList<int>.Print(swapLinkedList.Head);

            //nodes[3].Next = linkedList.Head;
            //hasCycle = SinglyLinkeList<int>.HasCycle(linkedList.Head);

            Node<int> mthNode = linkedList.FindMthLastNode(1);
            mthNode = linkedList.FindMthLastNodeWithQueue(1);

            Node<int> randomNode = linkedList.Random();

            // Summation: 9182 + 517 = 9699.
            SinglyLinkeList<int> lhs = new SinglyLinkeList<int>();
            lhs.Head = new Node<int>(9);
            lhs.AddLast(new Node<int>(1));
            lhs.AddLast(new Node<int>(8));
            lhs.AddLast(new Node<int>(2));

            SinglyLinkeList<int> rhs = new SinglyLinkeList<int>();
            rhs.Head = new Node<int>(5);
            rhs.AddLast(new Node<int>(1));
            rhs.AddLast(new Node<int>(7));

            Node<int> sum = SinglyLinkeList<int>.Sum(lhs.Head, rhs.Head);


            Console.Read();
        }
    }
}

