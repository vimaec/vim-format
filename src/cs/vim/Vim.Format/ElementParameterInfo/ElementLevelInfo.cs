using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Util;

// ReSharper disable InconsistentNaming

// SOME BACKGROUND INFORMATION ABOUT REVIT LEVELS AND ELEMENTS
//
// by: Martin Ashton, July 29, 2025
//
// Elements in Revit are not always directly associated to a level via their Element.Level property.
// In some cases, the element's level must be calculated or inferred. Here is the ordering of calculated level:
//
//   1. "Schedule Level"
//     - Corresponds to the schedule level parameter on an element which specifically defines its level.
//     - Not always present; the parameter must be explicitly assigned.
//
//   2. "Level"
//     - Corresponds to the Element.Level property.
//     - Not always present; if element (A) is hosted on another element (B),
//       then (A) will have an empty Element.Level property.
//
//   3. "Host Level":
//     - Corresponds to the host level parameter on an element. This parameter may point to either
//       a Level element or another element. In the later case, we infer that the host level is
//       the level of the host element.
//     - Not always present; the element must be hosted on either a level or another element.
//
//   4. "Reference Level"
//     - Corresponds to the reference level parameter on an element.
//     - Not always present; the element must be associated with a reference level.
//
//   5. "Base Level"
//     - Corresponds to the base level constraint parameter on an element.
//     - Not always present; the element must be constrained.
//
// We refer to the "Primary" level as the first non-null level association among the ones listed above.

namespace Vim.Format.ElementParameterInfo
{
    public enum PrimaryLevelKind
    {
        Unknown = 0,
        ScheduleLevel = 1,
        Level = 2,
        HostLevel = 3,
        ReferenceLevel = 4,
        BaseLevel = 5,
    }

    public enum BuildingStoryGeometryContainment
    {
        // Definitions:
        // min & max: element geometry bounding box min.z and max.z
        // lvlLow = LevelBuildingStoryCurrentOrBelow.z
        // lvlHi = LevelBuildingStoryAbove.z
        Unknown = 0,                // lvlLow and lvlHi are unknown
        CompletelyBelow = 1,        // min < max < lvlLow < lvlHi
        CrossingBelow = 2,          // min < lvlLow < max < lvlHi
        Contained = 3,              // lvlLow < min < max < lvlHi
        CrossingAbove = 4,          // lvlLow < min < lvlHi < max
        CompletelyAbove = 5,        // lvlLow < lvlHi < min < max
        SpanningBelowAndAbove = 6,  // min < lvlLow < lvlHi < max
        NoGeometry = 7,             // The element does not have geometry.
    }

    public class ElementLevelInfo : IElementIndex
    {
        /// <summary>
        /// The element.
        /// </summary>
        public Element Element { get; }

        /// <summary>
        /// Returns the element index.
        /// </summary>
        public int GetElementIndexOrNone()
            => EntityRelation.IndexOrDefault(Element);

        /// <summary>
        /// The schedule level associated to the element parameters. Can be null.
        /// </summary>
        public LevelInfo ScheduleLevelInfo { get; }
        //public const string TypeId_ScheduleLevel = "autodesk.revit.parameter:instanceScheduleOnlyLevelParam";
        public const string BuiltInId_ScheduleLevel = "-1001365";
        public static readonly HashSet<string> ScheduleLevelBuiltInIds = new HashSet<string> { BuiltInId_ScheduleLevel };

        /// <summary>
        /// The level associated with the element. Can be null.
        /// </summary>
        public LevelInfo LevelInfo { get; }

        /// <summary>
        /// The level of the host element (or the level element) if the element is hosted. Can be null.
        /// </summary>
        public LevelInfo HostLevelInfo { get; }

        /// <summary>
        /// The level of the reference of the element. Can be null.
        /// </summary>
        public LevelInfo ReferenceLevelInfo { get; }

        // NOTE: the values are correlated to the parameters named "Reference Level" in the BuiltInParameter enumerations https://www.revitapidocs.com/2025/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm
        public static readonly HashSet<string> ReferenceLevelBuiltInIds = new HashSet<string>
        {
            "-1001383", // InstanceReferenceLevel,
            "-1001651", // RoofConstraintLevel,
            "-1001715", // FaceRoofLevel,
            "-1114000", // RbsStartLevel,
            "-1114817", // SpaceReferenceLevel,
            "-1133500", // GroupLevel,
            "-1140709", // TrussLevel
            "-1140916", // FabricationLevel,
            "-1154630", // MultiStoryStairLevel
        };

        /// <summary>
        /// The base level of the element if it is constrained. Can be null.
        /// </summary>
        public LevelInfo BaseLevelInfo { get; }

        // NOTE: the values are correlated to the parameters named "Base Level" in the BuiltInParameter enumerations https://www.revitapidocs.com/2025/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm
        public static readonly HashSet<string> BaseLevelBuiltInIds = new HashSet<string>
        {
            "-1001708", // RoofBaseLevel,
            "-1002063", // ScheduleBaseLevel,
            "-1007200", // StairsBaseLevelParam,
            "-1151101", // StairsBaseLevel,
            "-1008620", // StairsRailingBaseLevel,
            "-1152335", // DpartBaseLevel,
            "-1152336", // DpartBaseLevelByOriginal
        };

        /// <summary>
        /// The primary level associated with the element. Can be null.
        /// </summary>
        public LevelInfo PrimaryLevelInfo
        {
            get
            {
                if (ScheduleLevelInfo != null)
                    return ScheduleLevelInfo;

                if (LevelInfo != null)
                    return LevelInfo;

                if (HostLevelInfo != null)
                    return HostLevelInfo;

                if (ReferenceLevelInfo != null)
                    return ReferenceLevelInfo;

                if (BaseLevelInfo != null)
                    return BaseLevelInfo;

                return null;
            }
        }

        /// <summary>
        /// The primary level kind associated with the element.
        /// </summary>
        public PrimaryLevelKind PrimaryLevelKind
        {
            get
            {
                if (ScheduleLevelInfo != null)
                    return PrimaryLevelKind.ScheduleLevel;

                if (LevelInfo != null)
                    return PrimaryLevelKind.Level;

                if (HostLevelInfo != null)
                    return PrimaryLevelKind.HostLevel;

                if (ReferenceLevelInfo != null)
                    return PrimaryLevelKind.ReferenceLevel;

                if (BaseLevelInfo != null)
                    return PrimaryLevelKind.BaseLevel;

                return PrimaryLevelKind.Unknown;
            }
        }

        /// <summary>
        /// The building story above the primary level.
        /// Null if the primary level is null.
        /// </summary>
        public LevelInfo BuildingStoryAbovePrimaryLevelInfo { get; }

        /// <summary>
        /// The building story below the primary level if the primary level is not a building story, or the primary level if it is a building story.
        /// Null if the primary level is null.
        /// </summary>
        public LevelInfo BuildingStoryCurrentOrBelowPrimaryLevelInfo { get; }

        /// <summary>
        /// The containment type of the element's geometry relative to the BuildingStoryAbove and the BuildingStoryCurrentOrBelow.
        /// </summary>
        public BuildingStoryGeometryContainment BuildingStoryGeometryContainment { get; }

        /// <summary>
        /// The building story immediately below the element's geometry minimum z coordinate. Can be null.
        /// </summary>
        public LevelInfo BuildingStoryGeometryMinLevelInfo { get; }

        /// <summary>
        /// The building story immediately below the element's geometry maximum z coordinate. Can be null.
        /// </summary>
        public LevelInfo BuildingStoryGeometryMaxLevelInfo { get; }

        /// <summary>
        /// The default tolerance value for the geometry level containment calculation.
        /// </summary>
        public const double DefaultGeometryContainmentTolerance = 0.001d;

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementLevelInfo(
            Element element,
            ElementTable elementTable,
            FamilyInstanceTable familyInstanceTable,
            LevelTable levelTable,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps,
            VimElementGeometryInfo[] elementGeometryMap,
            IReadOnlyDictionary<int, LevelInfo> levelInfoMap,
            IReadOnlyList<LevelInfo> orderedLevelInfosByProjectElevation,
            IReadOnlyDictionary<long, LevelInfo> elementIdToLevelInfoMap,
            double geometryContainmentTolerance = DefaultGeometryContainmentTolerance)
        {
            Element = element;

            var elementIndex = GetElementIndexOrNone();

            // The level index of the element
            if (elementIndex != EntityRelation.None)
            {
                var elementLevelIndex = elementTable.GetLevelIndex(elementIndex);
                if (elementLevelIndex != EntityRelation.None &&
                    levelInfoMap.TryGetValue(elementLevelIndex, out var levelInfo))
                {
                    LevelInfo = levelInfo;
                }
            }

            if (TryGetHostLevel(element, elementTable, familyInstanceTable, levelTable, elementIndexMaps, elementIdToLevelInfoMap, out var hostLevelInfo))
                HostLevelInfo = hostLevelInfo;

            var elementParameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);
            foreach (var parameterIndex in elementParameterIndices)
            {
                var p = parameterTable.Get(parameterIndex);
                var (nativeValue, _) = p.Values;
                var d = p.ParameterDescriptor;

                // NOTE: Guid is either the built-in ID (if the parameter is built-in), or a guid (if the parameter is shared).
                var paramNameLowerInvariant = d.Name.ToLowerInvariant();
                var builtInId = d.Guid;

                if (ScheduleLevelInfo == null &&
                    TryGetLevelInfoFromParameter(nativeValue, builtInId, paramNameLowerInvariant, "schedule level", ScheduleLevelBuiltInIds, elementIdToLevelInfoMap, out var scheduleLevelInfo))
                {
                    ScheduleLevelInfo = scheduleLevelInfo;
                }
                else if (
                    ReferenceLevelInfo == null &&
                    TryGetLevelInfoFromParameter(nativeValue, builtInId, paramNameLowerInvariant, "reference level", ReferenceLevelBuiltInIds, elementIdToLevelInfoMap, out var referenceLevelInfo))
                {
                    ReferenceLevelInfo = referenceLevelInfo;
                }
                else if (
                    BaseLevelInfo == null &&
                    TryGetLevelInfoFromParameter(nativeValue, builtInId, paramNameLowerInvariant, "base level", BaseLevelBuiltInIds, elementIdToLevelInfoMap, out var baseLevelInfo))
                {
                    BaseLevelInfo = baseLevelInfo;
                }
            }

            BuildingStoryGeometryContainment = GetBuildingStoryGeometryContainment(
                elementIndex,
                PrimaryLevelInfo?.Level?.ProjectElevation,
                orderedLevelInfosByProjectElevation,
                elementGeometryMap,
                geometryContainmentTolerance,
                out var maybeBuildingStoryAbove,
                out var maybeBuildingStoryCurrentOrBelow,
                out var maybeBuildingStoryGeometryMin,
                out var maybeBuildingStoryGeometryMax);

            BuildingStoryAbovePrimaryLevelInfo = maybeBuildingStoryAbove;
            BuildingStoryCurrentOrBelowPrimaryLevelInfo = maybeBuildingStoryCurrentOrBelow;
            BuildingStoryGeometryMinLevelInfo = maybeBuildingStoryGeometryMin;
            BuildingStoryGeometryMaxLevelInfo = maybeBuildingStoryGeometryMax;
        }

        /// <summary>
        /// Returns the host level of the element.
        /// </summary>
        private static bool TryGetHostLevel(
            Element element,
            ElementTable elementTable,
            FamilyInstanceTable familyInstanceTable,
            LevelTable levelTable,
            VimElementIndexMaps elementIndexMaps,
            IReadOnlyDictionary<long, LevelInfo> elementIdToLevelInfoMap,
            out LevelInfo hostLevelInfo)
        {
            hostLevelInfo = null;

            if (!elementIndexMaps.FamilyInstanceIndexFromElementIndex.TryGetValue(element.Index, out var familyInstanceIndex))
                return false;

            var hostElementIndex = familyInstanceTable.GetHostIndex(familyInstanceIndex);
            if (hostElementIndex == EntityRelation.None)
                return false;

            var hostElementId = elementTable.GetId(hostElementIndex);

            // If the host element is a level, use it.
            if (elementIdToLevelInfoMap.TryGetEntityFromElementId(hostElementId, out hostLevelInfo))
                return true;

            // If the host element is just a regular instance, then return the host element's level.
            var hostElementLevelIndex = elementTable.GetLevelIndex(hostElementIndex);
            if (hostElementLevelIndex == EntityRelation.None)
                return false;

            var hostElementLevelElementIndex = levelTable.GetElementIndex(hostElementLevelIndex);
            if (hostElementLevelElementIndex == EntityRelation.None)
                return false;

            var hostElementLevelElementId = elementTable.GetId(hostElementLevelElementIndex);

            return elementIdToLevelInfoMap.TryGetEntityFromElementId(hostElementLevelElementId, out hostLevelInfo);
        }

        /// <summary>
        /// Returns a level info based on the given built-in parameter id set, if present.
        /// </summary>
        private static bool TryGetLevelInfoFromParameter(
            string nativeValue,
            string builtInId,
            string paramNameLowerInvariant,
            string expectedParamNameLowerInvariant,
            HashSet<string> builtInIds,
            IReadOnlyDictionary<long, LevelInfo> elementIdToLevelInfoMap,
            out LevelInfo levelInfo)
        {
            levelInfo = null;

            if (paramNameLowerInvariant.Equals(expectedParamNameLowerInvariant) || builtInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsElementId(nativeValue, out var levelElementId) &&
                       elementIdToLevelInfoMap.TryGetEntityFromElementId(levelElementId, out levelInfo);
            }

            return false;
        }

        /// <summary>
        /// Returns the building story info and calculates the geometry
        /// </summary>
        private static BuildingStoryGeometryContainment GetBuildingStoryGeometryContainment(
            int elementIndex,
            double? primaryProjectElevation,
            IReadOnlyList<LevelInfo> orderedLevelInfosByProjectElevation,
            VimElementGeometryInfo[] elementGeometryMap,
            double geometryContainmentTolerance,
            out LevelInfo maybeBuildingStoryAbove,
            out LevelInfo maybeBuildingStoryCurrentOrBelow,
            out LevelInfo maybeBuildingStoryGeometryMin,
            out LevelInfo maybeBuildingStoryGeometryMax)
        {
            maybeBuildingStoryAbove = null;
            maybeBuildingStoryCurrentOrBelow = null;
            maybeBuildingStoryGeometryMin = null;
            maybeBuildingStoryGeometryMax = null;

            if (primaryProjectElevation == null)
                return BuildingStoryGeometryContainment.Unknown;

            // Note: Level.ProjectElevation is relative to the internal scene origin (0,0,0), and so is the vim scene's geometry.
            var elementGeometryInfo = elementGeometryMap.ElementAtOrDefault(elementIndex);

            var hasGeometry = elementGeometryInfo?.HasMesh ?? false;
            if (!hasGeometry)
                return BuildingStoryGeometryContainment.NoGeometry;

            var bb = elementGeometryInfo.WorldSpaceBoundingBox;
            var bbIsValid = bb.IsValid;
            var bbMin = bb.Min.Z;
            var bbMax = bb.Max.Z;

            // Iterate over all the levels to compare project elevations.
            foreach (var levelInfo in orderedLevelInfosByProjectElevation)
            {
                var levelProjectElevation = levelInfo.Level.ProjectElevation;

                if (!levelInfo.IsBuildingStory)
                    continue;

                if (levelProjectElevation <= primaryProjectElevation)
                {
                    // Find the building story below or at the primary level.
                    maybeBuildingStoryCurrentOrBelow = levelInfo;
                }

                if (maybeBuildingStoryAbove == null && levelProjectElevation > primaryProjectElevation)
                {
                    // Find the first building story above the primary level.
                    maybeBuildingStoryAbove = levelInfo;
                }

                if (bbIsValid && bbMin >= levelProjectElevation)
                {
                    // Find the building story below or at the geometric minimum.
                    maybeBuildingStoryGeometryMin = levelInfo;
                }

                if (bbIsValid && bbMax >= levelProjectElevation)
                {
                    // Find the first building story below or at the geometric maximum.
                    maybeBuildingStoryGeometryMax = levelInfo;
                }
            }

            var maybeLvlLow = maybeBuildingStoryCurrentOrBelow?.Level.ProjectElevation;
            var maybeLvlHi = maybeBuildingStoryAbove?.Level.ProjectElevation;

            // Case 1: both are null, so unknown containment.
            if (maybeLvlLow == null && maybeLvlHi == null)
                return BuildingStoryGeometryContainment.Unknown;

            // Case 2:
            //
            //                                    v CrossingAbove 
            //
            //                                          v CompletelyAbove
            //
            //                                          _           _
            //                                         | |         | |
            //                                    _    | |         | |
            //                                   | |   |_|         | |
            //    --------------------------_----| |----------_----| |--- lvlHi
            //                             | |   |_|         | |   | |
            //                        _    | |               | |   | |
            //                       | |   |_|               | |   | |
            //                  _    | |                     | |   | |
            //                 | |   |_|                     |_|   | |
            //    --------_----| |---------------------------------| |--- lvlLow
            //           | |   |_|                                 | |
            //           | |                                       | | 
            //           |_|                                       |_|
            //
            //                        ^____^_____Contained___^ 
            //                                         
            //                  ^ CrossingBelow                     ^ SpanningBelowAndAbove
            //
            //            ^ CompletelyBelow
            //
            // bbMin_LessThan_LvlLow
            // bbMin_LessThanOrEqualTo_LvlLow
            // bbMin_GreaterThanOrEqualTo_LvlLow
            // bbMin_GreaterThan_LvlLow
            // bbMin_LessThan_LvlHi
            // bbMin_LessThanOrEqualTo_LvlHi
            // bbMin_GreaterThanOrEqualTo_LvlHi
            // bbMin_GreaterThan_LvlHi
            //
            // bbMax_LessThan_LvlLow
            // bbMax_LessThanOrEqualTo_LvlLow
            // bbMax_GreaterThanOrEqualTo_LvlLow
            // bbMax_GreaterThan_LvlLow
            // bbMax_LessThan_LvlHi
            // bbMax_LessThanOrEqualTo_LvlHi
            // bbMax_GreaterThanOrEqualTo_LvlHi
            // bbMax_GreaterThan_LvlHi
            //
            // CompletelyBelow:       bbMin_LessThan_LvlLow && bbMax_LessThan_LvlLow
            // CrossingBelow:         bbMin_LessThan_LvlLow && bbMax_GreaterThanOrEqualTo_lvlLow && bbMax_LessThanOrEqualTo_LvlHi
            // Contained:             bbMin_GreaterThanOrEqualTo_LvlLow && bbMax_LessThanOrEqualTo_LvlHi
            // CrossingAbove:         bbMin_GreaterThanOrEqualTo_LvlLow && bbMin_LessThanOrEqualTo_LvlHi && bbMax_GreaterThan_LvlHi
            // CompletelyAbove:       bbMin_GreaterThan_LvlHi && bbMax_GreaterThan_LvlHi
            // SpanningBelowAndAbove: bbMin_LessThan_LvlLow && bbMax_GreaterThan_LvlHi

            // Assume min/max values if one of the levels is null.
            var lvlLow = maybeLvlLow ?? double.MinValue;
            var lvlHi = maybeLvlHi ?? double.MaxValue;

            // Nudge the bounding box min/max z values to avoid numeric precision issues.
            var nudge = Math.Abs(geometryContainmentTolerance);
            var nudgedBbMin = bbMin + nudge;
            var nudgedBbMax = bbMax - nudge;

            if (nudgedBbMin < lvlLow && nudgedBbMax < lvlLow)
                return BuildingStoryGeometryContainment.CompletelyBelow;

            if (nudgedBbMin < lvlLow && nudgedBbMax >= lvlLow && nudgedBbMax <= lvlHi)
                return BuildingStoryGeometryContainment.CrossingBelow;

            if (nudgedBbMin >= lvlLow && nudgedBbMax <= lvlHi)
                return BuildingStoryGeometryContainment.Contained;

            if (nudgedBbMin >= lvlLow && nudgedBbMin <= lvlHi && nudgedBbMax > lvlHi)
                return BuildingStoryGeometryContainment.CrossingAbove;

            if (nudgedBbMin > lvlHi && nudgedBbMax > lvlHi)
                return BuildingStoryGeometryContainment.CompletelyAbove;

            if (nudgedBbMin < lvlLow && nudgedBbMax > lvlHi)
                return BuildingStoryGeometryContainment.SpanningBelowAndAbove;

            Debug.Fail($"Unexpected geometry containment case. nudgedBbMin: {nudgedBbMin}, nudgedBbMax: {nudgedBbMax}, lvlLow: {lvlLow}, lvlHi: {lvlHi}");

            return BuildingStoryGeometryContainment.Unknown;
        }

        public string PropertiesToString()
            => string.Join(Environment.NewLine, this.PropertiesToStrings());
    }
}
