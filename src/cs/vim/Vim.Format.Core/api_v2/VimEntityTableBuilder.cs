using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public interface IReadOnlyVimEntityTableBuilder
    {
        string Name { get; }
        int RowCount { get; }
        IReadOnlyDictionary<string, IBuffer> DataColumns { get; }
        IReadOnlyDictionary<string, IReadOnlyList<int>> IndexColumns { get; }
        IReadOnlyDictionary<string, IReadOnlyList<string>> StringColumns { get; }
    }

    public class VimEntityTableBuilder : IReadOnlyVimEntityTableBuilder
    {
        public string Name { get; }
        public Dictionary<string, IBuffer> DataColumns { get; } = new Dictionary<string, IBuffer>();
        IReadOnlyDictionary<string, IBuffer> IReadOnlyVimEntityTableBuilder.DataColumns => DataColumns;

        public Dictionary<string, int[]> IndexColumns { get; } = new Dictionary<string, int[]>();
        IReadOnlyDictionary<string, IReadOnlyList<int>> IReadOnlyVimEntityTableBuilder.IndexColumns
            => IndexColumns.ToDictionary(kv => kv.Key, kv => {
                var result = kv.Value as IReadOnlyList<int>;
                Debug.Assert(result != null, "Invalid readonly index column cast");
                return result;
            });

        public Dictionary<string, string[]> StringColumns { get; } = new Dictionary<string, string[]>();
        IReadOnlyDictionary<string, IReadOnlyList<string>>IReadOnlyVimEntityTableBuilder.StringColumns
            => StringColumns.ToDictionary(kv => kv.Key, kv => {
                var result = kv.Value as IReadOnlyList<string>;
                Debug.Assert(result != null, "Invalid readonly string column cast");
                return result;
            });

        public int RowCount { get; private set; }

        public VimEntityTableBuilder(string name)
            => Name = name;

        public VimEntityTableBuilder(VimEntityTableData tableData, string[] stringTable)
        {
            Name = tableData.Name;
            RowCount = tableData.GetRowCount();

            foreach (var column in tableData.IndexColumns)
                IndexColumns[column.Name] = column.AsArray<int>();

            foreach (var column in tableData.StringColumns)
                StringColumns[column.Name] = column.AsArray<int>().Select(i => stringTable.ElementAtOrDefault(i, "")).ToArray();

            foreach (var column in tableData.DataColumns)
                DataColumns[column.Name] = column;
        }

        public VimEntityTableBuilder UpdateOrValidateRows(int n)
        {
            if (RowCount == 0) RowCount = n;
            else if (RowCount != n) throw new Exception($"Value count {n} does not match the expected number of rows {RowCount}");
            return this;
        }

        public void ValidateHasDataColumnPrefix(string columnName)
        {
            if (!ColumnExtensions.IsDataColumnName(columnName))
                throw new Exception($"{nameof(columnName)} {columnName} does not begin with a data column prefix");
        }

        public void ValidateHasPrefix(string columnName, string expectedPrefix)
        {
            if (!columnName.StartsWith(expectedPrefix))
                throw new Exception($"{nameof(columnName)} {columnName} must start with {expectedPrefix}");
        }

        public VimEntityTableBuilder AddIndexColumn(string columnName, int[] indices)
        {
            ValidateHasPrefix(columnName, VimConstants.IndexColumnNameTypePrefix);
            UpdateOrValidateRows(indices.Length);
            IndexColumns.Add(columnName, indices);
            return this;
        }

        public VimEntityTableBuilder AddIndexColumn(string columnName, IEnumerable<int> ids)
            => AddIndexColumn(columnName, ids.ToArray());

        public VimEntityTableBuilder AddStringColumn(string columnName, string[] values)
        {
            ValidateHasPrefix(columnName, VimConstants.StringColumnNameTypePrefix);
            UpdateOrValidateRows(values.Length);
            StringColumns.Add(columnName, values);
            return this;
        }

        public VimEntityTableBuilder AddStringColumn(string columnName, IEnumerable<string> values)
            => AddStringColumn(columnName, values.ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IBuffer values)
        {
            ValidateHasDataColumnPrefix(columnName);
            UpdateOrValidateRows(values.Data.Length);
            DataColumns.Add(columnName, values);
            return this;
        }

        public VimEntityTableBuilder AddDataColumn<T>(string columnName, T[] values) where T : unmanaged
        {
            ValidateHasPrefix(columnName, typeof(T).GetDataColumnNameTypePrefix());
            return AddDataColumn(columnName, values.ToBuffer());
        }

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<double> values)
            => AddDataColumn(columnName, values.ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<float> values)
            => AddDataColumn(columnName, values.ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<int> values)
            => AddDataColumn(columnName, values.ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<long> values)
            => AddDataColumn(columnName, values.ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<short> values)
            => AddDataColumn(columnName, values.Select(x => (int)x).ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<byte> values)
            => AddDataColumn(columnName, values.ToArray());

        public VimEntityTableBuilder AddDataColumn(string columnName, IEnumerable<bool> values)
            => AddDataColumn(columnName, values.Select(x => x ? (byte)1 : (byte)0).ToArray());

        public IEnumerable<string> GetAllStrings()
            => StringColumns.Values.SelectMany(sc => sc)
            .Where(x => x != null);

        public void Clear()
        {
            RowCount = 0;
            DataColumns.Clear();
            StringColumns.Clear();
            IndexColumns.Clear();
        }
    }
}
