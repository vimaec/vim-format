using System.Collections.Generic;
using System.Text;
using Vim.Util;

namespace Vim.Format.VimxNS
{
    public class VimxHeader : SerializableHeader
    {
        new protected const string FormatVersionField = "vimx";
        public VimxHeader(string generator, SerializableVersion schema, string versionString, IReadOnlyDictionary<string, string> values = null) : base(generator, schema, versionString, values)
        {
        }

        public VimxHeader(SerializableHeader header) : this(header.Generator, header.Schema, header.FileFormatVersion.ToString())
        {
        }

        public VimxHeader(string header) : this(Parse(header))
        {
        }

        public VimxHeader(byte[] bytes) : this(Encoding.UTF8.GetString(bytes))
        {
        }

        public static VimxHeader CreateDefault()
        {
            return new VimxHeader(
                "Vim.Vimx.Converter",
                CurrentVimFormatVersion,
                CurrentVimFormatVersion.ToString()
            );
        }
    }
}
