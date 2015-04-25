using System;
using System.Collections.Generic;

namespace InterPrep
{
    class MatrixEx
    {
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
                int e = m[r, c];

                if (e == x)
                    return true;
                else if (e > x)
                    c--; // move left
                else // if (e < x)
                    r++; // move down
            }

            return false;
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

    class Program
    {
        static void Main(string[] args)
        {
            MatrixEx.FindSmallestNumber(5);

            char[,] matrix = new char[3, 3] { { 'a', 'b', 'c' }, { 'd', 'e', 'f' }, { 'g', 'h', 'i' } };

            MatrixEx.ReplaceMatrix(matrix, 'a');
        }
    }
}
