using System;
using System.Collections.Generic;
using System.Linq;

namespace tst
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 100;
            var k = 100;
            var r = 48;
            var c = 81;

            var obstacles = new int[k][];
            var inputText = System.IO.File.ReadAllText("./input.txt");
            var inputDataLines = Array.ConvertAll(inputText.Split('\n'), item => item);
            for (int i = 0; i < k; i++)
            {
                obstacles[i] = Array.ConvertAll(inputDataLines[i].Split(' '), item => Convert.ToInt32(item));
            }
            System.Console.WriteLine(queensAttack(n, obstacles.GetLength(0), r, c, obstacles));
        }
        // Complete the queensAttack function below.
        static int queensAttack(int n, int k, int r, int c, int[][] obstacles)
        {
            if (n == 1) return 0;
            var m1 = (n - 1) / (n - 1);
            var m2 = (1 - n) / (n - 1);

            var obs45 = new List<int[]>();
            var obs225 = new List<int[]>();

            var obs135 = new List<int[]>();
            var obs315 = new List<int[]>();

            var obs360 = new List<int[]>();
            var obs180 = new List<int[]>();

            var obs90 = new List<int[]>();
            var obs270 = new List<int[]>();

            foreach (var o in obstacles)
            {
                if (o[0] - r == m2 * (o[1] - c) && o[0] < r && o[1] > c) obs45.Add(o);
                else if (o[0] - r == m2 * (o[1] - c) && o[0] > r && o[1] < c) obs225.Add(o);
                else if (o[0] - r == m1 * (o[1] - c) && o[0] < r && o[1] < c) obs135.Add(o);
                else if (o[0] - r == m1 * (o[1] - c) && o[0] > r && o[1] > c) obs315.Add(o);
                else if (o[0] == r && o[1] > c) obs360.Add(o);
                else if (o[0] == r && o[1] < c) obs180.Add(o);
                else if (o[1] == c && o[0] < r) obs90.Add(o);
                else if (o[1] == c && o[0] > r) obs270.Add(o);
            }

            var count = 0;
            for (int i = r, j = c; i <= n && j <= n; ++i, ++j)//315
            {
                if (obs315.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; j <= n; ++j)//360
            {
                if (obs360.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; i >= 1 && j <= n; --i, ++j)//45
            {
                if (obs45.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; i >= 1; --i)//90
            {
                if (obs90.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; i >= 1 && j >= 1; --i, --j)//135
            {
                if (obs135.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; j >= 1; --j)//180
            {
                if (obs180.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; i <= n && j >= 1; ++i, --j)//225
            {
                if (obs225.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            for (int i = r, j = c; i <= n; ++i)//270
            {
                if (obs270.Any(o => o[0] == i && o[1] == j)) break;
                ++count;
            }
            return count - 8;
        }
    }
}
