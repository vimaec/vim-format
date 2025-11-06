namespace Vim.Format.api_v2
{
    public class VimEntityTableSetOptions
    {
        /// <summary>
        /// The string table, which can be loaded separately.
        /// If null (and schemaOnly is false), the string table will be loaded from the VIM file.
        /// Provide an empty array to avoid loading the string table.
        /// </summary>
        public string[] StringTable { get; set; } = null;

        /// <summary>
        /// A filter which specifies which entity tables to load by name.
        /// </summary>
        public VimEntityTableData.EntityTableFilter EntityTableNameFilter { get; set; } = null;

        /// <summary>
        /// A filter which specifies which entity table columns to load based on the name of the tablea nd the name of the column.
        /// </summary>
        public VimEntityTableData.EntityTableColumnFilter EntityTableColumnFilter { get; set; } = null;

        /// <summary>
        /// Determines whether the loading process may occur in parallel (speeds up the loading time).
        /// </summary>
        public bool InParallel { get; set; } = true;

        /// <summary>
        /// If true, only the entity table schema will be returned; none of the entries in the entity tables will be populated.
        /// </summary>
        public bool SchemaOnly { get; set; } = false;
    }
}