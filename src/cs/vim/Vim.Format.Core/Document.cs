using Vim.LinqArray;
using Vim.BFastNextNS;

namespace Vim.Format
{
    // TODO: this should be merged into Serializable document. 
    public class Document
    {
        public Document(SerializableDocument document)
        {
            _Document = document;
            Header = _Document.Header;
            Geometry = _Document.Geometry;
            StringTable = _Document.StringTable.ToIArray();
            EntityTables = _Document.EntityTables.ToLookup(
                et => et.Name,
                et => et.ToEntityTable(this));
            Assets = _Document.Assets.ToLookup(et => et.Name, et => et);
        }

        public string FileName => _Document.FileName;
        public SerializableDocument _Document { get; }
        public SerializableHeader Header { get; }
        public ILookup<string, EntityTable> EntityTables { get; }
        public ILookup<string, INamedBuffer> Assets { get; }
        public IArray<string> StringTable { get; }
        public string GetString(int index) => StringTable.ElementAtOrDefault(index);
        public G3d.G3D Geometry { get; }
    }
}
