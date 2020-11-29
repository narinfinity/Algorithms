using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiStack {

    class MultiStack <T> {
        
        T[] arr = null;
        int N1;
        int N2;
        int N3;
        int top1 = -1;
        int top2;
        int top3;

        // The constructor to create a Stack.
        public MultiStack(int capacity) {
            N1 = N2 = N3 = capacity;
            arr = new T[3*capacity];
            top2 = N1-1;
            top3 = N2-1;
        }

        public void push1(T a) {
            if (++top1 == N1) {
                T[] brr = new T[2*N1+N2+N3];
                for (int i = 0; i < N1; i++)
                    brr[i] = arr[i];
                for (int i = 2 * N1,j=N1; i < N2; i++,j++)
                    brr[i] = arr[j];
                for (int i = 2 * N1+N2,j=N2; i < N3; i++,j++)
                    brr[i] = arr[j];
                arr = brr;                
                N2 += N1;
                N3 += N1;
                top2 += N1;
                top3 += N1;
                N1 *= 2;
            }
            arr[top1] = a;
        }

        public T pop1() {            
            T res;
            if (top1 > -1)
               res = arr[top1--];
            else throw new StackOverflowException();
            return res;
        }

        public int getSize1() {
            return top1+1;
        }


        public void push2(T a) {
            if (++top2 == N2) {
                T[] brr = new T[N1 + 2*N2 + N3];
                for (int i = 0; i < N1; i++)
                    brr[i] = arr[i];
                for (int i = N1; i < N2; i++)
                    brr[i] = arr[i];
                for (int i = N1+2 * N2,j=N2; i < N3; i++,j++)
                    brr[i] = arr[j];                
                arr = brr;
                N3 += N2;
                top3 += N2;
                N2 *= 2;
            }
            arr[top2] = a;
        }

        public T pop2() {
            T res;
            if (top2 > N1-1)
                res = arr[top2--];
            else throw new StackOverflowException();
            return res;
        }

        public int getSize2() {
            return (top2 + 1 - N1);
        }


        public void push3(T a) {
            if (++top3 == N3) {
                T[] brr = new T[N1 + N2 + 2*N3];
                for (int i = 0; i < N1; i++)
                    brr[i] = arr[i];
                for (int i = N1; i < N2; i++)
                    brr[i] = arr[i];
                for (int i = N2; i < N3; i++)
                    brr[i] = arr[i];
                arr = brr;
                N3 *= 2;
            }
            arr[top3] = a;
        }

        public T pop3() {
            T res;
            if (top3 > N2-1)
                res = arr[top3--];
            else throw new StackOverflowException();
            return res;
        }

        public int getSize3() {
            return (top3 + 1 - N2);
        }
    }

class Demo {

    public static void Main() {

        MultiStack<int> stack = new MultiStack<int>(10);
        int N1 = 20;
        int N2 = 30;
        int N3 = 10;

        for (int i = 0; i < N1;i++ ) 
            stack.push1(i);
        for (int i = 0; i < N2; i++)
            stack.push2(i);
        for (int i = 0; i < N3; i++)
            stack.push3(i);
        
        Console.WriteLine("The MultiStack's 1 mini stack's size is " + stack.getSize1());
        Console.WriteLine("The MultiStack's 2 mini stack's size is " + stack.getSize2());
        Console.WriteLine("The MultiStack's 3 mini stack's size is " + stack.getSize3()+"\n");
                
            try
            {
                Console.WriteLine("1 mini stack's elements are\n");
                for (int i = 0; i < N1; i++)
                    Console.Write(stack.pop1()+" ");
                Console.WriteLine();
                Console.WriteLine("\n2 mini stack's elements are\n");
                for (int i = 0; i < N2; i++)
                    Console.Write(stack.pop2() + " ");
                Console.WriteLine();
                Console.WriteLine("3 mini stack's elements are\n");
                for (int i = 0; i < N3; i++)
                    Console.Write(stack.pop3()+" ");
                Console.WriteLine("\n");

            } catch(StackOverflowException e) {
                Console.WriteLine("\n\nThere is empty mini stack\n"+e.Message+"\n");
            }      


    }

}




}
