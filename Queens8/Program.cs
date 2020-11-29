using System;

namespace Queens8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                foreach (Queen qq in Queen.PlaceQueens(0))
                    Console.WriteLine("\n(" + qq.x + " , " + qq.y + ")\n");

            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n" + e.StackTrace + "\n"); }
        }
    }
}