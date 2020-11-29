using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListStackProg
{
    class Stack<T>
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

        public void push(T item, bool ignore)
        {     
            try {   this.indexOf(item, true);
                    if (!ignore)
                    {
                        while (this.indexOf(item, true) >= 0)
                            this.removeAt(this.indexOf(item, true));
                        this.insertAt(this.Length, item); ;
                    }
            }
            catch (MissingMemberException e) { this.insertAt(this.Length, item); }                                           
        }

        public T pop()
        {
            T res = default(T);
            if (this.Length > 0)
            {
                res = this.itemAt(this.Length - 1);
                this.removeAt(this.Length - 1);
            }
            else Console.WriteLine("\nStack is Empty\n");           
            return res;
        }

        public void insertAt(uint ind, T item)
        {
            if (ind == 0)
            {
                (cur = new Member()).item = item;
                cur.next = head;
                if (head == null) end = cur;
                else head.prev = cur;
                head = cur;
                cur = null;
                count++;
            }            
            else if (ind > 0 & ind <= (count / 2))
            {
                cur = head;
                for (uint i = 0; i < ind - 1; i++)
                    cur = cur.next;
                (tmp = new Member()).item = item;
                tmp.next = cur.next;
                tmp.prev = cur;
                cur.next.prev = tmp;
                cur.next = tmp;
                tmp = null;
                cur = null;
                count++;
            }
            else if (ind > (count / 2) & ind < count)
            {
                cur = end;
                for (uint i = 0; i < count - 1 - ind; i++)
                    cur = cur.prev;
                (tmp = new Member()).item = item;
                tmp.prev = cur.prev;
                cur.prev.next = tmp;
                cur.prev = tmp;
                tmp.next = cur;
                tmp = null;
                cur = null;
                count++;
            }
            else if (ind == count)
            {
                (tmp = new Member()).item = item;
                end.next = tmp;
                tmp.prev = end;
                end = tmp;
                tmp = null;
                count++;
            }
            else throw new ArgumentException();
        }

        private void removeAt(uint ind)
        {
            if (ind == 0)
            {
                if (head.next != null)
                {
                    head.next.prev = null;
                    head = head.next;
                }
                else head = null;
                count--;                
            }
            else if (ind == count - 1 & end != null)
            {                
                end = end.prev;
                count--;
            }
            else if (ind > (count / 2) & ind < count - 1)
            {
                cur = end;
                for (uint i = 0; i < count - 1 - ind; i++)
                    cur = cur.prev;
                cur.prev = cur.prev.prev;
                cur.prev.next.prev = null;
                cur.prev.next.next = null;
                cur.prev.next = cur;
                cur = null;
                count--;
            }
            else if (ind > 0 & ind <= (count / 2))
            {
                cur = head;
                for (uint i = 0; i < ind - 1; i++)
                    cur = cur.next;
                cur.next = cur.next.next;
                cur.next.prev.prev = null;
                cur.next.prev.next = null;
                cur.next.prev = cur;
                cur = null;
                count--;
            }
            
            else throw new ArgumentOutOfRangeException();
        }

        private uint indexOf(T item, bool first)
        {
            uint i = 0;
            if (first) cur = head;
            else cur = end;
            while (cur != null && !Equals(item, cur.item))
            {
                if (first) cur = cur.next;
                else cur = cur.prev;
                i++;
            }
            cur = null;
            if (i >= count) throw new MissingMemberException();
            return first ? i : count - 1 - i;
        }

        private T itemAt(uint ind)
        {
            T res = default(T);
            if (ind == 0)
                res = head.item;
            else if (ind > 0 & ind <= (count / 2))
            {
                cur = head;
                for (uint i = 0; i < ind; i++)
                    res = (cur = cur.next).item;
                cur = null;
            }
            else if (ind > (count / 2) & ind < count - 1)
            {
                cur = end;
                for (uint i = 0; i < count - 1 - ind; i++)
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
                for (uint i = 0; i < this.Length; i++)
                {
                    this.insertAt(i, this.itemAt(this.Length - 1));
                    this.removeAt(this.Length - 1);
                }
            else if (count == 0) Console.WriteLine("\n"+ this +" is Empty\n");
        }


        public uint Length { get { return count; } }

    }


    class Demo
    {
        static void Main()
        {
            Stack<string> stack = new Stack<string>();

            string[] s = { "Myra", "Myra", "Natalie", "Barbara", "Barbara", "Carmen", "Simona", "Simona" , "Gloria" };            
            
            for (uint i = 0; i < s.Length ; ++i)
            {
                stack.insertAt(i, s[i]);                
            }

            string item = "Adele"; //"Adele" has no dublicates
            for (int i = 0; i < 5; i++)
            {
                stack.push(item, false);
            }

            item = "Myra";
            for (int i = 0; i < 5; i++)
            {
                stack.push(item, false);
            }

            item = "Simona";
            for (int i = 0; i < 5; i++)
            {
                stack.push(item, false);
            }

            item = "Barbara";
            for (int i = 0; i < 5; i++)
            {
                stack.push(item, false);
            }
            //stack.reverse();

            Console.WriteLine("Stack's elements are \n");
            for (uint i = 0; stack.Length != 0; ++i)
            {
                Console.WriteLine(stack.pop());                
            }
            Console.WriteLine();

        }

    }
}