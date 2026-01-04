using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;

namespace Vim.Format
{
    public static class VimEntityTableColumnActions
    {
        public const string UnknownNamedBufferPrefix = "Unknown NamedBuffer prefix";

        public static IBuffer RemapOrSelfDataColumn(IBuffer dataColumn, string typePrefix, IReadOnlyList<int> remapping = null)
        {
            switch (typePrefix)
            {
                case VimEntityTableColumnName.IntColumnNameTypePrefix:
                    return new Buffer<int>(RemapOrSelf(dataColumn.Data as int[], remapping));
                case VimEntityTableColumnName.LongColumnNameTypePrefix:
                    return new Buffer<long>(RemapOrSelf(dataColumn.Data as long[], remapping));
                case VimEntityTableColumnName.DoubleColumnNameTypePrefix:
                    return new Buffer<double>(RemapOrSelf(dataColumn.Data as double[], remapping));
                case VimEntityTableColumnName.FloatColumnNameTypePrefix:
                    return new Buffer<float>(RemapOrSelf(dataColumn.Data as float[], remapping));
                case VimEntityTableColumnName.ByteColumnNameTypePrefix:
                    return new Buffer<byte>(RemapOrSelf(dataColumn.Data as byte[], remapping));
                default:
                    throw new ArgumentException($"{nameof(RemapOrSelfDataColumn)} - {UnknownNamedBufferPrefix}: {typePrefix}");
            }
        }

        public static INamedBuffer RemapOrSelfDataColumn(INamedBuffer dataColumn, List<int> remapping = null)
        {
            if (!VimEntityTableColumnName.TryParseColumnTypePrefix(dataColumn.Name, out var typePrefix))
                return null;

            return new NamedBuffer(RemapOrSelfDataColumn(dataColumn, typePrefix, remapping), dataColumn.Name);
        }

        public static T[] RemapOrSelf<T>(T[] source, IReadOnlyList<int> remapping = null)
        {
            if (remapping == null)
                return source;

            var dst = new T[remapping.Count];

            for (var i = 0; i < dst.Length; ++i)
            {
                var remappedIndex = remapping[i];
                dst[i] = source[remappedIndex];
            }

            return dst;
        }

        public static IBuffer CreateDefaultDataColumnBuffer(int length, string typePrefix)
        {
            switch (typePrefix)
            {
                case VimEntityTableColumnName.IntColumnNameTypePrefix:
                    return new Buffer<int>(new int[length]);
                case VimEntityTableColumnName.LongColumnNameTypePrefix:
                    return new Buffer<long>(new long[length]);
                case VimEntityTableColumnName.ByteColumnNameTypePrefix:
                    return new Buffer<byte>(new byte[length]);
                case VimEntityTableColumnName.FloatColumnNameTypePrefix:
                    return new Buffer<float>(new float[length]);
                case VimEntityTableColumnName.DoubleColumnNameTypePrefix:
                    return new Buffer<double>(new double[length]);
                default:
                    throw new ArgumentException($"{nameof(CreateDefaultDataColumnBuffer)} - {UnknownNamedBufferPrefix}: {typePrefix}");
            }
        }

        public static IBuffer Concat<T>(IBuffer thisBuffer, IBuffer otherBuffer) where T : unmanaged
        {
            var arrayA = thisBuffer.AsArray<T>();
            var arrayB = otherBuffer.AsArray<T>();
            var result = new T[arrayA.Length + arrayB.Length];

            for (var i = 0; i < arrayA.Length; ++i)
            {
                result[i] = arrayA[i];
            }

            var a = arrayA.Length;
            for (var i = 0; i < arrayB.Length; ++i)
            {
                result[a + i] = arrayB[i];
            }

            return result.ToBuffer();
        }

        public static IBuffer ConcatDataColumnBuffers(IBuffer thisBuffer, IBuffer otherBuffer, string typePrefix)
        {
            switch (typePrefix)
            {
                case VimEntityTableColumnName.IntColumnNameTypePrefix:
                    return Concat<int>(thisBuffer, otherBuffer);
                case VimEntityTableColumnName.LongColumnNameTypePrefix:
                    return Concat<long>(thisBuffer, otherBuffer);
                case VimEntityTableColumnName.ByteColumnNameTypePrefix:
                    return Concat<byte>(thisBuffer, otherBuffer);
                case VimEntityTableColumnName.FloatColumnNameTypePrefix:
                    return Concat<float>(thisBuffer, otherBuffer);
                case VimEntityTableColumnName.DoubleColumnNameTypePrefix:
                    return Concat<double>(thisBuffer, otherBuffer);
                default:
                    throw new ArgumentException($"{nameof(ConcatDataColumnBuffers)} - {UnknownNamedBufferPrefix}: {typePrefix}");
            }
        }
        
        public static string ValidateCanConcatBuffers(INamedBuffer thisBuffer, INamedBuffer otherBuffer)
        {
            if (!VimEntityTableColumnName.TryParseColumnTypePrefix(thisBuffer.Name, out var thisPrefix))
                throw new Exception("NamedBuffer prefix not found");

            if (!VimEntityTableColumnName.TryParseColumnTypePrefix(otherBuffer.Name, out var otherPrefix))
                throw new Exception("NamedBuffer prefix not found");

            if (thisPrefix != otherPrefix)
                throw new Exception($"NamedBuffer prefixes are not equal: {thisPrefix} <> {otherPrefix}");

            return thisPrefix;
        }

        public static INamedBuffer ConcatDataColumns(INamedBuffer thisColumn, INamedBuffer otherColumn)
        {
            var typePrefix = ValidateCanConcatBuffers(thisColumn, otherColumn);
            var combinedBuffer = ConcatDataColumnBuffers(thisColumn, otherColumn, typePrefix);
            return new NamedBuffer(combinedBuffer, thisColumn.Name);
        }

        public static List<INamedBuffer> ConcatDataColumns(
            IReadOnlyList<INamedBuffer> thisColumnList,
            IReadOnlyList<INamedBuffer> otherColumnList)
            => ConcatColumns(
                thisColumnList,
                otherColumnList,
                (a, b) => ConcatDataColumns(a, b));

        public static List<NamedBuffer<int>> ConcatIntColumns(
            IReadOnlyList<NamedBuffer<int>> thisColumnList,
            IReadOnlyList<NamedBuffer<int>> otherColumnList)
            => ConcatColumns(thisColumnList, otherColumnList, 
                (a, b) => new NamedBuffer<int>(a.GetTypedData().Concat(b.GetTypedData()).ToArray(), a.Name));

        public static List<T> ConcatColumns<T>(
            IReadOnlyList<T> thisColumnList,
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

        public static T[] Copy<T>(T[] source, List<int> remapping = null)
        {
            T[] result;

            if (remapping == null)
            {
                // Copy from the source.
                result = new T[source.Length];

                for (var i = 0; i < result.Length; ++i)
                    result[i] = source[i];
            }
            else
            {
                // Copy from the remapping.
                result = new T[remapping.Count];

                for (var i = 0; i < result.Length; ++i)
                {
                    var remappedIndex = remapping[i];
                    result[i] = source[remappedIndex];
                }
            }

            return result;
        }
    }
}