using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    public class ParameterSummary : IElementKindInfo
    {
        public string SummaryKey { get; set; }

        public int Descriptor { get; set; }

        public int ElementKindEnum { get; set; }

        public string ElementKind { get; set; }

        public bool ElementKindIsLeaf { get; set; }

        public string CategoryNameFull { get; set; }

        public string Name { get; set; } // Derived from Descriptor

        public string NamePbiCaseSensitive { get; set; }

        public string Group { get; set; } // Derived from Descriptor

        public string Guid { get; set; }

        public bool IsShared { get; set; }

        public string Flag { get; set; }

        public int CountTotal { get; set; }

        public int CountEmpty
            => CountTotal - CountFilled;

        public float PercentEmpty
            => CountTotal == 0 ? 0.0f : (float)CountEmpty / CountTotal;

        public int CountFilled { get; set; }

        public float PercentFilled
            => CountTotal == 0 ? 0.0f : (float)CountFilled / CountTotal;

        public int CountDubious { get; set; }

        public float PercentDubious
            => CountTotal == 0 ? 0.0f : (float)CountDubious / CountTotal;

        public int CountDistinct { get; set; }

        public int CountQuality
            => CountFilled - CountDubious;

        public float PercentQuality
            => CountFilled == 0 ? 0.0f : (float)CountQuality / CountFilled;

        public float PercentOverall
            => PercentFilled * PercentQuality;

        public enum Rating
        {
            Perfect = 1,
            Excellent = 2,
            Good = 3,
            Fair = 4,
            Poor = 5,
            SingleValue = 6,
            Empty = 7
        }

        public const float ExcellentThreshold = .90f;
        public const float GoodThreshold = .75f;
        public const float FairThreshold = .5f;

        // Number prefixes are used to facilitate sorting.
        public const string PerfectString = "1-Perfect";
        public const string ExcellentString = "2-Excellent";
        public const string GoodString = "3-Good";
        public const string FairString = "4-Fair";
        public const string PoorString = "5-Poor";
        public const string SingleValueString = "6-Single Value";
        public const string EmptyValueString = "7-Empty";

        public static string GetRatingString(Rating rating)
        {
            switch (rating)
            {
                case Rating.Perfect: return PerfectString;
                case Rating.Excellent: return ExcellentString;
                case Rating.Good: return GoodString;
                case Rating.Fair: return FairString;
                case Rating.Poor: return PoorString;
                case Rating.SingleValue: return SingleValueString;
                case Rating.Empty: return EmptyValueString;
                default: return "Unknown Rating";
            };
        }

        public const string PerfectColor = "#98CC73";
        public const string ExcellentColor = "#b1dd92";
        public const string GoodColor = "#dbdd92";
        public const string FairColor = "#ffe299";
        public const string PoorColor = "#ffa9cb";
        public const string SingleValueColor = "#ff9191";
        public const string EmptyValueColor = "#ff9191";

        public static string GetRatingColor(Rating rating)
        {
            switch (rating)
            {
                case Rating.Perfect: return PerfectColor;
                case Rating.Excellent: return ExcellentColor;
                case Rating.Good: return GoodColor;
                case Rating.Fair: return FairColor;
                case Rating.Poor: return PoorColor;
                case Rating.SingleValue: return SingleValueColor;
                case Rating.Empty: return EmptyValueColor;
                default: return "#FFFFFF";
            }
        }

        public Rating ScoreFilledValue
        {
            get
            {
                // If there is only one distinct value, this information takes precedence.
                if (CountEmpty == CountTotal)                 return Rating.Empty;
                else if (CountDistinct == 1)                  return Rating.SingleValue;
                else if (CountEmpty == 0)                     return Rating.Perfect;
                else if (PercentFilled >= ExcellentThreshold) return Rating.Excellent;
                else if (PercentFilled >= GoodThreshold)      return Rating.Good;
                else if (PercentFilled >= FairThreshold)      return Rating.Fair;
                else                                          return Rating.Poor;
            }
        }

        public string ScoreFilled
            => GetRatingString(ScoreFilledValue);

        public string ScoreFilledColor
            => GetRatingColor(ScoreFilledValue);

        public Rating ScoreQualityValue
        {
            get
            {
                // If there is only one distinct value, this information takes precedence.
                if (CountEmpty == CountTotal)                  return Rating.Empty;
                else if (CountDistinct == 1)                   return Rating.SingleValue;
                else if (CountDubious == 0)                    return Rating.Perfect;
                else if (PercentQuality >= ExcellentThreshold) return Rating.Excellent;
                else if (PercentQuality >= GoodThreshold)      return Rating.Good;
                else if (PercentQuality >= FairThreshold)      return Rating.Fair;
                else                                           return Rating.Poor;
            }
        }

        public string ScoreQuality
            => GetRatingString(ScoreQualityValue);

        public string ScoreQualityColor
            => GetRatingColor(ScoreQualityValue);

        public Rating ScoreOverallValue
        {
            get
            {
                // If there is only one distinct value, this information takes precedence.
                if (CountEmpty == CountTotal)                  return Rating.Empty;
                else if (CountDistinct == 1)                   return Rating.SingleValue;
                else if (CountDubious == 0 && CountEmpty == 0) return Rating.Perfect;
                else if (PercentOverall >= ExcellentThreshold) return Rating.Excellent;
                else if (PercentOverall >= GoodThreshold)      return Rating.Good;
                else if (PercentOverall >= FairThreshold)      return Rating.Fair;
                else                                           return Rating.Poor;
            }
        }

        public string ScoreOverall
            => GetRatingString(ScoreOverallValue);

        public string ScoreOverallColor
            => GetRatingColor(ScoreOverallValue);

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
                    var elementKindValue = parameterSummaryKey.ElementKind;
                    var categoryIndex = parameterSummaryKey.CategoryIndex;

                    var displayValues = g.Select(i => Parameter.SplitValues(parameterTable.GetValue(i)).DisplayValue).ToList();

                    SortAndGetCounts(displayValues, out var countTotal, out var countFilled, out var countDubious, out var countDistinct);

                    var name = descriptorTable.GetName(descriptorIndex);

                    var ps = new ParameterSummary()
                    {
                        SummaryKey = parameterSummaryKey.ToString(),
                        Descriptor = descriptorIndex,
                        CategoryNameFull = categoryTable.GetNameFull(categoryIndex),
                        Name = name,
                        NamePbiCaseSensitive = name.ToPbiCaseSensitiveString(),
                        Group = descriptorTable.GetGroup(descriptorIndex),
                        Guid = descriptorTable.GetGuid(descriptorIndex),
                        IsShared = descriptorTable.GetIsShared(descriptorIndex),
                        Flag = ParameterDescriptor.GetParameterDescriptorFlagString((ParameterDescriptorFlag) descriptorTable.GetFlags(descriptorIndex)),
                        CountTotal = countTotal,
                        CountFilled = countFilled,
                        CountDubious = countDubious,
                        CountDistinct = countDistinct
                    };

                    ps.SetElementKindInfo(elementKindValue);

                    return ps;
                });
        }

        public static void SortAndGetCounts(
            List<string> items,
            out int countTotal,
            out int countFilled,
            out int countDubious,
            out int countDistinct)
        {
            countTotal = items.Count;
            countFilled = 0;
            countDubious = 0;
            countDistinct = 0;

            if (countTotal == 0)
                return;

            // Sort the collection in place (O(N log N))
            items.Sort();

            // Count (O(N))
            for (var i = 0; i < items.Count; ++i)
            {
                var item = items[i];

                var (hasValue, isDubious) = Parameter.GetValueInfo(item);

                if (hasValue)
                {
                    countFilled += 1;
                }

                if (isDubious)
                {
                    countDubious += 1;
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
