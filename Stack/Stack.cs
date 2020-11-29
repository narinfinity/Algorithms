using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{   
    // This class represents the data structure (base) of generic type <T>.
    class MyStack <T>
    {
        T[] arr = null;
        int top = -1;
        // The constructor to create a Stack.
        public MyStack(int capacity)
        {
            arr = new T[capacity];
        }

        public void push(T a)
        {

            if (++top == arr.Length)
            {
                T[] brr = new T[2 * arr.Length];

                for (int i = 0; i < arr.Length; i++)
                    brr[i] = arr[i];
                arr = brr;
            }
            arr[top] = a;
        }

        public T pop()
        {            
            T res;
            if (top > -1)
               res = arr[top--];
            else
            {               
                arr = null;
                throw new StackOverflowException();
            }
            return res;
        }

        public int getSize()
        {
            return top+1;
        }
        
        // This method removes the MyStak's elements that equals to "a".
        public void remove(T a) {
            int j = 0;
            T[] brr = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i].Equals(a))
                    brr[j++] = arr[i];
            }
            arr = brr;
        }
    }

class DemoStack {
/*        public static void Main() {
            
            // To remove Stack's those elements whose values are equal to value of given parameter.
/*          MyStack<int> stack = new MyStack<int>(10);

            for (int i = 10; i <= 15; i++)
                stack.push(i);
            for (int i = 1; i <= 12; i++)
                stack.push(i);
            
            // Using the method remove()
                stack.remove(10);
                stack.remove(11);
                stack.remove(12);

            for (int i = 0; i < 20; i++)
            {
                try
                {                    
                    Console.WriteLine(" " + stack.pop());
                }
                catch (StackOverflowException e) { Console.WriteLine(e.Message); }
            }

            // To calculate the expression.

            string str = "2 + 3 * 5 / 5";
            MyStack<char> stch = new MyStack<char>(str.Length);
            double sum = 0.0;
            char[] ch = str.ToCharArray(0, str.Length);

            for (int i = str.Length-1; i >= 0; i--)            
                stch.push(ch[i]);           

            for (int i = 0; stch.getSize()!=0; i++) {
                char chr=stch.pop();
                char c;
                if (chr == '+') {
                    while ((c = stch.pop()) == ' ') ;
                    sum += double.Parse(c.ToString()); }
                else if (chr == '-') {
                    while ((c = stch.pop()) == ' ') ;
                    sum -= double.Parse(c.ToString()); }
                else if (chr == '*') {
                    while ((c = stch.pop()) == ' ') ;
                    sum *= double.Parse(c.ToString()); }
                else if (chr == '/') {
                    while ((c = stch.pop()) == ' ') ;
                    sum /= double.Parse(c.ToString()); }
                else if (chr != ' ')
                    sum += double.Parse(chr.ToString()); 
            }
            Console.WriteLine("\nThe calculated value of expression "+str+" = "+sum); 

         }
*/

    }
}
