using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    public class ParameterSummary
    {
        public string SummaryKey { get; set; }

        public int Descriptor { get; set; }

        public ElementKind ElementKindEnum { get; set; }

        public string ElementKind { get; set; }

        public string CategoryNameFull { get; set; }

        public string Name { get; set; } // Derived from Descriptor

        public string NamePbiCaseSensitive { get; set; }

        public string Group { get; set; } // Derived from Descriptor

        public int CountTotal { get; set; }

        public int CountFilled { get; set; }

        public int CountNullish { get; set; }

        public int CountDistinct { get; set; }

        public int CountEmpty => CountTotal - CountFilled;

        public static IEnumerable<ParameterSummary> GetParameterSummary(
            ParameterTable parameterTable,
            ParameterDescriptorTable descriptorTable,
            ElementTable elementTable,
            ElementKind[] elementKindArray,
            CategoryTable categoryTable)
        {
            return Enumerable.Range(0, parameterTable.RowCount)
                .GroupBy(i => new ParameterSummaryKey(i, parameterTable, elementTable, elementKindArray, categoryTable))
                .AsParallel()
                .Select(g =>
                {
                    var parameterSummaryKey = g.Key;
                    var descriptorIndex = parameterSummaryKey.DescriptorIndex;
                    var elementKind = parameterSummaryKey.ElementKind;
                    var categoryIndex = parameterSummaryKey.CategoryIndex;

                    var displayValues = g.Select(i => Parameter.SplitValues(parameterTable.GetValue(i)).DisplayValue).ToList();

                    SortAndGetCounts(displayValues, out var countTotal, out var countFilled, out var countNullish, out var countDistinct);

                    var name = descriptorTable.GetName(descriptorIndex);

                    var ps = new ParameterSummary()
                    {
                        SummaryKey = parameterSummaryKey.ToString(),
                        Descriptor = descriptorIndex,
                        ElementKindEnum = elementKind,
                        ElementKind = elementKind.ToDisplayString(),
                        CategoryNameFull = categoryTable.GetNameFull(categoryIndex),
                        Name = name,
                        NamePbiCaseSensitive = name.ToPbiCaseSensitiveString(),
                        Group = descriptorTable.GetGroup(descriptorIndex),
                        CountTotal = countTotal,
                        CountFilled = countFilled,
                        CountNullish = countNullish,
                        CountDistinct = countDistinct
                    };

                    return ps;
                });
        }

        public static void SortAndGetCounts(List<string> items, out int countTotal, out int countFilled, out int countNullish, out int countDistinct)
        {
            countTotal = items.Count;
            countFilled = 0;
            countNullish = 0;
            countDistinct = 0;

            if (countTotal == 0)
                return;

            // Sort the collection in place (O(N log N))
            items.Sort();

            // Count (O(N))
            for (var i = 0; i < items.Count; ++i)
            {
                var item = items[i];

                var (hasValue, isNullish) = Parameter.GetValueInfo(item);

                if (hasValue)
                {
                    countFilled += 1;
                }

                if (isNullish)
                {
                    countNullish += 1;
                }

                // Compare current item with the previous one
                if (i == 0)
                {
                    countDistinct = 1;
                }
                else
                {
                    if (item != items[i - 1])
                    {
                        countDistinct += 1;
                    }
                }
            }
        }
    }

    public class ParameterSummaryKey : IEquatable<ParameterSummaryKey>
    {
        public int DescriptorIndex { get; }

        public ElementKind ElementKind { get; }

        public int CategoryIndex { get; }

        /// <summary>
        /// Constructor used for grouping
        /// </summary>
        public ParameterSummaryKey(
            int parameterIndex,
            ParameterTable parameterTable,
            ElementTable elementTable,
            ElementKind[] elementKindArray,
            CategoryTable categoryTable)
        {
            DescriptorIndex = parameterTable.GetParameterDescriptorIndex(parameterIndex);
            var elementIndex = parameterTable.GetElementIndex(parameterIndex);
            ElementKind = elementKindArray.ElementAtOrDefault(elementIndex, ElementKind.Unknown);
            CategoryIndex = elementTable.GetCategoryIndex(elementIndex);
        }

        public override bool Equals(object obj)
            => obj is ParameterSummaryKey psk && Equals(psk);

        public bool Equals(ParameterSummaryKey other)
            => DescriptorIndex == other.DescriptorIndex
            && ElementKind == other.ElementKind
            && CategoryIndex == other.CategoryIndex;

        public override int GetHashCode()
            => HashCodeStd2.Combine(DescriptorIndex, ElementKind, CategoryIndex);

        public override string ToString()
            => GetStringKey(DescriptorIndex, ElementKind, CategoryIndex);

        public static string GetStringKey(int descriptorIndex, ElementKind elementKind, int categoryIndex)
            => $"{descriptorIndex}|{(int)elementKind}|{categoryIndex}";
    }
}
