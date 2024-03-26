namespace Vim.BFastLib.Core
{
    /// <summary>
    /// Constants.
    /// </summary>
    public static class BFastConstants
    {
        public const long Magic = 0xBFA5;

        // https://en.wikipedia.org/wiki/Endianness
        public const long SameEndian = Magic;
        public const long SwappedEndian = 0xA5BFL << 48;

    }
}
