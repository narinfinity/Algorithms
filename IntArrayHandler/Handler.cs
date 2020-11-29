using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Handler
    {
        
        //This is the private integer type attribute of an object in type of Handler.
        private int[] handl = null;

        /*
         * his is the constructor to initialise the object's attribute "handle".
         */
      public  Handler(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            handl = arr;
        }

       /*
        * This method also will initialise the attribute "handle" for object 
        * which will take this service.
        */
        public void Set_Data(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            handl = arr;
        }

        /*
         * This method sorts array "arr" type of integer by using:
         * InsertionSort algorithm if the lenght of array is less or equal to 100 or
         * MergeSort algorithm if the lenght of array is big than 100. 
         */
        public void Sort(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            if (arr.Length <= 100) InsertionSort(ref arr);
            else                  MergeSort(arr, 0, arr.Length - 1);
        }

        //This method sorts the array by using the InsertionSort algorithm.
        private void InsretionSort(ref int[] arr)
        {
            for (int i = 1; i < arr.Length; ++i)
                for (int j = i; j > 0 && (arr[j - 1] > arr[j]); --j)
                {
                    int tmp = arr[j];
                    arr[j] = arr[j - 1];
                    arr[j - 1] = tmp;
                }
        }

        //This method sorts the array in MergeSort algorithm by using the Merge() method.
        private void MergeSort(int[] a, int f, int l)
        {
            if (f >= l) return;
            int m = (f + l) / 2;
            MergeSort(a, f, m);
            MergeSort(a, m + 1, l);
            Merge(a, f, m, l);
        }

        private void Merge(int[] a, int f, int mid, int hi)
        {
            int f1 = f;
            int f2 = mid + 1;

            int[] tmp = new int[hi - f + 1];

            for (int i = 0; i < tmp.Length; ++i)
            {
                if (f1 > mid) 		tmp[i] = a[f2++];
                else if (f2 > hi) 	tmp[i] = a[f1++];
                else if (a[f1] < a[f2]) tmp[i] = a[f1++];
                else 			tmp[i] = a[f2++];
            }

            for (int j = 0; j < tmp.Length; ++j)
                a[f++] = tmp[j];           
        }

       /*
        * This method will search and return the index of first argument "someint" in second argument array "arr" by using:
        * SequentialSearch method if the length of "arr" is less than or equal to 100 or
        * BinarySearch (only if "arr" is already sorted) methid if the length of "arr" is big than 100.
        */
        public int SearchIndexOf(int someint, int[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            int res = -1;
            if (handl.Length <= 100) 
                res = SequentialSearch(someint, arr);
            else
            {
                this.Sort(arr);
                res = BinarySearch(someint, arr, 0, arr.Length - 1);
            }
            return res;
        }

        //This method will search the first argument "elem" in second argument array "a" 
        //and if finds, will return the first index of "elem".
        private int SequentialSearch(int elem, int[] a)
        {
            int res = -1;
            for (int i = 0; i < a.Length; ++i)
                if (a[i] == elem)
                {
                    res = i; break;
                }
            return res;
        }

        //This method will search the first argument "elem" in second argument - sorted array "a"
        //only if "a" is sorted in increasing order, and will return index of "elem" in "a"
        //from index "first" to index "last".
        private int BinarySearch(int elem, int[] a, int first, int last)
        {
            int mid = (first + last) / 2;
            if (a[mid] == elem) return mid;
            else if (a[mid] > elem) return BinarySearch(elem, a, first, mid);
            else                    return BinarySearch(elem, a, mid + 1, last);
        }

        /*
         * This method will return the argument "arr" as an object of MyStack type.                   
         */
        public MyStack GetStackOf(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            MyStack ms = null;            
                ms = new MyStack(100);
                for (int i = 0; i < arr.Length; ++i)
                    ms.push(arr[i]);    
            return ms;
        }


        //This method gets the attribute "handle" for object which will take this service.
        public int[] Get_HandlArr()
        {
            return (handl != null) ? handl : null;
        }
    }

    //This class represents the Stack of elements with integer type.
    class MyStack
    {
        int[] arr = null;
        int top = -1;
        // The constructor to create a Stack.
        public MyStack(int capacity)
        {
            arr = new int[capacity];
        }

        //This method will push the element into the Stack.
        public void push(int a)
        {

            if (++top == arr.Length)
            {
                int[] brr = new int[2 * arr.Length];

                for (int i = 0; i < arr.Length; ++i)
                    brr[i] = arr[i];
                arr = brr;
            }
            arr[top] = a;
        }

        //This method will return the last element that has been pushed into the Stack.
        public int pop()
        {
            int res = 0;
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
            return top + 1;
        }

        // This method removes the MyStak's first element that will be equals to argument - "a".
        public void remove(int a)
        {
            int j = 0;
            int[] brr = new int[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i] != a)
                    brr[j++] = arr[i];
            }
            arr = brr;
        }
    }



    //This class demonstrates the services of Handler class
    public class Demo
    {

        static void Main()
        {

            int[] a = { 0, 7, -3, 4, 29, 29, -10, 29, 29, 8 };
            int[] b = { 0, 7, -3, 4, 29, 29, -10, 29, 29, 8 };
            int[] aa = { 5, -8, 30, 13, 0, 12, 5, 6, 8, 5 };
            
            try
            {
                //Initialising the object "h" with array "a" by using constructor of class Handler.
                Handler h = new Handler(a);

                //Searching the index of "inum" in array "a" after in array "b".
                
                int inum = 29;

                Console.WriteLine("\nThe index of " + inum + " in array \"a\" is " + h.SearchIndexOf(inum, h.Get_HandlArr()));
                Console.WriteLine("\nThe index of " + inum + " in array \"b\" is " + h.SearchIndexOf(inum, b) + "\n");
                
                //Sorting the array "a" as the given attribute of object "h" (by using "Get_HandlArr()" method).
                h.Sort(h.Get_HandlArr());
                //Sorting the array "b", which elements are equal to elements of array "a".
                h.Sort(b);

                Console.WriteLine("The sorted array \"a\" and the sorted array \"b\" \n");
                for (int i = 0; i < b.Length; ++i)
                    Console.WriteLine("{0,3}   {1,3}", a[i], b[i]);
                Console.WriteLine();

                //Setting the new attribute for object "h".
                h.Set_Data(aa);

                Console.WriteLine("The new attribute's (by using the method \"Set_Data(\"aa\")\") elements are\n");
                for (int i = 0; i < h.Get_HandlArr().Length; ++i)
                    Console.Write(h.Get_HandlArr()[i]+" ");
                Console.WriteLine("\n");
                
                //Sorting the array "aa" as the given attribute of object "h" (by using "Get_HandlArr()" method).
                h.Sort(h.Get_HandlArr());
                
                Console.WriteLine("The sorted attribute of object \"h\"\n  1. by using name of array \"aa\"");
                Console.WriteLine("  2. by using the method \"Get_HandlArr()\" \n");
                for (int i = 0; i < h.Get_HandlArr().Length; ++i)                
                    Console.WriteLine("{0,3} {1,3}", aa[i], h.Get_HandlArr()[i]);
                    
                Console.WriteLine("\n");

                //Sorting the array "aa".
                h.Sort(aa);

                Console.WriteLine("The sorted array \"aa\" is \n");
                for (int i = 0; i < aa.Length; ++i)
                    Console.Write("{0,3} ", aa[i]);
                Console.WriteLine("\n");

                //Getting the attribute of object "h" as a Stack by using the "GetStackOf()" method.
               MyStack attrStack = h.GetStackOf(h.Get_HandlArr());

               //Printing the Stack with "h" object's attribute ("handl") elements.
               Console.WriteLine("\nThe Stack with \"h\" object's attribute (\"handl\") elements is\n");
                while( attrStack.getSize()!= 0)
                   Console.Write(attrStack.pop() + " ");
               Console.WriteLine();

                //Getting the array "aa" as a Scack by using the "GetStackOf()" method.
               MyStack aaStack = h.GetStackOf(aa);

                //Printing the Stack with elements of array "aa".
               Console.WriteLine("\nThe Stack with elements of array \"aa\" is\n");
               while( aaStack.getSize() != 0)
                   Console.Write(aaStack.pop() + " ");
               Console.WriteLine("\n");

            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n" + e.StackTrace +"\n"); }
       


        }

    }
}
