using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.BFastLib;

namespace Vim.Format
{
    public static partial class ColumnExtensions
    {
        public static void ValidateColumnRowsAreAligned(this IEnumerable<INamedBuffer> columns)
        {
            var numRows = columns.FirstOrDefault()?.NumElements() ?? 0;

            foreach (var column in columns)
            {
                var columnRows = column.NumElements();
                if (columnRows == numRows)
                    continue;

                var msg = $"Column '{column.Name}' has {columnRows} rows which does not match the first column's {numRows} rows";
                Debug.Fail(msg);
            }
        }

        public static SerializableEntityTable ValidateColumnRowsAreAligned(this SerializableEntityTable et)
        {
            et.AllColumns.ValidateColumnRowsAreAligned();
            return et;
        }

        public static string ValidateCanConcatBuffers(this INamedBuffer thisBuffer, INamedBuffer otherBuffer)
        {
            var thisPrefix = SerializableEntityTable.GetTypeFromName(thisBuffer.Name);
            if (string.IsNullOrEmpty(thisPrefix))
                throw new Exception("NamedBuffer prefix not found");

            var otherPrefix = SerializableEntityTable.GetTypeFromName(otherBuffer.Name);
            if (string.IsNullOrEmpty(otherPrefix))
                throw new Exception("NamedBuffer prefix not found");

            if (thisPrefix != otherPrefix)
                throw new Exception("NamedBuffer prefixes are not equal");

            return thisPrefix;
        }

        public static T[] RemapData<T>(this T[] self, List<int> remapping = null)
            => remapping?.Select(x => self[x])?.ToArray() ?? self;

        public static IBuffer ToBuffer<T>(this T[] array) where T : unmanaged
            => new Buffer<T>(array);

        public const string UnknownNamedBufferPrefix = "Unknown NamedBuffer prefix";

        public static object GetDataColumnValue(this IBuffer dataColumn, string typePrefix, int rowIndex)
        {
            switch (typePrefix)
            {
                case VimConstants.IntColumnNameTypePrefix:
                    return dataColumn.AsArray<int>().ElementAtOrDefault(rowIndex);
                case VimConstants.LongColumnNameTypePrefix:
                    return dataColumn.AsArray<long>().ElementAtOrDefault(rowIndex);
                case VimConstants.ByteColumnNameTypePrefix:
                    return dataColumn.AsArray<byte>().ElementAtOrDefault(rowIndex);
                case VimConstants.FloatColumnNameTypePrefix:
                    return dataColumn.AsArray<float>().ElementAtOrDefault(rowIndex);
                case VimConstants.DoubleColumnNameTypePrefix:
                    return dataColumn.AsArray<double>().ElementAtOrDefault(rowIndex);
                default:
                    return null;
            }
        }

        public static object GetDataColumnValue(this INamedBuffer dataColumn, int rowIndex)
        {
            var prefix = SerializableEntityTable.GetTypeFromName(dataColumn.Name);
            return dataColumn.GetDataColumnValue(prefix, rowIndex);
        }

        public static IBuffer CreateDefaultDataColumnBuffer(int length, string typePrefix)
        {
            switch (typePrefix)
            {
                case (VimConstants.IntColumnNameTypePrefix):
                    return new int[length].ToBuffer();
                case (VimConstants.LongColumnNameTypePrefix):
                    return new long[length].ToBuffer();
                case (VimConstants.ByteColumnNameTypePrefix):
                    return new byte[length].ToBuffer();
                case (VimConstants.FloatColumnNameTypePrefix):
                    return new float[length].ToBuffer();
                case (VimConstants.DoubleColumnNameTypePrefix):
                    return new double[length].ToBuffer();
                default:
                    throw new Exception($"{nameof(CreateDefaultDataColumnBuffer)} - {UnknownNamedBufferPrefix}");
            }
        }

        public static IBuffer CopyDataColumn(this IBuffer dataColumn, string typePrefix, List<int> remapping = null)
        {
            switch (typePrefix)
            {
                case (VimConstants.IntColumnNameTypePrefix):
                    return (dataColumn.Data as int[]).RemapData(remapping).ToBuffer();
                case (VimConstants.LongColumnNameTypePrefix):
                    return (dataColumn.Data as long[]).RemapData(remapping).ToBuffer();
                case (VimConstants.DoubleColumnNameTypePrefix):
                    return (dataColumn.Data as double[]).RemapData(remapping).ToBuffer();
                case (VimConstants.FloatColumnNameTypePrefix):
                    return (dataColumn.Data as float[]).RemapData(remapping).ToBuffer();
                case (VimConstants.ByteColumnNameTypePrefix):
                    return (dataColumn.Data as byte[]).RemapData(remapping).ToBuffer();
                default:
                    throw new Exception($"{nameof(CopyDataColumn)} - {UnknownNamedBufferPrefix}");
            }
        }

        public static INamedBuffer CopyDataColumn(this INamedBuffer dataColumn, List<int> remapping = null)
        {
            var typePrefix = SerializableEntityTable.GetTypeFromName(dataColumn.Name);
            return new NamedBuffer(dataColumn.CopyDataColumn(typePrefix, remapping), dataColumn.Name);
        }

        public static IBuffer Concat<T>(this IBuffer thisBuffer, IBuffer otherBuffer) where T : unmanaged
            => thisBuffer.AsArray<T>().Concat(otherBuffer.AsArray<T>()).ToArray().ToBuffer();

        public static IBuffer ConcatDataColumnBuffers(this IBuffer thisBuffer, IBuffer otherBuffer, string typePrefix)
        {
            switch (typePrefix)
            {
                case (VimConstants.IntColumnNameTypePrefix):
                    return thisBuffer.Concat<int>(otherBuffer);
                case (VimConstants.LongColumnNameTypePrefix):
                    return thisBuffer.Concat<long>(otherBuffer);
                case (VimConstants.ByteColumnNameTypePrefix):
                    return thisBuffer.Concat<byte>(otherBuffer);
                case (VimConstants.FloatColumnNameTypePrefix):
                    return thisBuffer.Concat<float>(otherBuffer);
                case (VimConstants.DoubleColumnNameTypePrefix):
                    return thisBuffer.Concat<double>(otherBuffer);
                default:
                    throw new Exception($"{nameof(ConcatDataColumnBuffers)} - {UnknownNamedBufferPrefix}");
            }
        }

        public static INamedBuffer ConcatDataColumns(this INamedBuffer thisColumn, INamedBuffer otherColumn)
        {
            var typePrefix = thisColumn.ValidateCanConcatBuffers(otherColumn);
            var combinedBuffer = thisColumn.ConcatDataColumnBuffers(otherColumn, typePrefix);
            return new NamedBuffer(combinedBuffer, thisColumn.Name);
        }

        public static List<T> ConcatColumns<T>(
            this IReadOnlyList<T> thisColumnList,
            IReadOnlyList<T> otherColumnList,
            Func<T, T, T> concatFunc) where T : INamedBuffer
        {
            var mergedColumns = new List<T>();

            foreach (var thisColumn in thisColumnList)
            {
                var otherColumn = otherColumnList.FirstOrDefault(c => c.Name == thisColumn.Name);
                if (otherColumn == null)
                    continue;

                var newNamedBuffer = concatFunc(thisColumn, otherColumn);

                mergedColumns.Add(newNamedBuffer);
            }

            return mergedColumns;
        }

        public static List<INamedBuffer> ConcatDataColumns(this IReadOnlyList<INamedBuffer> thisColumnList, IReadOnlyList<INamedBuffer> otherColumnList)
            => thisColumnList.ConcatColumns(otherColumnList,
                (a, b) => a.ConcatDataColumns(b));

        public static List<NamedBuffer<int>> ConcatIntColumns(this IReadOnlyList<NamedBuffer<int>> thisColumnList, IReadOnlyList<NamedBuffer<int>> otherColumnList)
            => thisColumnList.ConcatColumns(otherColumnList, 
                (a, b) => new NamedBuffer<int>(a.GetTypedData().Concat(b.GetTypedData()).ToArray(), a.Name));

        /// <summary>
        /// Replaces the entity table contained in the document with the given entity table if it is not null.
        /// </summary>
        public static void ReplaceEntityTable(this SerializableDocument document, SerializableEntityTable et)
        {
            if (document == null || et == null)
                return;

            document.EntityTables = document.EntityTables.Where(e => e.Name != et.Name).Append(et).ToList();
        }
    }
}
