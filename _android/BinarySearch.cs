namespace android {
    public class P3 {
        public static int find(int[] arr, int x) {
            var start = 0;
            var end = arr.Length - 1;
            int i = -1;
            while (start <= end) {
                i = start + (end - start) / 2;
                if (x == arr[i]) return i;
                else if (x > arr[i]) start = i + 1;
                else end = i - 1;
            }
            return -1;
        }
    }
}