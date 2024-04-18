using System.Collections;
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
            GeometryNext = _Document.GeometryNext;
            StringTable = _Document.StringTable;
            EntityTables = _Document.EntityTables.ToDictionary(
                et => et.Name,
                et => et.ToEntityTable(this));
            Assets = _Document.Assets.ToDictionary(et => et.Name, et => et);
        }

        public int TableCount => Tables.Count();

        public EntityTable GetTable(string name)
            => EntityTables.GetOrDefault(name);

        public IEnumerable<string> TableNames => _Document.EntityTables.Select(e => e.Name);
        public IEnumerable<EntityTable> Tables => EntityTables.Values;
        public VimSchema GetSchema() => VimSchema.Create(_Document);

        public string FileName => _Document.FileName;
        public SerializableDocument _Document { get; }
        public SerializableHeader Header { get; }
        public Dictionary<string, EntityTable> EntityTables { get; }
        public Dictionary<string, INamedBuffer> Assets { get; }
        public string[] StringTable { get; }
        public string GetString(int index) => StringTable.ElementAtOrDefault(index);
        public G3dVim GeometryNext { get; }
    }
}
