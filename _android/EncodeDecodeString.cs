using System;

namespace android {
    public class P4 {
        public static string encodeOrDecode(string str) {
            var length = str.Length;
            for (int i = 0; i < length; i++) {
                if (int.TryParse(str[i].ToString(), out var digit))
                    return decode(str);
            }
            return encode(str);
        }

        static string encode(string str) {
            var length = str.Length;
            var curr = str[0];
            var count = 1;
            var res = "";
            for (int i = 1; i < length; i++) {

                if (curr == str[i]) {
                    ++count;
                    if (i == length - 1) goto @continue;
                    continue;
                }

                @continue:
                if (count > 1) {
                    res += $"{count}{curr}";
                    count = 1;
                } else if (count == 1) {
                    res += $"{curr}";
                }
                curr = str[i];
            }
            return res;
        }
        static string decode(string str) {
            var length = str.Length;
            var count = 0;
            var res = "";

            for (int i = 0; i < length; i++) {

                if (int.TryParse(str[i].ToString(), out var digit)) {
                    if (count > 0) count = count * 10 + digit;
                    else count = digit;
                    continue;
                } else if (count == 0) count = 1;
                for (int j = 0; j < count; j++) {
                    res += str[i];
                }
                count = 0;
            }
            return res;
        }
    }
}