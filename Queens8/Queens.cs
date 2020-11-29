using System;

namespace Queens8
{
    public class Queen
    {
        static int N = 8;
        static int count = 0;
        static Queen[] q = new Queen[N];
        public int x = 0;
        public int y = 0;

        public static Queen[] PlaceQueens(int i)
        {

            if (i < 0 || i >= N) throw new ArgumentException();

            if (isOK(i))
            {
                ++count;
                q[count - 1] = new Queen();
                q[count - 1].y = count - 1;
                q[count - 1].x = i;
                if (count == N) return q;
                else return PlaceQueens(0);
            }
            else if (++i < N) { return PlaceQueens(i); }
            else
            {
            again:
                if ((i = q[count - 1].x + 1) < N) { q[count - 1] = null; --count; return PlaceQueens(i); }
                else { q[count - 1] = null; --count; goto again; }
            }
        }

        static bool isOK(int i)
        {
            if (count > 0)
            {
                for (int j = 0; j < count; j++)
                    if (i == q[j].x || count - i == q[j].y - q[j].x || count + i == q[j].y + q[j].x)
                        return false;
            }
            return true;
        }


    }
}




