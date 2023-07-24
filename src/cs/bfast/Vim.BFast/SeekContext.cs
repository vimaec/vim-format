using System;
using System.IO;

namespace Vim.BFast
{
    /// <summary>
    /// Manages a Stream's seek pointer within a given `using` scope.
    /// When the stream context is disposed, the seek position is reset
    /// to the original position when the object was created.
    /// </summary>
    public sealed class SeekContext : IDisposable
    {
        /// <summary>
        /// The seekable stream.
        /// </summary>
        public readonly Stream Stream;

        /// <summary>
        /// The original stream seek position when the object was created.
        /// </summary>
        public readonly long OriginalSeekPosition;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SeekContext(Stream stream)
        {
            if (!stream.CanSeek)
                throw new ArgumentException("Stream must be seekable.");

            Stream = stream;
            OriginalSeekPosition = stream.Position;
        }

        /// <summary>
        /// Disposer.
        /// </summary>
        public void Dispose()
            => Stream.Seek(OriginalSeekPosition, SeekOrigin.Begin);
    }
}
