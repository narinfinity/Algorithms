using System;
using System.Collections.Generic;
using System.Linq;

namespace frequent
{
    public class Freq
    {
        public static void findFrequent(int[] arr)
        {
            // var myDict = new Dictionary<int, int>();
            // int index = 0, maxCount = 1, key = 0, val = 0;

            // for (int i = 0; i < arr.Length; i++)
            // {
            //     key = arr[i];
            //     bool keyExists = myDict.ContainsKey(key);

            //     val = 0;
            //     if (keyExists)
            //     {
            //         val = (++myDict[key]);
            //     }
            //     else
            //     {
            //         myDict.Add(key, 1);
            //     }

            //     if (val > maxCount)
            //     {
            //         maxCount = val;
            //         index = i;
            //     }
            // }

            // Console.WriteLine($"-> el: {arr[index]} -> max: {maxCount}");

            //******************************************************************
            // var freq = (from n in arr
            //             group n by n into gr
            //             orderby
            //             gr.Count() descending,
            //             gr.Key descending
            //             select new { el = gr.Key, count = gr.Count() });

            // foreach (var fr in freq)
            //     Console.WriteLine($"most frequent: {fr.el} -> count: {fr.count}");

            //*******************************************

            int frequent = arr[0];
            int count = 0;
            var map = new Dictionary<int, int>();
            for (int i = 0, N = arr.Length; i < N; i++)
            {
                var arri = arr[i];
                map[arri] = map.ContainsKey(arri) ? map[arri] + 1 : 1;
                if (map[arri] > count)
                {
                    count = map[arri];
                    frequent = arri;
                }
            }
            Console.WriteLine($"most frequent: {frequent} -> count: {count}");
        }
    }
}