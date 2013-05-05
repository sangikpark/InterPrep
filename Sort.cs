using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InterPrep
{
    class SelectionSorts
    {
        /// <summary>
        /// Selection sort - in-place comparison sort.
        /// It has O(n^2) complexity, making it inefficient on large lists, and generally performs worse than the similar insertion sort. 
        /// Selection sort is noted for its simplicity, and also has performance advantages over more complicated algorithms in certain situations.
        /// Better than bubble sort in almost all cases.
        /// 
        /// Best case - O(n^2)
        /// Average case - O(n^2)
        /// Worst case - O(n^2)  
        /// Stability - depends on the implementation
        /// </summary>
        /// <param name="inputArray"></param>
        public static void SelectionSort(ref int[] inputArray)
        {
            int indexOfMin = 0;
            for (int iterator = 0; iterator < inputArray.Length - 1; iterator++)
            {
                indexOfMin = iterator;
                for (int index = iterator + 1; index < inputArray.Length; index++)
                {
                    if (inputArray[index] < inputArray[indexOfMin])
                        indexOfMin = index;
                }
                SortUtil.Swap(ref inputArray[iterator], ref inputArray[indexOfMin]);
            }
        }
    }

    class InsertionSorts
    {
        /// <summary>
        /// Insertion sort - It is a simple sorting algorithm that is relatively efficient for small lists and mostly-sorted lists, 
        /// and often is used as part of more sophisticated algorithms. 
        /// It works by taking elements from the list one by one and inserting them in their correct position into a new sorted list.
        /// Best case - O(n)
        /// Average case - O(n^2)
        /// Worst case - O(n^2) 
        /// Stability - stable
        /// </summary>
        /// <param name="inputArray"></param>
        public static void InsertionSort(ref int[] inputArray)
        {
            for (int index = 1; index < inputArray.Length; index++)
            {
                int j = index;
                int temp = inputArray[index];

                while ((j > 0) && (inputArray[j - 1] > temp))
                {
                    inputArray[j] = inputArray[j - 1];
                    j--;
                }
                inputArray[j] = temp;
            }
        }
    }

    class ExchangeSorts
    {
        public static void QuickSort(int[] inputArray)
        {
            int left = 0;
            int right = inputArray.Length - 1;

            QuickSort(inputArray, left, right);
        }

        /// <summary>
        /// Quick Sort - It is a divide and conquer algorithm which relies on a partition operation: 
        /// to partition an array, choose an element, called a pivot, move all smaller elements before the pivot, 
        /// and move all greater elements after it. This can be done efficiently in linear time and in-place. 
        /// Later, recursively sort the lesser and greater sublists.
        /// Best case - O(n log n)
        /// Average case - O(n log n)
        /// Worst case - O(n^2)  
        /// </summary>
        private static void QuickSort(int[] inputArray, int left, int right)
        {
            int i = left, j = right;

            long pivot = inputArray[((left + right) / 2)];

            while (i <= j)
            {
                while (inputArray[i] < pivot)
                {
                    i++;
                }
                while (pivot < inputArray[j])
                {
                    j--;
                }
                if (i <= j)
                {
                    SortUtil.Swap(ref inputArray[i], ref inputArray[j]);
                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                QuickSort(inputArray, left, j);
            }
            if (i < right)
            {
                QuickSort(inputArray, i, right);
            }
        }
    }

    class MergeSorts
    {
        /// <summary>
        /// Merge sort - It takes advantage of the ease of merging already sorted lists into a new sorted list. 
        /// It starts by comparing every two elements (i.e., 1 with 2, then 3 with 4...) and swapping them if the first should come after the second. 
        /// It then merges each of the resulting lists of two into lists of four, then merges those lists of four, and so on; 
        /// until at last two lists are merged into the final sorted list.
        /// In most implementations it is stable, meaning that it preserves the input order of equal elements in the sorted output. 
        /// It is an example of the divide and conquer algorithmic paradigm.
        /// Best case - O(n) or O(n log n)
        /// Average case - O(n log n)
        /// Worst case - O(n log n)  
        /// </summary>
        public static int[] MergeSort(int[] inputArray)
        {
            if (inputArray.Length == 1)
                return inputArray;

            int middle = inputArray.Length / 2; 
            
            int[] left = new int[middle]; 
            for (int i = 0; i < middle; i++)
            {
                left[i] = inputArray[i];
            }
            
            int[] right = new int[inputArray.Length - middle];
            for (int i = 0; i < inputArray.Length - middle; i++)
            {
                right[i] = inputArray[i + middle];
            }
            
            left = MergeSort(left);
            right = MergeSort(right);
            
            int leftptr = 0;
            int rightptr = 0;

            int[] sorted = new int[inputArray.Length];

            for (int i = 0; i < inputArray.Length; i++)
            {
                if (leftptr < left.Length && rightptr < right.Length)
                {
                    if (left[leftptr] < right[rightptr]) 
                        sorted[i] = left[leftptr++];
                    else
                        sorted[i] = right[rightptr++];
                }
                else
                {
                    if (leftptr < left.Length)
                        sorted[i] = left[leftptr++];
                    else 
                        sorted[i] = right[rightptr++];
                }
            } 
            
            return sorted;
        }
    }

    class SortEx
    {
        // Q: Given two ascending sorted arrays, how to combine the two arrays into one sorted array?
        public static int[] MergeSort(int[] array1, int[] array2)
        {
            int[] resultArray = new int[array1.Length + array2.Length];

            int nIndex1 = 0;
            int nIndex2 = 0;
            int nIndexResult = 0;

            while (nIndex1 < array1.Length && nIndex2 < array2.Length)
            {
                if (array1[nIndex1] <= array2[nIndex2])
                {
                    resultArray[nIndexResult++] = array1[nIndex1++];
                }
                else
                {
                    resultArray[nIndexResult++] = array2[nIndex2++];
                }
            }

            while (nIndex1 < array1.Length)
            {
                resultArray[nIndexResult++] = array1[nIndex1++];
            }

            while (nIndex2 < array2.Length)
            {
                resultArray[nIndexResult++] = array2[nIndex2++];
            }

            return resultArray;
        }
    }

    class SortUtil
    {
        public static void Swap(ref int valOne, ref int valTwo)
        {
            int temp = valOne;
            valOne = valTwo;
            valTwo = temp;
        }

        public static bool AreArrayEqual(int[] array1, int[] array2)
        {
            if (array1 == null || array2 == null)
                return false;

            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] inputArray = new int[6] { 5, 4, 3, 2, 1, 6 };
            int[] outputArray = new int[6];
            int[] resultArray = new int[6] { 1, 2, 3, 4, 5, 6 };

            SelectionSorts.SelectionSort(ref inputArray);
            Debug.Assert(SortUtil.AreArrayEqual(inputArray, resultArray));

            inputArray = new int[6] { 5, 4, 3, 2, 1, 6 };
            InsertionSorts.InsertionSort(ref inputArray);
            Debug.Assert(SortUtil.AreArrayEqual(inputArray, resultArray));

            inputArray = new int[6] { 5, 4, 3, 2, 1, 6 };
            ExchangeSorts.QuickSort(inputArray);
            Debug.Assert(SortUtil.AreArrayEqual(inputArray, resultArray));

            inputArray = new int[6] { 5, 4, 3, 2, 1, 6 };
            outputArray = MergeSorts.MergeSort(inputArray);
            Debug.Assert(SortUtil.AreArrayEqual(outputArray, resultArray));

            int[] array1 = new int[5] { 1, 5, 6, 7, 8 };
            int[] array2 = new int[7] { 3, 4, 5, 9, 10, 11, 15 };
            int[] result = new int[12] { 1, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 15 };

            Debug.Assert(SortUtil.AreArrayEqual(SortEx.MergeSort(array1, array2), result));


        }
    }
}
