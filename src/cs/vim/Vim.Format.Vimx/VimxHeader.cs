using System.Text;
using Vim.Util;

namespace Vim.Format.VimxNS
{
    public static class VimxHeader 
    {
        static SerializableVersion CurrentVersion = SerializableVersion.Parse("0.1.0");
        public static SerializableHeader FromString(string header)
        {
            return SerializableHeader.Parse(header.Replace("vimx", "vim"));
        }
        public static SerializableHeader FromBytes(byte[] header)
        {
            return FromString(Encoding.UTF8.GetString(header));
        }

        public static string ToVimxString(this SerializableHeader header)
        {
            return header.ToString().Replace("vim", "vimx"); 
        }

        public static byte[] ToVimxBytes(this SerializableHeader header)
        {
            return header.ToVimxString().ToBytesUtf8();
        }

        public static SerializableHeader CreateDefault()
        {
            return new SerializableHeader(
                "Vim.Vimx.Converter", new SerializableVersion(), CurrentVersion.ToString()
            );
        }
    }
}
