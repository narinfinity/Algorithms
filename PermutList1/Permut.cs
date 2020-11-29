using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllPermut
{

    public class MyList<T>
    {
        uint count = 0;
        Member head = null;
        Member cur = null;
        Member tmp = null;
        Member end = null;

        private class Member
        {
            public T item = default(T);
            public Member next = null;
            public Member prev = null;
        }

        public void insertAt(uint ind, T item)
        {
            if (ind == 0)
            {
                (tmp = new Member()).item = item;
                tmp.next = head;
                if (head == null) end = tmp;
                else head.prev = tmp;
                head = tmp;
                tmp = null;
                ++count;
            }
            else if (ind > 0 & ind <= (count / 2))
            {
                cur = head;
                for (uint i = 0; i < ind; ++i)
                    cur = cur.next;
                (tmp = new Member()).item = item;
                tmp.next = cur;
                tmp.prev = cur.prev;
                cur.prev.next = tmp;
                cur.prev = tmp;
                tmp = null;
                cur = null;
                ++count;
            }
            else if (ind > (count / 2) & ind < count)
            {
                cur = end;
                for (uint i = 0; i < count - 1 - ind; ++i)
                    cur = cur.prev;
                (tmp = new Member()).item = item;
                tmp.prev = cur.prev;
                tmp.next = cur;
                cur.prev.next = tmp;
                cur.prev = tmp;
                tmp = null;
                cur = null;
                ++count;
            }
            else if (ind == count)
            {
                (tmp = new Member()).item = item;
                end.next = tmp;
                tmp.prev = end;
                end = tmp;
                tmp = null;
                ++count;
            }
            else throw new ArgumentException();
        }

        public void removeAt(uint ind)
        {
            if (ind == 0)
            {
                if (head.next != null)
                {
                    head.next.prev = null;
                    head = head.next;
                }
                else { head = null; end = null; }
                --count;
            }
            else if (ind == count - 1)
            {
                if (end.prev != null)
                {
                    end.prev.next = null;
                    end = end.prev;
                }
                else { end = null; head = null; }
                --count;
            }

            else if (ind > 0 & ind <= (count / 2))
            {
                cur = head;
                for (uint i = 0; i < ind; ++i)
                    cur = cur.next;
                cur.next.prev = cur.prev;
                cur.prev.next = cur.next;
                cur.next = null;
                cur.prev = null;
                cur = null;
                --count;
            }
            else if (ind > (count / 2) & ind < count - 1)
            {
                cur = end;
                for (uint i = 0; i < count - 1 - ind; ++i)
                    cur = cur.prev;
                cur.next.prev = cur.prev;
                cur.prev.next = cur.next;
                cur.next = null;
                cur.prev = null;
                cur = null;
                --count;
            }
            else throw new ArgumentOutOfRangeException();
        }

        public uint indexOf(T item, bool first)
        {
            uint i = 0;
            if (first) cur = head;
            else cur = end;
            while (cur != null && !Equals(item, cur.item))
            {
                if (first) cur = cur.next;
                else cur = cur.prev;
                ++i;
            }
            cur = null;
            if (i >= count) throw new MissingMemberException();
            return first ? i : count - 1 - i;
        }

        public T itemAt(uint ind)
        {
            T res = default(T);
            if (ind == 0)
                res = head.item;
            else if (ind > 0 & ind <= (count / 2))
            {
                cur = head;
                for (uint i = 0; i < ind; ++i)
                    res = (cur = cur.next).item;
                cur = null;
            }

            else if (ind > (count / 2) & ind < count - 1)
            {
                cur = end;
                for (uint i = 0; i < count - 1 - ind; ++i)
                    res = (cur = cur.prev).item;
                cur = null;
            }
            else if (ind == count - 1)
                res = end.item;
            else throw new ArgumentOutOfRangeException();
            return res;
        }

        public void reverse()
        {
            if (count > 1)
                for (uint i = 0; i < this.Length; ++i)
                {
                    this.insertAt(i, this.itemAt(this.Length - 1));
                    this.removeAt(this.Length - 1);
                }
            else if (count == 0) Console.WriteLine("\n" + this + " is Empty\n");
        }

        public void removeAll()
        {
            while ((cur = head).next != null)
            {
                head = head.next;
                head.prev = null;
                cur.next = null;
            }
            count = 0;
        }

        public uint Length { get { return count; } }
    }



    public class Permut
    {

        public uint[] GetPermuts(double[] arr, double sum, double diff)
        {

            string ind = null;
            uint N = (uint)arr.Length;

            MyList<string> a = new MyList<string>();

            for (uint i = 0; i < N; i++)
            {
                a.insertAt(i, " " + i.ToString());
                uint t = 0;
                double s = 0.0;
                while (++t < a.itemAt(i).Split().Length)
                    if (Math.Abs((s += arr[uint.Parse(a.itemAt(i).Split()[t])]) - sum) <= diff)
                    {
                        ind = a.itemAt(i); goto end;
                    }
            }

            for (ulong h = 0; h < ulong.Parse(Math.Pow(2, N).ToString()) - 1 - N;)
            {

                for (uint k = uint.Parse(a.itemAt(0).Substring(a.itemAt(0).LastIndexOf(" ") + 1)) + 1; k < N; k++)
                {
                    a.insertAt(a.Length, a.itemAt(0) + " " + k.ToString());

                    Console.WriteLine(a.itemAt(a.Length - 1));
                    uint t = 0;
                    double s = 0.0;
                    while (++t < a.itemAt(a.Length - 1).Split().Length)
                        if (Math.Abs((s += arr[uint.Parse(a.itemAt(a.Length - 1).Split()[t])]) - sum) <= diff)
                        {
                            ind = a.itemAt(a.Length - 1); goto end;
                        }
                    h++;
                }
                a.removeAt(0);
            }
        end:
            uint[] index = null;
            if (ind != null)
                index = new uint[ind.Substring(1).Split().Length];
            for (uint i = 0; i < ind.Substring(1).Split().Length; i++)
                index[i] = uint.Parse(ind.Substring(1).Split()[i]);
            return index;
        }
    }

    class Demo
    {
        static void Main()
        {
            Permut p = new Permut();

            double[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            double sum = 0;
            uint[] index = null;
            try
            {
                index = p.GetPermuts(a, 325, 0);

                for (int i = 0; i < index.Length; i++)
                {
                    sum += a[index[i]];
                    Console.Write((i == 0 ? "   " : "") + "(a[" + index[i] + "] = " + a[index[i]] + ")\n");
                    Console.Write(i == index.Length - 1 ? "\n = " : " + ");
                }
                Console.Write(sum + "\n\n");
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); }
        }
    }
}
