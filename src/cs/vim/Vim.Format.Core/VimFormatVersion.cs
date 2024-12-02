using Vim.Util;

namespace Vim.Format
{
    public static class VimFormatVersion
    {
        public static SerializableVersion Current => v1_0_0;
        public static SerializableVersion v1_0_0 => SerializableVersion.Parse("1.0.0");
    }
}
