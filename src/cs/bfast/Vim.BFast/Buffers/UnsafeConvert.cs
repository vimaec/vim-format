using System.Collections.Generic;
using System.IO;

namespace Vim.BFastLib
{ 
    public static class UnsafeConvert
    {
        /// <summary>
        /// Converts an array of type TInput to an array of type TOutput
        /// This is not a Cast but an actual byte level conversion.
        /// </summary>
        public static unsafe TResult[] Convert<TInput, TResult>(this TInput[] array)
            where TInput : unmanaged
            where TResult : unmanaged
        {
            var count = array.Length * (sizeof(TInput) / sizeof(TResult));
            using (var mem = array.ToMemoryStream())
            {
                return mem.ReadArray<TResult>(count);
            }
        }

        /// <summary>
        /// Converts an enumerable of type TInput to an enumerable of type TOutput
        /// This is not a Cast but an actual byte level conversion.
        /// </summary>
        public static IEnumerable<TResult> Convert<TInput, TResult>(this IEnumerable<TInput> input, int chunksize = 1048576)
            where TInput : unmanaged
            where TResult : unmanaged
        {
            var stream = new MemoryStream();
            var array = new TResult[chunksize];
            var chunks = UnsafeHelpers.Chunkify(input, chunksize);
            while (chunks.MoveNext())
            {
                (var chunk, var size) = chunks.Current;
                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(chunk, size);
                var count = ReadArray(stream, array);

                if (count > 0)
                {
                    for (var i = 0; i < count; i++)
                    {
                        yield return array[i];
                    }
                }
            }
        }

        // Function is extracted because unsafe code cannot appear in generator
        private static unsafe int ReadArray<T>(MemoryStream stream, T[] array) where T : unmanaged
        {
            var length = (int)stream.Position;
            if (length < sizeof(T))
            {
                return 0;
            }

            var count = length / sizeof(T);
            stream.Seek(0, SeekOrigin.Begin);
            stream.ReadArray<T>(array, count);
            return count;
        }
    }
}
