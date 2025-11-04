namespace Vim.Format.api_v2
{
    public class VimOpenOptions
    {
        public bool IncludeGeometry { get; set; } = true;
        public bool IncludeStringTable { get; set; } = true;
        public bool IncludeDataTables { get; set; } = true;
        public bool SchemaOnly { get; set; } = false;
        public bool IncludeAssets { get; set; } = true;
    }
}