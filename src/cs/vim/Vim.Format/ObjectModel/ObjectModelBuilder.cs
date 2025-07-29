namespace Vim.Format.ObjectModel
{
    /// <summary>
    /// This class makes it easy to fill out the entity tables of a VIM, by allowing the user to specify a mapping
    /// between objects in the source domain (e.g. Revit.Element) and entities. This allows a programmer to not
    /// have to worry about adding the same element twice, and allows them to add entity objects to the document
    /// builder in a single pass without worrying about lookup tables.
    /// </summary>
    public partial class ObjectModelBuilder
    {
        public DocumentBuilder ToDocumentBuilder(string generator, string versionString)
            => AddEntityTableSets(new DocumentBuilder(generator, SchemaVersion.Current, versionString));
    }
}
