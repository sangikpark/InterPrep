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

        // N machines
        // Each machine has M integers sorted in increasing order
        // Find K smallest integers across all machines
        public static void FindSmallestNumber(int k)
        {
            // N = 3
            // M = 10
            const int N = 3;
            const int M = 10;

            int[,] array = new int[N, M] { 
                                        { 1, 3, 5, 6, 7, 8, 10, 11, 13, 20 },
                                        { 4, 5, 7, 17, 20, 25, 26, 28, 30, 32 },
                                        { 2, 4, 5, 13, 15, 16, 17, 18, 19, 21 } };

            int[] currentIndex = new int[N] { 0, 0, 0 }; // Current index for each machine.

            for (int i = 0; i < k; i++)
            {
                int smallestInteger = 0;
                int foundMachineIndex = 0;

                for (int j = 0; j < N; j++)
                {
                    int currentInteger = array[j, currentIndex[j]];

                    if (j == 0 || currentInteger < smallestInteger)
                    {
                        smallestInteger = currentInteger;
                        foundMachineIndex = j;
                    }
                }

                currentIndex[foundMachineIndex]++;

                System.Console.WriteLine(smallestInteger);
            }
        }

        // Q: Given an n x n matrix, where every row and column is sorted in increasing order. 
        // Given a number x, how to decide whether this x is in the matrix. The designed algorithm should have linear time complexity.
        //
        // 1. Start with top right
        // 2. Loop
        //  a. e == x; return true
        //  b. e > x; move left
        //  c. e < x; move down
        bool SearchMatrix(int[,] m, int n, int x)
        {
            if (n == 0)
                return false;

            int r = 0, c = n - 1; // top right
            while (r < n && c >= 0)
            {
                int e = m[r,c];

                if (e == x)
                    return true;
                else if (e > x)
                    c--; // move left
                else // if (e < x)
                    r++; // move down
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

        // Q: In NxN matrix, if an element is 'a', its entire row and column are set to 'a'.
        // a b c    a a a
        // d e f -> a e g
        // g h i    a c d
        public static void ReplaceMatrix(char[,] matrix, char ch)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            bool[] row = new bool[rowLength];
            bool[] col = new bool[colLength];

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (matrix[i, j] == ch)
                    {
                        row[i] = true;
                        col[j] = true;
                    }
                }
            }

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (row[i] == true || col[j] == true)
                        matrix[i, j] = ch;

                    Console.Write(matrix[i, j]);
                }
                Console.Write("\r\n");
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

            char[,] matrix = new char[3,3] { {'a', 'b', 'c'}, {'d', 'e', 'f'}, {'g', 'h', 'i'}};

            ArrayEx.ReplaceMatrix(matrix, 'a');
        }
    }
}
