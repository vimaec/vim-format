using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format
{
    public class VimSchema
    {
        public const string TableNameSeparator = "__";

        public readonly SerializableVersion VimFormatVersion;
        public readonly SerializableVersion SchemaVersion;
        public readonly Dictionary<string, VimEntityTableSchema> EntityTableSchemas = new Dictionary<string, VimEntityTableSchema>();

        public VimSchema(SerializableVersion vimFormatVersion, SerializableVersion schemaVersion)
            => (VimFormatVersion, SchemaVersion) = (vimFormatVersion, schemaVersion);

        public VimSchema(VimHeader header)
            : this(header.FileFormatVersion, header.Schema)
        { }

        public IEnumerable<(string TableName, string[] ColumnName)> GetTableNamesAndColumns()
            => EntityTableSchemas.Values
                .Select(ets => (ets.TableName, ets.ColumnNames.OrderBy(n => n).ToArray()))
                .OrderBy(n => n);

        public IEnumerable<(string TableName, string ColumnName)> GetFlattenedTableNamesAndColumnNames()
            => EntityTableSchemas.Values.SelectMany(ets => ets.ColumnNames.Select(c => (ets.TableName, c)));

        public IEnumerable<string> GetAllQualifiedColumnNames()
            => GetFlattenedTableNamesAndColumnNames()
                .Select(t => string.Join(TableNameSeparator, t.TableName, t.ColumnName))
                .OrderBy(x => x);

        public VimEntityTableSchema AddEntityTableSchema(string entityTableName)
        {
            if (EntityTableSchemas.ContainsKey(entityTableName))
                throw new Exception($"Entity Table {entityTableName} already exists in the VIM schema");

            var ets = new VimEntityTableSchema(entityTableName);
            EntityTableSchemas.Add(entityTableName, ets);
            return ets;
        }

        public static VimSchema Create(string filePath)
            => Create(VIM.Open(
                filePath,
                options: new VimOpenOptions() {
                    IncludeAssets = false,
                    IncludeGeometry = false,
                    SchemaOnly = true
                }));

        public static VimSchema Create(VIM vim)
        {
            var vimSchema = new VimSchema(vim.Header);
            foreach (var entityTable in vim.EntityTableData)
            {
                var ets = vimSchema.AddEntityTableSchema(entityTable.Name);

                // Collect all the column names in the entity table and sort them alphabetically.
                foreach (var columnName in entityTable.GetColumns().Select(nb => nb.Name).OrderBy(n => n))
                    ets.AddColumn(columnName);
            }
            return vimSchema;
        }

        public static VimSchema GetCurrentVimSchema()
        {
            var vimSchema = new VimSchema(Format.VimFormatVersion.Current, SchemaVersion.Current);

            foreach (var entityType in ObjectModelReflection.GetEntityTypes())
            {
                var entityTableSchema = vimSchema.AddEntityTableSchema(entityType.GetEntityTableName());

                foreach (var fieldInfo in entityType.GetRelationFields())
                {
                    var (indexColumnName, _) = fieldInfo.GetIndexColumnInfo();
                    entityTableSchema.AddColumn(indexColumnName);
                }

                foreach (var fieldInfo in entityType.GetEntityFields())
                {
                    var loadingInfos = fieldInfo.GetEntityColumnLoadingInfo();

                    foreach (var li in loadingInfos)
                        entityTableSchema.AddColumn(li.EntityColumnAttribute.SerializedValueColumnName);
                }
            }
            return vimSchema;
        }

        public VimSchemaDiff Diff(VimSchema other)
        {
            var aCols = GetAllQualifiedColumnNames().ToArray();
            var bCols = other.GetAllQualifiedColumnNames().ToArray();
            return new VimSchemaDiff(aCols, bCols);
        }

        public static VimSchemaDiff Diff(VimSchema a, VimSchema b)
            => a.Diff(b);

        public bool IsSame(VimSchema other)
        {
            var diff = Diff(other);
            return diff.AddedQualifiedColumnNames.Length == 0 && diff.RemovedQualifiedColumnNames.Length == 0;
        }

        public static bool IsSame(VimSchema a, VimSchema b)
            => a.IsSame(b);

        public bool IsSuperSetOf(VimSchema other)
        {
            var diff = Diff(other);
            return diff.RemovedQualifiedColumnNames.Length == 0;
        }

        public static bool IsSuperSetOf(VimSchema a, VimSchema b)
            => a.IsSuperSetOf(b);
    }

    public class VimSchemaDiff
    {
        public readonly string[] AddedQualifiedColumnNames; // tableName__columnName is present in A but not B
        public readonly string[] RemovedQualifiedColumnNames; // tableName__columnName is present in B but not A

        /// <summary>
        /// Constructor.
        /// </summary>
        public VimSchemaDiff(string[] qualifiedColumnNamesA, string[] qualifiedColumnNamesB)
        {
            var added = new HashSet<string>(qualifiedColumnNamesA);
            added.ExceptWith(qualifiedColumnNamesB);
            AddedQualifiedColumnNames = added.ToArray();

            var removed = new HashSet<string>(qualifiedColumnNamesB);
            removed.ExceptWith(qualifiedColumnNamesA);
            RemovedQualifiedColumnNames = removed.ToArray();
        }
    }

    public class VimEntityTableSchema
    {
        public readonly string TableName;
        public readonly HashSet<string> ColumnNames = new HashSet<string>();

        public VimEntityTableSchema(string tableName)
            => TableName = tableName;

        public void AddColumn(string columnName)
            => ColumnNames.Add(columnName);
    }
}
