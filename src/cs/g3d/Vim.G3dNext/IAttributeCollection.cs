using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFastNS;

namespace Vim.G3dNext
{
    /// <summary>
    /// Defines method for additionnal setup after constructors in generated G3d classes.
    /// </summary>
    public interface ISetup
    {
        void Setup();
    }

    /// <summary>
    /// A collection of attributes and readers which can be used to deserialize attributes from a stream.<br/>
    /// <br/>
    /// A class may implement this interface to define the specialized set of attributes and attribute readers for 
    /// a given context.<br/>
    /// <br/>
    /// For example, the geometry and instance information in a VIM file is defined in a class named VimAttributeCollection.
    /// </summary>
    public interface IAttributeCollection
    {
        /// <summary>
        /// A mapping from attribute name to its corresponding attribute.<br/>
        /// This is populated when reading attributes from a stream.
        /// </summary>
        IDictionary<string, IAttribute> Map { get; }

        /// <summary>
        /// Returns the attribute corresponding to the given type.
        /// </summary>
        IAttribute GetAttribute(Type attributeType);

        /// <summary>
        /// Merges the attribute with the given name with any other matching attributes in the other collections.
        /// </summary>
        IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections);

        /// <summary>
        /// Validates the attribute collection. May throw an exception if the collection is invalid.
        /// </summary>
        void Validate();

    }

    /// <summary>
    /// Extension functions and helpers for attribute collections
    /// </summary>
    public static class AttributeCollectionExtensions
    {
        public static IEnumerable<string> GetAttributeNames(this IAttributeCollection attributeCollection)
            => attributeCollection.Map.Keys;

        public static long GetSize(this IAttributeCollection attributeCollection)
            => attributeCollection.Map.Values
                .Sum(a => a.GetSizeInBytes());

        public static BFastNS.BFast ToBFast(this IAttributeCollection attributeCollection)
        {
            var attributes = attributeCollection.Map.Values
                .OrderBy(n => n.Name)
                .ToArray(); // Order the attributes by name for consistency

            var bfast = new BFastNS.BFast();
            foreach (var a in attributes)
            {
                a.AddTo(bfast);
            }
            return bfast;
        }

        public static IEnumerable<TAttr> GetAttributesOfType<TAttr>(
            this IReadOnlyList<IAttributeCollection> collections)
            where TAttr : IAttribute
            => collections
                .Select(c => c.Map.Values.OfType<TAttr>().FirstOrDefault())
                .Where(a => a != null);

        public static IEnumerable<(TAttr Attribute, int IndexedCount)> GetIndexedAttributesOfType<TAttr>(
            this IReadOnlyList<IAttributeCollection> collections)
            where TAttr : IAttribute<int>
            => collections
                .Select(c =>
                {
                    var attr = c.Map.Values.OfType<TAttr>().FirstOrDefault();
                    if (attr == null || attr.IndexInto == null)
                        return (attr, 0);

                    var indexedAttr = c.GetAttribute(attr.IndexInto);
                    return (attr, indexedAttr.Data.Length);
                })
                .Where(t => t.attr != null);

        public static T Merge<T>(this IReadOnlyList<T> collections)
            where T: IAttributeCollection, new()
        {
            if (collections == null)
                return new T();

            if (collections.Count == 1)
                return collections[0];

            // More than one collection; the first collection dictates the attributes to merge.
            var @base = collections.First();
            
            var others = new List<IAttributeCollection>();
            for (var i = 1; i < collections.Count; ++i)
            {
                others.Add(collections[i]);
            }

            var result = new T();
            
            foreach (var item in @base.Map)
            {
                var name = item.Key;

                var merged = @base.MergeAttribute(name, others);

                result.Map[name] = merged;
            }

            return result;
        }
    }
}
