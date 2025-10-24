using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    public static class StringFormatting
    {
        public static string ReplaceNonAlphaNumeric(this string self, string replace)
            => Regex.Replace(self, "[^a-zA-Z0-9]", replace);

        /// <summary>
        /// Returns an 8 character lowercase alphanumeric string representing the hexadecimal format of the given value's hash code.
        /// </summary>
        public static string ToHexHash<T>(this T value)
            => $"{value.GetHashCode():x}";

        public static string ToHex(this byte[] bytes, bool upperCase = false)
            => string.Join("", bytes.Select(b => b.ToString(upperCase ? "X2" : "x2")));

        public static string ToBase64(this byte[] bytes)
            => Convert.ToBase64String(bytes);

        public static string Base64ToHex(this string base64)
            => Convert.FromBase64String(base64).ToHex();

        public static byte[] ToBytesUtf8(this string s)
            => Encoding.UTF8.GetBytes(s);

        public static readonly string[] ByteSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

        /// Improved version of https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        public static string BytesToString(long byteCount, int numPlacesToRound = 1)
        {
            if (byteCount == 0) return "0B";
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), numPlacesToRound);
            return $"{(Math.Sign(byteCount) * num).ToString($"F{numPlacesToRound}")}{ByteSuffixes[place]}";
        }

        /// <summary>
        /// The normalized DateTime format, suitable for inclusion in a filename.
        /// </summary>
        public const string NormalizedDateTimeFormat = "yyyy-MM-dd_HH-mm-ss";

        /// <summary>
        /// Returns the normalized representation of the given DateTime.
        /// </summary>
        public static string ToNormalizedString(this DateTime dateTime)
            => dateTime.ToString(NormalizedDateTimeFormat);

        /// <summary>
        /// Returns the current date-time in a format appropriate for appending to files.
        /// </summary>
        public static string GetTimeStamp()
            => DateTime.Now.ToNormalizedString();

        /// <summary>
        /// Adds a zero-width character after each lowercase letter
        /// to force PowerBI into treating the string in a case-sensitive manner.
        /// See: https://blog.crossjoin.co.uk/2019/10/06/power-bi-and-case-sensitivity/
        /// </summary>
        public static string ToPbiCaseSensitiveString(this string input)
        {
            const char zeroWidthChar = (char)8203;
            var sb = new StringBuilder(input.Length * 2);

            foreach (var c in input)
            {
                sb.Append(c);
                if (char.IsLower(c))
                {
                    sb.Append(zeroWidthChar);
                }
            }

            return sb.ToString();
        }
    }
}
