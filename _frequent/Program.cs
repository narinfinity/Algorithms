using System;
using System.Collections.Generic;
using System.Linq;

namespace frequent {
    class Program {
        static Random r = new Random (DateTime.Now.Millisecond);
        static void Main (string[] args) {
            int[] arr = new int[30];

            for (int i = 0; i < arr.Length; i++) {
                arr[i] = r.Next (1, 100);
            }
            Console.WriteLine (string.Join (", ", arr));

            Freq.findFrequent(arr);
            // for (int i = 0, j = i + 1; i < 1000; i += j, j += i) {
            //     Console.Write($", {i}, {j}");
            // }
            
        }
    }
}