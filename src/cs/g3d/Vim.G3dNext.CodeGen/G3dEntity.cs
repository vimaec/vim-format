using System.Collections.Generic;

namespace Vim.G3dNext.CodeGen
{
    /// <summary>
    /// Holds the data to generate the code for a g3d entity.
    /// </summary>
    public class G3dEntity
    {
        public readonly string ClassName;
        public readonly List<G3dBuffer> Buffers = new List<G3dBuffer>();

        public G3dEntity(string name)
        {
            ClassName = name;
        }

        public G3dEntity Index(string name, string bufferName, string indexInto = null)
        {
            if (indexInto == null)
            {
                return Data<int>(name, bufferName);
            }
            Buffers.Add(new G3dBuffer(name, bufferName, BufferType.Index, typeof(int), indexInto));
            return this;
        }

        public G3dEntity Data<T>(string name, string bufferName, string indexInto = null)
        {
            Buffers.Add(new G3dBuffer(name, bufferName, BufferType.Data, typeof(T), indexInto));
            return this;
        }
    }
}
