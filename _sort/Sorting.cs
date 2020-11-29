using System;

namespace sort
{
    public class Sorting
    {
        public static void MergeSort(int[] arr, int f, int l)
        {
            if (f >= l) return;
            int m = (f + l) / 2;
            MergeSort(arr, f, m);
            MergeSort(arr, m + 1, l);
            Merge(arr, f, m, l);
        }

        static void Merge(int[] arr, int f, int m, int l)
        {
            int f1 = f;
            int f2 = m + 1;
            int[] tmp = new int[l - f + 1];
            for (int i = 0; i < tmp.Length; ++i)
            {
                if (f1 > m) tmp[i] = arr[f2++];
                else if (f2 > l) tmp[i] = arr[f1++];
                else if (arr[f1] < arr[f2]) tmp[i] = arr[f1++];
                else tmp[i] = arr[f2++];
            }

            for (int i = 0; i < tmp.Length; ++i)
            {
                arr[f++] = tmp[i];
            }
        }

        public static void QuickSort(int[] arr, int l, int r)
        {
            if (l >= r) return;
            int pivot = Partition(arr, l, r);
            if (pivot > 1) QuickSort(arr, l, pivot - 1);
            if (pivot + 1 < r) QuickSort(arr, pivot + 1, r);
        }

        private static int Partition(int[] arr, int l, int r)
        {
            int pivot = arr[l];
            while (true)
            {
                while (arr[l] < pivot)
                {
                    l++;
                }
                while (arr[r] > pivot)
                {
                    r--;
                }
                if (l < r)
                {
                    if (arr[l] == arr[r]) return l;
                    int tmp = arr[l];
                    arr[l] = arr[r];
                    arr[r] = tmp;
                }
                else
                {
                    return r;
                }
            }
        }
    }
}