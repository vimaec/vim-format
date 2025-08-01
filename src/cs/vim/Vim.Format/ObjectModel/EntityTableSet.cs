using System;
using System.IO;
using System.Linq;

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
        ///   The VIM file from which to load the entity tables.
        /// </param>
        /// <param name="stringTable">
        ///   The string table, which can be loaded separately.
        ///   If null (and schemaOnly is false), the string table will be loaded from the VIM file.
        ///   Provide an empty array to avoid loading the string table.
        /// </param>
        /// <param name="entityTableNameFilterFunc">
        ///   A filter allowing you to specify which entity tables to load by name.
        /// </param>
        /// <param name="inParallel">
        ///   Determines whether the loading process may occur in parallel (speeds up the loading time).
        /// </param>
        /// <param name="schemaOnly">
        ///   If true, only the table schema will be returned.
        /// </param>
        public EntityTableSet(
            FileInfo vimFileInfo,
            string[] stringTable = null,
            Func<string, bool> entityTableNameFilterFunc = null,
            bool inParallel = true,
            bool schemaOnly = false)
            : this(
                vimFileInfo.EnumerateEntityTables(schemaOnly, entityTableNameFilterFunc).ToArray(),
                stringTable ?? (schemaOnly ? null : vimFileInfo.GetStringTable()),
                inParallel)
        { }
    }
}
