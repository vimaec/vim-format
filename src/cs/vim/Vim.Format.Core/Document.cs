using System.Collections.Generic;
using System.Linq;
using Vim.BFastLib;
using Vim.G3d;
using Vim.Util;

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
            StringTable = _Document.StringTable;
            EntityTables = _Document.EntityTables.ToDictionary(
                et => et.Name,
                et => new EntityTable(this, et));
            Assets = _Document.Assets.ToDictionary(et => et.Name, et => et);
        }

        private SerializableDocument _Document { get; }
        public SerializableHeader Header { get; }
        public IDictionary<string, EntityTable> EntityTables { get; }
        public IDictionary<string, INamedBuffer> Assets { get; }
        public IList<string> StringTable { get; }
        public string GetString(int index) => StringTable.ElementAtOrDefault(index);
        public G3d.G3D Geometry { get; }

        public EntityTable GetTable(string name)
            => EntityTables.GetOrDefault(name);
    }
}
