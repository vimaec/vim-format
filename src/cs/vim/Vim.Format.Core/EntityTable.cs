using System;
using Vim.BFast;
using Vim.LinqArray;

namespace Vim.Format
{
    public class EntityTable
    {
        public EntityTable(Document document, SerializableEntityTable entityTable)
        {
            Document = document;
            _EntityTable = entityTable;
            Name = _EntityTable.Name;

            DataColumns = _EntityTable.DataColumns.ToLookup(c => c.Name, c => c);
            IndexColumns = _EntityTable.IndexColumns.ToLookup(c => c.Name, c => c);
            StringColumns = _EntityTable.StringColumns.ToLookup(c => c.Name, c => c);
            NumRows = Columns.FirstOrDefault()?.NumElements() ?? 0;
        }

        private SerializableEntityTable _EntityTable { get; }
        public Document Document { get; }
        public string Name { get; }
        public int NumRows { get; }
        public ILookup<string, INamedBuffer> DataColumns { get; }
        public ILookup<string, NamedBuffer<int>> StringColumns { get; }
        public ILookup<string, NamedBuffer<int>> IndexColumns { get; }
        public IArray<INamedBuffer> Columns
            => DataColumns.Values
                .Concatenate(IndexColumns.Values.Select(x => (INamedBuffer)x))
                .Concatenate(StringColumns.Values.Select(x => (INamedBuffer)x));

        public IArray<int> GetIndexColumnValues(string columnName)
            => IndexColumns.GetOrDefault(columnName)?.GetColumnValues<int>().ToIArray();

        public IArray<string> GetStringColumnValues(string columnName)
            => StringColumns.GetOrDefault(columnName)
                ?.GetColumnValues<int>()
                ?.Select(Document.GetString)
                .ToIArray();

        public IArray<T> GetDataColumnValues<T>(string columnName) where T : unmanaged
        {
            var type = typeof(T);

            if (!ColumnExtensions.DataColumnTypes.Contains(type))
                throw new Exception($"{nameof(GetDataColumnValues)} error - unsupported data column type {type}");

            var namedBuffer = DataColumns.GetOrDefault(columnName);
            if (namedBuffer == null)
                return null;

            if (type == typeof(short))
                return namedBuffer.GetColumnValues<int>().Select(i => (short)i).ToIArray() as IArray<T>;

            if (type == typeof(bool))
                return namedBuffer.GetColumnValues<byte>().Select(b => b != 0).ToIArray() as IArray<T>;

            return namedBuffer.GetColumnValues<T>().ToIArray();
        }
    }
}
