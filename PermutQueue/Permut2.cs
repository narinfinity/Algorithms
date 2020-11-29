using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllPermut {

    class MyQueue<T> {
        ulong f = 0;
        ulong b = 0;
        ulong count = 0;
        uint N = 0;
        T[] arr = null;

        public MyQueue(uint n) {
            N = n;
            arr = new T[N];
        }

        public void enqueue(T a) {
            if (count == (ulong)arr.Length) {
                T[] brr = new T[ulong.Parse(Math.Pow(2, 25).ToString())];
                for (ulong j = 0, i = f; j < count; j++, i = (i + 1) % ((ulong)arr.Length)) {
                    //Console.WriteLine(arr[i]);
                    brr[j] = arr[i];
                }
                f = 0;
                b = count;
                arr = brr;
            }
            arr[b] = a;
            b = (b + 1) % ((ulong)arr.Length);            
            ++count;
        }

        public T dequeue() {
            T res = default(T);
            if (count > 0) {
                res = arr[f];
                f = (f + 1) % ((ulong)arr.Length);
                --count;
            }
            return res;
        }

        public ulong Length { get { return count; } }

    }



    class Permut {

        public uint[] permut(double[] arr, double sum, double diff) {

            string ind = null;
            string aitem = null;
            string bitem = null;
            
            uint N = (uint)arr.Length;

            MyQueue<string> a = new MyQueue<string>(N);
            
            for (uint i = 0; i < N; i++) {
                a.enqueue(aitem = " " + i.ToString());
                uint t = 0;
                double s = 0.0;
                while (++t < aitem.Split().Length)
                    if (Math.Abs((s += arr[uint.Parse(aitem.Split()[t])]) - sum) <= diff)
                    {
                        ind = aitem; goto end;
                    }
            }

            for (ulong h = 0; h < ulong.Parse(Math.Pow(2, N).ToString()) - 1 - N;) {
                aitem = a.dequeue();
                for (uint k = uint.Parse(aitem.Substring(aitem.LastIndexOf(" ") + 1)) + 1; k < N; k++) {
                    a.enqueue((bitem = aitem + " " + k.ToString()));
                    //Console.WriteLine(bitem);
                    uint t = 0;
                    double s = 0.0;
                    while (++t < bitem.Split().Length)
                        if (Math.Abs((s += arr[uint.Parse(bitem.Split()[t])]) - sum) <= diff)
                        {
                            ind = bitem; goto end;
                        }
                    h++;
                }
            }
        end:
            uint[] index = null;
            if (ind != null) index = new uint[ind.Substring(1).Split().Length];
            for (uint i = 0; i < ind.Substring(1).Split().Length; i++)
                index[i] = uint.Parse(ind.Substring(1).Split()[i]);

            return index;
        }
    }

    class Demo {
        static void Main() {
            Permut p = new Permut();

            double[] a = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25};
            double sum = 0;
            uint[] index = null;
            try {
                index = p.permut(a, 325, 0);

                for (uint i = 0; i < index.Length; i++) {
                    sum += a[index[i]];
                    Console.Write((i == 0 ? "   " : "") + "(a[" + index[i] + "] = " + a[index[i]] + ")\n");
                    Console.Write(i == index.Length - 1 ? "\n = " : " + ");
                }
                Console.Write(sum + "\n\n");
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message +"\n"+e.StackTrace+ "\n"); }
        }
    }
}
