namespace android {
    public class P1 {
        public static string friendly(int a, int b) {

            var length = a.ToString().Length;
            var xor = 0;

            for (int i = length - 1; i >= 0; --i) {
                var a1 = a % 10;
                var b1 = b % 10;
                a = a / 10;
                b = b / 10;

                xor ^= a1 ^ b1;
            }

            return xor == 0 ? "Yes" : "No";
        }
    }
}