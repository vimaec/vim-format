using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    public class ParameterSummary
    {
        public int Descriptor { get; set; }

        public ElementKind ElementKind { get; set; }

        public string CategoryNameFull { get; set; }

        public string Name { get; set; } // Derived from Descriptor

        public string Group { get; set; } // Derived from Descriptor

        public int CountTotal { get; set; }

        public int CountFilled { get; set; }

        public int CountSuspicious { get; set; }

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
                .GroupBy(i =>
                {
                    var descriptorIndex = parameterTable.GetParameterDescriptorIndex(i);
                    var elementIndex = parameterTable.GetElementIndex(i);
                    var elementKind = elementKindArray.ElementAtOrDefault(elementIndex, ElementKind.Unknown);
                    var elementCategoryIndex = elementTable.GetCategoryIndex(elementIndex);

                    return (descriptorIndex, elementKind, elementCategoryIndex);
                })
                .AsParallel()
                .Select(g =>
                {
                    var (descriptorIndex, elementKind, categoryIndex) = g.Key;

                    var displayValues = g.Select(i => Parameter.SplitValues(parameterTable.GetValue(i)).DisplayValue).ToList();

                    SortAndGetCounts(displayValues, out var countTotal, out var countFilled, out var countSuspicious, out var countDistinct);

                    var ps = new ParameterSummary()
                    {
                        Descriptor = descriptorIndex,
                        ElementKind = elementKind,
                        CategoryNameFull = categoryTable.GetNameFull(categoryIndex),
                        Name = descriptorTable.GetName(descriptorIndex),
                        Group = descriptorTable.GetGroup(descriptorIndex),
                        CountTotal = countTotal,
                        CountFilled = countFilled,
                        CountSuspicious = countSuspicious,
                        CountDistinct = countDistinct
                    };

                    return ps;
                });
        }

        public static void SortAndGetCounts(List<string> items, out int countTotal, out int countFilled, out int countSuspicious, out int countDistinct)
        {
            countTotal = items.Count;
            countFilled = 0;
            countSuspicious = 0;
            countDistinct = 0;

            if (countTotal == 0)
                return;

            // Sort the collection in place (O(N log N))
            items.Sort();

            // Count (O(N))
            for (var i = 0; i < items.Count; ++i)
            {
                var item = items[i];

                if (!string.IsNullOrEmpty(item))
                {
                    countFilled += 1;

                    if (item == "-1" || item == "0")
                    {
                        countSuspicious += 1;
                    }
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
}
