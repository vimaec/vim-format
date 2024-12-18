using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Util
{
    public static class ArrayExtensions
    {
        public static IList<T> Select<T>(this int count, Func<int, T> f)
        {
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = f(i);
            }

            return array;
        }

        public static IList<T> Repeat<T>(this T value, int count)
        {
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = value;
            }

            return array;
        }

        public static IList<T> SelectByIndex<T>(this IList<T> self, IList<int> indices)
            => indices.Select(i => self[i]).ToArray();

        public static IEnumerable<int> Indices<T>(this IList<T> self)
            => Enumerable.Range(0, self.Count).ToArray();

        public static IList<T> Slice<T>(this IList<T> self, int from, int to)
            => Select(to - from, i => self[i + from]);

        public static IList<T> SubArray<T>(this IList<T> self, int from, int count)
            => self.Slice(from, count + from);

        public static IList<int> Range(this int self)
            => Select(self, i => i);

        /// <summary>
        /// Creates a new array that concatenates a unit item list of one item before it
        /// Repeatedly calling Prepend would result in significant performance degradation.
        /// </summary>
        public static IList<T> Prepend<T>(this IList<T> self, T x)
            => (self.Count + 1).Select(i => i == 0 ? x : self[i - 1]);

        /// <summary>
        /// Applies a function (like "+") to each element in the series to create an effect similar to partial sums.
        /// The first value in the array will be zero.
        /// </summary>
        public static IList<T> PostAccumulate<T>(this IList<T> self, Func<T, T, T> f, T init = default)
        {
            var n = self.Count;
            var r = new T[n + 1];
            var prev = r[0] = init;
            if (n == 0) return r;
            for (var i = 0; i < n; ++i)
            {
                prev = r[i + 1] = f(prev, self[i]);
            }
            return r;
        }

        /// <summary>
        /// Returns a new array containing all elements excluding the last n elements.
        /// </summary>
        public static IList<T> DropLast<T>(this IList<T> self, int n = 1)
            => self.Count > n ? self.Take(self.Count - n).ToArray() : Array.Empty<T>();

        public static int IndexOf(this long[] self, long value)
        {
            for (var i = 0; i < self.Length; i++)
            {
                if (self[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
