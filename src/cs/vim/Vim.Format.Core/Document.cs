using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Vim.BFastLib;
using Vim.Format;
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

            var tables = document.EntityTables.ToDictionary(
               et => et.Name,
               et => et.ToEntityTable(this));
            _bim = new Bim(tables, _Document.StringTable);
        }

        Bim _bim;

        public int TableCount => _bim.TableCount;

        public EntityTable GetTable(string name)
            => _bim.GetTable(name);

        public IEnumerable<string> TableNames => _bim.TableNames;
        public IEnumerable<EntityTable> Tables => _bim.Tables;
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

public class Bim
{
    private string[] _strings;
    private Dictionary<string, EntityTable> _tables { get; }

    public Bim(Dictionary<string, EntityTable> tables, string[] strings)
    {
        _tables = tables;
        _strings = strings;
    }

    public int TableCount => Tables.Count();

    public EntityTable GetTable(string name)
        => _tables.GetOrDefault(name);
    
    public IEnumerable<EntityTable> Tables => _tables.Values;
    public IEnumerable<string> TableNames => Tables.Select(e => e.Name);

}
