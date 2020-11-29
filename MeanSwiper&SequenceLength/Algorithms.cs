using System;


namespace Test
{
    public class Algorithm
    {
        
        // This method represents the algorithm of sorting the "a" vector's elements at increase mode if the second parameter (increase) is true.
        public static int[] toSortAZ(int[] a, bool increase)
        {
            int val = 0;
            int i = 0;
            int j = 1;
            while (i < a.Length-1)
            {
                if(increase) {
                    if (a[i] > a[j])
                    {
                        val = a[j];
                        a[j] = a[i];
                        a[i] = val;
                    }
                }
                else if (a[i] < a[j])
                {
                    val = a[j];
                    a[j] = a[i];
                    a[i] = val;
                }
                j++;
                if (j == a.Length) { j = ++i + 1; }
            }
            return a;
        }

        // This method represents the algorithm for getting the sequence's (with N*N elements) element-permutations (in N count) with equal sums.
        public static uint[,] magic(int n)
        {
            uint[,] a= new uint[n,n];           
            int i = 0;
            int j = 0;
            uint k = 0;
            while(++k <= n*n) 
            {
                a[i,j]=k;                
                if(j++ == n-1) { i++; j = 0; }
            }

            uint[,] s = new uint[n, 2 * n];                    
                for (j=0, k = 0; j < 2*n; j++, k++)
                {
                    if (k == n) k = 0;             
                    for (i = 0; i < n; i++)                
                    {                       
                        if (j < n & i >= i - k & i - k >= 0)
                            s[i, j] = a[i, i - k];                            
                        if (j < n & i < i + n - 1 - k & i + n - 1 - k < n)
                            s[i, j + 1] = a[i, i + n - 1 - k];

                        if (j > n - 1 & i + n -1 - i - k <= n-1 & n - 1 - i - k >= 0)
                            s[i, j] = a[i, n - 1 - i - k];
                        if (j > n - 1 & i + n -1 - i + n - 1 - k > n-1 & n - 1 - i + n - 1 - k < n)
                            s[i, j + 1] = a[i, n - 1 - i + n - 1 - k]; 
                    }                    
            }
                return s;
        }

        // This method represents the cycle length or count of elements are created in period of algorithm cycle.
        public static uint getMaxCycleLength(uint a, uint b) 
        {
            uint next = 1;            
            uint k = 0;
            for (uint j = (a > 0 ? a : 1); j <= b; j++)
            {
                uint prev = j;
                uint i = 0;               
                do
                {
                    if (prev % 2 == 0)
                        next = prev / 2;
                    else
                        next = prev * 3 + 1;
                    prev = next;
                    i++;
                } while (next != 1);
                if (i > k)
                    k = i + 1;                    
            }
            return k;
        }

        // This method represents the Minesweeper game's table values in number style.
        public static int[,] mineSweeper(char[,] mat)
        {
            int[,] num = new int[mat.GetLength(0), mat.GetLength(1)];           
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == '*')
                    {
                        num[i, j] = 9;
                        if (j != mat.GetLength(1) - 1) num[i, j + 1]++;
                        if (i != mat.GetLength(0) - 1 & j != mat.GetLength(1) - 1) num[i + 1, j + 1]++;
                        if (i != mat.GetLength(0) - 1 ) num[i + 1, j]++;
                        if (i != mat.GetLength(0) - 1 & j != 0 ) num[i + 1, j - 1]++;
                        if (j != 0) { if (num[i, j - 1] != 9) num[i, j - 1]++; }
                        if (i != 0 & j != 0) { if (num[i - 1, j - 1] != 9) num[i - 1, j - 1]++; }
                        if (i != 0 ) { if (num[i - 1, j] != 9) num[i - 1, j]++;}
                        if (i != 0 & j != mat.GetLength(1) - 1 ) { if (num[i - 1, j + 1] != 9) num[i - 1, j + 1]++; }                       
                    }
                }
            }
            return num;
        }

        
        
        public static void Main()
        {            
            // Task 1. Sorting the "a" Vector elements.
  /*          int[] a = new int[] { 1, 2, 100, 5, 8, 6, 7, 23, 54, 1 };
            a = toSortAZ(a, true);
            Console.WriteLine("The sorted vector's elements are ");
            foreach (int i in a)
            Console.Write(i + " ");
            Console.WriteLine("\n");
*/
            // Task 2. Sequence's equal sum element-permutations.
            Console.WriteLine("Sequence's equal sum element-permutations are\n");
            int N = 9;
            int j = 1;
            foreach (uint aa in magic(N))
            {
                Console.Write("{0,3: ##}",aa); 
                Console.Write((j == N)?"  ":"");
                if (j++ == 2*N) { j = 1; Console.WriteLine("\n"); }
            }


            // Task 3. Getting the cycle length for [0,1000000] interval.
 /*           uint maxcyclelength = getMaxCycleLength(0, 1000000);
            Console.Write("The cycle length for [0,"+"1000000] interval is " + maxcyclelength+"\n");
  

            // Task 4. Minesweeper game's table values in number style.
            char[,] mat = { {'.', '.', '*', '.', '.', '.', '*', '.', '.', '.', '*', '.'}, 
                            {'.', '.', '*', '.', '.', '.', '*', '.', '.', '.', '*', '.'},
                            {'.', '.', '*', '.', '.', '.', '*', '.', '.', '.', '*', '.'},
                            {'.', '.', '*', '.', '.', '.', '*', '.', '.', '.', '*', '.'},
                            {'.', '*', '.', '*', '.', '*', '.', '*', '.', '*', '.', '*'},
                            {'*', '.', '.', '.', '*', '.', '.', '.', '*', '.', '.', '.'},
                            {'.', '*', '.', '*', '.', '*', '.', '*', '.', '*', '.', '*'},
                            {'.', '.', '*', '.', '.', '.', '*', '.', '.', '.', '*', '.'},
                            {'.', '.', '*', '.', '.', '.', '*', '*', '.', '.', '*', '.'} };
            
            Console.WriteLine("Minesweeper game's table in number style is\n");
            int r = 1;
            foreach (int n in mineSweeper(mat))
            {
                if (n == 9) Console.Write("* ");
                else Console.Write(n + " ");
                if (r++ == mat.GetLength(1)) { r = 1; Console.WriteLine("\n"); }
            }*/
        }

    }
}