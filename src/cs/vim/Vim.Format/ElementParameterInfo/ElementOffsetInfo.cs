using System;
using System.Collections.Generic;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Collects the "Top Offset" and "Base Offset" parameters
    /// </summary>
    public class ElementOffsetInfo : IElementIndex
    {
        public Element Element { get; }

        public int GetElementIndexOrNone()
            => Element.IndexOrDefault();

        public double? TopOffset { get; } = null;
        public double? TopOffsetRvtMetric => Units.FeetToMeters(TopOffset);
        public const string TopOffsetParameterName = "Top Offset";
        public static readonly ISet<string> TopOffsetIds = new HashSet<string>()
        {
            "-1001109", // WALL_TOP_OFFSET
            "-1001358", // FAMILY_TOP_LEVEL_OFFSET_PARAM
            "-1002066", // SCHEDULE_TOP_LEVEL_OFFSET_PARAM
            "-1007219", // STAIRS_TOP_OFFSET
            "-1114707", // ZONE_LEVEL_OFFSET_TOP
            "-1140751", // MATCHLINE_TOP_OFFSET
        };
        public static bool DescriptorIsTopOffset(ParameterDescriptor pd)
            => pd.Name.Equals(TopOffsetParameterName, StringComparison.InvariantCultureIgnoreCase) ||
               TopOffsetIds.Contains(pd.Guid);

        public double? BaseOffset { get; } = null;
        public double? BaseOffsetRvtMetric => Units.FeetToMeters(BaseOffset);
        public const string BaseOffsetParameterName = "Base Offset";
        public static readonly ISet<string> BaseOffsetIds = new HashSet<string>()
        {
            "-1001108", // WALL_BASE_OFFSET
            "-1001357", // FAMILY_BASE_LEVEL_OFFSET_PARAM
            "-1001701", // ROOF_LEVEL_OFFSET_PARAM
            "-1002065", // SCHEDULE_BASE_LEVEL_OFFSET_PARAM
            "-1006925", // ROOM_LOWER_OFFSET
            "-1006927", // ALWAYS_ZERO_LENGTH
            "-1007218", // STAIRS_BASE_OFFSET
            "-1008621", // STAIRS_RAILING_HEIGHT_OFFSET
        };
        public static bool DescriptorIsBaseOffset(ParameterDescriptor pd)
            => pd.Name.Equals(BaseOffsetParameterName, StringComparison.InvariantCultureIgnoreCase) ||
               BaseOffsetIds.Contains(pd.Guid);

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementOffsetInfo(Element element, ParameterTable parameterTable, VimElementIndexMaps elementIndexMaps)
        {
            Element = element;

            var elementIndex = GetElementIndexOrNone();
            if (elementIndex == EntityRelation.None)
                return;

            var parameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);

            foreach (var paramIndex in parameterIndices)
            {
                var p = parameterTable.Get(paramIndex);
                var pd = p.ParameterDescriptor;

                if (DescriptorIsTopOffset(pd))
                {
                    var (nativeValue, _) = p.Values;
                    var topOffset = Parameter.ParseNativeValueAsDouble(nativeValue);
                    if (!topOffset.HasValue)
                        continue;

                    TopOffset = !TopOffset.HasValue
                        ? topOffset
                        : Math.Max(TopOffset.Value, topOffset.Value);
                }

                if (DescriptorIsBaseOffset(pd))
                {
                    var (nativeValue, _) = p.Values;
                    var baseOffset = Parameter.ParseNativeValueAsDouble(nativeValue);
                    if (!baseOffset.HasValue)
                        continue;

                    BaseOffset = !BaseOffset.HasValue
                        ? baseOffset
                        : Math.Max(BaseOffset.Value, baseOffset.Value);
                }
            }
        }
    }
}
