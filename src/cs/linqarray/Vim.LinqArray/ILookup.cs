﻿// MIT License - Copyright 2019 (C) VIMaec, LLC.
// MIT License - Copyright 2018 (C) Ara 3D, Inc.
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.Collections.Generic;
using System.Linq;

namespace Vim.LinqArray
{
    /// <summary>
    /// Lookup table: mapping from a key to some value.
    /// </summary>
    public interface ILookup<TKey, TValue>
    {
        IEnumerable<TKey> Keys { get; }
        IEnumerable<TValue> Values { get; }
        bool Contains(TKey key);
        TValue this[TKey key] { get; }
    }

    public class EmptyLookup<TKey, TValue> : ILookup<TKey, TValue>
    {
        public IEnumerable<TKey> Keys => Enumerable.Empty<TKey>();
        public IEnumerable<TValue> Values => Enumerable.Empty<TValue>();
        public bool Contains(TKey key) => false;
        public TValue this[TKey key] => default;
    }

    public class LookupFromDictionary<TKey, TValue> : ILookup<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Dictionary;
        private TValue _default;

        public LookupFromDictionary(IDictionary<TKey, TValue> d = null, TValue defaultValue = default)
        {
            Dictionary = d ?? new Dictionary<TKey, TValue>();
            // TODO: sort?
            _default = defaultValue;
        }

        public IEnumerable<TKey> Keys => Dictionary.Keys;
        public IEnumerable<TValue> Values => Dictionary.Values;
        public TValue this[TKey key] => Contains(key) ? Dictionary[key] : _default;
        public bool Contains(TKey key) => Dictionary.ContainsKey(key);
    }

    public class LookupFromArray<TValue> : ILookup<int, TValue>
    {
        private IArray<TValue> array;

        public LookupFromArray(IArray<TValue> xs)
        {
            array = xs;
        }

        public IEnumerable<int> Keys => array.Indices().ToEnumerable();
        public IEnumerable<TValue> Values => array.ToEnumerable();
        public TValue this[int key] => array[key];
        public bool Contains(int key) => key >= 0 && key <= array.Count;
    }

    public static class LookupExtensions
    {
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IDictionary<TKey, TValue> d, TValue defaultValue = default)
            => new LookupFromDictionary<TKey, TValue>(d, defaultValue);

        public static TValue GetOrDefault<TKey, TValue>(this ILookup<TKey, TValue> lookup, TKey key)
            => lookup.Contains(key) ? lookup[key] : default;

        public static IEnumerable<TValue> GetValues<TKey, TValue>(this ILookup<TKey, TValue> lookup)
            => lookup.Keys.Select(k => lookup[k]);
    }
}
