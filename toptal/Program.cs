using System.Globalization;
using System.Text;
using System.Linq;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace toptal
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] arr1 = new[] { 6, 5, 1, 4, 9, 3, 8, 2, 7 };
            // int[] arr2 = new[] { 1, 3, 6, 4, 1, 2 };
            // int[] arr3 = new[] { 1, 2, 3 };
            // Console.WriteLine(smallestPositive(arr1));

            // int[] C = new[] { 2, 1, 1, 0, 1 };
            // Console.WriteLine(bitRows(3, 2, C));
            // int[] A = new[] { 10, 7, 9, 3, 9, 3, 9, 7, 9, 7 };
            // Console.WriteLine(UniqueElement(A));

            //int[] A = new[] { 4, 1, 3 };
            //Console.WriteLine(isPermutation(A));

            // int[] A = new[] { 3, 8, 9, 7, 6 };
            // int K = 1;
            // Console.WriteLine(String.Join(",", Rotate(A, K)));
            // int N = new Random().Next(1, 2147483647);
            // Console.WriteLine(BinaryGap(529));

            int[,] arr = new int[,] {
                {9,9,7},
                {9,7,2},
                {6,9,5},
                {9,1,2}
            };
            var nums = new Dictionary<string, List<int>>();
            var max = 0;
            MaxNum(arr, 0, 0, nums, ref max);
            Console.WriteLine(
                string.Join(",", nums.SelectMany(e => e.Value))
            );

            // Fact(5000);

            //var path = new List<string>();
            //task33(10, 0, 1, path);
            // Console.WriteLine(beyond());

            // var tg1 = new Dictionary<string, string>
            // {
            //     {"test1a", "Wrong answer"},
            //     {"test2", "OK"},
            //     {"test1b", "Runtime error"},
            //     {"test1c", "OK"},
            //     {"test3", "Time limit exceeded"}
            // };
            // var tg2 = new Dictionary<string, string>
            // {
            //     {"codility1", "Wrong answer"},
            //     {"codility3", "OK"},
            //     {"codility2", "OK"},
            //     {"codility4b", "Runtime error"},
            //     {"codility4a", "OK"},
            // };
            // Console.WriteLine(testGroupsOKPercentage(tg2.Keys.ToArray(), tg2.Values.ToArray()));
            // var grid = new char[][] {
            //     new char[] {'1','1','1','1','0'},
            //     new char[] {'1','1','0','1','0'},
            //     new char[] {'1','1','0','0','0'},
            //     new char[] {'0','0','0','0','0'}
            // };
            // var grid1 = new char[][] {
            //     new char[] {'1', '1', '0', '0', '0'},
            //     new char[] {'1', '1', '0', '0', '0'},
            //     new char[] {'0', '0', '1', '0', '0'},
            //     new char[] {'0', '0', '0', '1', '1'}
            // };
            // Console.WriteLine(GetResult(grid));
        }

        static int GetResult(char[][] g)
        {
            int result = 0;
            int conn = 0;
            int rows = g.Length;
            int cols = g[0].Length;
            var grid = new int[rows][];
            for (var i = 0; i < rows; i++)
            {
                grid[i] = new int[cols];
                for (var j = 0; j < cols; j++)
                {
                    grid[i][j] = int.Parse(g[i][j].ToString());
                }
            }
            for (var j = 0; j < cols - 1; j++)
            {
                for (var i = 0; i < rows - 1; i++)
                {
                    var el = grid[i][j];
                    if (el == 1 && grid[i + 1][j] == 1) conn++;
                    if (el == 1 && grid[i][j + 1] == 1) conn++;
                    if (el == 1 && grid[i + 1][j + 1] == 1) conn++;
                    if (conn > 0) { grid[i][j] = conn; conn = 0; }
                    else grid[i][j] = 0;
                    if (grid[i][j] > 0) result++;
                }
            }
            return result / 2 + 1;
        }



        public static int testGroupsOKPercentage(string[] T, string[] R)
        {
            var groups = new Dictionary<string, string>();

            for (int i = 0, n = T.Length; i < n; i++)
            {
                var ti = T[i];
                var ri = R[i];

                var lastChar = ti.Substring(ti.Length - 1, 1);
                var test = char.IsDigit(lastChar[0])
                    ? lastChar
                    : ti.Substring(ti.Length - 2, 1);

                if (!groups.ContainsKey(test)) groups.Add(test, ri);
                else if (groups[test] == "OK" && ri == "OK") groups[test] = ri;
                else groups[test] = "";
            }

            return (int)Math.Round((double)(groups.Count(g => g.Value == "OK") * 100 / groups.Count));
        }

        public static void task33(int N, int x, int y, List<string> path)
        {
            while (true)
            {
                if (Math.Abs(N) >= Math.Abs(2 * x - y))
                {
                    x = 2 * x - y;
                    path.Add("L");
                    if (Math.Abs(N) == Math.Abs(x))
                    {
                        break;
                    }
                    continue;
                }
                else if (path.Count > 0 && path.Last().Equals("L"))
                {
                    if (Math.Abs(N) < Math.Abs(2 * x - y) && Math.Abs(2 * N) < Math.Abs(2 * y - x))
                    {
                        path.Clear();
                        path.Add("impossible");
                        break;
                    }
                    x = (x + y) / 2;
                    path.RemoveAt(path.Count - 1);
                }


                if (Math.Abs(N) >= Math.Abs(2 * y - x))
                {
                    y = 2 * y - x;
                    path.Add("R");
                    if (Math.Abs(N) == Math.Abs(y))
                    {
                        break;
                    }
                    continue;
                }
                else if (path.Count > 0 && path.Last().Equals("R"))
                {
                    if (Math.Abs(N) < Math.Abs(2 * x - y) && Math.Abs(2 * N) < Math.Abs(2 * y - x))
                    {
                        path.Clear();
                        path.Add("impossible");
                        break;
                    }
                    y = (x + y) / 2;
                    path.RemoveAt(path.Count - 1);
                }
            }
            System.Console.WriteLine(string.Join("", path));
        }

        public static string beyond()
        {
            var arr = new bool[100];
            for (int i = 1; i <= 100; i++)
            {
                for (int j = 1; j <= 100; j++)
                {
                    if (j % i == 0)
                        arr[j - 1] = !arr[j - 1];
                }
            }
            return string.Join(",\n", arr.Select((e, i) => e ? $"{i + 1}-րդը բաց" : $"{i + 1}-րդը փակ"));
        }
        static void Fact(int n)
        {
            var factorial = BigInteger.One;
            for (int i = 1; i <= n; i++)
                factorial = factorial * i;

            Console.WriteLine(factorial);
        }
        public static void MaxNum(
            int[,] arr, int i, int j, Dictionary<string, List<int>> nums, ref int max)
        {
            int n = arr.GetLength(0);
            int m = arr.GetLength(1);

            if (!nums.ContainsKey("max"))
            {
                nums.Add("max", new List<int> { arr[i, j] });
            }
            else
            {
                nums["max"].Add(arr[i, j]);
            }

            if (i < n - 1)
                MaxNum(arr, i + 1, j, nums, ref max);

            if (j < m - 1)
                MaxNum(arr, i, j + 1, nums, ref max);

            if (i == n - 1 && nums["max"].Count == n + m - 1)
            {
                for (int jj = j; jj < m; jj++)
                {
                    //MaxNum(arr, i, jj, nums);
                    nums["max"].Add(arr[i, jj]);
                }
                var mx = nums["max"].Sum();
                if (mx > max)
                {
                    max = mx;
                }
                else
                {
                    nums["max"].Clear();
                }
                return;
            }

            if (j == m - 1 && nums["max"].Count == n + m - 1)
            {
                for (int ii = i; ii < n; ii++)
                {
                    //MaxNum(arr, ii, j, nums);
                    nums["max"].Add(arr[ii, j]);
                }
                var mx = nums["max"].Sum();
                if (mx > max)
                {
                    max = mx;
                }
                else
                {
                    nums["max"].Clear();
                }
                return;
            }

        }

        public static int BinaryGap(int N)
        {
            var sb = new StringBuilder();
            while (N > 0)
            {
                int remainder = N % 2;
                sb.Append(remainder);
                N = N / 2;
            }
            var arr = sb.ToString().Split(new char[] { '1' });
            int max = 0;
            for (int i = 1, K = arr.Length; i < K; ++i)
            {
                string ai = arr[i];
                if (max < ai.Length) max = ai.Length;
            }
            return max;
        }

        public static int[] Rotate(int[] A, int K)
        {
            int N = A.Length;
            int[] arr = new int[N];
            for (int i = 0; i < N; ++i)
            {
                int ai = A[i];
                int index = (i + K) % N;
                arr[index] = ai;
            }
            return arr;
        }
        public static int isPermutation(int[] A)
        {
            var map = new Dictionary<int, int>(A.Length);
            int min = A[0];
            for (int i = 0, N = A.Length; i < N; ++i)
            {
                int ai = A[i];
                map[ai] = 1;
                if (min > ai) min = ai;
            }

            for (int i = 0, N = A.Length; i < N; ++i)
            {
                if (!map.ContainsKey(min++)) return 0;
            }
            return 1;
        }
        public static int max1max2(int[] A)
        {
            int max1 = A[0];
            int max2 = A[1];
            for (int i = 2, N = A.Length; i < N; ++i)
            {
                int arri = A[i];
                if (max1 < arri) { max1 = arri; }
                if (max2 < arri && arri < max1) { max2 = arri; }
            }
            return max2;
        }
        public static int smallestPositive(int[] A)
        {
            var map = new Dictionary<int, int>(A.Length);
            int min = 1;
            for (int i = 0, N = A.Length; i < N; ++i)
            {
                int ai = A[i];
                map[ai] = 1;
            }

            while (map.ContainsKey(min)) ++min;

            return min;
        }
        public static string bitRows(int U, int L, int[] C)
        {
            int[] row1 = new int[C.Length];
            int[] row2 = new int[C.Length];
            for (int i = 0, N = C.Length; i < N; ++i)
            {
                int ci = C[i];
                if (ci == 0) { row1[i] = 0; row2[i] = 0; }
                else if (ci == 2)
                {
                    row1[i] = 1;
                    row2[i] = 1;
                    U -= 1;
                    L -= 1;
                }
                else if (ci == 1)
                {
                    if (U > 0) { row1[i] = 1; U -= 1; }
                    if (L > 0 && row1[i] != 1) { row2[i] = 1; L -= 1; }

                }
            }

            if (U != 0 || L != 0) return "IMPOSSIBLE";

            return $"{String.Join("", row1)},{String.Join("", row2)}";
        }

        public static int UniqueElement(int[] A)
        {
            var map = new Dictionary<int, int>();
            //var unique = 0;
            for (int i = 0, N = A.Length; i < N; ++i)
            {
                int arri = A[i];
                map[arri] = map.ContainsKey(arri) ? map[arri] + 1 : 1;
                //unique ^= A[i];
            }
            return map.First(item => item.Value == 1).Key;//unique;
        }
    }
}
