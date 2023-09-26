using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format.Vimx
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

        public static VimxHeader CreateDefault()
        {
            return new VimxHeader(
                "Vim.Vimx.Converter",
                CurrentVimFormatVersion,
                CurrentVimFormatVersion.ToString()
            );
        }

        public static VimxHeader FromBytes(byte[] bytes)
        {
            var str = Encoding.UTF8.GetString(bytes);
            return new VimxHeader(Parse(str));
        }
    }
}
