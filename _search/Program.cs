using System;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sortedArray = new[] { -10, 0, 1, 52, 73, 94, 105, 185 };
            //Console.WriteLine(find(sortedArray, 1));

            int[] arr = new[] { 6, 5, 1, 3, 9, 4, 8, 2 };

            Console.WriteLine(sum(arr));
        }
        public static int sum(int[] arr)
        {
            int min1 = arr[0];
            int min2 = arr[1];
            for (int i = 2, N = arr.Length; i < N; ++i)
            {
                int arri = arr[i];
                if (min1 > arri) { min1 = arri; }
                if (min2 > arri && arri > min1) { min2 = arri; }
            }
            return min1 + min2;
        }
        public static int find(int[] arr, int key)
        {
            return Search(arr, key, 0, arr.Length - 1);//findIndex(array, key, 0, array.Length - 1);
        }

        public static int findIndex(int[] arr, int key, int start, int end)
        {
            if (start > end) return -1;
            int i = (start + end) / 2;
            if (arr[i] == key) return i;
            if (arr[i] < key) return findIndex(arr, key, i + 1, end);
            else return findIndex(arr, key, start, i - 1);
        }

        public static int Search(int[] arr, int key, int start, int end)
        {
            int i = -1;
            while (start <= end)
            {
                i = start + (end - start) / 2;
                if (arr[i] == key) return i;
                if (arr[i] < key) start = i + 1;
                else end = i - 1;
            }
            return -1;
        }

    }
}
