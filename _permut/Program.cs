using System;
using System.Collections.Generic;
using System.Linq;

namespace permut {
    class Program {
        static long maximumSum(long[] arr, long m) {
            long max = 0;
            int N = arr.Length;
            var a = new List<List<int>>();
            a.Add(new List<int>(Enumerable.Range(0, N)));
            foreach (var i in a[0]) {
                if (arr[i] % m > max) max = arr[i] % m;
            }

            while (true) {
                var a0 = a[0];
                var last = a0[a0.Count - 1];
                var sum = a0.Sum(i => arr[i]);
                a0.Remove(last);

                if (a0.Count == 1) return max;

                for (int j = N - 1; j >= last; j--) {
                    long s = sum + arr[j];
                    //Console.WriteLine(string.Join(",", new List<int>(a0) { j }));
                    if (s % m > max) max = s % m;
                }
                a.Add(a0);
                a.RemoveAt(0);
            }
        }
        static void Main(string[] args) {
            var q = 1; // Convert.ToInt32(Console.ReadLine());

            for (var qItr = 0; qItr < q; qItr++) {
                var nm = "100000 10002143548612".Split(' '); //Console.ReadLine().Split(' ');
                var n = Convert.ToInt32(nm[0]);
                var m = Convert.ToInt64(nm[1]);
                var arr = System.IO.File.ReadAllText("./arr.txt");
                var a = Array.ConvertAll(arr.Split(' '), aTemp => Convert.ToInt64(aTemp));
                var result = maximumSum(a, m);
                Console.WriteLine(result);
            }
        }
    }
}