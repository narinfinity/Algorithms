using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortableEmployeeList
{
    //
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

        public static void SortList(MyList<T> le, string[] by)
        {
            if (le != null)
                MergeSort(le, 0, le.Length-1, by);
        }

        private static void MergeSort(MyList<T> le, uint lo, uint hi, string[] by)
        {
            if (lo >= hi) return;
            uint m = (lo+hi)/2;
            MergeSort(le, lo, m, by);
            MergeSort(le, m + 1, hi, by);
            Merge(le, lo, m, hi, by);
        }

        private static void Merge(MyList<T> le, uint lo, uint mid, uint hi, string[] by)
        {
            MyList<Employee> e = le as MyList<Employee>;

            uint ii = lo, jj = mid + 1;

            MyList<Employee> t = new MyList<Employee>();

            for (uint i = 0; i < hi - lo + 1; ++i)
            {
                if(ii > mid)                                                t.insertAt(i, e.itemAt(jj++));
                else if(jj > hi)                                            t.insertAt(i, e.itemAt(ii++));
                else if(Employee.compare(e.itemAt(ii),e.itemAt(jj),by) < 0) t.insertAt(i, e.itemAt(ii++));
                else                                                        t.insertAt(i, e.itemAt(jj++));
            }

            for (uint j = 0; j < t.Length; ++j, ++lo)
            {
                e.removeAt(lo);
                e.insertAt(lo, t.itemAt(j));
            }
        }

        public uint Length { get { return count; } }
    }

    public enum Positions { intern, junior, middle, senior, manager, vicepresident, ceo }

    //
    public class Employee
    {
        string name = "";
        Positions post = Positions.intern;
        double salary = 0;
        double experiance = 0;

        public Employee(string name, Positions pos, double sal, double exper)
        {
            this.name = name;
            this.post = pos;
            this.salary = sal;
            this.experiance = exper;
        }

        public string Name { get { return this.name; } }

        public Positions Post { get { return this.post; } }

        public double Salary { get { return this.salary; } }

        public double Experience { get { return this.experiance; } }

        public static int compare(Employee emp1, Employee emp2, string[] by)
        {
            int res = 0;
            if (by == null || by[0] == "") throw new ArgumentNullException();
            for (int i = 0; i < by.Length; ++i)
            {
                if (by[i] == "Name" & res == 0)
                {
                    if (string.Compare(emp1.Name, emp2.Name, true) > 0) res = 1;
                    else if (string.Compare(emp1.Name, emp2.Name, true) < 0) res = -1;
                    else res = 0;
                }
                if (by[i] == "Post" & res == 0)
                {
                    if (emp1.Post > emp2.Post) res = 1;
                    else if (emp1.Post < emp2.Post) res = -1;
                    else res = 0;
                }
                if (by[i] == "Salary" & res == 0)
                {
                    if (emp1.Salary > emp2.Salary) res = 1;
                    else if (emp1.Salary < emp2.Salary) res = -1;
                    else res = 0;
                }
                if (by[i] == "Experience" & res == 0)
                {
                    if (emp1.Experience > emp2.Experience) res = 1;
                    else if (emp1.Experience < emp2.Experience) res = -1;
                    else res = 0;
                }
            }
            return res;
        }
    }

    class Demo
    {
        static void Main()
        {
            Employee[] emp = { 
                                    new Employee("Anna", Positions.senior, 400000.0, 5.5),
                                    new Employee("Armen", Positions.vicepresident, 1500000.0, 3.5 ),
                                    new Employee("Samvel", Positions.senior, 400000.0, 6.5 ),
                                    new Employee("Emma", Positions.junior, 100000.0, 1.5 ),
                                    new Employee("Emma", Positions.junior, 100000.0, 1.5 ),
                                    new Employee("Emma", Positions.junior, 100000.0, 1.5 ),
                                    new Employee("Mari", Positions.junior, 200000.0, 1 ),
                                    new Employee("Mari", Positions.junior, 200000.0, 1 ),
                                    new Employee("Mari", Positions.junior, 200000.0, 1 ),
                                    new Employee("Mari", Positions.junior, 200000.0, 1 )                                   
                              };            

            MyList<Employee> list = new MyList<Employee>();

            for (uint i = 0; i < emp.Length; ++i) list.insertAt(i, emp[i]);
            

            string[] by = { /*"Name",*/ "Post", "Salary", "Experience" };
            
            MyList<Employee>.SortList(list, by);

            for (uint i = 0; i < list.Length; ++i) Console.WriteLine(list.itemAt(i).Name);
            
            Console.WriteLine();
        }
    }

}



