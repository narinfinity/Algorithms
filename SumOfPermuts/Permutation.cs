using System;
using System.Collections.Generic;
using System.Linq;

namespace SumOfPermuts
{
    public class Permutation
    {

        public List<int> GetIndexList(double[] arr, double sum, double diff)
        {
            int N = arr.Length;

            var a = new List<List<int>> { Enumerable.Range(0, N).ToList() };
            
            double s = a[0].Sum(i => arr[i]);
            if (Math.Abs(s - sum) <= diff)
            {
                return a[0];
            }

            s = 0.0;
            while (true)
            {
                var a0 = a[0];
                var last = a0[a0.Count - 1];
                a0.RemoveAt(a0.Count - 1);

                if (a0.Count == 1) break;

                var sm = a0.Sum(i => arr[i]);
                for (int j = N - 1; j >= last; j--)
                {
                    s = sm + arr[j];
                    //Console.WriteLine(string.Join(",", new List<int>(a0) { j }));
                    if (Math.Abs(s - sum) <= diff)
                    {
                        return new List<int>(a0) { j };
                    }
                }

                a.Add(a0);
                a.RemoveAt(0);
            }
            return new List<int>();
        }
    }

}