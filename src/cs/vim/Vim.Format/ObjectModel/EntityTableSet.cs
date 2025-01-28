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
        /// Convenience constructor for seeking entity table information in a VIM file.
        /// </summary>
        public EntityTableSet(
            FileInfo vimFileInfo,
            bool schemaOnly,
            string[] stringBuffer,
            Func<string, bool> entityTableNameFilterFunc = null,
            bool inParallel = true)
            : this(
                vimFileInfo.EnumerateEntityTables(schemaOnly, entityTableNameFilterFunc).ToArray(),
                stringBuffer,
                inParallel)
        { }
    }
}
