using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;

namespace Vim.Format
{
    public class EntityTableBuilder
    {
        public string Name { get; }
        public readonly Dictionary<string, IBuffer> DataColumns = new Dictionary<string, IBuffer>();
        public readonly Dictionary<string, int[]> IndexColumns = new Dictionary<string, int[]>();
        public readonly Dictionary<string, string[]> StringColumns = new Dictionary<string, string[]>();

        public int NumRows { get; private set; }

        public EntityTableBuilder(string name)
            => Name = name;

        public EntityTableBuilder UpdateOrValidateRows(int n)
        {
            if (NumRows == 0) NumRows = n;
            else if (NumRows != n) throw new Exception($"Value count {n} does not match the expected number of rows {NumRows}");
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

        public EntityTableBuilder AddIndexColumn(string columnName, int[] indices)
        {
            ValidateHasPrefix(columnName, VimConstants.IndexColumnNameTypePrefix);
            UpdateOrValidateRows(indices.Length);
            IndexColumns.Add(columnName, indices);
            return this;
        }

        public EntityTableBuilder AddIndexColumn(string columnName, IEnumerable<int> ids)
            => AddIndexColumn(columnName, ids.ToArray());

        public EntityTableBuilder AddStringColumn(string columnName, string[] values)
        {
            ValidateHasPrefix(columnName, VimConstants.StringColumnNameTypePrefix);
            UpdateOrValidateRows(values.Length);
            StringColumns.Add(columnName, values);
            return this;
        }

        public EntityTableBuilder AddStringColumn(string columnName, IEnumerable<string> values)
            => AddStringColumn(columnName, values.ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IBuffer values)
        {
            ValidateHasDataColumnPrefix(columnName);
            UpdateOrValidateRows(values.Data.Length);
            DataColumns.Add(columnName, values);
            return this;
        }

        public EntityTableBuilder AddDataColumn<T>(string columnName, T[] values) where T : unmanaged
        {
            ValidateHasPrefix(columnName, typeof(T).GetDataColumnNameTypePrefix());
            return AddDataColumn(columnName, values.ToBuffer());
        }

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<double> values)
            => AddDataColumn(columnName, values.ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<float> values)
            => AddDataColumn(columnName, values.ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<int> values)
            => AddDataColumn(columnName, values.ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<long> values)
            => AddDataColumn(columnName, values.ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<short> values)
            => AddDataColumn(columnName, values.Select(x => (int)x).ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<byte> values)
            => AddDataColumn(columnName, values.ToArray());

        public EntityTableBuilder AddDataColumn(string columnName, IEnumerable<bool> values)
            => AddDataColumn(columnName, values.Select(x => x ? (byte)1 : (byte)0).ToArray());

        public IEnumerable<string> GetAllStrings()
            => StringColumns.Values.SelectMany(sc => sc)
            .Where(x => x != null);

        public void Clear()
        {
            NumRows = 0;
            DataColumns.Clear();
            StringColumns.Clear();
            IndexColumns.Clear();
        }
    }
}
