namespace Vim.Format.api_v2
{
    public static class ColumnTypePrefixes
    {
        // Index column prefix

        public const string IndexColumnNameTypePrefix = "index:";

        // String column prefix

        public const string StringColumnNameTypePrefix = "string:";

        // Data column prefixes

        public const string IntColumnNameTypePrefix = "int:";
        public const string UintColumnNameTypePrefix = "uint:"; // unused for now
        public const string LongColumnNameTypePrefix = "long:";
        public const string UlongColumnNameTypePrefix = "ulong:"; // unused for now
        public const string ByteColumnNameTypePrefix = "byte:";
        public const string UbyteColumNameTypePrefix = "ubyte:"; // unused for now
        public const string FloatColumnNameTypePrefix = "float:";
        public const string DoubleColumnNameTypePrefix = "double:";
    }
}
