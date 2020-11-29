using System;

namespace sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new[] { 1, 2, 5, 4, 3, 2, 8, 7, 65, 78, 9, 75, 43 };
            //Sorting.MergeSort(arr, 0, arr.Length - 1);
            Sorting.QuickSort(arr, 0, arr.Length - 1);
            Console.WriteLine(string.Join(",", arr));
        }

    }
}
