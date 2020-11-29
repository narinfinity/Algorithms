using System;

namespace android {
    public class P2 {
        public static int[][] matrix(int n) {
            var arr = new int[n][];

            for (int i = 0; i < n; i++) {
                arr[i] = new int[n];

                for (int j = 0; j < n; j++) {
                    arr[i][j] = i + j <= n - 1
                        ? Math.Min(i, j) + 1
                        : n - 1 - Math.Max(i, j) + 1;
                    //arr[i][j] = n-1 - i+j;
                }
            }
            return arr;
        }
    }
}