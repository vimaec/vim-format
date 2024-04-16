using System;
using Vim.LinqArray;
using Vim.BFastLib;
using System.Linq;
using System.Collections.Generic;

namespace Vim.Format
{
    public class EntityTable
    {
        public EntityTable(Document document, SerializableEntityTable entityTable)
        {
            Document = document;
            _EntityTable = entityTable;
            Name = _EntityTable.Name;

            DataColumns = LinqArray.LinqArray.ToLookup(_EntityTable.DataColumns, c => c.Name, c => c);
            IndexColumns = LinqArray.LinqArray.ToLookup(_EntityTable.IndexColumns, c => c.Name, c => c);
            StringColumns = LinqArray.LinqArray.ToLookup(_EntityTable.StringColumns, c => c.Name, c => c);
            NumRows = Columns.FirstOrDefault()?.NumElements() ?? 0;

            Columns.ValidateColumnRowsAreAligned();
        }

        private SerializableEntityTable _EntityTable { get; }
        public Document Document { get; }
        public string Name { get; }
        public int NumRows { get; }
        public LinqArray.ILookup<string, INamedBuffer> DataColumns { get; }
        public LinqArray.ILookup<string, NamedBuffer<int>> StringColumns { get; }
        public LinqArray.ILookup<string, NamedBuffer<int>> IndexColumns { get; }
        public IEnumerable<INamedBuffer> Columns
            => DataColumns.Values
                .Concat(IndexColumns.Values.Select(x => (INamedBuffer)x))
                .Concat(StringColumns.Values.Select(x => (INamedBuffer)x));

        public int[] GetIndexColumnValues(string columnName)
            => IndexColumns.GetOrDefault(columnName)?.GetColumnValues<int>();

        public string[] GetStringColumnValues(string columnName)
            => StringColumns.GetOrDefault(columnName)
                ?.GetColumnValues<int>()
                ?.Select(Document.GetString).ToArray();

        public T[] GetDataColumnValues<T>(string columnName) where T : unmanaged
        {
            var type = typeof(T);

            if (!ColumnExtensions.DataColumnTypes.Contains(type))
                throw new Exception($"{nameof(GetDataColumnValues)} error - unsupported data column type {type}");

            var namedBuffer = DataColumns.GetOrDefault(columnName);
            if (namedBuffer == null)
                return null;

            if (type == typeof(short))
                return namedBuffer.GetColumnValues<int>().Select(i => (short)i) as T[];

            if (type == typeof(bool))
                return namedBuffer.GetColumnValues<byte>().Select(b => b != 0) as T[];
            return namedBuffer.GetColumnValues<T>();
        }
    }
}
