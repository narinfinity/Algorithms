using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinSearch
{

    class Binarys
    {
        public int sumFibo(int n)
        {
            int sum = 0;
            for (int i = 1, j = 1; i < n; i = i + j, j = i + j)
            {
                sum += (j < n) ? i + j : i;
            }
            return sum;
        }

        public int binsearch(int val, int[] b, int lo, int hi)
        {
            if (lo >= hi) return -1;

            int mid = (lo + hi) / 2;
            if (val == b[mid]) return mid;
            else if (val > b[mid]) return this.binsearch(val, b, mid + 1, hi);
            else return this.binsearch(val, b, lo, mid - 1);
        }

    }

    class Demo
    {
        static void Main()
        {
            Binarys bs = new Binarys();
            int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine(bs.binsearch(0, a, 0, 9));
            //Console.WriteLine(bs.sumFibo(22));
        }
    }


}
