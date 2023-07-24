using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    /// <summary>
    /// A collection of extension functions and utilities
    /// </summary>
    public static class Util
    {
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
        /// A helper function for append one or more items to an IEnumerable.
        /// </summary>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> xs, params T[] x)
            => xs.Concat(x);

        public static T ElementAtOrDefault<T>(this IReadOnlyList<T> items, int index, T @default)
            => index < 0 || index >= items.Count ? @default : items[index];

        /// <summary>
        /// Given a collection of items, and a map function, counts how often each mapped item is found.
        /// </summary>
        public static Dictionary<U, int> CountInstances<T, U>(this IEnumerable<T> self, Func<T, U> map)
            => self.Select(map).Where(x => x != null).GroupBy(x => x).ToDictionary(grp => grp.Key, grp => grp.Count());

        public static T Minimize<T, U>(this IEnumerable<T> xs, U init, Func<T, U> func) where U : IComparable<U>
        {
            var r = default(T);
            foreach (var x in xs)
            {
                if (func(x).CompareTo(init) < 0)
                {
                    init = func(x);
                    r = x;
                }
            }

            return r;
        }

        public static T Maximize<T, U>(this IEnumerable<T> xs, U init, Func<T, U> func) where U : IComparable<U>
        {
            var r = default(T);
            foreach (var x in xs)
            {
                if (func(x).CompareTo(init) > 0)
                {
                    init = func(x);
                    r = x;
                }
            }

            return r;
        }

        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        /// <summary>
        /// Returns a shallow clone of the given object's properties.
        /// </summary>
        public static T ShallowClone<T>(T obj) where T : class, new()
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var clonedObj = new T();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(obj);
                    property.SetValue(clonedObj, value);
                }
            }

            return clonedObj;
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
            var feet = m.Groups["feet"].Success ? Convert.ToDouble(m.Groups["feet"].Value) : 0;
            var inch = m.Groups["inch"].Success ? Convert.ToInt32(m.Groups["inch"].Value) : 0;
            var sixt = m.Groups["sixt"].Success ? Convert.ToInt32(m.Groups["sixt"].Value) : 0;
            var numer = m.Groups["numer"].Success ? Convert.ToInt32(m.Groups["numer"].Value) : 0;
            var denom = m.Groups["denom"].Success ? Convert.ToInt32(m.Groups["denom"].Value) : 1;
            value = sign * (feet * 12 + inch + sixt / 16.0 + numer / Convert.ToDouble(denom));
            return true;
        }
    }
}
