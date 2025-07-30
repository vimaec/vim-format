using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

// ReSharper disable InconsistentNaming

// SOME BACKGROUND INFORMATION ABOUT REVIT LEVELS AND FAMILY INSTANCES
//
// by: Martin Ashton, July 29, 2025
//
// Family instance elements in Revit are not always directly associated to a level via their Element.Level property.
// In some cases, the element's level must be calculated or inferred. Here is the ordering of calculated level:
//
//   1. "Schedule Level"
//     - Corresponds to the schedule level parameter on a family instance which specifically defines its level.
//     - Not always present; the parameter must be explicitly assigned.
//
//   2. "Level"
//     - Corresponds to the Element.Level property.
//     - Not always present; if family instance (A) is hosted on another family instance (B),
//       then (A) will have an empty Element.Level property.
//
//   3. "Host Level":
//     - Corresponds to the host level parameter on a family instance. This parameter may point to either
//       a Level element or another family instance. In the later case, we infer that the host level is
//       the level of the host family instance.
//     - Not always present; the family instance must be hosted on either a level or another instance.
//
//   4. "Reference Level"
//     - Corresponds to the reference level parameter on a family instance.
//     - Not always present; the family instance must be associated with a reference level.
//
//   5. "Base Level"
//     - Corresponds to the base level constraint parameter on a family instance.
//     - Not always present; the family instance must be constrained.
//
// We refer to the "Primary" level as the first non-null level association among the ones listed above.

namespace Vim.Format.Levels
{
    public enum PrimaryLevelKind
    {
        Unknown = 0,
        ScheduleLevel = 1,
        Level = 2,
        HostLevel = 3,
        ReferenceLevel = 4,
        BaseLevel = 5,
        GroupLevel = 6,
        SystemLevel = 7
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
    }

    public class FamilyInstanceLevelInfo : IElementIndex
    {
        /// <summary>
        /// The family instance.
        /// </summary>
        public FamilyInstance FamilyInstance { get; }

        /// <summary>
        /// Returns the element index of the family instance.
        /// </summary>
        public int GetElementIndexOrNone()
            => FamilyInstance.GetElementIndexOrNone();

        /// <summary>
        /// The schedule level associated to the family instance's element parameters. Can be null.
        /// </summary>
        public LevelInfo ScheduleLevelInfo { get; }
        //public const string TypeId_ScheduleLevel = "autodesk.revit.parameter:instanceScheduleOnlyLevelParam";
        public const string BuiltInId_ScheduleLevel = "-1001365";
        public static readonly HashSet<string> ScheduleLevelBuiltInIds = new HashSet<string> { BuiltInId_ScheduleLevel };

        /// <summary>
        /// The level associated with the family instance's element. Can be null.
        /// </summary>
        public LevelInfo LevelInfo { get; }

        /// <summary>
        /// The level of the host element (or the level element) if the family instance is hosted. Can be null.
        /// </summary>
        public LevelInfo HostLevelInfo { get; }

        /// <summary>
        /// The level of the reference of the family instance. Can be null.
        /// </summary>
        public LevelInfo ReferenceLevelInfo { get; }

        // NOTE 1: the BuiltInId_ values are correlated to the parameters named "Reference Level" in the BuiltInParameter enumerations https://www.revitapidocs.com/2025/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm
        // NOTE 2: The TypeId_ values are kept here for reference - they were added in Revit 2022 and beyond, however to keep comparisons simple, we just use the built-in IDs which Revit continues to use internally.
        //         These TypeId values were obtained by exporting a VIM file after building Vim.Revit.Core with <DefineConstants>$(DefineConstants);COLLECT_ALL_PARAMETER_TYPE_IDS</DefineConstants>,
        //         which writes all the built-in parameter ForgeTypeIds into the log file.

        // "Reference Level" for instances
        //public const string TypeId_InstanceReferenceLevel = "autodesk.revit.parameter:instanceReferenceLevelParam";
        public const string BuiltInId_InstanceReferenceLevel = "-1001383";

        // "Reference Level" for roof constraints
        //public const string TypeId_RoofConstraintLevel = "autodesk.revit.parameter:roofConstraintLevelParam";
        public const string BuiltInId_RoofConstraintLevel = "-1001651";

        // "Reference Level" for faceroof (???)
        //public const string TypeId_FaceRoofLevel = "autodesk.revit.parameter:faceroofLevelParam";
        public const string BuiltInId_FaceRoofLevel = "-1001715";

        // "Reference Level" for rbs (???)
        //public const string TypeId_RbsStartLevel = "autodesk.revit.parameter:rbsStartLevelParam";
        public const string BuiltInId_RbsStartLevel = "-1114000";

        // "Reference Level" for space (???)
        //public const string TypeId_SpaceReferenceLevel = "autodesk.revit.parameter:spaceReferenceLevelParam";
        public const string BuiltInId_SpaceReferenceLevel = "-1114817";

        // "Reference Level" for group
        //public const string TypeId_GroupLevel = "autodesk.revit.parameter:groupLevel";
        public const string BuiltInId_GroupLevel = "-1133500";

        // "Reference Level" for truss
        //public const string TypeId_TrussLevel = "autodesk.revit.parameter:trussElementReferenceLevelParam";
        public const string BuiltInId_TrussLevel = "-1140709";

        // "Reference Level" for fabrication
        //public const string TypeId_FabricationLevel = "autodesk.revit.parameter:fabricationLevelParam";
        public const string BuiltInId_FabricationLevel = "-1140916";

        // "Reference Level" for multistory stairs
        //public const string TypeId_MultiStoryStairLevel = "autodesk.revit.parameter:multistoryStairsRefLevel";
        public const string BuiltInId_MultiStoryStairLevel = "-1154630";

        public static readonly HashSet<string> ReferenceLevelBuiltInIds = new HashSet<string>
        {
            BuiltInId_InstanceReferenceLevel,
            BuiltInId_RoofConstraintLevel,
            BuiltInId_FaceRoofLevel,
            BuiltInId_RbsStartLevel,
            BuiltInId_SpaceReferenceLevel,
            BuiltInId_GroupLevel,
            BuiltInId_TrussLevel,
            BuiltInId_FabricationLevel,
            BuiltInId_MultiStoryStairLevel
        };

        /// <summary>
        /// The base level of the family instance if it is constrained. Can be null.
        /// </summary>
        public LevelInfo BaseLevelInfo { get; }

        // NOTE : the BuiltInId_ values are correlated to the parameters named "Base Level" in the BuiltInParameter enumerations https://www.revitapidocs.com/2025/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm

        // "Base Level" for roof
        //public const string TypeId_RoofBaseLevel = "autodesk.revit.parameter:roofBaseLevelParam";
        public const string BuiltInId_RoofBaseLevel = "-1001708";

        // "Base Level" for schedule
        //public const string TypeId_ScheduleBaseLevel = "autodesk.revit.parameter:scheduleBaseLevelParam";
        public const string BuiltInId_ScheduleBaseLevel = "-1002063";

        // "Base Level" for stairs
        //public const string TypeId_StairsBaseLevelParam = "autodesk.revit.parameter:stairsBaseLevelParam";
        public const string BuiltInId_StairsBaseLevelParam = "-1007200";

        // "Base Level" for stairs (another?)
        //public const string TypeId_StairsBaseLevel = "autodesk.revit.parameter:stairsBaseLevel";
        public const string BuiltInId_StairsBaseLevel = "-1151101";

        // "Base Level" for stair railing
        //public const string TypeId_StairsRailingBaseLevel = "autodesk.revit.parameter:stairsRailingBaseLevelParam";
        public const string BuiltInId_StairsRailingBaseLevel = "-1008620";

        // "Base Level" for dpart (???)
        //public const string TypeId_DpartBaseLevel = "autodesk.revit.parameter:dpartBaseLevel";
        public const string BuiltInId_DpartBaseLevel = "-1152335";

        // "Base Level" for dpart (??? DPART_BASE_LEVEL_BY_ORIGINAL ???)
        //public const string TypeId_DpartBaseLevelByOriginal = "autodesk.revit.parameter:dpartBaseLevelByOriginal";
        public const string BuiltInId_DpartBaseLevelByOriginal = "-1152336";

        public static readonly HashSet<string> BaseLevelBuiltInIds = new HashSet<string>
        {
            BuiltInId_RoofBaseLevel,
            BuiltInId_ScheduleBaseLevel,
            BuiltInId_StairsBaseLevelParam,
            BuiltInId_StairsBaseLevel,
            BuiltInId_StairsRailingBaseLevel,
            BuiltInId_DpartBaseLevel,
            BuiltInId_DpartBaseLevelByOriginal
        };

        /// <summary>
        /// The primary level associated with the family instance. Can be null.
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
        /// The primary level kind associated with the family instance.
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
        public LevelInfo BuildingStoryAbovePrimaryLevel { get; }

        /// <summary>
        /// The building story below the primary level if the primary level is not a building story, or the primary level if it is a building story.
        /// Null if the primary level is null.
        /// </summary>
        public LevelInfo BuildingStoryCurrentOrBelowPrimaryLevel { get; }

        /// <summary>
        /// The containment type of the family instance's geometry relative to the BuildingStoryAbove and the BuildingStoryCurrentOrBelow.
        /// </summary>
        public BuildingStoryGeometryContainment BuildingStoryGeometryContainment { get; }

        /// <summary>
        /// The building story immediately below the family instance's geometry minimum z coordinate. Can be null.
        /// </summary>
        public LevelInfo GeometryMinBuildingStory { get; }

        /// <summary>
        /// The building story immediately below the family instance's geometry maximum z coordinate. Can be null.
        /// </summary>
        public LevelInfo GeometryMaxBuildingStory { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FamilyInstanceLevelInfo(
            VimScene vimScene,
            FamilyInstance fi,
            IReadOnlyList<long> elementIds, // an optimization to avoid re-instantiating the object chain in calls to familyInstance.Host.Id or Element.Id
            IReadOnlyList<int> elementLevelIndices, // an optimization to avoid re-instantiating the object chain in calls to familyInstance.Element.Level
            IReadOnlyList<int> levelElementIndices, // an optimization to avoid re-instantiating the object chain in calls to Level.Element
            IReadOnlyList<LevelInfo> orderedLevelInfosByProjectElevation,
            IReadOnlyDictionary<long, LevelInfo> elementIdToLevelInfoMap,
            DictionaryOfLists<int, int> elementIndexToNodeIndicesMap) 
        {
            FamilyInstance = fi;

            var familyInstanceElementIndex = GetElementIndexOrNone();

            // The level index of the family instance's element
            if (familyInstanceElementIndex != EntityRelation.None)
            {
                var familyInstanceElementLevelIndex = elementLevelIndices[familyInstanceElementIndex];
                if (familyInstanceElementLevelIndex != EntityRelation.None)
                    LevelInfo = orderedLevelInfosByProjectElevation.FirstOrDefault(li => li.GetElementIndexOrNone() == familyInstanceElementLevelIndex);
            }
            
            if (TryGetHostLevel(fi, elementIds, elementLevelIndices, levelElementIndices, elementIdToLevelInfoMap, out var hostLevelInfo))
                HostLevelInfo = hostLevelInfo;

            var dm = vimScene.DocumentModel;

            var paramInfo = dm.GetParameterIndicesFromElementIndex(familyInstanceElementIndex).Select(i =>
            {
                var p = dm.GetParameter(i);

                // NOTE 1: we cache the ParameterDescriptor's Guid here to avoid having to re-instantiate the ParameterDescriptor object every time we access p.ParameterDescriptor
                // NOTE 2: Guid is either the built-in ID (if the parameter is built-in), or a guid (if the parameter is shared).
                var builtInId = p.ParameterDescriptor.Guid; 

                return (p, builtInId);
            }).ToArray();

            if (TryGetLevelParameter(paramInfo, ScheduleLevelBuiltInIds, elementIdToLevelInfoMap, out var scheduleLevelInfo))
                ScheduleLevelInfo = scheduleLevelInfo;

            if (TryGetLevelParameter(paramInfo, ReferenceLevelBuiltInIds, elementIdToLevelInfoMap, out var referenceLevelInfo))
                ReferenceLevelInfo = referenceLevelInfo;

            if (TryGetLevelParameter(paramInfo, BaseLevelBuiltInIds, elementIdToLevelInfoMap, out var baseLevelInfo))
                BaseLevelInfo = baseLevelInfo;

            BuildingStoryGeometryContainment = GetBuildingStoryGeometryContainment(
                vimScene,
                familyInstanceElementIndex,
                PrimaryLevelInfo?.Level?.ProjectElevation,
                elementIndexToNodeIndicesMap,
                orderedLevelInfosByProjectElevation,
                out var maybeBuildingStoryAbove,
                out var maybeBuildingStoryCurrentOrBelow,
                out var maybeGeometryMinBuildingStory,
                out var maybeGeometryMaxBuildingStory);

            BuildingStoryAbovePrimaryLevel = maybeBuildingStoryAbove;
            BuildingStoryCurrentOrBelowPrimaryLevel = maybeBuildingStoryCurrentOrBelow;
            GeometryMinBuildingStory = maybeGeometryMinBuildingStory;
            GeometryMaxBuildingStory = maybeGeometryMaxBuildingStory;
        }

        /// <summary>
        /// Returns the host level of the family instance.
        /// </summary>
        private static bool TryGetHostLevel(
            FamilyInstance fi,
            IReadOnlyList<long> elementIds,         // for optimized data access
            IReadOnlyList<int> elementLevelIndices, // for optimized data access
            IReadOnlyList<int> levelElementIndices, // for optimized data access
            IReadOnlyDictionary<long, LevelInfo> elementIdToLevelMap,
            out LevelInfo hostLevelInfo)
        {
            hostLevelInfo = null;

            var hostElementIndex = fi._Host?.Index ?? EntityRelation.None;
            if (hostElementIndex == EntityRelation.None)
                return false;

            var hostElementId = elementIds[hostElementIndex];

            // If the host element is a level, use it.
            if (elementIdToLevelMap.TryGetEntityFromElementId(hostElementId, out hostLevelInfo))
                return true;

            // If the host element is just a regular instance, then return the host element's level.
            var hostElementLevelIndex = elementLevelIndices[hostElementIndex];
            if (hostElementLevelIndex == EntityRelation.None)
                return false;

            var hostElementLevelElementIndex = levelElementIndices[hostElementLevelIndex];
            if (hostElementLevelElementIndex == EntityRelation.None)
                return false;

            var hostElementLevelElementId = elementIds[hostElementLevelElementIndex];

            return elementIdToLevelMap.TryGetEntityFromElementId(hostElementLevelElementId, out hostLevelInfo);
        }

        /// <summary>
        /// Returns a level based on the given built-in parameter id set, if present.
        /// </summary>
        private static bool TryGetLevelParameter(
            (Parameter, string)[] paramInfo,
            HashSet<string> builtInIds,
            IReadOnlyDictionary<long, LevelInfo> elementIdToLevelInfoMap,
            out LevelInfo levelInfo)
        {
            levelInfo = null;

            foreach (var (p, builtInId) in paramInfo)
            {
                if (!builtInIds.Contains(builtInId))
                    continue;

                if (p.TryParseRevitParameterValueAsElementId(out var levelElementId) &&
                    elementIdToLevelInfoMap.TryGetEntityFromElementId(levelElementId, out levelInfo))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the building story info and calculates the geometry
        /// </summary>
        private static BuildingStoryGeometryContainment GetBuildingStoryGeometryContainment(
            VimScene vimScene,
            int elementIndex,
            double? primaryProjectElevation,
            DictionaryOfLists<int, int> elementIndexToNodeIndicesMap,
            IReadOnlyList<LevelInfo> orderedLevelInfosByProjectElevation,
            out LevelInfo maybeBuildingStoryAbove, 
            out LevelInfo maybeBuildingStoryCurrentOrBelow,
            out LevelInfo maybeGeometryMinBuildingStory,
            out LevelInfo maybeGeometryMaxBuildingStory)
        {
            maybeBuildingStoryAbove = null;
            maybeBuildingStoryCurrentOrBelow = null;
            maybeGeometryMinBuildingStory = null;
            maybeGeometryMaxBuildingStory = null;

            if (primaryProjectElevation == null)
                return BuildingStoryGeometryContainment.Unknown;

            // Note: Level.ProjectElevation is relative to the internal scene origin (0,0,0), and so is the vim scene's geometry.
            var bb = vimScene.GetElementWorldSpaceBoundingBox(elementIndex, elementIndexToNodeIndicesMap);
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

                if (bbMin >= levelProjectElevation)
                {
                    // Find the building story below or at the geometric minimum.
                    maybeGeometryMinBuildingStory = levelInfo;
                }

                if (bbMax >= levelProjectElevation)
                {
                    // Find the first building story below or at the geometric maximum.
                    maybeGeometryMaxBuildingStory = levelInfo;
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

            if (bbMin < lvlLow && bbMax < lvlLow)
                return BuildingStoryGeometryContainment.CompletelyBelow;

            if (bbMin < lvlLow && bbMax >= lvlLow && bbMax <= lvlHi)
                return BuildingStoryGeometryContainment.CrossingBelow;

            if (bbMin >= lvlLow && bbMax <= lvlHi)
                return BuildingStoryGeometryContainment.Contained;

            if (bbMin >= lvlLow && bbMin <= lvlHi && bbMax > lvlHi)
                return BuildingStoryGeometryContainment.CrossingAbove;

            if (bbMin > lvlHi && bbMax > lvlHi)
                return BuildingStoryGeometryContainment.CompletelyAbove;

            if (bbMin < lvlLow && bbMax > lvlHi)
                return BuildingStoryGeometryContainment.SpanningBelowAndAbove;

            Debug.Fail($"Unexpected geometry containment case. bbMin: {bbMin}, bbMax: {bbMax}, lvlLow: {lvlLow}, lvlHi: {lvlHi}");

            return BuildingStoryGeometryContainment.Unknown;
        }
    }
}
