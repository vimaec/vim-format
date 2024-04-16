using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vim.Util
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Returns the index of the first element matching the given predicate.
        /// </summary>
        public static int IndexOf<T>(this IList<T> self, Func<T, bool> predicate)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (predicate(self[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the first element matching the given item.
        /// </summary>
        public static int IndexOf<T>(this IList<T> self, T value) where T : IEquatable<T>
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i].Equals(value))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// [a, b, c ..] => [0, a, a+b, a+b+c ...]
        /// </summary>
        public static int[] OffsetsFromCounts(this IList<int> self)
        {
            var result = new int[self.Count];
            for (var i = 1; i < self.Count; ++i)
            {
                result[i] = result[i - 1] + self[i-1];
            }
            return result;
        }

        /// <summary>
        /// Returns an IEnumerable containing only indices of the array for which the function satisfies a specific predicate.
        /// </summary>
        public static IEnumerable<int> IndicesWhere<T>(this IList<T> self, Func<T, bool> predicate)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (predicate(self[i]))
                    yield return i;
            }
        }

        /// <summary>
        /// Returns an IEnumerable containing only indices of the array for which the function satisfies a specific predicate.
        /// </summary>
        public static IEnumerable<int> IndicesWhere<T>(this IList<T> self, Func<T, int, bool> predicate)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (predicate(self[i], i))
                    yield return i;
            }
        }

        /// <summary>
        /// Returns the top of a stack, or the default T value if none is present.
        /// </summary>
        public static T PeekOrDefault<T>(this Stack<T> self)
            => self.Count > 0 ? self.Peek() : default;

        public static T PopIfNotEmpty<T>(this Stack<T> self)
            => self.Count > 0 ? self.Pop() : default;

        /// <summary>
        /// A substitute for SingleOrDefault which does not throw an exception when the collection size is greater than 1.
        /// If the collection size is greater than 1, returns default.
        /// </summary>
        public static T OneOrDefault<T>(this IEnumerable values)
        {
            var items = values?.OfType<T>().ToList();
            return items?.Count == 1 ? items[0] : default;
        }

        public static T ElementAtOrDefault<T>(this IReadOnlyList<T> items, int index, T @default = default(T))
            => index < 0 || index >= items.Count ? @default : items[index];

        /// <summary>
        /// Given a collection of items, and a map function, counts how often each mapped item is found.
        /// </summary>
        public static Dictionary<U, int> CountInstances<T, U>(this IEnumerable<T> self, Func<T, U> map)
            => self.Select(map).Where(x => x != null).GroupBy(x => x).ToDictionary(grp => grp.Key, grp => grp.Count<U>());

        /// <summary>
        /// Like Linq.ToDictionary but skips duplicate keys, without throwing an exception.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionaryIgnoreDuplicates<TSrc, TKey, TValue>(
            this IEnumerable<TSrc> self, Func<TSrc, TKey> keySelector, Func<TSrc, TValue> valueSelector)
        {
            var r = new Dictionary<TKey, TValue>();
            foreach (var x in self)
                r.AddIfNotPresent(keySelector(x), valueSelector(x));
            return r;
        }

        /// <summary>
        /// Returns distinct values each one assigned a new incremented index.
        /// </summary>
        public static IndexedSet<T> ToIndexedSet<T>(this IEnumerable<T> self)
            => new IndexedSet<T>(self);

        public static bool DictionaryEqual<TKey, TValue>(
            this IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second,
            IEqualityComparer<TValue> valueComparer = null)
        {
            // From: https://stackoverflow.com/a/3928856

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

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static void AddIfNotPresent<K, V>(this IDictionary<K, V> self, K key, V value)
        {
            if (!self.ContainsKey(key))
                self.Add(key, value);
        }

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
        public static V GetOrDefault<K, V>(this IDictionary<K, V> self, K key)
            => self.GetOrDefault(key, default);

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue)
            => self.ContainsKey(key) ? self[key] : defaultValue;

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
        public static V GetOrDefaultAllowNulls<K, V>(this IDictionary<K, V> self, K key, V defaultValue) where K : class
            => key != null && self.ContainsKey(key) ? self[key] : defaultValue;

        /// <summary>
        /// Returns a value if found in the dictionary, or default if not present.
        /// </summary>
        public static V GetOrDefaultAllowNulls<K, V>(this IDictionary<K, V> self, K key) where K : class
            => self.GetOrDefaultAllowNulls(key, default);

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> self)
            => self.Where(x => x != null);

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
    }
}
