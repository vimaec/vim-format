﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Vim.BFastLib;
using Vim.Util;

namespace Vim.Format.Tests
{
    public static class FormatTests
    {
        public static void AssertNameAndSizesAreEqual(INamedBuffer b1, INamedBuffer b2)
        {
            Assert.AreEqual(b1.ElementSize, b2.ElementSize);
            Assert.AreEqual(b1.NumElements(), b2.NumElements());
            Assert.AreEqual(b1.Name, b2.Name);
        }

        public static void AssertNameAndSizesAreEqual(SerializableEntityTable t1, SerializableEntityTable t2)
        {
            Assert.AreEqual(t1.Name, t2.Name);
            Assert.AreEqual(t1.IndexColumns.Count, t2.IndexColumns.Count);
            for (var i = 0; i < t1.IndexColumns.Count; ++i)
                AssertNameAndSizesAreEqual(t1.IndexColumns[i], t2.IndexColumns[i]);

            Assert.AreEqual(t1.DataColumns.Count, t2.DataColumns.Count);
            for (var i = 0; i < t1.DataColumns.Count; ++i)
                AssertNameAndSizesAreEqual(t1.DataColumns[i], t2.DataColumns[i]);

            Assert.AreEqual(t1.StringColumns.Count, t2.StringColumns.Count);
            for (var i = 0; i < t1.StringColumns.Count; ++i)
                AssertNameAndSizesAreEqual(t1.StringColumns[i], t2.StringColumns[i]);
        }

        public static void AssertEquals(SerializableDocument d1, SerializableDocument d2, bool compareStringTables = true)
        {
            Assert.AreEqual(d1.EntityTables.Count, d2.EntityTables.Count);
            Assert.AreEqual(d1.Header, d2.Header);
            if (compareStringTables)
            {
                var strings1 = d1.StringTable.OrderBy(x => x).ToArray();
                var strings2 = d2.StringTable.OrderBy(x => x).ToArray();
                Assert.AreEqual(strings1, strings2);
            }

            // TODO: upgrade to a proper Geometry comparer
            //Assert.AreEqual(d1.Geometry.FaceCount(), d2.Geometry.FaceCount());
            //Assert.AreEqual(d1.Geometry.VertexCount(), d2.Geometry.VertexCount());

            Assert.AreEqual(d1.Assets.Length, d2.Assets.Length);

            for (var i = 0; i < d1.EntityTables.Count; ++i)
            {
                AssertNameAndSizesAreEqual(d1.EntityTables[i], d2.EntityTables[i]);
            }
        }

        public static void AssertEquals(EntityTable et1, EntityTable et2)
        {
            Assert.AreEqual(et1.Name, et2.Name);
            Assert.AreEqual(et1.DataColumnNames.OrderBy(n => n).ToArray(), et2.DataColumnNames.OrderBy(n => n).ToArray());
            Assert.AreEqual(et1.IndexColumns.Keys.OrderBy(n => n).ToArray(), et2.IndexColumns.Keys.OrderBy(n => n).ToArray());
            Assert.AreEqual(et1.StringColumnNames.OrderBy(n => n).ToArray(), et2.StringColumnNames.OrderBy(n => n).ToArray());

            var columns1 = et1.Columns.OrderBy(c => c.Name).ToArray();
            var columns2 = et2.Columns.OrderBy(c => c.Name).ToArray();

            Assert.AreEqual(
                columns1.Select(ec => ec.Name).ToArray(),
                columns2.Select(ec => ec.Name).ToArray());

            Assert.AreEqual(
                columns1.Select(ec => ec.NumElements()).ToArray(),
                columns2.Select(ec => ec.NumElements()).ToArray());

            Assert.AreEqual(
                columns1.Select(ec => ec.NumBytes()).ToArray(),
                columns2.Select(ec => ec.NumBytes()).ToArray());
        }

        /// <summary>
        /// Returns true if |(set(a)-set(b))| >= 0
        /// </summary>
        public static bool IsSupersetOf<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            var hashSetA = new HashSet<T>(a);
            var hashSetB = new HashSet<T>(b);
            return hashSetA.IsSupersetOf(hashSetB);
        }

        /// <summary>
        /// Asserts that et1 is equal or a superset of et2.
        /// </summary>
        public static void AssertIsSupersetOf(EntityTable et1, EntityTable et2)
        {
            Assert.AreEqual(et1.Name, et2.Name);
            Assert.IsTrue(IsSupersetOf(et1.DataColumnNames, et2.DataColumnNames));
            Assert.IsTrue(IsSupersetOf(et1.IndexColumns.Keys, et2.IndexColumns.Keys));
            Assert.IsTrue(IsSupersetOf(et1.StringColumnNames, et2.StringColumnNames));

            var columns1 = et1.Columns.ToArray();
            var columns2 = et2.Columns.ToArray();
            Assert.IsTrue(IsSupersetOf(
                columns1.Select(c => c.Name),
                columns2.Select(c => c.Name)));

            foreach (var column in columns2)
            {
                var key = column.Name;
                var matchingColumn =
                    columns1.FirstOrDefault(c => c.Name == key);
                if (matchingColumn == null)
                    Assert.Fail($"No matching column key found: {key}");
                Assert.GreaterOrEqual(matchingColumn.NumElements(), column.NumElements());
                Assert.GreaterOrEqual(matchingColumn.NumBytes(), column.NumBytes());
            }
        }

        /// <summary>
        /// Returns true if d1 is equal or a superset of d2.
        /// </summary>
        public static void AssertIsSuperSetOf(Document d1, Document d2, bool skipGeometryAndNodes = true)
        {
            var schema1 = d1.GetSchema();
            var schema2 = d2.GetSchema();
            Assert.IsTrue(VimSchema.IsSuperSetOf(schema1, schema2));

            var etKeys1 = d1.TableNames;
            var etKeys2 = d2.TableNames;
            Assert.IsTrue(IsSupersetOf(etKeys1, etKeys2));

            foreach (var key in etKeys2)
            {
                if (skipGeometryAndNodes && key.ToLowerInvariant().Contains("geometry"))
                    continue;

                var et2 = d2.GetTable(key);
                var et1 = d1.GetTable(key);
                if (et1 == null)
                    Assert.Fail($"No matching entity table found: {key}");
                AssertIsSupersetOf(et1, et2);
            }
        }

        public static void AssertEquals(Document d1, Document d2, bool skipGeometryAndNodes = false)
        {
            var schema1 = d1.GetSchema();
            var schema2 = d2.GetSchema();
            Assert.IsTrue(VimSchema.IsSame(schema1, schema2));

            var entityTables1 = d1.TableNames.OrderBy(n => n).ToArray();
            var entityTables2 = d2.TableNames.OrderBy(n => n).ToArray();
            Assert.AreEqual(entityTables1, entityTables2);

            foreach (var k in entityTables1)
            {
                if (skipGeometryAndNodes && k.ToLowerInvariant().Contains("geometry"))
                    continue;
                AssertEquals(d1.GetTable(k), d2.GetTable(k));
            }
        }
    }
}
