using System;
using System.Collections.Generic;

namespace android {
    public class L2 {
        public static int[] calc() {
            var res = new List<int>();
            var abc = 99;

            while (++abc < 1000) {
                var c = abc % 10;
                var b = (abc / 10) % 10;
                var a = (abc / 100) % 10;
                if (abc == (int) Math.Pow(a + b, c)) {
                    res.Add(abc);
                }
            }
            return res.ToArray();
        }
    }
}