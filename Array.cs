using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace InterPrep
{
    class ArrayEx
    {
        // Q: ArrayList A, B, C are sorted int arraylists.
        // When A[i] + B[j] = C[k], remove C[k] from ArrayList C.
        //
        // 3 SUM - http://en.wikipedia.org/wiki/3SUM
        // Take pointers i and j so that i points to the start of the array A and j points to the end of array B.
        // Find the sum A[i] + B[j] and compare it with K
        // - if A[i] + B[j] == K, we have found the pair A[i] and B[j]
        // - if A[i] + B[j] < K, we need to increase the sum, so increment i.
        // - if A[i] + B[j] > K, we need to decrease the sum, so decrement j.
        public static void Remove(List<int> A, List<int> B, ref List<int> C)
        {
            for (int k = C.Count - 1; k >= 0; k--) // Reverse loop for removing elements in array correctly.
            {
                int i = 0, j = B.Count - 1;

                while (i < A.Count && j >= 0)
                {
                    if (A[i] + B[j] == C[k])
                    {
                        C.RemoveAt(k);
                        break;
                    }
                    else if (A[i] + B[j] < C[k])
                    {
                        i++;
                    }
                    else
                    {
                        j--;
                    }
                }
            }
        }

        // Q: Given a sorted sequence of numbers, write a program to find all pairs of numbers that add to 10. 
        // if (head + tail) < 10, then increment head
        // if (head + tail) > 10, then decrement tail
        // if (head + tail) == 10, then increment head and decrement tail
        public static void FindPair(int[] array, int n)
        {
            // Array is assumed to be sorted in ascending order
            
            int head = 0;
            int tail = array.Length - 1;

            int sum;

            while (head <= tail)
            {
                sum = array[head] + array[tail];

                if (sum < n)
                {
                    head++;
                }
                else if (sum > n)
                {
                    tail--;
                }                
                else if (sum == n)
                {
                    Console.WriteLine(array[head] + " + " + array[tail] + " = " + n);
                    head++;
                    tail--;
                }
            }
        }

        // Q: Given a set S of n integers, are there elements a, b, c in S such that a + b + c = 0?
        public static bool ThreeSum(List<int> array)
        {
            for (int i = 0; i < array.Count - 3; i++)
            {
                int a = array[i], b, c;
                int j = i + 1;
                int k = array.Count - 1;

                while (j < k)
                {
                    b = array[j];
                    c = array[k];

                    if (a + b + c == 0)
                    {
                        Console.WriteLine("{0} {1} {2}", a, b, c);
                        return true;
                    }
                    else if (a + b + c > 0)
                    {
                        k--;
                    }
                    else
                    {
                        j++;
                    }
                }
            }

            return false;
        }
        
        // Q: Find the second highest number in an unsorted array
        public static void FindHighestNumbers(int[] array)
        {
            int largest = int.MinValue;
            int second = int.MinValue;
            int third = int.MinValue;

            foreach (int n in array)
            {
                if (n > largest)
                {
                    third = second;
    	            second = largest;
    	            largest = n;
                }
                else if (n > second)
                {
                    third = second;
                    second = n;
                }
                else if (n > third)
                {
                    third = n;
                }
            }
        }
    }

    // Q: Implement a hashtable using arrays
    class HashtableEx
    {
        private List<object> keys = new List<object>();
        private List<object> values = new List<object>();

        public object this[object key]
        {
            get
            {
                int index = keys.IndexOf(key);
                if (index == -1)
                    return null;
                else
                    return values[index];
            }
            set
            {
                if (keys.Contains(key))
                {
                    int index = keys.IndexOf(key);
                    values[index] = value;
                }
                else
                {
                    keys.Add(key);
                    values.Add(value);
                }
            }
        }

        public object Get(object key)
        {
            int index = keys.IndexOf(key);
            return values[index];
        }

        public void Add(object key, object value)
        {
            keys.Add(key);
            values.Add(value);
        }

        public void Remove(object key)
        {
            int index = keys.IndexOf(key);
            keys.RemoveAt(index);
            values.RemoveAt(index);
        }

        public void Clear()
        {
            keys = new List<object>();
            values = new List<object>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArrayEx.FindPair(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 10);

            List<int> A = new List<int> { 1, 2, 4, 5 };
            List<int> B = new List<int> { 4, 6, 7 };
            List<int> C = new List<int> { 2, 3, 4, 5, 6 };

            ArrayEx.Remove(A, B, ref C);

            ArrayEx.ThreeSum(new List<int> { -25, -10, -7, -3, 2, 4, 8, 10 });

            ArrayEx.FindSmallestNumber(10);
        }
    }
}
