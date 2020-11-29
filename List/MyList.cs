using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liststruc {

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
                for (uint i = 0; i < ind ; ++i)
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

        public void removeAll() {
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

    class Test {
        public static void Main() {

            MyList<int> ml = new MyList<int>();

            // Inserting count of 10 members for MyList.
            for (uint i = 0; i < 19; i++)
                ml.insertAt(i , (int) i);             
           
            // Changing the List's elements in reverse order.
            ml.reverse();
           // ml.reverse();

            try {
                // Printing MyList's.
                for (uint i = 0; i < ml.Lenght; i++)
                    Console.WriteLine("{0,2}   {1,-2}", ml.indexOf(ml.itemAt(i)), ml.itemAt(i));
                Console.WriteLine("\nNow MyList's length = " + ml.Lenght + "\n");
            }
            catch (Exception e) { Console.WriteLine("\nWrong argument item or index, "+e.Message+"\n"); }
        }

    }
}