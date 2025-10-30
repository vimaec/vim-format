using System;
using Vim.Format.ObjectModel;

namespace Vim.Format.ElementParameterInfo
{
    public class FamilyOmniClassInfo
    {
        public Family Family { get; }

        /// <summary>
        /// The value of the "OmniClass Number" parameter
        /// </summary>
        public string OmniClassNumber { get; } = "";
        public const string OmniClassNumberParameterName = "OmniClass Number";
        public const string OmniClassNumberBuiltInId = "-1002502";
        public static bool DescriptorIsOmniClassNumber(ParameterDescriptor pd)
            => pd.Guid == OmniClassNumberBuiltInId ||
               pd.Name.Equals(OmniClassNumberParameterName, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// The value of the "OmniClass Title" parameter
        /// </summary>
        public string OmniClassTitle { get; } = "";
        public const string OmniClassTitleParameterName = "OmniClass Title";
        public const string OmniClassTitleBuiltInId = "-1002503";
        public static bool DescriptorIsOmniClassTitle(ParameterDescriptor pd)
            => pd.Guid == OmniClassTitleBuiltInId ||
               pd.Name.Equals(OmniClassTitleParameterName);

        /// <summary>
        /// The substring left of the first dot of the OmniClassNumber.
        /// </summary>
        public string OmniClassTable
        {
            get
            {
                // Input:  23.40.70.14.64.21.11
                // Output: 23
                var firstDotIndex = OmniClassNumber.IndexOf('.');
                return firstDotIndex == -1
                    ? ""
                    : OmniClassNumber.Substring(0, firstDotIndex);
            }
        }

        /// <summary>
        /// The substring up to the 3rd dot followed by 00 of the OmniClassNumber.
        /// </summary>
        public string OmniClassParent
        {
            get
            {
                // Input:  23.40.70.14.64.21.11
                // Output: 23.40.70.00
                var thirdDotIndex = IndexOfThird(OmniClassNumber, '.');
                return thirdDotIndex == -1
                    ? ""
                    : OmniClassNumber.Substring(0, thirdDotIndex) + ".00";
            }
        }

        private static int IndexOfThird(string str, char c)
        {
            var first = str.IndexOf(c);
            if (first == -1)
                return -1; // no first dot

            var second = str.IndexOf(c, first + 1);
            if (second == -1)
                return -1; // no second dot.

            var third = str.IndexOf(c, second + 1);
            if (third == -1)
                return -1; // no third dot.

            return third;
        }

        public FamilyOmniClassInfo(
            Family family,
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps)
        {
            Family = family;

            var elementIndex = Family.GetElementIndexOrNone();
            if (elementIndex == EntityRelation.None)
                return;

            var parameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);

            foreach (var paramIndex in parameterIndices)
            {
                var p = parameterTable.Get(paramIndex);
                var pd = p.ParameterDescriptor;

                if (string.IsNullOrEmpty(OmniClassNumber) && DescriptorIsOmniClassNumber(pd))
                {
                    var (nativeValue, _) = p.Values;
                    OmniClassNumber = nativeValue;
                }

                if (string.IsNullOrEmpty(OmniClassTitle) && DescriptorIsOmniClassTitle(pd))
                {
                    var (nativeValue, _) = p.Values;
                    OmniClassTitle = nativeValue;
                }

                if (!string.IsNullOrEmpty(OmniClassNumber) && !string.IsNullOrEmpty(OmniClassTitle))
                    break;
            }
        }
    }
}
