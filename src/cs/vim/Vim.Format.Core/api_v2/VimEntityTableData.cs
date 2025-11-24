using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// Represents the serializable data buffers which define an entity table.
    /// </summary>
    public class VimEntityTableData
    {
        /// <summary>
        /// The name of the entity table.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The relational index columns of the entity table.
        /// </summary>
        public List<NamedBuffer<int>> IndexColumns { get; set; } = new List<NamedBuffer<int>>();

        /// <summary>
        /// The string columns of the entity table.
        /// </summary>
        public List<NamedBuffer<int>> StringColumns { get; set; } = new List<NamedBuffer<int>>();

        /// <summary>
        /// The data columns of the entity table.
        /// </summary>
        public List<INamedBuffer> DataColumns { get; set; } = new List<INamedBuffer>();

        /// <summary>
        /// A delegate which filters entity tables by name.
        /// </summary>
        public delegate bool EntityTableFilter(string entityTableName);

        /// <summary>
        /// A delegate which filters entity table columns.
        /// </summary>
        public delegate bool EntityTableColumnFilter(string entityTableName, string columnName);

        /// <summary>
        /// Default constructor
        /// </summary>
        public VimEntityTableData()
        { }

        /// <summary>
        /// Constructor. Loads data from an entity table buffer reader.
        /// </summary>
        public VimEntityTableData(
            BFastBufferReader entityTableBufferReader,
            bool schemaOnly,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            Name = entityTableBufferReader.Name;

            foreach (var colBr in entityTableBufferReader.Seek().GetBFastBufferReaders())
            {
                var columnName = colBr.Name;

                if (!VimEntityTableColumnName.TryParseColumnTypePrefix(columnName, out var columnTypePrefix))
                    continue;

                if (entityTableColumnFilter != null && !entityTableColumnFilter(Name, columnName))
                    continue;

                switch (columnTypePrefix)
                {
                    case VimEntityTableColumnName.IndexColumnNameTypePrefix:
                        {
                            IndexColumns.Add(ReadEntityTableColumn<int>(colBr, schemaOnly));
                            break;
                        }
                    case VimEntityTableColumnName.StringColumnNameTypePrefix:
                        {
                            StringColumns.Add(ReadEntityTableColumn<int>(colBr, schemaOnly));
                            break;
                        }
                    case VimEntityTableColumnName.IntColumnNameTypePrefix:
                        {
                            DataColumns.Add(ReadEntityTableColumn<int>(colBr, schemaOnly));
                            break;
                        }
                    case VimEntityTableColumnName.LongColumnNameTypePrefix:
                        {
                            DataColumns.Add(ReadEntityTableColumn<long>(colBr, schemaOnly));
                            break;
                        }
                    case VimEntityTableColumnName.DoubleColumnNameTypePrefix:
                        {
                            DataColumns.Add(ReadEntityTableColumn<double>(colBr, schemaOnly));
                            break;
                        }
                    case VimEntityTableColumnName.FloatColumnNameTypePrefix:
                        {
                            DataColumns.Add(ReadEntityTableColumn<float>(colBr, schemaOnly));
                            break;
                        }
                    case VimEntityTableColumnName.ByteColumnNameTypePrefix:
                        {
                            DataColumns.Add(ReadEntityTableColumn<byte>(colBr, schemaOnly));
                            break;
                        }
                    default:
                        // For flexibility, we ignore the columns which do not contain a recognized prefix.
                        break;
                }
            }
        }

        /// <summary>
        /// Returns a NamedBuffer representing a entity table column.
        /// If schemaOnly is enabled, the column is returned without any of its contained data;
        /// this is useful for rapidly querying the schema of the table.
        /// </summary>
        private static NamedBuffer<T> ReadEntityTableColumn<T>(
            BFastBufferReader columnBufferReader, bool schemaOnly) where T : unmanaged
        {
            var (name, size) = columnBufferReader;

            if (schemaOnly)
                return new Buffer<T>(Array.Empty<T>()).ToNamedBuffer(name);

            return columnBufferReader
                .Seek()
                .ReadBufferFromNumberOfBytes<T>(size)
                .ToNamedBuffer(name);
        }

        /// <summary>
        /// Returns all the columns as an array of named buffers.
        /// </summary>
        public INamedBuffer[] GetAllColumns()
            => DataColumns.Concat(IndexColumns).Concat(StringColumns).ToArray();

        /// <summary>
        /// Returns the column names contained in the entity table.
        /// </summary>
        public IEnumerable<string> ColumnNames
            => IndexColumns.Select(c => c.Name)
                .Concat(StringColumns.Select(c => c.Name))
                .Concat(DataColumns.Select(c => c.Name));

        /// <summary>
        /// Causes an assertion error in debug mode if the number of rows is not consistent among the columns. 
        /// </summary>
        public INamedBuffer[] AssertColumnRowsAreAligned()
            => AssertColumnRowsAreAligned(GetAllColumns());

        /// <summary>
        /// Causes an assertion error in debug mode if the number of rows is not consistent among the columns. 
        /// </summary>
        public static INamedBuffer[] AssertColumnRowsAreAligned(INamedBuffer[] columns)
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

            return columns;
        }

        /// <summary>
        /// Returns the number of rows in the entity table.
        /// </summary>
        public int GetRowCount()
            => AssertColumnRowsAreAligned().FirstOrDefault()?.NumElements() ?? 0;

        /// <summary>
        /// Enumerates the VimEntityTables contained in the given VIM file.
        /// </summary>
        public static IEnumerable<VimEntityTableData> EnumerateEntityTables(
            FileInfo vimFileInfo,
            bool schemaOnly,
            EntityTableFilter entityTableNameFilter = null,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            using (var fileStream = vimFileInfo.OpenRead())
            {
                foreach (var entityTable in EnumerateEntityTables(fileStream, schemaOnly, entityTableNameFilter, entityTableColumnFilter))
                {
                    yield return entityTable;
                }
            }
        }

        /// <summary>
        /// Enumerates the VimEntityTables contained in the given VIM stream.
        /// </summary>
        public static IEnumerable<VimEntityTableData> EnumerateEntityTables(
            Stream vimStream,
            bool schemaOnly,
            EntityTableFilter entityTableNameFilter = null,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            vimStream.ThrowIfNotSeekable("Could not enumerate entity tables.");

            var entitiesBufferReader = vimStream.GetBFastBufferReader(BufferNames.Entities);
            if (entitiesBufferReader == null)
                yield break;

            foreach (var entityTable in EnumerateEntityTables(entitiesBufferReader, schemaOnly, entityTableNameFilter, entityTableColumnFilter))
            {
                yield return entityTable;
            }
        }

        /// <summary>
        /// Enumerates the VimEntityTables contained in the given buffer.
        /// </summary>
        public static IEnumerable<VimEntityTableData> EnumerateEntityTables(
            BFastBufferReader entitiesBufferReader,
            bool schemaOnly,
            EntityTableFilter entityTableNameFilter = null,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            var entityTableBufferReaders = entitiesBufferReader.Seek()
                .GetBFastBufferReaders(br => entityTableNameFilter?.Invoke(br.Name) ?? true);

            foreach (var entityTableBufferReader in entityTableBufferReaders)
            {
                yield return new VimEntityTableData(entityTableBufferReader, schemaOnly, entityTableColumnFilter);
            }
        }

        /// <summary>
        /// Returns true along with the buffer, type prefix, and column type based on the given column field name, if it matches 
        /// any of the columns in the data.
        /// </summary>
        public bool TryGetColumnByFieldName(
            string columnFieldName,
            out INamedBuffer buffer,
            out string typePrefix,
            out VimEntityTableColumnType columnType)
        {
            buffer = null;
            columnType = VimEntityTableColumnType.IndexColumn;
            typePrefix = null;

            foreach (var column in GetAllColumns())
            {
                if (!VimEntityTableColumnName.TryParseVimEntityTableColumnName(column.Name, out var components))
                    continue;

                if (components.FieldName != columnFieldName)
                    continue;

                columnType = components.ColumnType;
                typePrefix = components.TypePrefix;
                buffer = column;

                return true;
            }

            return false;
        }

        public static VimEntityTableData Concat(
            VimEntityTableData thisTable,
            VimEntityTableData otherTable)
        {
            var concatenated = new VimEntityTableData
            {
                Name = thisTable.Name,
                IndexColumns = VimEntityTableColumnActions.ConcatIntColumns(
                    thisTable.IndexColumns, otherTable.IndexColumns),
                StringColumns = VimEntityTableColumnActions.ConcatIntColumns(
                    thisTable.StringColumns, otherTable.StringColumns),
                DataColumns = VimEntityTableColumnActions.ConcatDataColumns(
                    thisTable.DataColumns, otherTable.DataColumns),
            };

            concatenated.AssertColumnRowsAreAligned();

            return concatenated;
        }
    }
}