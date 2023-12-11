﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Vim.BFastNS.Core;

namespace Vim.BFastNS
{
    public class BFastNode : IBFastNode
    {
        private readonly Stream _stream;
        private readonly BFastRange _range;

        public static BFastNode FromArray<T>(T[] array) where T : unmanaged
        {
            /*
            Is a memory leak created if a MemoryStream in .NET is not closed?
            -----------------------------------------]-------------------------
            You won't leak anything - at least in the current implementation.
            Calling Dispose won't clean up the memory used by MemoryStream any faster. It will stop your stream from being viable for Read/Write calls after the call, which may or may not be useful to you.
            If you're absolutely sure that you never want to move from a MemoryStream to another kind of stream, it's not going to do you any harm to not call Dispose. However, it's generally good practice partly because if you ever do change to use a different Stream, you don't want to get bitten by a hard-to-find bug because you chose the easy way out early on. (On the other hand, there's the YAGNI argument...)
            The other reason to do it anyway is that a new implementation may introduce resources which would be freed on Dispose.
            https://stackoverflow.com/questions/234059/is-a-memory-leak-created-if-a-memorystream-in-net-is-not-closed
            */
            var stream = new MemoryStream();
            stream.Write(array);
            return new BFastNode(stream, stream.FullRange());
        }


        public BFastNode(Stream stream, BFastRange range, Action cleanup = null)
        {
            _stream = stream;
            _range = range;
        }

        public BFast AsBFast()
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            try
            {
                return new BFast(_stream);
            }
            catch (Exception ex)
            {
                // It is expected most buffers are not valid bfasts.
                return null;
            }
        }

        public T[] AsArray<T>() where T : unmanaged
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            return _stream.ReadArrayBytes<T>(_range.Count);
        }

        public IEnumerable<T> AsEnumerable<T>() where T : unmanaged
            => AsArray<T>();


        public long GetSize()
        {
            return _range.Count;
        }

        public void Write(Stream stream)
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            CopyStream(_stream, stream, (int)_range.Count);
        }

        private static void CopyStream(Stream input, Stream output, int bytes)
        {
            var buffer = new byte[32768];
            int read;
            while (bytes > 0 &&
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }
    }
}