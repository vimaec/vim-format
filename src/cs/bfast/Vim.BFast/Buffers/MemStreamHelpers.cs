using System.Collections.Generic;
using System.IO;

namespace Vim.BFastNS
{
    /// <summary>
    /// This class would benefit from being in a generic utilities class, however, having it here allows BFAST to be a standalone without dependencies.
    /// </summary>
    public static class MemStreamHelpers
    {
        /// <summary>
        /// Creates and fills a new Memory Stream from the given array.
        /// The stream is returned at Position 0.
        /// </summary>
        public static unsafe MemoryStream ToMemoryStream<T>(this T[] array) where T : unmanaged
        {
            var mem = new MemoryStream();
            mem.Write(array);
            mem.Seek(0, SeekOrigin.Begin);
            return mem;
        }

        /// <summary>
        /// Creates and fills a new Memory Stream from the given array.
        /// The stream is returned at Position 0.
        /// </summary>
        public static unsafe MemoryStream ToMemoryStream<T>(this IEnumerable<T> enumerable) where T : unmanaged
        {
            var mem = new MemoryStream();
            foreach(var e in enumerable)
            {
                mem.WriteValue(e);
            }
            mem.Seek(0, SeekOrigin.Begin);
            return mem;
        }
    }
}
