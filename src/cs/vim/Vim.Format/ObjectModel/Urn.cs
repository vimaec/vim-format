using System.Linq;

namespace Vim.ObjectModel
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

        public const string Separator = ":";
        public const string VimNID = "vim";
        public const string SystemPrefix = "sys";
        public const string DocumentPrefix = "doc";
        public const string ElementPrefix = "elem";
        public const string Null = "null";

        public static string CreateUrn(string nid, params string[] nss)
            => string.Join(Separator, new[] {"urn", nid}.Concat(nss));

        // Context-specific helpers

        public static string GetSystemUrn(string nid, string value)
            => CreateUrn(nid, SystemPrefix, value);

        public static string GetBimDocumentUrn(string nid, string guid, int numSaves)
            => CreateUrn(nid, DocumentPrefix, guid, numSaves.ToString());

        public static string GetBimDocumentUrn(string nid, BimDocument bimDocument)
            => GetBimDocumentUrn(nid, bimDocument?.Guid ?? Null, bimDocument?.NumSaves ?? default);

        public static string GetElementUrn(string documentUrn, int elementId)
            => documentUrn + Separator + ElementPrefix + Separator + elementId;

        public static string GetElementUrn(string nid, string documentGuid, int documentNumSaves, int elementId)
            => GetElementUrn(GetBimDocumentUrn(nid, documentGuid, documentNumSaves), elementId);

        public static string GetElementUrn(string nid, Element element)
            => GetElementUrn(GetBimDocumentUrn(nid, element?.BimDocument), element?.Id ?? -1);
    }
}
