using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Util
{
    /// <summary>
    /// This class is similar to a hash set (and is enumerable like a dictionary), 
    /// but it allows us to create an ordered list for each value added in the order it was first seen. 
    /// TODO: should be called "OrderedSet" probably. And the "OrderedKeys" doesn't make sense.
    /// </summary>
    public class IndexedSet<K> : IDictionary<K, int>
    {
        private readonly Dictionary<K, int> _dictionary = new Dictionary<K, int>();

        public IndexedSet()
        { }

        public IndexedSet(IEnumerable<K> values)
        {
            foreach (var x in values)
                GetOrAdd(x);
        }

        public IEnumerable<K> Keys
           => OrderedKeys;

        public IEnumerable<K> OrderedKeys
            => _dictionary.OrderBy(kv => kv.Value).Select(kv => kv.Key);

        public IEnumerable<int> Values
            => Enumerable.Range(0, _dictionary.Count);

        // It is unfortunate the IDictionary implements Keys and Values as ICollection
        ICollection<K> IDictionary<K, int>.Keys
            => ((IDictionary<K, int>)_dictionary).Keys;

        ICollection<int> IDictionary<K, int>.Values
            => ((IDictionary<K, int>)_dictionary).Values;

        public int Count
            => ((IDictionary<K, int>)_dictionary).Count;

        public bool IsReadOnly
            => ((IDictionary<K, int>)_dictionary).IsReadOnly;

        public int this[K key]
        {
            get => ((IDictionary<K, int>)_dictionary)[key];
            set => throw new System.NotImplementedException();
        }

        public int GetOrAdd(K k)
        {
            if (_dictionary.TryGetValue(k, out var result))
                return result;

            var count = _dictionary.Count;
            _dictionary.Add(k, count);

            return count;
        }

        public int Add(K k)
            => GetOrAdd(k);

        public IEnumerator<KeyValuePair<K, int>> GetEnumerator()
            => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _dictionary.GetEnumerator();

        public void Add(K key, int value)
        => ((IDictionary<K, int>)_dictionary).Add(key, value);

        public bool ContainsKey(K key)
            => ((IDictionary<K, int>)_dictionary).ContainsKey(key);

        public bool Remove(K key)
            => throw new System.NotSupportedException();

        public bool TryGetValue(K key, out int value)
            => ((IDictionary<K, int>)_dictionary).TryGetValue(key, out value);

        public void Add(KeyValuePair<K, int> item)
            => ((IDictionary<K, int>)_dictionary).Add(item);

        public void Clear()
            => throw new System.NotSupportedException();

        public bool Contains(KeyValuePair<K, int> item)
            => ((IDictionary<K, int>)_dictionary).Contains(item);

        public void CopyTo(KeyValuePair<K, int>[] array, int arrayIndex)
            => ((IDictionary<K, int>)_dictionary).CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<K, int> item)
            => throw new System.NotSupportedException();
    }

    public static class IndexedSetExtensions
    {
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
    }
}
