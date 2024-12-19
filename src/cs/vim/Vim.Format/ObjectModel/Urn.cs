using System.Linq;

namespace Vim.Format.ObjectModel
{
    public static class Urn
    {
        // Based on: https://en.wikipedia.org/wiki/Uniform_Resource_Name#Syntax
        //
        // "urn:<NID>:<NSS>"
        //
        //  - urn is the "urn" string literal
        //
        //  - <NID> is the "namespace identifier":
        //      More than two letters long.
        //
        //  - <NSS> is the "namespace-specific string":
        //      The interpretation of which depends on the specified namespace.
        //      The NSS may contain ASCII letters and digits, and many punctuation and special characters.
        //
        // Example (from the wiki link):
        //
        //     urn:epc:id:sscc:0614141.1234567890	Serial Shipping Container Code
        //

        private const string Separator = ":";
        public const string VimNID = "vim";
        private const string SystemPrefix = "sys";
        private const string DocumentPrefix = "doc";
        private const string ElementPrefix = "elem";
        private const string Null = "null";

        private static string CreateUrn(string nid, params string[] nss)
            => string.Join(Separator, new[] {"urn", nid}.Concat(nss));

        // Context-specific helpers

        public static string GetSystemUrn(string nid, string value)
            => CreateUrn(nid, SystemPrefix, value);

        public static string GetBimDocumentUrn(string nid, string guid, int numSaves)
            => CreateUrn(nid, DocumentPrefix, guid, numSaves.ToString());

        private static string GetBimDocumentUrn(string nid, BimDocument bimDocument)
            => GetBimDocumentUrn(nid, bimDocument?.Guid ?? Null, bimDocument?.NumSaves ?? default);

        public static string GetElementUrn(string documentUrn, int elementId)
            => documentUrn + Separator + ElementPrefix + Separator + elementId;

        public static string GetElementUrn(string documentUrn, long elementId)
            => documentUrn + Separator + ElementPrefix + Separator + elementId;

        public static string GetElementUrn(string nid, Element element)
            => GetElementUrn(GetBimDocumentUrn(nid, element?.BimDocument), element?.Id ?? -1);
    }
}
