using System.IO;

namespace Vim.BFastNextNS
{
    /// <summary>
    /// Anything that can be added to a BFAST must have a size and write to a stream.
    /// </summary>
    public interface IWritable
    {
        long GetSize();
        void Write(Stream stream);
    }

    public interface IBFastNextNode : IWritable
    {
        T[] AsArray<T>() where T : unmanaged;
        BFastNext AsBFast();
    }
}
