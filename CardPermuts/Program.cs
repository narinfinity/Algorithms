using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CardPermuts
{

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(StringSimilarity("ababaa"));
            //System.Console.WriteLine(solve(new[] { 0, 2, 0 }));
            //System.Console.WriteLine(AreSimilar("abbca", "caabb"));
            var inputTesxt = File.ReadAllText("input.txt");
            var arr = Array.ConvertAll(inputTesxt.Split(' '), aTemp => Convert.ToInt32(aTemp));
            System.Console.WriteLine(solve(arr));
        }
        static bool ArrayEquals(int[] first, int[] second)
        {
            if (first == second)
                return true;
            if (first == null || second == null)
                return false;
            if (first.Length != second.Length)
                return false;
            for (var i = 0; i < first.Length; i++)
            {
                if (first[i] == 0) continue;
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }
        static Dictionary<int, List<int>> di = new Dictionary<int, List<int>>();
        static IEnumerable<List<int>> Permuts(IEnumerable<int> source)
        {
            if (source.Count() == 1) return new List<List<int>> { source.ToList() };

            return source.SelectMany((c, i) => Permuts(source.Where(x => x != c)).Select((p, j) => { p.Insert(0, c); return p; }));
        }
        public static IEnumerable<IEnumerable<T>> CombOfK<T>(T[] arr, int k)
        {
            int size = arr.Length;

            IEnumerable<IEnumerable<T>> Runner(IEnumerable<T> list, int n)
            {
                int skip = 1;
                foreach (var headList in list.Take(size - k + 1).Select(h => new T[] { h }))
                {
                    if (n == 1)
                        yield return headList;
                    else
                    {
                        foreach (var tailList in Runner(list.Skip(skip), n - 1))
                        {
                            yield return headList.Concat(tailList);
                        }
                        skip++;
                    }
                }
            }

            return Runner(arr, k);
        }
        
        // Complete the solve function below.
        static long solve(int[] arr)
        {
            var range = Enumerable.Range(1, arr.Length).ToList();
            long sum = 0;
            var pn = new Permutation(arr.Length);// Permuts(Enumerable.Range(1, arr.Length));
            foreach (var row in pn.GetRows())
            {
                //System.Console.WriteLine($"{row.Rank + 1}: {row.ToString()}");

                if (ArrayEquals(arr, row.Select(i => range[i]).ToArray())) sum += row.Rank + 1;
            }
            return sum > Math.Pow(10, 9) ? (long)(sum % Math.Pow(10, 9)) + 7 : sum;
        }
        static bool AreSimilar(string a, string b)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var seed = 0;//a.Select(i => (int)i).Sum() == b.Select(i => (int)i).Sum();
            for (int i = 0, N = a.Length; i < N; i++)
            {
                seed ^= a[i] ^ b[i];
            }
            stopwatch.Stop();

            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            return seed == 0;
        }
        static int StringSimilarity(string s)
        {
            var sum = 0;
            for (int i = 0, N = s.Length; i < N; i++)
            {
                var suffix = s.Substring(i);
                for (int j = 0, length = suffix.Length; j < length; j++)
                {
                    var prefix = s.Substring(0, length - j);
                    if (suffix.StartsWith(prefix))
                    {
                        sum += prefix.Length;
                        break;
                    }
                }
            }
            return sum;
        }
        private static bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }
        private static IEnumerable<List<T>> Combinations<T>(IEnumerable<T> elements, int k)
        {
            var arr = elements.ToArray();
            var size = arr.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                yield return numbers.Select(n => arr[n]).ToList();
            } while (NextCombination(numbers, size, k));
        }
    }
}
