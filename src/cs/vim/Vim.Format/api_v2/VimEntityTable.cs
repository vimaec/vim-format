using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Vim.BFast;

namespace Vim.Format.api_v2
{
    public class VimEntityTable
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
        /// A delegate which filters entity table columns.
        /// </summary>
        public delegate bool EntityTableColumnFilter(string entityTableName, string columnName);

        /// <summary>
        /// Constructor
        /// </summary>
        public VimEntityTable(
            BFastBufferReader entityTableBufferReader,
            bool schemaOnly,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            Name = entityTableBufferReader.Name;

            foreach (var colBr in entityTableBufferReader.Seek().GetBFastBufferReaders())
            {
                var name = colBr.Name;
                var typePrefix = name.GetTypePrefix();

                if (entityTableColumnFilter != null && !entityTableColumnFilter(Name, name))
                    continue;

                switch (typePrefix)
                {
                    case VimConstants.IndexColumnNameTypePrefix:
                        {
                            IndexColumns.Add(colBr.ReadEntityTableColumn<int>(schemaOnly));
                            break;
                        }
                    case VimConstants.StringColumnNameTypePrefix:
                        {
                            StringColumns.Add(colBr.ReadEntityTableColumn<int>(schemaOnly));
                            break;
                        }
                    case VimConstants.IntColumnNameTypePrefix:
                        {
                            DataColumns.Add(colBr.ReadEntityTableColumn<int>(schemaOnly));
                            break;
                        }
                    case VimConstants.LongColumnNameTypePrefix:
                        {
                            DataColumns.Add(colBr.ReadEntityTableColumn<long>(schemaOnly));
                            break;
                        }
                    case VimConstants.DoubleColumnNameTypePrefix:
                        {
                            DataColumns.Add(colBr.ReadEntityTableColumn<double>(schemaOnly));
                            break;
                        }
                    case VimConstants.FloatColumnNameTypePrefix:
                        {
                            DataColumns.Add(colBr.ReadEntityTableColumn<float>(schemaOnly));
                            break;
                        }
                    case VimConstants.ByteColumnNameTypePrefix:
                        {
                            DataColumns.Add(colBr.ReadEntityTableColumn<byte>(schemaOnly));
                            break;
                        }
                        // For flexibility, we ignore the columns which do not contain a recognized prefix.
                }
            }
        }

        /// <summary>
        /// Returns the column names contained in the entity table.
        /// </summary>
        public IEnumerable<string> ColumnNames
            => IndexColumns.Select(c => c.Name)
                .Concat(StringColumns.Select(c => c.Name))
                .Concat(DataColumns.Select(c => c.Name));

        /// <summary>
        /// Returns the columns as named buffers.
        /// </summary>
        public List<INamedBuffer> ToBuffers()
        {
            var r = new List<INamedBuffer>();

            r.AddRange(DataColumns);
            r.AddRange(IndexColumns);
            r.AddRange(StringColumns);

            return r;
        }

        /// <summary>
        /// Returns a BFastBuilder used to serialize the given collection of entity tables.
        /// </summary>
        public static BFastBuilder GetBFastBuilder(IEnumerable<VimEntityTable> entityTables)
        {
            var bldr = new BFastBuilder();
            foreach (var et in entityTables)
            {
                bldr.Add(et.Name, et.ToBuffers());
            }
            return bldr;
        }

        /// <summary>
        /// Enumerates the VimEntityTables contained in the given VIM file.
        /// </summary>
        public static IEnumerable<VimEntityTable> EnumerateEntityTables(
            FileInfo vimFileInfo,
            bool schemaOnly,
            Func<string, bool> entityTableNameFilterFunc = null,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            using (var stream = vimFileInfo.OpenRead())
            {
                var entitiesBufferReader = stream.GetBFastBufferReader(BufferNames.Entities);
                if (entitiesBufferReader == null)
                    yield break;

                foreach (var entityTable in EnumerateEntityTables(entitiesBufferReader, schemaOnly, entityTableNameFilterFunc, entityTableColumnFilter))
                {
                    yield return entityTable;
                }
            }
        }

        /// <summary>
        /// Enumerates the VimEntityTables contained in the given buffer.
        /// </summary>
        public static IEnumerable<VimEntityTable> EnumerateEntityTables(
            BFastBufferReader entitiesBufferReader,
            bool schemaOnly,
            Func<string, bool> entityTableNameFilterFunc = null,
            EntityTableColumnFilter entityTableColumnFilter = null)
        {
            var entityTableBufferReaders = entitiesBufferReader.Seek()
                .GetBFastBufferReaders(br => entityTableNameFilterFunc?.Invoke(br.Name) ?? true);
            
            foreach (var entityTableBufferReader in entityTableBufferReaders)
            {
                yield return new VimEntityTable(entityTableBufferReader, schemaOnly, entityTableColumnFilter);
            }
        }

        public static readonly Regex TypePrefixRegex = new Regex(@"(\w+:).*");

        public static string GetTypePrefix(string name)
        {
            var match = TypePrefixRegex.Match(name);
            return match.Success ? match.Groups[1].Value : "";
        }

        public static string GetTypePrefix(INamedBuffer namedBuffer)
            => namedBuffer.Name.GetTypePrefix();

        /// <summary>
        /// Returns a NamedBuffer representing a entity table column.
        /// If schemaOnly is enabled, the column is returned without any of its contained data;
        /// this is useful for rapidly querying the schema of the table.
        /// </summary>
        public static NamedBuffer<T> ReadEntityTableColumn<T>(
            BFastBufferReader columnBufferReader,
            bool schemaOnly) where T : unmanaged
        {
            var (name, size) = columnBufferReader;

            if (schemaOnly)
                return new Buffer<T>(Array.Empty<T>()).ToNamedBuffer(name);

            return columnBufferReader
                .Seek()
                .ReadBufferFromNumberOfBytes<T>(size)
                .ToNamedBuffer(name);
        }
    }
}