using System;

namespace SumOfPermuts
{
    class Program
    {
        static void Main(string[] args)
        {
            Permutation p = new Permutation();

            double[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            double sum = 0;
            try
            {
                var indexList = p.GetIndexList(a, 91, 0);

                for (int i = 0; i < indexList.Count; i++)
                {
                    sum += a[indexList[i]];
                    Console.Write((i == 0 ? "   " : "") + "(a[" + indexList[i] + "] = " + a[indexList[i]] + ")\n");
                    Console.Write(i == indexList.Count - 1 ? "\n = " : " + ");
                }
                Console.Write(sum + "\n\n");
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); }
        }
    }
}
