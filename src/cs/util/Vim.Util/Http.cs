using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Vim.Util
{
    public static class Http
    {
        public const int DefaultDownloadBufferSize = 1000000; // 1 MB

        /// <summary>
        /// Downloads the content of the URL to the given stream. Returns the number of bytes read.
        /// </summary>
        /// <param name="url">The URL from which to download</param>
        /// <param name="stream">The stream to populate</param>
        /// <param name="progress">The download progress between 0.0 and 1.0</param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="bufferSize">The buffer size used to copy into the given stream</param>
        public static async Task<long> DownloadAsync(
            string url,
            Stream stream,
            IProgress<double> progress = null,
            CancellationToken ct = default,
            int bufferSize = DefaultDownloadBufferSize)
        {
            var streamOffset = stream.Position;

            long bytesRead = 0;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct);
                response.EnsureSuccessStatusCode();

                var contentLength = response.Content.Headers.ContentLength;
                using (var source = await response.Content.ReadAsStreamAsync())
                {
                    bytesRead = await WriteToStreamAsync(source, stream, contentLength, progress, ct, bufferSize);
                }
            }

            stream.Seek(streamOffset, SeekOrigin.Begin); // Reset the stream so it can be read it.

            return bytesRead;
        }

        private static async Task<long> WriteToStreamAsync(
            Stream source,
            Stream destination,
            long? contentLength,
            IProgress<double> progress = null,
            CancellationToken ct = default,
            int bufferSize = DefaultDownloadBufferSize)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalRead = 0;
            int read;

            while ((read = await source.ReadAsync(buffer, 0, buffer.Length, ct)) != 0)
            {
                await destination.WriteAsync(buffer, 0, read, ct);
                totalRead += read;

                if (!contentLength.HasValue)
                    continue;

                // Report the progress if the content length is known.
                var percentComplete = (double)totalRead / contentLength.Value;
                progress?.Report(percentComplete);
            }

            return totalRead;
        }
    }
}
