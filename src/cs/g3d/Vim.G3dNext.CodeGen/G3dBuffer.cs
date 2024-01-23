using System;
using System.Diagnostics;

namespace Vim.G3dNext.CodeGen
{
    public enum BufferType
    {
        Singleton,
        Data,
        Index
    }

    public class G3dBuffer
    {
        public string MemberName;
        public string BufferName;
        public BufferType BufferType;
        public Type ValueType;
        public string IndexInto;

        public string ArgumentName => LowerFirst(MemberName);

        public G3dBuffer(string name, string bufferName, BufferType bufferType, Type valueType, string indexInto = null)
        {
            Debug.Assert(bufferName.ToLower() == bufferName, "G3dCodeGen: Expected buffer name to be lowercase.");

            MemberName = name;
            BufferName = bufferName;
            BufferType = bufferType;
            ValueType = valueType;
            IndexInto = indexInto;
        }

        public static string LowerFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToLower(input[0]) + input.Substring(1);
        }
    }
}

