using System.Collections.Generic;

namespace android
{
    public class P5
    {
        public static int calc(string[] arr)
        {
            var dgs = new Stack<int>();
            var ops = new Stack<string>();

            var N = arr.Length;

            for (int i = 0; i < N; i++)
            {
                if (!int.TryParse(arr[i], out var d))
                {
                    var op = arr[i];
                    if (op == "*")
                    {
                        var res = dgs.Pop();
                        dgs.Push(res * int.Parse(arr[++i]));
                        continue;
                    }
                    if (op == "/")
                    {
                        var res = dgs.Pop();
                        dgs.Push(res / int.Parse(arr[++i]));
                        continue;
                    }
                    ops.Push(op);
                }
                else
                {
                    dgs.Push(d);
                }
            }
            ops = new Stack<string>(ops);
            dgs = new Stack<int>(dgs);
            while (ops.Count > 0)
                switch (ops.Pop())
                {
                    case "+":
                        dgs.Push(dgs.Pop() + dgs.Pop());
                        break;
                    case "-":
                        dgs.Push(dgs.Pop() - dgs.Pop());
                        break;
                }
            return dgs.Pop();
        }
    }
}