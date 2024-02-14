using System;
using System.Collections.Generic;
using System.IO;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
    public class BFastStreamNode : IBFastNode
    {
        private readonly Stream _stream;
        private readonly BFastRange _range;

        public BFastStreamNode(Stream stream, BFastRange range)
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
            catch (Exception e) 
            {
                throw new Exception("Requested data is not a valid BFast or is compressed and needs decompression.", e);
            }
        }

        public T[] AsArray<T>() where T : unmanaged
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            return _stream.ReadArrayBytes<T>(_range.Count);
        }

        public IEnumerable<T> AsEnumerable<T>() where T : unmanaged
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            return _stream.ReadEnumerableByte<T>(_range.Count);
        }

        public void Write(Stream stream)
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            _stream.CopySome(stream, (int)_range.Count);
        }
    }
}
