using System;
using System.IO;

namespace Vim.BFastNS
{
    public static class UnsafeRead
    {
        /// <summary>
        /// Reads and converts the next value of the stream.
        /// </summary>
        public static unsafe T ReadValue<T>(this Stream stream) where T : unmanaged
        {
            T r;
            stream.ReadBytesBuffered((byte*)&r, sizeof(T));
            return r;
        }

        /// <summary>
        /// Reads bytes until the end of the stream and converts them to T.
        /// </summary>
        public static unsafe T[] ReadArray<T>(this Stream stream) where T : unmanaged
        {
            return ReadArrayBytes<T>(stream, stream.Length - stream.Position);
        }

        /// <summary>
        /// Reads and converts the next ByteCount bytes from the stream and returns the result as a new array.
        /// Will throw if ByteCount is not a multiple of sizeof T.
        /// </summary>
        public static unsafe T[] ReadArrayBytes<T>(this Stream stream, long byteCount) where T : unmanaged
        {
            var count = byteCount / sizeof(T);
            if (byteCount % sizeof(T) != 0)
                throw new Exception($"The number of bytes {byteCount} is not divisible by the size of the type {sizeof(T)}");
            if (count >= int.MaxValue)
                throw new Exception($"{count} exceeds the maximum number of items that can be read into an array {int.MaxValue}");
            return ReadArray<T>(stream, (int)count);
        }

        /// <summary>
        /// Reads and converts the next Count value from the stream and returns the result as a new array.
        /// </summary>
        public static unsafe T[] ReadArray<T>(this Stream stream, int count) where T : unmanaged
        {
            var r = new T[count];
            fixed (T* pDest = r)
            {

                var pBytes = (byte*)pDest;
                stream.ReadBytesBuffered(pBytes, (long)count * sizeof(T));
            }
            return r;
        }

        /// <summary>
        /// Reads and converts the next Count values from the stream and writes the result into the given array.
        /// </summary>
        public static unsafe void ReadArray<T>(this Stream stream, T[] array, int count) where T : unmanaged
        {
            if (array.Length < count)
                throw new Exception("Destination array needs to be larger than count.");

            fixed (T* pDest = array)
            {
                var pBytes = (byte*)pDest;
                stream.ReadBytesBuffered(pBytes, (long)count * sizeof(T));
            }
        }

        /// <summary>
        /// Helper for reading arrays of arbitrary unmanaged types from a Stream, that might be over 2GB of size.
        /// That said, in C#, you can never load more int.MaxValue numbers of items.
        /// NOTE: Arrays are still limited to 2gb in size unless gcAllowVeryLargeObjects is set to true
        /// in the runtime environment.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.array?redirectedfrom=MSDN&view=netframework-4.7.2#remarks
        /// Alternatively, we could convert to .Net Core
        /// </summary>
        private static unsafe void ReadBytesBuffered(this Stream stream, byte* dest, long count, int bufferSize = 4096)
        {
            var buffer = new byte[bufferSize];
            int bytesRead;
            fixed (byte* pBuffer = buffer)
            {
                while ((bytesRead = stream.Read(buffer, 0, (int)Math.Min(buffer.Length, count))) > 0)
                {
                    if (dest != null)
                        Buffer.MemoryCopy(pBuffer, dest, count, bytesRead);
                    count -= bytesRead;
                    dest += bytesRead;
                }
            }
        }
    }
}
