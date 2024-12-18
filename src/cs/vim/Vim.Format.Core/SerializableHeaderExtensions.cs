using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vim.Format
{
    public static class SerializableHeaderExtensions
    {
        /// <summary>
        /// Returns true if the SerializableHeader in the stream is successfully parsed.
        /// </summary>
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

        /// <summary>
        /// Returns true if the SerializableHeader in the stream is successfully parsed.
        /// </summary>
        public static bool TryParseSerializableHeader(this FileInfo file, out SerializableHeader header)
        {
            using (var stream = file.OpenRead())
            {
                try
                {
                    header = SerializableHeader.FromStream(stream);
                }
                catch
                {
                    header = null;
                }
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
