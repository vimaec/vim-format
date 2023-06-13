using System;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.DataFormat
{
    public static partial class ColumnExtensions
    {
        public static string GetDataColumnNameWithTypePrefix(this string colNameNoPrefix, Type type)
            => type.GetDataColumnNameTypePrefix() + colNameNoPrefix;

        public static string GetDataColumnNameWithTypePrefix<T>(this string colNameNoPrefix)
            => colNameNoPrefix.GetDataColumnNameWithTypePrefix(typeof(T));

        public static IArray<T>[] GetDataColumnValues<T>(this EntityTable et, string colName, string[] suffixes) where T : unmanaged
            => et == null
                ? null
                : suffixes.Select(suffix => et.GetDataColumnValues<T>(colName + suffix)).ToArray();

        public static IArray<U> CombineRows<T, U>(this IArray<T>[] columns, Func<T[], U> combineRowFunc)
        {
            if (columns == null)
                return null;

            var numColumns = columns.Length;
            var numRows = columns[0]?.Count ?? 0;

            return new FunctionalArray<U>(numRows, i =>
            {
                var row = new T[numColumns];
                for (var c = 0; c < numColumns; ++c)
                    row[c] = columns[c][i];

                return combineRowFunc(row);
            });
        }

        public interface ICompositeDataColumn
        {
            IEnumerable<string> GetDataColumnNames(string colNameNoPrefix);
        }

        public abstract class CompositeDataColumn<TCombined, TRaw> : ICompositeDataColumn where TRaw : unmanaged
        {
            public abstract string[] Suffixes { get; }
            public abstract Func<TCombined, TRaw>[] Decomposers();
            public abstract TCombined Combine(TRaw[] row);
            public IEnumerable<string> GetDataColumnNames(string colNameNoPrefix)
                => Suffixes.Select(suffix => typeof(TRaw).GetDataColumnNameTypePrefix() + colNameNoPrefix + suffix);

            public IArray<TCombined> GetDataColumnValues(EntityTable et, string colNameNoPrefix)
                => et.GetDataColumnValues<TRaw>(colNameNoPrefix.GetDataColumnNameWithTypePrefix<TRaw>(), Suffixes)
                    .CombineRows(Combine);

            public EntityTableBuilder AddDataColumns(
                EntityTableBuilder tb,
                string colNameNoPrefix,
                IEnumerable<TCombined> rows)
            {
                var typePrefix = typeof(TRaw).GetDataColumnNameTypePrefix();
                var suffixes = Suffixes;
                var decomposeFuncs = Decomposers();

                for (var i = 0; i < suffixes.Length; ++i)
                {
                    var suffix = suffixes[i];
                    var deocmposeFunc = decomposeFuncs[i];

                    // ReSharper disable once PossibleMultipleEnumeration
                    tb.AddDataColumn($"{typePrefix}{colNameNoPrefix}{suffix}", rows.Select(deocmposeFunc).ToArray());
                }

                return tb;
            }
        }

        // DVector2

        public class DVector2CompositeDataColumn : CompositeDataColumn<DVector2, double>
        {
            public override string[] Suffixes => new [] { ".X", ".Y" };
            public override DVector2 Combine(double[] row) => new DVector2(row[0], row[1]);
            public override Func<DVector2, double>[] Decomposers() => new Func<DVector2, double>[] { (v => v.X), (v => v.Y) };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<DVector2> xs)
            => new DVector2CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<DVector2> GetDataColumnValuesAsDVector2(this EntityTable et, string colNameNoPrefix)
            => new DVector2CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // Vector2

        public class Vector2CompositeDataColumn : CompositeDataColumn<Vector2, float>
        {
            public override string[] Suffixes => new[] { ".X", ".Y" };
            public override Vector2 Combine(float[] row) => new Vector2(row[0], row[1]);
            public override Func<Vector2, float>[] Decomposers() => new Func<Vector2, float>[] { (v => v.X), (v => v.Y) };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<Vector2> xs)
            => new Vector2CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<Vector2> GetDataColumnValuesAsVector2(this EntityTable et, string colNameNoPrefix)
            => new Vector2CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // DVector3

        public class DVector3CompositeDataColumn : CompositeDataColumn<DVector3, double>
        {
            public override string[] Suffixes => new[] { ".X", ".Y", ".Z" };
            public override DVector3 Combine(double[] row) => new DVector3(row[0], row[1], row[2]);
            public override Func<DVector3, double>[] Decomposers() => new Func<DVector3, double>[] { (v => v.X), (v => v.Y), (v => v.Z) };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<DVector3> xs)
            => new DVector3CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<DVector3> GetDataColumnValuesAsDVector3(this EntityTable et, string colNameNoPrefix)
            => new DVector3CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // Vector3

        public class Vector3CompositeDataColumn : CompositeDataColumn<Vector3, float>
        {
            public override string[] Suffixes => new[] { ".X", ".Y", ".Z" };
            public override Vector3 Combine(float[] row) => new Vector3(row[0], row[1], row[2]);
            public override Func<Vector3, float>[] Decomposers() => new Func<Vector3, float>[] { (v => v.X), (v => v.Y), (v => v.Z) };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<Vector3> xs)
            => new Vector3CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<Vector3> GetDataColumnValuesAsVector3(this EntityTable et, string colNameNoPrefix)
            => new Vector3CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // DVector4

        public class DVector4CompositeDataColumn : CompositeDataColumn<DVector4, double>
        {
            public override string[] Suffixes => new[] { ".X", ".Y", ".Z", ".W" };
            public override DVector4 Combine(double[] row) => new DVector4(row[0], row[1], row[2], row[3]);
            public override Func<DVector4, double>[] Decomposers() => new Func<DVector4, double>[] { (v => v.X), (v => v.Y), (v => v.Z), (v => v.W) };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<DVector4> xs)
            => new DVector4CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<DVector4> GetDataColumnValuesAsDVector4(this EntityTable et, string colNameNoPrefix)
            => new DVector4CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // Vector4

        public class Vector4CompositeDataColumn : CompositeDataColumn<Vector4, float>
        {
            public override string[] Suffixes => new[] { ".X", ".Y", ".Z", ".W" };
            public override Vector4 Combine(float[] row) => new Vector4(row[0], row[1], row[2], row[3]);
            public override Func<Vector4, float>[] Decomposers() => new Func<Vector4, float>[] { (v => v.X), (v => v.Y), (v => v.Z), (v => v.W) };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<Vector4> xs)
            => new Vector4CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<Vector4> GetDataColumnValuesAsVector4(this EntityTable et, string colNameNoPrefix)
            => new Vector4CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // DAABox

        public class DAABoxCompositeDataColumn : CompositeDataColumn<DAABox, double>
        {
            public override string[] Suffixes => new[]
            {
                ".Min.X",
                ".Min.Y",
                ".Min.Z",
                ".Max.X",
                ".Max.Y",
                ".Max.Z",
            };

            public override DAABox Combine(double[] row)
                => new DAABox(new DVector3(row[0], row[1], row[2]), new DVector3(row[3], row[4], row[5]));

            public override Func<DAABox, double>[] Decomposers() => new Func<DAABox, double>[]
            {
                b => b.Min.X,
                b => b.Min.Y,
                b => b.Min.Z,
                b => b.Max.X,
                b => b.Max.Y,
                b => b.Max.Z,
            };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<DAABox> xs)
            => new DAABoxCompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<DAABox> GetDataColumnValuesAsDAABox(this EntityTable et, string colNameNoPrefix)
            => new DAABoxCompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // AABox

        public class AABoxCompositeDataColumn : CompositeDataColumn<AABox, float>
        {
            public override string[] Suffixes => new[]
            {
                ".Min.X",
                ".Min.Y",
                ".Min.Z",
                ".Max.X",
                ".Max.Y",
                ".Max.Z",
            };

            public override AABox Combine(float[] row)
                => new AABox(new Vector3(row[0], row[1], row[2]), new Vector3(row[3], row[4], row[5]));

            public override Func<AABox, float>[] Decomposers() => new Func<AABox, float>[]
            {
                b => b.Min.X,
                b => b.Min.Y,
                b => b.Min.Z,
                b => b.Max.X,
                b => b.Max.Y,
                b => b.Max.Z,
            };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<AABox> xs)
            => new AABoxCompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<AABox> GetDataColumnValuesAsAABox(this EntityTable et, string colNameNoPrefix)
            => new AABoxCompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // DAABox2D

        public class DAABox2DCompositeDataColumn : CompositeDataColumn<DAABox2D, double>
        {
            public override string[] Suffixes => new[]
            {
                ".Min.X",
                ".Min.Y",
                ".Max.X",
                ".Max.Y",
            };

            public override DAABox2D Combine(double[] row)
                => new DAABox2D(new DVector2(row[0], row[1]), new DVector2(row[2], row[3]));

            public override Func<DAABox2D, double>[] Decomposers() => new Func<DAABox2D, double>[]
            {
                b => b.Min.X,
                b => b.Min.Y,
                b => b.Max.X,
                b => b.Max.Y,
            };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<DAABox2D> xs)
            => new DAABox2DCompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<DAABox2D> GetDataColumnValuesAsDAABox2D(this EntityTable et, string colNameNoPrefix)
            => new DAABox2DCompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // AABox2D

        public class AABox2DCompositeDataColumn : CompositeDataColumn<AABox2D, float>
        {
            public override string[] Suffixes => new[]
            {
                ".Min.X",
                ".Min.Y",
                ".Max.X",
                ".Max.Y",
            };

            public override AABox2D Combine(float[] row)
                => new AABox2D(new Vector2(row[0], row[1]), new Vector2(row[2], row[3]));

            public override Func<AABox2D, float>[] Decomposers() => new Func<AABox2D, float>[]
            {
                b => b.Min.X,
                b => b.Min.Y,
                b => b.Max.X,
                b => b.Max.Y,
            };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<AABox2D> xs)
            => new AABox2DCompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<AABox2D> GetDataColumnValuesAsAABox2D(this EntityTable et, string colNameNoPrefix)
            => new AABox2DCompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        // Matrix4x4

        public class Matrix4x4CompositeDataColumn : CompositeDataColumn<Matrix4x4, float>
        {
            public override string[] Suffixes => new[]
            {
                ".M11", ".M12", ".M13", ".M14", ".M21", ".M22", ".M23", ".M24", ".M31", ".M32", ".M33",
                ".M34", ".M41", ".M42", ".M43", ".M44",
            };

            public override Matrix4x4 Combine(float[] row)
                => new Matrix4x4(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9],
                    row[10], row[11], row[12], row[13], row[14], row[15]);

            public override Func<Matrix4x4, float>[] Decomposers() => new Func<Matrix4x4, float>[]
            {
                m => m.M11, m => m.M12, m => m.M13, m => m.M14, m => m.M21, m => m.M22, m => m.M23,
                m => m.M24, m => m.M31, m => m.M32, m => m.M33, m => m.M34, m => m.M41, m => m.M42, m => m.M43,
                m => m.M44,
            };
        }

        public static EntityTableBuilder AddCompositeDataColumns(this EntityTableBuilder tb, string colNameNoPrefix, IEnumerable<Matrix4x4> xs)
            => new Matrix4x4CompositeDataColumn().AddDataColumns(tb, colNameNoPrefix, xs);

        public static IArray<Matrix4x4> GetDataColumnValuesAsMatrix4x4(this EntityTable et, string colNameNoPrefix)
            => new Matrix4x4CompositeDataColumn().GetDataColumnValues(et, colNameNoPrefix);

        public static readonly IReadOnlyDictionary<Type, ICompositeDataColumn> CompositeTypeMap
            = new Dictionary<Type, ICompositeDataColumn>
            {
                { typeof(DVector2), new DVector2CompositeDataColumn() },
                { typeof(Vector2), new Vector2CompositeDataColumn() },
                { typeof(DVector3), new DVector3CompositeDataColumn() },
                { typeof(Vector3), new Vector3CompositeDataColumn() },
                { typeof(DVector4), new DVector4CompositeDataColumn() },
                { typeof(Vector4), new Vector4CompositeDataColumn() },
                { typeof(DAABox), new DAABoxCompositeDataColumn() },
                { typeof(AABox), new AABoxCompositeDataColumn() },
                { typeof(DAABox2D), new DAABox2DCompositeDataColumn() },
                { typeof(AABox2D), new AABox2DCompositeDataColumn() },
                { typeof(Matrix4x4), new Matrix4x4CompositeDataColumn() },
            };

        public static IArray<T> GetCompositeDataColumnValues<T>(this EntityTable et, string colNameNoPrefix)
        {
            if (et == null)
                return null;

            var type = typeof(T);

            if (!CompositeTypeMap.ContainsKey(type))
                throw new Exception($"{nameof(GetCompositeDataColumnValues)} error - unsupported composite data column type {type}");

            if (type == typeof(DVector2))
                return et.GetDataColumnValuesAsDVector2(colNameNoPrefix) as IArray<T>;

            if (type == typeof(Vector2))
                return et.GetDataColumnValuesAsVector2(colNameNoPrefix) as IArray<T>;

            if (type == typeof(DVector3))
                return et.GetDataColumnValuesAsDVector3(colNameNoPrefix) as IArray<T>;

            if (type == typeof(Vector3))
                return et.GetDataColumnValuesAsVector3(colNameNoPrefix) as IArray<T>;

            if (type == typeof(DVector4))
                return et.GetDataColumnValuesAsDVector4(colNameNoPrefix) as IArray<T>;

            if (type == typeof(Vector4))
                return et.GetDataColumnValuesAsVector4(colNameNoPrefix) as IArray<T>;

            if (type == typeof(DAABox))
                return et.GetDataColumnValuesAsDAABox(colNameNoPrefix) as IArray<T>;

            if (type == typeof(AABox))
                return et.GetDataColumnValuesAsAABox(colNameNoPrefix) as IArray<T>;

            if (type == typeof(DAABox2D))
                return et.GetDataColumnValuesAsDAABox2D(colNameNoPrefix) as IArray<T>;

            if (type == typeof(AABox2D))
                return et.GetDataColumnValuesAsAABox2D(colNameNoPrefix) as IArray<T>;

            if (type == typeof(Matrix4x4))
                return et.GetDataColumnValuesAsMatrix4x4(colNameNoPrefix) as IArray<T>;

            return null;
        }
    }
}
