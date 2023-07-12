using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Format.Utils
{
    public class DictionaryOfLists<TKey, TValue> : Dictionary<TKey, List<TValue>>
    {
        public DictionaryOfLists()
        { }

        public DictionaryOfLists(IEnumerable<IGrouping<TKey, TValue>> groups)
        {
            foreach (var grp in groups)
                Add(grp.Key, grp.ToList());
        }

        public void SortLists(IComparer<TValue> comparer)
        {
            foreach (var (_, list) in this)
                list.Sort(comparer);
        }

        public void Add(TKey k, TValue v)
        {
            if (!ContainsKey(k))
                Add(k, new List<TValue>());
            this[k].Add(v);
        }

        public IEnumerable<TValue> AllValues
            => Values.SelectMany(xs => xs);


        public List<TValue> GetOrDefault(TKey k)
        {
            if (!ContainsKey(k))
                return new List<TValue>();
            return this[k];
        }
    }

    public static class DictionaryOfListExtensions
    {
        public static DictionaryOfLists<K, V> ToDictionaryOfLists<K, V>(this IEnumerable<V> self, Func<V, K> keySelector)
        {
            var r = new DictionaryOfLists<K, V>();
            foreach (var x in self)
            {
                var key = keySelector(x);
                if (key != null)
                    r.Add(keySelector(x), x);
            }
            return r;
        }
    }
}
