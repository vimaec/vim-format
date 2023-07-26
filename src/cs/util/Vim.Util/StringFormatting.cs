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

        // https://stackoverflow.com/questions/22794466/parsing-all-possible-types-of-varying-architectural-dimension-input
        // https://stackoverflow.com/questions/6157865/c-sharp-function-to-convert-text-input-of-feet-inches-meters-centimeters-millime

        public static Regex FeetAndInchesRegex
            = new Regex(
                "^\\s*(?<minus>-)?\\s*(((?<feet>\\d+)(?<inch>\\d{2})(?<sixt>\\d{2}))|((?<feet>[\\d.]+)')?[\\s-]*((?<inch>\\d+)?[\\s-]*((?<numer>\\d+)/(?<denom>\\d+))?\")?)\\s*$",
                RegexOptions.Compiled);

        public static Regex NumberAndUnitsRegex
            = new Regex("^\\s*(?<number>-?[0-9]*[.]?[0-9]+)([\\s]+(?<units>[a-zA-Z°²³$£%/*^@#`]*))?\\s*$", RegexOptions.Compiled);

        /// <summary>
        /// Parses the input string and outputs the value in decimal inches upon success
        /// </summary>
        public static bool TryParseFeetAndInchesToDecimalInches(string input, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var m = FeetAndInchesRegex.Match(input);
            if (!m.Success)
                return false;

            var sign = m.Groups["minus"].Success ? -1 : 1;
            var feet = m.Groups["feet"].Success ? Convert.ToDouble((string)m.Groups["feet"].Value) : 0;
            var inch = m.Groups["inch"].Success ? Convert.ToInt32((string)m.Groups["inch"].Value) : 0;
            var sixt = m.Groups["sixt"].Success ? Convert.ToInt32((string)m.Groups["sixt"].Value) : 0;
            var numer = m.Groups["numer"].Success ? Convert.ToInt32((string)m.Groups["numer"].Value) : 0;
            var denom = m.Groups["denom"].Success ? Convert.ToInt32((string)m.Groups["denom"].Value) : 1;
            value = sign * (feet * 12 + inch + sixt / 16.0 + numer / Convert.ToDouble(denom));
            return true;
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
    }
}
