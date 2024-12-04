using System.IO;

namespace Vim.Format
{
    public static class SerializableHeaderExtensions
    {
        public static bool TryParseSerializableHeader(this Stream stream, out SerializableHeader header)
        {
            try
            {
                header = SerializableHeader.FromStream(stream);
            }
            catch
            {
                header = null;
            }
            return header != null;
        }

        public static bool TryParseSerializableHeader(this FileInfo fileInfo, out SerializableHeader header)
        {
            try
            {
                header = SerializableHeader.FromPath(fileInfo.FullName);
            }
            catch
            {
                header = null;
            }
            return header != null;
        }

        /// <summary>
        /// Returns the VIM file's header schema version. Returns null if the header schema is not found.
        /// </summary>
        public static string GetSchemaVersion(this FileInfo fileInfo)
            => fileInfo.TryParseSerializableHeader(out var header)
                ? header.Schema?.ToString()
                : null;
    }
}
