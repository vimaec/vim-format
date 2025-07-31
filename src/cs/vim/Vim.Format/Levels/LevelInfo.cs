using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

// ReSharper disable InconsistentNaming

namespace Vim.Format.Levels
{
    public class LevelInfo : IElementIndex
    {
        // SOME BACKGROUND INFORMATION ABOUT REVIT LEVELS
        //
        // by: Martin Ashton, July 25 2025
        //
        // In Revit, a Level has two elevation values:
        //
        //   - Level.ProjectElevation: this value is relative to the internal scene origin at (0,0,0)
        //
        //   - Level.Elevation: this value is relative to either
        //
        //     a) the "project base point" of the bim document
        //
        //     b) the "survey point" of the bim document
        //
        //     For more information on these values, check the Revit API documentation and see ObjectModel.cs > BasePoint.
        //
        // Additionally, a Level may be qualified as either a building story or not.
        //
        //   - When the level is a building story, it typically corresponds to the actual floors of a building.
        //
        //   - When the level is not a building story, it can be a working plane used by the designer to align things
        //     like mechanical items in the ceiling, stair systems, etc.
        //
        // ... other relevant notes in FamilyInstanceLevelInfo.cs ...

        /// <summary>
        /// The Level.
        /// </summary>
        public Level Level { get; }

        /// <summary>
        /// The element index of the level.
        /// </summary>
        public int GetElementIndexOrNone()
            => Level.GetElementIndexOrNone();
        
        /// <summary>
        /// The name of the Level.
        /// </summary>
        public string LevelName
            => Level?.Element?.Name ?? "";

        const string WholeFeetFormatString = "0000";
        const string DecimalFormatString = "0000.0000";
        const string PositivePrefix = "+";
        private const int RoundingDigits = 4;

        /// <summary>
        /// "{-|+}{elevationDecimalFeetWithLeadingZeroes}ft - {name}
        /// </summary>
        public string NameWithElevationFeetDecimal
            => $"{Units.ToDecimalFeetString(Level.Elevation, DecimalFormatString, PositivePrefix)} - {LevelName}";

        /// <summary>
        /// "{-|+}{elevationFractionalFeetAndInchesWithLeadingZeroes} - {name}
        /// </summary>
        public string NameWithElevationFeetAndFractionalInches
            => $"{Units.ToFeetAndFractionalInchesString(Level.Elevation, WholeFeetFormatString, PositivePrefix)} - {LevelName}";

        /// <summary>
        /// "{-|+}{elevationMetersWithLeadingZeroes}m - {name}
        /// </summary>
        public string NameWithElevationMeters
            => $"{Units.ToMetersString(Units.FeetToMeters(Level.Elevation, RoundingDigits), DecimalFormatString, PositivePrefix)} - {LevelName}";

        /// <summary>
        /// The elevation in feet relative to the bim document's project base point (unrounded), or null if no project base point exists.
        /// </summary>
        public double? ElevationRelativeToProjectBasePointDecimalFeetUnrounded { get; }

        /// <summary>
        /// The elevation in decimal feet relative to the bim document's project base point (rounded to 4 digits), or null if no project base point exists.
        /// </summary>
        public double? ElevationRelativeToProjectBasePointDecimalFeet
            => ElevationRelativeToProjectBasePointDecimalFeetUnrounded == null
                ? (double?) null
                : Math.Round(ElevationRelativeToProjectBasePointDecimalFeetUnrounded.Value, RoundingDigits);

        /// <summary>
        /// The elevation in feet and fractional inches relative to the bim document's project base point, or empty if no project base point exists.
        /// </summary>
        public string ElevationRelativeToProjectBasePointFeetAndFractionalInches
            => Units.ToFeetAndFractionalInchesString(ElevationRelativeToProjectBasePointDecimalFeetUnrounded);

        /// <summary>
        /// The elevation in meters relative to the bim document's project base point, or null if no project base point exists.
        /// </summary>
        public double? ElevationRelativeToProjectBasePointMeters
            => Units.FeetToMeters(ElevationRelativeToProjectBasePointDecimalFeetUnrounded, RoundingDigits);

        /// <summary>
        /// The elevation in feet relative to the bim document's survey point (unrounded), or null if no survey point exists.
        /// </summary>
        public double? ElevationRelativeToSurveyPointDecimalFeetUnrounded { get; }

        /// <summary>
        /// The elevation in feet relative to the bim document's survey point (rounded to 4 digits), or null if no survey point exists.
        /// </summary>
        public double? ElevationRelativeToSurveyPointDecimalFeet
            => ElevationRelativeToSurveyPointDecimalFeetUnrounded == null
                ? (double?)null
                : Math.Round(ElevationRelativeToSurveyPointDecimalFeetUnrounded.Value, RoundingDigits);

        /// <summary>
        /// The elevation in feet and fractional inches relative to the bim document's survey point, or empty if no survey point exists.
        /// </summary>
        public string ElevationRelativeToSurveyPointFeetAndFractionalInches
            => Units.ToFeetAndFractionalInchesString(ElevationRelativeToSurveyPointDecimalFeetUnrounded);

        /// <summary>
        /// The elevation in meters relative to the bim document's survey point, or null if no survey point exists.
        /// </summary>
        public double? ElevationRelativeToSurveyPointMeters
            => Units.FeetToMeters(ElevationRelativeToSurveyPointDecimalFeetUnrounded, RoundingDigits);

        /// <summary>
        /// This is derived from the level's type.
        ///   - true: level is relative to bim document's project base point
        ///   - false: level is relative to bim document's survey point
        ///   - null: not specified
        /// </summary>
        public bool? IsRelativeToProjectBasePoint { get; private set; }
        //public const string TypeId_ElevationBase = "autodesk.revit.parameter:levelRelativeBaseType";
        public const string BuiltInId_ElevationBase = "-1007109";
        public static bool DescriptorIsProjectBasePoint(ParameterDescriptor pd)
            => pd.Guid == BuiltInId_ElevationBase;

        /// <summary>
        /// Determines whether the level is considered a structural level.
        /// </summary>
        public bool IsStructural { get; private set; }
        //public const string TypeId_IsStructural = "autodesk.revit.parameter:levelIsStructural";
        public const string BuiltInId_IsStructural = "-1007112";
        public static bool DescriptorIsStructural(ParameterDescriptor pd)
            => pd.Guid == BuiltInId_IsStructural;

        /// <summary>
        /// Determines whether the level is considered a building story.
        /// </summary>
        public bool IsBuildingStory { get; private set; }
        //public const string TypeId_IsBuildingStory = "autodesk.revit.parameter:levelIsBuildingStory";
        public const string BuiltInId_IsBuildingStory = "-1007111";
        public static bool DescriptorIsBuildingStory(ParameterDescriptor pd)
            => pd.Guid == BuiltInId_IsBuildingStory;

        /// <summary>
        /// The building story above this one. Can be null if this is set to "Default" in Revit or if the level is the topmost building story.
        /// </summary>
        public Level BuildingStoryAbove { get; set; }
        //public const string TypeId_BuildingStoryAbove = "autodesk.revit.parameter:levelUpToLevel";
        public const string BuiltInId_BuildingStoryAbove = "-1007110";
        public static bool DescriptorIsBuildingStoryAbove(ParameterDescriptor pd)
            => pd.Guid == BuiltInId_BuildingStoryAbove;

        /// <summary>
        /// The height of the building story above in decimal feet (unrounded).
        /// </summary>
        public double? BuildingStoryAboveHeightFeetDecimalUnrounded
            => BuildingStoryAbove == null
                ? (double?)null
                : BuildingStoryAbove.ProjectElevation - Level.ProjectElevation;

        /// <summary>
        /// The height of the building story above in decimal feet.
        /// Calculated based on the difference in BuildingStoryAbove.ProjectElevation and Level.ProjectElevation.
        /// </summary>
        public double? BuildingStoryAboveHeightFeetDecimal
            => BuildingStoryAboveHeightFeetDecimalUnrounded == null
                ? (double?) null
                : Math.Round(BuildingStoryAboveHeightFeetDecimalUnrounded.Value, RoundingDigits);

        /// <summary>
        /// The height of the building story above in feet and fractional inches.
        /// </summary>
        public string BuildingStoryAboveFeetAndFractionalInches
            => Units.ToFeetAndFractionalInchesString(BuildingStoryAboveHeightFeetDecimalUnrounded);

        /// <summary>
        /// The height of the building story above in meters.
        /// Calculated based on the difference in BuildingStoryAbove.ProjectElevation and Level.ProjectElevation.
        /// </summary>
        public double? BuildingStoryAboveHeightMeters
            => Units.FeetToMeters(BuildingStoryAboveHeightFeetDecimalUnrounded, RoundingDigits);

        /// <summary>
        /// Returns true if the level info has a BuildingStoryAbove.
        /// </summary>
        public bool HasBuildingStoryAbove
            => BuildingStoryAbove != null;

        /// <summary>
        /// Constructor
        /// </summary>
        public LevelInfo(
            Level level,
            FamilyTypeTable familyTypeTable,
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps,
            IReadOnlyDictionary<long, Level> elementIdToLevelMap,
            IReadOnlyDictionary<long, BasePoint> elementIdToBasePointMap)
        {
            Level = level;

            var projectBasePoint = elementIdToBasePointMap.Values.FirstOrDefault(bp => bp.IsSurveyPoint == false);
            ElevationRelativeToProjectBasePointDecimalFeetUnrounded = projectBasePoint == null
                ? (double?) null
                : Level.ProjectElevation - projectBasePoint.Position_Z;
            
            var surveyPoint = elementIdToBasePointMap.Values.FirstOrDefault(bp => bp.IsSurveyPoint);
            ElevationRelativeToSurveyPointDecimalFeetUnrounded = surveyPoint == null
                ? (double?) null
                : Level.ProjectElevation - surveyPoint.Position_Z;

            ReadLevelParameters(parameterTable, elementIndexMaps, elementIdToLevelMap);

            ReadLevelTypeParameters(familyTypeTable, parameterTable, elementIndexMaps);
        }

        private void ReadLevelParameters(
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps,
            IReadOnlyDictionary<long, Level> elementIdToLevelMap)
        {
            var levelElementParameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(GetElementIndexOrNone());

            foreach (var paramIndex in levelElementParameterIndices)
            {
                var p = parameterTable.Get(paramIndex);
                var desc = p.ParameterDescriptor;

                if (DescriptorIsStructural(desc) &&
                    p.TryParseRevitParameterValueAsBoolean(desc, out var isStructural))
                {
                    IsStructural = isStructural;
                }
                else if (
                    DescriptorIsBuildingStory(desc) &&
                    p.TryParseRevitParameterValueAsBoolean(desc, out var isBuildingStory))
                {
                    IsBuildingStory = isBuildingStory;
                }
                else if (
                    DescriptorIsBuildingStoryAbove(desc) &&
                    p.TryParseRevitParameterValueAsElementId(out var storyAboveElementId) &&
                    elementIdToLevelMap.TryGetEntityFromElementId(storyAboveElementId, out var buildingStoryAbove))
                {
                    // If the building story above has an element ID of -1, then it could be set to "Default" in Revit,
                    // and so BuildingStoryAbove will remain null here. To resolve this, the BuildingStoryAbove must
                    // be calculated in a subsequent pass (see LevelService.PatchBuildingStoryAbove)
                    BuildingStoryAbove = buildingStoryAbove;
                }
            }
        }

        private void ReadLevelTypeParameters(
            FamilyTypeTable familyTypeTable,
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps)
        {
            var levelTypeElementIndex = familyTypeTable.GetElementIndex(Level.FamilyTypeIndex);

            if (!elementIndexMaps.ParameterIndicesFromElementIndex.TryGetValue(levelTypeElementIndex, out var levelTypeParameterIndices) ||
                levelTypeParameterIndices.Count <= 0)
            {
                return;
            }

            var typeParameters = levelTypeParameterIndices.Select(parameterTable.Get);
            foreach (var p in typeParameters)
            {
                var desc = p.ParameterDescriptor;

                if (DescriptorIsProjectBasePoint(desc) &&
                    p.TryParseRevitParameterAsLong(out var relativeToProjectBasePoint))
                {
                    IsRelativeToProjectBasePoint = relativeToProjectBasePoint == 0L; // 0L == "Project Base Point", 1L == "Survey Point"
                }
            }
        }

        public string PropertiesToString()
            => string.Join(Environment.NewLine, this.PropertiesToStrings());
    }
}
