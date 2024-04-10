namespace Vim.G3dNext
{
    /// <summary>
    /// Defines method for additionnal setup after constructors in generated G3d classes.
    /// </summary>
    public interface ISetup
    {
        void Setup();
    }

    public enum MeshSection
    {
        Opaque,
        Transparent,
        All
    }


    public static class Utils {
        public static bool SafeEqual<T>(this T[] a, T[] b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;
            if(b == null) return false;
            if(a.Length != b.Length) return false;
            for(var i= 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i])) return false;
            }
            return true;
        }

        public static T SafeGet<T>(this T[] a, int i) where T : class
        {
            if (i < 0) return null;
            if (i >= a.Length) return null;
            return a[i];
        }
    }

    public static class Constants
    {
        public const string G3dPrefix = "g3d";
        public const string Separator = ":";
        public const char SeparatorChar = ':';

        public const string MetaHeaderSegmentName = "meta";
        public const long MetaHeaderSegmentNumBytes = 8; // The header is 7 bytes + 1 bytes padding.
        public const byte MetaHeaderMagicA = 0x63;
        public const byte MetaHeaderMagicB = 0xD0;

        public static readonly string[] MetaHeaderSupportedUnits = { "mm", "cm", "m\0", "km", "in", "ft", "yd", "mi" };
    }
}
