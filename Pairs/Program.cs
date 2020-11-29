using System;
using System.Collections.Generic;
using System.Linq;

namespace Pairs
{
    class Program
    {
        static void Main(string[] args)
        {
            var k = 909306;
            var inputText = System.IO.File.ReadAllText("./input.txt");
            var arr = Array.ConvertAll(inputText.Split(' '), item => Convert.ToInt32(item));
            System.Console.WriteLine(pairs(k, arr));
        }

        static int pairs(int k, int[] arr)
        {
            var previousValues = new Dictionary<int, bool>();
            for (int i = 0, N = arr.Length; i < N; ++i)
            {
                previousValues[arr[i]] = true;
            }
            for (int i = 0, N = arr.Length; i < N; ++i)
            {
                if (previousValues.ContainsKey(arr[i] - k)) previousValues[arr[i] - k] = false;
            }
            return previousValues.Count(i => !i.Value);//43260
        }

        static int pairs2(int k, int[] arr)
        {
            var previousValues = new Dictionary<int, int>();
            
            for (int i = 0, N = arr.Length; i < N; ++i)
            {
                var complement = arr[i] - k;
                if (previousValues.ContainsKey(complement))
                {
                    previousValues[complement] = 0;
                    
                }

                previousValues[arr[i]] = 1;
            }
            return previousValues.Count(i => i.Value == 0);//43260
        }
    }
}
