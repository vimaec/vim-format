using System;
using System.Collections.Generic;

// SOME BACKGROUND INFORMATION ABOUT ELEMENT IFC GUIDS
//
// by: Martin Ashton, August 25, 2025
//
// - VIM Elements sourced from Revit may have the parameter IfcGUID, which defines the IFC GUID of the element (which is distinct from the value stored in Element.UniqueId)
// - VIM Elements sourced from IFC files use the Element.UniqueId field to store the equivalent IfcGUID.
// - This IfcGUID is a "compressed" 22 character case-sensitive string.
//   - This unfortunately does not play nicely with systems which merge records in a case-insensitive manner (ex: PowerBI)
//   - To resolve the casing issue, we expand the 22 character IfcGuid (if present) into its canonical GUID representation composed of 36 hexadecimal characters and dashes ('-').

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Convenience class which extracts IfcGuid from the Element's parameters or from its UniqueId.
    /// </summary>
    public class ElementIfcInfo : IElementIndex
    {
        public Element Element { get; }

        public int GetElementIndexOrNone()
            => EntityRelation.IndexOrDefault(Element);

        /// <summary>
        /// A 22 character IFC encoded GUID composed of case-sensitive characters.
        /// </summary>
        public string IfcGuid { get; }

        /// <summary>
        /// The built-in Revit parameters which contain the IFC GUID.
        /// </summary>
        public readonly HashSet<string> BuiltInIfcGuidParameterIds = new HashSet<string>()
        {
            "-1019000", //IFC_GUID, "IfcGUID"
            "-1019001", //IFC_TYPE_GUID, "Type IfcGUID"
            "-1019002", //IFC_PROJECT_GUID, "IfcProject GUID"
            "-1019003", //IFC_BUILDING_GUID, "IfcBuilding GUID"
            "-1019004", //IFC_SITE_GUID, "IfcSite GUID"
        };

        /// <summary>
        /// The expanded canonical Guid based on the 22 character IfcGuid.
        /// </summary>
        public Guid? IfcGuidCanonical { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementIfcInfo(
            Element element,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps)
        {
            Element = element;

            var elementIndex = GetElementIndexOrNone();

            var elementParameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);

            // 0. Initialize the properties to their default null values.
            IfcGuid = null;
            IfcGuidCanonical = null;

            // 1. Check if the unique ID can be parsed from the element.UniqueId field.
            var candidateIfcGuidFromUniqueId = element.UniqueId;
            if (TryParseIfcGuidAsCanonicalGuid(candidateIfcGuidFromUniqueId, out var guidFromUniqueId))
            {
                IfcGuid = candidateIfcGuidFromUniqueId;
                IfcGuidCanonical = guidFromUniqueId;
                return;
            }

            // 2. Look for the relevant parameters associated to this element.
            foreach (var parameterIndex in elementParameterIndices)
            {
                var p = parameterTable.Get(parameterIndex);
                var d = p.ParameterDescriptor;
                var builtInId = d.Guid; // This is the built-in ID of the parameter (not to be confused with the actual IFC GUID value we're looking for)

                if (!BuiltInIfcGuidParameterIds.Contains(builtInId) &&
                    !d.Name.Equals("IfcGUID", StringComparison.InvariantCultureIgnoreCase))
                {
                    // This is not the parameter you are looking for.
                    continue;
                }

                var (candidateIfcGuidFromParameter, _) = p.Values; // check the native value.

                if (TryParseIfcGuidAsCanonicalGuid(candidateIfcGuidFromParameter, out var ifcGuidCanonical))
                {
                    IfcGuid = candidateIfcGuidFromParameter;
                    IfcGuidCanonical = ifcGuidCanonical;
                }

                // We have just visited a IfcGUID parameter, so we can end our search.
                break;
            }
        }

        // Characters used in the 22-char encoding
        public const string Base64Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_$";
        public const uint IfcGuidLength = 22;
        public const uint IfcGuidCanonicalLength = 36;

        /// <summary>
        /// Converts a 22-character IFC GUID into a System.Guid
        /// </summary>
        public static bool TryParseIfcGuidAsCanonicalGuid(string ifcGuid, out Guid guid)
        {
            guid = Guid.Empty;
            if (string.IsNullOrEmpty(ifcGuid) || ifcGuid.Length != IfcGuidLength)
                return false;

            var bytes = new byte[16];
            var bytePos = 0;
            var value = 0;
            var bitsLeft = 0;

            foreach (var c in ifcGuid)
            {
                var index = Base64Chars.IndexOf(c);
                if (index < 0)
                    return false;

                value = (value << 6) | index;
                bitsLeft += 6;

                if (bitsLeft >= 8)
                {
                    bitsLeft -= 8;
                    bytes[bytePos++] = (byte)((value >> bitsLeft) & 0xFF);
                    if (bytePos == 16)
                        break;
                }
            }

            guid = new Guid(bytes);
            return true;
        }

        /// <summary>
        /// Converts a Guid into the 22-character IFC GUID format
        /// </summary>
        public static string ToIfcGuid(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return "0000000000000000000000"; // 22 characters of 0s
            }

            var bytes = guid.ToByteArray();

            var value = 0;
            var bitsLeft = 0;
            var result = new char[IfcGuidLength];
            var charPos = 0;

            foreach (var b in bytes)
            {
                value = (value << 8) | b;
                bitsLeft += 8;

                while (bitsLeft >= 6)
                {
                    bitsLeft -= 6;
                    result[charPos++] = Base64Chars[(value >> bitsLeft) & 0x3F];
                }
            }

            // handle remaining bits (pad if necessary)
            if (charPos < IfcGuidLength)
            {
                if (bitsLeft > 0)
                    result[charPos++] = Base64Chars[(value << (6 - bitsLeft)) & 0x3F];

                // pad with zeroes if still short (shouldn’t normally happen except for Guid.Empty)
                while (charPos < IfcGuidLength)
                    result[charPos++] = Base64Chars[0];
            }

            return new string(result);
        }
    }
}
