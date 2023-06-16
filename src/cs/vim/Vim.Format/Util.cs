using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Vim.Format
{
    public static class Util
    {
        /// <summary>
        /// Given a dictionary looks up the key, or uses the function to add to the dictionary, and returns that result.
        /// </summary>
        public static V GetOrCompute<K, V>(this IDictionary<K, V> self, K key, Func<K, V> func)
        {
            if (self.ContainsKey(key))
                return self[key];
            var value = func(key);
            self.Add(key, value);
            return value;
        }

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue)
            => self.ContainsKey(key) ? self[key] : defaultValue;

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> self, K key)
            => self.GetOrDefault(key, default);

        /// <summary>
        /// Adds a key and value to a dictionary if the key is not already present, otherwise does nothing and returns false.
        /// </summary>
        public static bool TryAdd<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            if (self.ContainsKey(key)) return false;
            self.Add(key, value);
            return true;
        }

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static void AddIfNotPresent<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            if (!self.ContainsKey(key))
                self.Add(key, value);
        }

        public class ArrayEqualityComparer<T> : IEqualityComparer<T[]> where T : IEquatable<T>
        {
            public bool Equals(T[] x, T[] y)
            {
                if (x == y) return true;
                if (x == null || y == null) return false;
                if (x.Length != y.Length) return false;
                for (var i = 0; i < x.Length; ++i)
                {
                    if (!x[i].Equals(y[i]))
                        return false;
                }

                return true;
            }

            public int GetHashCode(T[] obj) => obj.GetHashCode();
        }

        // From: https://stackoverflow.com/a/3928856
        public static bool DictionaryEqual<TKey, TValue>(
            this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second,
            IEqualityComparer<TValue> valueComparer = null)
        {
            if (first == second) return true;
            if ((first == null) || (second == null)) return false;
            if (first.Count != second.Count) return false;

            valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;

            foreach (var kvp in first)
            {
                if (!second.TryGetValue(kvp.Key, out var secondValue)) return false;
                if (!valueComparer.Equals(kvp.Value, secondValue)) return false;
            }
            return true;
        }

        public static byte[] ToBytesUtf8(this string s)
            => Encoding.UTF8.GetBytes(s);

        /// <summary>
        /// Create the directory for the given filepath if it doesn't exist.
        /// </summary>
        public static string CreateFileDirectory(string filepath)
        {
            var dirPath = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return filepath;
        }

        /// <summary>
        /// A helper function for append one or more items to an IEnumerable.
        /// </summary>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> xs, params T[] x)
            => xs.Concat(x);

        /// <summary>
        /// Generates a Regular Expression character set from an array of characters
        /// </summary>
        private static Regex CharSetToRegex(params char[] chars)
            => new Regex($"[{Regex.Escape(new string(chars))}]");

        /// <summary>
        /// Creates a regular expression for finding illegal file name characters.
        /// </summary>
        private static Regex InvalidFileNameRegex =>
            CharSetToRegex(Path.GetInvalidFileNameChars());

        /// <summary>
        /// Convert a string to a valid name
        /// https://stackoverflow.com/questions/146134/how-to-remove-illegal-characters-from-path-and-filenames
        /// https://stackoverflow.com/questions/2230826/remove-invalid-disallowed-bad-characters-from-filename-or-directory-folder?noredirect=1&lq=1
        /// https://stackoverflow.com/questions/10898338/c-sharp-string-replace-to-remove-illegal-characters?noredirect=1&lq=1
        /// </summary>
        public static string ToValidFileName(this string s, string replacement = "_", int maxLength = -1)
        {
            var replaced = InvalidFileNameRegex.Replace(s, m => replacement);
            
            if (maxLength >= 0 && maxLength != replaced.Length)
            {
                replaced = replaced.Substring(0, Math.Min(maxLength, replaced.Length));
            }

            return replaced;
        }
        
        /// <summary>
        /// Returns distinct values each one assigned a new incremented index.
        /// </summary>
        public static IndexedSet<T> ToIndexedSet<T>(this IEnumerable<T> self)
            => new IndexedSet<T>(self);

        /// <summary>
        /// Given a collection of items, and a map function, counts how often each mapped item is found.
        /// </summary>
        public static Dictionary<U, int> CountInstances<T, U>(this IEnumerable<T> self, Func<T, U> map)
            => self.Select(map).Where(x => x != null).GroupBy(x => x).ToDictionary(grp => grp.Key, grp => grp.Count());

        public static DictionaryOfLists<TKey, TValue> ToDictionaryOfLists<TKey, TValue>(
            this IEnumerable<IGrouping<TKey, TValue>> groups)
            => new DictionaryOfLists<TKey, TValue>(groups);

        public static IEnumerable<Type> GetAllSubclassesOf(Assembly asm, Type t)
            => asm.GetTypes().Where(x => x.IsSubclassOf(t));

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
    }
}
