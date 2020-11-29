using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueTest
{
    class MyQueue<T> {
        ulong f = 0;
        ulong b = 0;
        ulong count = 0;
        T[] arr = null;

        public MyQueue(uint N) {
            arr = new T[N];
        }

        public void enqueue(T a) {
            if (count == (ulong)arr.Length) {
                T[] brr = new T[2 * arr.Length];
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



    class Demo
    {
        static void Main()
        {

            MyQueue<char> q = new MyQueue<char>(1);

                    string str = " AB an radar na BA ";

                        for (int i = 0; i < str.Length; i++)
                            q.enqueue(str.ToCharArray()[i]);

                       bool simetric=false;
                       int j = q.Length - 1;
                        while (j != 0 & (simetric = (str.ToCharArray()[j--] == q.dequeue())));
                            if (simetric)
                                 Console.WriteLine("The string \"" + str + "\" is simetric.");
                            else Console.WriteLine("The string \"" + str + "\" is not simetric.");             
        }
    }
}