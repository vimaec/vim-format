using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Levels
{
    public class LevelInfo
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
        // What's more, family instance elements in Revit are not always directly associated to a level in the Element.Level property.
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
        //   6. "Group Level"
        //     - The level associated to the parent group of the family instance.
        //     - Not always present; the family instance must be part of a group.
        //
        //   7. "System Level"
        //     - The level associated to the system of the family instance.
        //     - Not always present; the family instance must be part of a system.
        //
        // We refer to the "Primary" level as the first non-null level association among the ones listed above.

        /// <summary>
        /// The Level.
        /// </summary>
        public Level Level { get; }
        
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
        public const string ParameterDescriptorTypeIdElevationBase = "autodesk.revit.parameter:levelRelativeBaseType"; // Revit 2022 and beyond
        public const string ParameterDescriptorIdElevationBase = "-1007109"; // Revit 2021 and prior
        public bool ParameterDescriptorIsProjectBasePoint(ParameterDescriptor pd)
            => pd.Guid.StartsWith(ParameterDescriptorTypeIdElevationBase, StringComparison.InvariantCultureIgnoreCase) ||
               pd.Guid == ParameterDescriptorIdElevationBase;

        /// <summary>
        /// Determines whether the level is considered a structural level.
        /// </summary>
        public bool IsStructural { get; private set; }
        public const string ParameterDescriptorTypeIdIsStructural = "autodesk.revit.parameter:levelIsStructural"; // Revit 2022 and beyond
        public const string ParameterDescriptorIdIsStructural = "-1007112"; // Revit 2021 and prior
        public bool ParameterDescriptorIsStructural(ParameterDescriptor pd)
            => pd.Guid.StartsWith(ParameterDescriptorTypeIdIsStructural, StringComparison.InvariantCultureIgnoreCase) ||
               pd.Guid == ParameterDescriptorIdIsStructural;

        /// <summary>
        /// Determines whether the level is considered a building story.
        /// </summary>
        public bool IsBuildingStory { get; private set; }
        public const string ParameterDescriptorTypeIdIsBuildingStory = "autodesk.revit.parameter:levelIsBuildingStory"; // Revit 2022 and beyond
        public const string ParameterDescriptorIdIsBuildingStory = "-1007111"; // Revit 2021 and prior
        public bool ParameterDescriptorIsBuildingStory(ParameterDescriptor pd)
            => pd.Guid.StartsWith(ParameterDescriptorTypeIdIsBuildingStory, StringComparison.InvariantCultureIgnoreCase) ||
               pd.Guid == ParameterDescriptorIdIsBuildingStory;

        /// <summary>
        /// The building story above this one. Can be null if this is set to "Default" in Revit or if the level is the topmost building story.
        /// </summary>
        public Level BuildingStoryAbove { get; set; }
        public const string ParameterDescriptorTypeIdBuildingStoryAbove = "autodesk.revit.parameter:levelUpToLevel"; // Revit 2022 and beyond
        public const string ParameterDescriptorIdBuildingStoryAbove = "-1007110"; // Revit 2021 and prior
        public bool ParameterDescriptorIsBuildingStoryAbove(ParameterDescriptor pd)
            => pd.Guid.StartsWith(ParameterDescriptorTypeIdBuildingStoryAbove, StringComparison.InvariantCultureIgnoreCase) ||
               pd.Guid == ParameterDescriptorIdBuildingStoryAbove;

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
        public LevelInfo(DocumentModel dm, Level level, IReadOnlyList<Level> levelsInBimDocument, IReadOnlyList<BasePoint> basePointsInBimDocument)
        {
            Level = level;

            var projectBasePoint = basePointsInBimDocument.FirstOrDefault(bp => bp.IsSurveyPoint == false);
            ElevationRelativeToProjectBasePointDecimalFeetUnrounded = projectBasePoint == null
                ? (double?) null
                : Level.ProjectElevation - projectBasePoint.Position_Z;
            
            var surveyPoint = basePointsInBimDocument.FirstOrDefault(bp => bp.IsSurveyPoint);
            ElevationRelativeToSurveyPointDecimalFeetUnrounded = surveyPoint == null
                ? (double?) null
                : Level.ProjectElevation - surveyPoint.Position_Z;

            ReadLevelParameters(dm, levelsInBimDocument);

            ReadLevelTypeParameters(dm);
        }

        private void ReadLevelParameters(DocumentModel dm, IReadOnlyList<Level> levelsInBimDocument)
        {
            var levelElementIndex = Level.Element?.IndexOrDefault() ?? EntityRelation.None;
            if (!dm.ElementIndexMaps.ParameterIndicesFromElementIndex.TryGetValue(levelElementIndex, out var levelElementParameterIndices) ||
                levelElementParameterIndices.Count <= 0)
            {
                return;
            }

            var parameters = levelElementParameterIndices.Select(dm.GetParameter);
            foreach (var p in parameters)
            {
                var desc = p.ParameterDescriptor;

                if (ParameterDescriptorIsStructural(desc) &&
                    p.TryParseRevitParameterValueAsBoolean(desc, out var isStructural))
                {
                    IsStructural = isStructural;
                }
                else if (
                    ParameterDescriptorIsBuildingStory(desc) &&
                    p.TryParseRevitParameterValueAsBoolean(desc, out var isBuildingStory))
                {
                    IsBuildingStory = isBuildingStory;
                }
                else if (
                    ParameterDescriptorIsBuildingStoryAbove(desc) &&
                    p.TryParseRevitParameterValueAsElementId(out var storyAboveElementId))
                {
                    // If the building story above has an element ID of -1, then it could be set to "Default" in Revit,
                    // meaning that the building story above must be calculated in a separate pass (see LevelService.PatchBuildingStoryAbove)
                    BuildingStoryAbove = TryGetLevelFromElementId(storyAboveElementId, levelsInBimDocument, out var buildingStoryAbove)
                        ? buildingStoryAbove
                        : null;
                }
            }
        }

        private void ReadLevelTypeParameters(DocumentModel dm)
        {
            var levelTypeElementIndex = Level.FamilyType?.Element.IndexOrDefault() ?? EntityRelation.None;
            if (!dm.ElementIndexMaps.ParameterIndicesFromElementIndex.TryGetValue(levelTypeElementIndex, out var levelTypeParameterIndices) ||
                levelTypeParameterIndices.Count <= 0)
            {
                return;
            }

            var typeParameters = levelTypeParameterIndices.Select(dm.GetParameter);
            foreach (var p in typeParameters)
            {
                var desc = p.ParameterDescriptor;

                if (ParameterDescriptorIsProjectBasePoint(desc) &&
                    p.TryParseRevitParameterAsLong(out var relativeToProjectBasePoint))
                {
                    IsRelativeToProjectBasePoint = relativeToProjectBasePoint == 0L; // 0L == "Project Base Point", 1L == "Survey Point"
                }
            }
        }

        private static bool TryGetLevelFromElementId(long levelElementId, IReadOnlyList<Level> levelsInBimDocument, out Level level)
        {
            level = null;

            if (levelElementId == -1L)
                return false;

            level = levelsInBimDocument.FirstOrDefault(l => l.Element.Id == levelElementId);

            return level != null;
        }

        public override string ToString()
            => string.Join(Environment.NewLine, this.PropertiesToStrings());
    }
}
