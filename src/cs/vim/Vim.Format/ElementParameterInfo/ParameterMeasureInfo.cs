using System.Collections.Generic;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// A collection of well-known measure types
    /// </summary>
    public enum MeasureType
    {
        Unknown = 0,
        Length = 1,
        Width = 2,
        Height = 3,
        Area = 4,
        Volume = 5,
        Angle = 6,
        Slope = 7,
        // [MAINTAIN]
        //   - Add more measure types as needed.
        //   - *DO NOT CHANGE THE ACTUAL ENUM VALUES*
    }

    public static class ParameterMeasureInfo
    {
        public static MeasureType ParseMeasureType(
            string parameterDescriptorNameLowerInvariant, string parameterDescriptorGuid)
            => NameLowerInvariantToMeasureTypeMap.TryGetValue(parameterDescriptorNameLowerInvariant, out var measureType) ||
               BuiltInIdToMeasureTypeMap.TryGetValue(parameterDescriptorGuid, out measureType) // NOTE: Guid is either the built-in ID (if the parameter is built-in), or a guid (if the parameter is shared).
                ? measureType
                : MeasureType.Unknown;

        /// <summary>
        /// Maps the lowercase parameter name to the MeasureType.
        /// </summary>
        public static readonly Dictionary<string, MeasureType> NameLowerInvariantToMeasureTypeMap = new Dictionary<string, MeasureType>()
        {
            { "angle", MeasureType.Angle },
            { "slope", MeasureType.Slope },
            { "length", MeasureType.Length },
            { "width", MeasureType.Width },
            { "height", MeasureType.Height },
            { "area", MeasureType.Area },
            { "volume", MeasureType.Volume },
        };

        // The built-in id sets below are correlated to the parameters named "Angle/Slope/Length/Width/Height/..."
        // in the BuiltInParameter enumerations https://www.revitapidocs.com/2025/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm

        /// <summary>
        /// Maps the built-in parameter Revit ID to the MeasureType
        /// </summary>
        public static readonly Dictionary<string, MeasureType> BuiltInIdToMeasureTypeMap = new Dictionary<string, MeasureType>()
        {
            { "-1001817", MeasureType.Angle }, // PROFILE_ANGLE, "Angle"
            { "-1001826", MeasureType.Angle }, // PROFILE1_ANGLE, "Angle"
            { "-1001831", MeasureType.Angle }, // PROFILE2_ANGLE, "Angle"
            { "-1004006", MeasureType.Angle }, // CURVE_ELEM_LINE_ANGLE, "Angle"
            { "-1007007", MeasureType.Angle }, // TAG_ANGLE_PARAM, "Angle"
            { "-1007363", MeasureType.Angle }, // MULLION_ANGLE, "Angle"
            { "-1013309", MeasureType.Angle }, // CURTAINGRID_ANGLE_VERT, "Angle"
            { "-1013310", MeasureType.Angle }, // CURTAINGRID_ANGLE_HORIZ, "Angle"
            { "-1013339", MeasureType.Angle }, // CURTAINGRID_ANGLE_1, "Angle"
            { "-1013340", MeasureType.Angle }, // CURTAINGRID_ANGLE_2, "Angle"
            { "-1133409", MeasureType.Angle }, // CONNECTOR_ANGLE, "Angle"
            { "-1140724", MeasureType.Angle }, // TRUSS_FAMILY_VERT_WEB_ANGLE_PARAM, "Angle"
            { "-1140734", MeasureType.Angle }, // TRUSS_FAMILY_DIAG_WEB_ANGLE_PARAM, "Angle"
            { "-1140744", MeasureType.Angle }, // TRUSS_FAMILY_TOP_CHORD_ANGLE_PARAM, "Angle"
            { "-1140764", MeasureType.Angle }, // TRUSS_FAMILY_BOTTOM_CHORD_ANGLE_PARAM, "Angle"
            { "-1140911", MeasureType.Angle }, // FABRICATION_PART_ANGLE, "Angle"
            { "-1150226", MeasureType.Angle }, // POINT_ELEMENT_ANGLE, "Angle"
            { "-1155075", MeasureType.Angle }, // STEEL_ELEM_PLATE_SHORTEN_ANGLE, "Angle"

            { "-1006016", MeasureType.Slope }, // ROOF_SLOPE, "Slope"
            { "-1140254", MeasureType.Slope }, // RBS_CURVE_UTSLOPE, "Slope"
            { "-1140255", MeasureType.Slope }, // RBS_DUCT_SLOPE, "Slope"
            { "-1140256", MeasureType.Slope }, // RBS_PIPE_SLOPE, "Slope"
            { "-1140923", MeasureType.Slope }, // FABRICATION_SLOPE_PARAM, "Slope"

            { "-1001136", MeasureType.Length }, // DPART_LENGTH_COMPUTED, "Length"
            { "-1001306", MeasureType.Length }, // FAMILY_LINE_LENGTH_PARAM, "Length"
            { "-1001375", MeasureType.Length }, // INSTANCE_LENGTH_PARAM, "Length"
            { "-1001567", MeasureType.Length }, // CONTINUOUS_FOOTING_LENGTH, "Length"
            { "-1001569", MeasureType.Length }, // STRUCTURAL_FOUNDATION_LENGTH, "Length"
            { "-1004005", MeasureType.Length }, // CURVE_ELEM_LENGTH, "Length"
            { "-1007736", MeasureType.Length }, // IMPORT_ADT_ENTITY_LENGTH, "Length"
            { "-1013434", MeasureType.Length }, // COVER_TYPE_LENGTH, "Length"
            { "-1015043", MeasureType.Length }, // LOAD_LINEAR_LENGTH, "Length"
            { "-1017608", MeasureType.Length }, // FABRIC_SHEET_LENGTH, "Length"
            { "-1140039", MeasureType.Length }, // RBS_ELEC_CIRCUIT_LENGTH_PARAM, "Length"
            { "-1140132", MeasureType.Length }, // RBS_CABLETRAYCONDUITRUN_LENGTH_PARAM, "Length"
            { "-1140337", MeasureType.Length }, // CONNECTOR_LENGTH, "Length"
            { "-1140944", MeasureType.Length }, // FABRICATION_PART_LENGTH, "Length"
            { "-1150348", MeasureType.Length }, // CONTINUOUSRAIL_EXTENSION_LENGTH_PARAM, "Length"
            { "-1150350", MeasureType.Length }, // CONTINUOUSRAIL_END_EXTENSION_LENGTH_PARAM, "Length"
            { "-1150360", MeasureType.Length }, // CONTINUOUSRAIL_LENGTH_PARAM, "Length"
            { "-1150461", MeasureType.Length }, // ANALYTICAL_MODEL_LENGTH, "Length"
            { "-1153545", MeasureType.Length }, // RBS_ELEC_ANALYTICAL_FEEDER_LENGTH, "Length"
            { "-1155017", MeasureType.Length }, // STEEL_ELEM_ANCHOR_LENGTH, "Length"
            { "-1155019", MeasureType.Length }, // STEEL_ELEM_SHEARSTUD_LENGTH, "Length"
            { "-1155020", MeasureType.Length }, // STEEL_ELEM_SHORTEN_REFLENGTH, "Length"
            { "-1155033", MeasureType.Length }, // STEEL_ELEM_WELD_LENGTH, "Length"
            { "-1155137", MeasureType.Length }, // STEEL_ELEM_PLATE_LENGTH, "Length"
            { "-1155147", MeasureType.Length }, // STEEL_ELEM_PROFILE_LENGTH, "Length"
            { "-1155247", MeasureType.Length }, // LINEAR_FRAMING_LENGTH, "Length"

            // duplicate: "-1001301", // CASEWORK_WIDTH, "Width"
            // duplicate: "-1001301", // DOOR_WIDTH, "Width"
            // duplicate: "-1001301", // FURNITURE_WIDTH, "Width"
            // duplicate: "-1001301", // GENERIC_WIDTH, "Width"
            // duplicate: "-1001301", // WINDOW_WIDTH, "Width"
            { "-1001000", MeasureType.Width }, // WALL_ATTR_WIDTH_PARAM, "Width"
            { "-1001301", MeasureType.Width }, // FAMILY_WIDTH_PARAM, "Width"
            { "-1001558", MeasureType.Width }, // CONTINUOUS_FOOTING_WIDTH, "Width"
            { "-1001562", MeasureType.Width }, // CONTINUOUS_FOOTING_BEARING_WIDTH, "Width"
            { "-1001568", MeasureType.Width }, // STRUCTURAL_FOUNDATION_WIDTH, "Width"
            { "-1005502", MeasureType.Width }, // STRUCTURAL_SECTION_COMMON_WIDTH, "Width"
            { "-1007204", MeasureType.Width }, // STAIRS_ATTR_TREAD_WIDTH, "Width"
            { "-1007600", MeasureType.Width }, // ELEV_WIDTH, "Width"
            { "-1007735", MeasureType.Width }, // IMPORT_ADT_ENTITY_WIDTH, "Width"
            { "-1007750", MeasureType.Width }, // RASTER_SHEETWIDTH, "Width"
            { "-1007765", MeasureType.Width }, // RASTER_SYMBOL_WIDTH, "Width"
            { "-1010301", MeasureType.Width }, // CURTAIN_WALL_PANELS_WIDTH, "Width"
            { "-1012814", MeasureType.Width }, // DECAL_WIDTH, "Width"
            { "-1017615", MeasureType.Width }, // FABRIC_SHEET_WIDTH, "Width": Width
            { "-1114101", MeasureType.Width }, // RBS_CURVE_WIDTH_PARAM, "Width"
            { "-1133403", MeasureType.Width }, // CONNECTOR_WIDTH, "Width"
            { "-1140122", MeasureType.Width }, // RBS_CABLETRAY_WIDTH_PARAM, "Width"
            { "-1140134", MeasureType.Width }, // RBS_CABLETRAYRUN_WIDTH_PARAM, "Width"
            { "-1150623", MeasureType.Width }, // DIVISION_PROFILE_WIDTH, "Width": Width
            { "-1151807", MeasureType.Width }, // STAIRS_SUPPORTTYPE_WIDTH, "Width": Width
            { "-1155138", MeasureType.Width }, // STEEL_ELEM_PLATE_WIDTH, "Width"
            { "-1155317", MeasureType.Width }, // BENDING_DETAIL_TYPE_SCHEMATIC_WIDTH, "Width"

            // duplicate: "-1001300", // CASEWORK_HEIGHT, "Height"
            // duplicate: "-1001300", // DOOR_HEIGHT, "Height"
            // duplicate: "-1001300", // FURNITURE_HEIGHT, "Height"
            // duplicate: "-1001300", // GENERIC_HEIGHT, "Height"
            // duplicate: "-1001300", // WINDOW_HEIGHT, "Height"
            {"-1001001", MeasureType.Height }, // WALL_ATTR_HEIGHT_PARAM, "Height"
            {"-1001135", MeasureType.Height }, // DPART_HEIGHT_COMPUTED, "Height"
            {"-1001300", MeasureType.Height }, // FAMILY_HEIGHT_PARAM, "Height"
            {"-1005193", MeasureType.Height }, // RENDER_PLANT_HEIGHT, "Height"
            {"-1005503", MeasureType.Height }, // STRUCTURAL_SECTION_COMMON_HEIGHT, "Height"
            {"-1007734", MeasureType.Height }, // IMPORT_ADT_ENTITY_HEIGHT, "Height"
            {"-1007751", MeasureType.Height }, // RASTER_SHEETHEIGHT, "Height"
            {"-1007766", MeasureType.Height }, // RASTER_SYMBOL_HEIGHT, "Height"
            {"-1010300", MeasureType.Height }, // CURTAIN_WALL_PANELS_HEIGHT, "Height"
            {"-1012114", MeasureType.Height }, // VOLUME_OF_INTEREST_HEIGHT, "Height"
            {"-1012815", MeasureType.Height }, // DECAL_HEIGHT, "Height"
            {"-1017043", MeasureType.Height }, // REBAR_SHAPE_SPIRAL_HEIGHT, "Height"
            {"-1114102", MeasureType.Height }, // RBS_CURVE_HEIGHT_PARAM, "Height"
            {"-1133404", MeasureType.Height }, // CONNECTOR_HEIGHT, "Height"
            {"-1140121", MeasureType.Height }, // RBS_CABLETRAY_HEIGHT_PARAM, "Height"
            {"-1140133", MeasureType.Height }, // RBS_CABLETRAYRUN_HEIGHT_PARAM, "Height"
            {"-1150328", MeasureType.Height }, // RAILING_SYSTEM_TOP_RAIL_HEIGHT_PARAM, "Height"
            {"-1150331", MeasureType.Height }, // RAILING_SYSTEM_HANDRAILS_HEIGHT_PARAM, "Height"
            {"-1150335", MeasureType.Height }, // RAILING_SYSTEM_SECONDARY_HANDRAILS_HEIGHT_PARAM, "Height"
            {"-1150340", MeasureType.Height }, // HANDRAIL_HEIGHT_PARAM, "Height"
            {"-1152166", MeasureType.Height }, // SUPPORT_HEIGHT, "Height"
            {"-1155318", MeasureType.Height }, // BENDING_DETAIL_TYPE_SCHEMATIC_HEIGHT, "Height"

            { "-1001133", MeasureType.Area}, // DPART_AREA_COMPUTED, "Area"
            { "-1006902", MeasureType.Area}, // ROOM_AREA, "Area"
            { "-1012600", MeasureType.Area}, // PROPERTY_AREA, "Area"
            { "-1012606", MeasureType.Area}, // PROPERTY_AREA_OPEN, "Area"
            { "-1012805", MeasureType.Area}, // HOST_AREA_COMPUTED, "Area"
            { "-1015069", MeasureType.Area}, // LOAD_AREA_AREA, "Area"
            { "-1114120", MeasureType.Area}, // RBS_CURVE_SURFACE_AREA, "Area"
            { "-1114247", MeasureType.Area}, // RBS_GBXML_SURFACE_AREA, "Area"
            { "-1114820", MeasureType.Area}, // SPACE_AREA, "Area"
            { "-1140360", MeasureType.Area}, // MATERIAL_AREA, "Area"
            { "-1150462", MeasureType.Area}, // ANALYTICAL_MODEL_AREA, "Area": The Area of Analytical Model
            { "-1153551", MeasureType.Area}, // RBS_ELEC_ANALYTICAL_AREA, "Area"
            { "-1155139", MeasureType.Area}, // STEEL_ELEM_PLATE_AREA, "Area"
            { "-1155234", MeasureType.Area}, // LAYER_ELEM_AREA_COMPUTED, "Area"

            { "-1001129", MeasureType.Volume }, // DPART_VOLUME_COMPUTED, "Volume"
            { "-1006921", MeasureType.Volume }, // ROOM_VOLUME, "Volume"
            { "-1012007", MeasureType.Volume }, // MASS_GROSS_VOLUME, "Gross Volume"
            { "-1012012", MeasureType.Volume }, // LEVEL_DATA_VOLUME, "Floor Volume"
            { "-1012021", MeasureType.Volume }, // MASS_ZONE_VOLUME, "Mass Zone Volume"
            { "-1012806", MeasureType.Volume }, // HOST_VOLUME_COMPUTED, "Volume"
            { "-1018502", MeasureType.Volume }, // REIN_EST_BAR_VOLUME, "Estimated Reinforcement Volume"
            { "-1018503", MeasureType.Volume }, // REINFORCEMENT_VOLUME, "Reinforcement Volume"
            { "-1114303", MeasureType.Volume }, // ZONE_VOLUME, "Occupied Volume"
            { "-1114331", MeasureType.Volume }, // ZONE_VOLUME_GROSS, "Gross Volume"
            { "-1114821", MeasureType.Volume }, // SPACE_VOLUME, "Volume"
            { "-1140253", MeasureType.Volume }, // RBS_PIPE_VOLUME_PARAM, "Volume"
            { "-1140361", MeasureType.Volume }, // MATERIAL_VOLUME, "Volume"
            { "-1150425", MeasureType.Volume }, // RBS_INSULATION_LINING_VOLUME, "Volume"
            { "-1155140", MeasureType.Volume }, // STEEL_ELEM_PLATE_VOLUME, "Volume"
            { "-1155148", MeasureType.Volume }, // STEEL_ELEM_PROFILE_VOLUME, "Volume"
            { "-1155232", MeasureType.Volume }, // LAYER_ELEM_VOLUME_COMPUTED, "Volume"
            { "-1180303", MeasureType.Volume }, // INDIVIDUAL_EXCAVATION_VOLUME, "Individual Excavation Volume"
            { "-1180306", MeasureType.Volume }, // EXCAVATION_VOLUME, "Excavation Volume"
            { "-1180307", MeasureType.Volume }, // TOTAL_EXCAVATION_VOLUME, "Total Excavation Volume"
        };
    }
}
