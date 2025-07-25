using System;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Levels
{
    public class LevelInfo
    {
        // SOME BACKGROUND INFORMATION ABOUT REVIT LEVELS
        // (Martin Ashton, July 25 2025)
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
        /// The current Level.
        /// </summary>
        public Level Level { get; }

        /// <summary>
        /// "{-| }{elevationFractionalFeetAndInchesWithLeadingZeroes}ft - {name}
        /// </summary>
        public string FullNameFeet { get; }

        /// <summary>
        /// "{-| }{elevationMetersWithLeadingZeroes}m - {name}
        /// </summary>
        public string FullNameMeters { get; }

        /// <summary>
        /// The elevation in feet relative to the bim document's project base point, or null if no project base point exists.
        /// </summary>
        public double? ElevationRelativeToProjectBasePointFeet { get; }

        /// <summary>
        /// The elevation in meters relative to the bim document's project base point, or null if no project base point exists.
        /// </summary>
        public double? ElevationRelativeToProjectBasePointMeters
            => Util.Units.FeetToMeters(ElevationRelativeToProjectBasePointFeet);

        /// <summary>
        /// The elevation in feet relative to the bim document's survey point, or null if no survey point exists.
        /// </summary>
        public double? ElevationRelativeToSurveyPointFeet { get; }

        /// <summary>
        /// The elevation in meters relative to the bim document's survey point, or null if no survey point exists.
        /// </summary>
        public double? ElevationRelativeToSurveyPointMeters
            => Util.Units.FeetToMeters(ElevationRelativeToSurveyPointFeet);

        /// <summary>
        /// true: level is relative to bim document's project base point
        /// false: level is relative to bim document's survey point
        /// null: not specified
        /// </summary>
        public bool? IsRelativeToProjectBasePoint { get; }

        /// <summary>
        /// Determines whether the level is considered a structural level.
        /// </summary>
        public bool IsStructural { get; }

        /// <summary>
        /// Determines whether the level is considered a building story.
        /// </summary>
        public bool IsBuildingStory { get; }

        private Level _buildingStoryAbove;

        /// <summary>
        /// The building story above this one. Can be null if this is the last level (ex: roof)
        /// </summary>
        public Level BuildingStoryAbove { get; set; }

        /// <summary>
        /// The height of the building story above in feet.
        /// Calculated based on the difference in BuildingStoryAbove.ProjectElevation and Level.ProjectElevation.
        /// </summary>
        public double? BuildingStoryAboveHeight
            => BuildingStoryAbove == null
                ? (double?) null
                : BuildingStoryAbove.ProjectElevation - Level.ProjectElevation;

        /// <summary>
        /// Constructor
        /// </summary>
        public LevelInfo(DocumentModel dm, Level level)
        {
            Level = level;

            var levelName = dm.ElementName[level._Element.Index];
            // {-| }{elevationFractionalFeetAndInchesWithLeadingZeroes}ft - {name}
            //FullNameFeet = 

        }

        public override string ToString()
            => string.Join(Environment.NewLine, this.PropertiesToStrings());
    }
}
