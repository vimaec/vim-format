using System;
using System.IO;
using System.Linq;
using static Vim.Format.Serializer;

namespace Vim.Format.ObjectModel
{
    /// <summary>
    /// Additional partial definitions of EntityTableSet.
    /// </summary>
    public partial class EntityTableSet
    {
        /// <summary>
        ///   Represents a collection of entity tables.
        /// </summary>
        /// <param name="vimFileInfo">
        ///     The VIM file from which to load the entity tables.
        /// </param>
        /// <param name="stringTable">
        ///     The string table, which can be loaded separately.
        ///     If null (and schemaOnly is false), the string table will be loaded from the VIM file.
        ///     Provide an empty array to avoid loading the string table.
        /// </param>
        /// <param name="entityTableNameFilterFunc">
        ///     A filter which specifies which entity tables to load by name.
        /// </param>
        /// <param name="entityTableColumnFilter">
        ///     A filter which specifies which entity table column to load by name.
        /// </param>
        /// <param name="inParallel">
        ///     Determines whether the loading process may occur in parallel (speeds up the loading time).
        /// </param>
        /// <param name="schemaOnly">
        ///     If true, only the table schema will be returned.
        /// </param>
        public EntityTableSet(
            FileInfo vimFileInfo,
            string[] stringTable = null,
            Func<string, bool> entityTableNameFilterFunc = null,
            EntityTableColumnFilter entityTableColumnFilter = null,
            bool inParallel = true,
            bool schemaOnly = false)
            : this(
                vimFileInfo.EnumerateEntityTables(schemaOnly, entityTableNameFilterFunc, entityTableColumnFilter).ToArray(),
                stringTable ?? (schemaOnly ? null : vimFileInfo.GetStringTable()),
                inParallel)
        { }

        public static ElementKind[] GetElementKinds(FileInfo vimFileInfo)
        {
            var elementTableName = TableNames.Element;

            var elementKindTableNames = GetElementKindTableNames();
            elementKindTableNames.Add(elementTableName);
            
            var ets = new EntityTableSet(
                vimFileInfo,
                Array.Empty<string>(),
                entityTableName => elementKindTableNames.Contains(entityTableName),
                (entityTableName, colName) =>
                    // If we're dealing with the element table, load a single column from the element table (i.e. the ID column)
                    (entityTableName == elementTableName && (colName is "long:Id" || colName is "int:Id")) ||
                    // Otherwise, load the element index column.
                    colName == "index:Vim.Element:Element"
            );

            return ets.GetElementKinds();
        }
    }
}
