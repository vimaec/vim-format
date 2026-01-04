using System;
using Vim.Format;

namespace Vim.Format.ElementParameterInfo
{
    public class FamilyTypeUniformatInfo
    {
        public FamilyType FamilyType { get; }

        /// <summary>
        /// The value of the "Assembly Code" parameter
        /// </summary>
        public string AssemblyCode { get; } = "";
        public const string AssemblyCodeParameterName = "Assembly Code";
        public const string AssemblyCodeBuiltInId = "-1002500";
        public static bool DescriptorIsAssemblyCode(ParameterDescriptor pd)
            => pd.Guid == AssemblyCodeBuiltInId ||
               pd.Name.Equals(AssemblyCodeParameterName, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// The value of the "Assembly Description" parameter
        /// </summary>
        public string AssemblyDescription { get; } = "";
        public const string AssemblyDescriptionParameterName = "Assembly Description";
        public const string AssemblyDescriptionBuiltInId = "-1002501";
        public static bool DescriptorIsAssemblyDescription(ParameterDescriptor pd)
            => pd.Guid == AssemblyDescriptionBuiltInId ||
               pd.Name.Equals(AssemblyDescriptionParameterName, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// The first character of the assembly code.
        /// </summary>
        public string UniformatLevel1
            // Input: "B1010240" Output: "B"
            => GetUniformatLevel1(AssemblyCode);

        /// <summary>
        /// The first three characters of the assembly code.
        /// </summary>
        public string UniformatLevel2
            // Input: "B1010240" Output: "B10"
            => GetUniformatLevel2(AssemblyCode);

        /// <summary>
        /// The first five characters of the assembly code.
        /// </summary>
        public string UniformatLevel3
            // Input: "B1010240" Output: "B1010"
            => GetUniformatLevel3(AssemblyCode);

        /// <summary>
        /// Constructor
        /// </summary>
        public FamilyTypeUniformatInfo(
            FamilyType familyType,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps)
        {
            FamilyType = familyType;

            var elementIndex = FamilyType.GetElementIndexOrNone();
            if (elementIndex == EntityRelation.None)
                return;

            var parameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);

            foreach (var paramIndex in parameterIndices)
            {
                var p = parameterTable.Get(paramIndex);
                var pd = p.ParameterDescriptor;

                if (string.IsNullOrEmpty(AssemblyCode) && DescriptorIsAssemblyCode(pd))
                {
                    var (nativeValue, _) = p.Values;
                    AssemblyCode = nativeValue;
                }

                if (string.IsNullOrEmpty(AssemblyDescription) && DescriptorIsAssemblyDescription(pd))
                {
                    var (nativeValue, _) = p.Values;
                    AssemblyDescription = nativeValue;
                }

                if (!string.IsNullOrEmpty(AssemblyCode) && !string.IsNullOrEmpty(AssemblyDescription))
                    break;
            }
        }

        private static string GetSubstringOrEmpty(string str, int length)
            => str.Length >= length ? str.Substring(0, length) : "";

        public static string GetUniformatLevel1(string str)
            => GetSubstringOrEmpty(str, 1);

        public static string GetUniformatLevel2(string str)
            => GetSubstringOrEmpty(str, 3);

        public static string GetUniformatLevel3(string str)
            => GetSubstringOrEmpty(str, 5);
    }
}
