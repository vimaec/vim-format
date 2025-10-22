using Vim.LinqArray;

namespace Vim.Format
{
    public static class DocumentExtensions
    {
        public static Document ToDocument(this SerializableDocument document)
            => new Document(document);

        public static EntityTable ToEntityTable(this SerializableEntityTable entityTable, Document document)
            => new EntityTable(document, entityTable);

        public static EntityTable GetTable(this Document doc, string name)
            => doc.EntityTables.GetOrDefault(name);

        public static SerializableDocument SetFileName(this SerializableDocument doc, string fileName)
        {
            doc.FileName = fileName;
            return doc;
        }
    }
}
