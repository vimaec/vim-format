using System;
using System.Diagnostics;

namespace Vim.G3d.CodeGen
{
    public enum BufferType
    {
        Singleton,
        Data,
        Index
    }

    /// <summary>
    /// Holds all necessary data to generate the code for a g3dBuffer.
    /// </summary>
    public class G3dBuffer
    {
        public readonly string MemberName;
        public readonly string BufferName;
        public readonly BufferType BufferType;
        public readonly Type ValueType;
        public readonly string IndexInto;

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

