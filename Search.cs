using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InterPrep
{
    class Search
    {
        public static int BinarySearch(int[] array, int target, bool isRecursive)
        {
            if (isRecursive)
                return BinarySearch_Recursive(array, target, 0, array.Length-1);
            else
                return BinarySearch_Iterative(array, target);
        }

        private static int BinarySearch_Recursive(int[] array, int target, int lower, int upper)
        {
            int center, range;

            range = upper - lower;
            if (range < 0)
            {
                throw new Exception("Limits reversed");
            }
            else if (range == 0 && array[lower] != target)
            {
                throw new Exception("Element is not in array");
            }
            if (array[lower] > array[upper])
            {
                throw new Exception("Array not sorted");
            }

            center = (range / 2) + lower;
            if (target == array[center])
            {
                return center;
            }
            else if (target < array[center])
            {
                return BinarySearch_Recursive(array, target, lower, center - 1);
            }
            else
            {
                return BinarySearch_Recursive(array, target, center + 1, upper);
            }
        }

        private static int BinarySearch_Iterative(int[] array, int target)
        {
            int lower = 0, upper = array.Length - 1;
            int center, range;

            while (true)
            {
                range = upper - lower;

                if (range < 0)
                {
                    throw new Exception("Limits reversed");
                }
                else if (range == 0 && array[lower] != target)
                {
                    throw new Exception("Element is not in array");
                }

                if (array[lower] > array[upper])
                {
                    throw new Exception("Array not sorted");
                }

                center = (range / 2) + lower;

                if (array[center] == target)
                {
                    return center;
                }
                else if (array[center] < target)
                {
                    upper = center - 1;
                }
                else
                {
                    lower = center + 1;
                }
            }            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Debug.Assert(Search.BinarySearch(array, 2, true) == 1);
            Debug.Assert(Search.BinarySearch(array, 2, false) == 1);
            
        }
    }
}
