using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// A collection of well-known scalar measure types
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
        Radius = 8,
        Diameter = 9,
        Thickness = 10,
        Elevation = 11,
        Offset = 12,
        Depth = 13,
        Size = 14,
        Distance = 15,
        // [MAINTAIN]
        //   - Add more measure types as needed.
        //   - Update documentation @ VIM SQL ObjectModelContextCustom.cs > PocoParameter > MeasureType
        //   - *DO NOT CHANGE THE ACTUAL ENUM VALUES*
    }

    public static class ParameterMeasureInfo
    {
        /// <summary>
        /// Maps the lowercase parameter name to the MeasureType.
        /// </summary>
        public static readonly Dictionary<string, MeasureType> NameLowerInvariantToMeasureTypeMap =
            Enum.GetValues(typeof(MeasureType))
                .OfType<MeasureType>()
                .ToDictionary(mt => mt.ToString("G").ToLowerInvariant(), mt => mt);

        /// <summary>
        /// Maps the lowercase parameter name to the regex which detects whether it ends with the parameter name followed by an optional space and digits (ex: "Pipe Length 1")
        /// </summary>
        public static readonly Regex[] MeasureTypeRegex =
            Enum.GetValues(typeof(MeasureType))
                .OfType<MeasureType>()
                .Select(mt =>
                {
                    var lowerStringEscaped = Regex.Escape(mt.ToString("G").ToLowerInvariant());
                    return new Regex($@"^(?:{lowerStringEscaped}|.*{lowerStringEscaped}\s*\d*)$", RegexOptions.Compiled);
                })
                .ToArray();

        public static MeasureType ParseMeasureType(string parameterDescriptorNameLowerInvariant, string parameterDescriptorGuid)
        {
            // NOTE: parameterDescriptorGuid is either the Revit built-in ID (if the parameter is built-in), or a guid (if the parameter is shared).
            if (BuiltInIdToMeasureTypeMap.TryGetValue(parameterDescriptorGuid, out var measureType))
                return measureType;

            // Compare the input descriptor name to the known mapping
            if (NameLowerInvariantToMeasureTypeMap.TryGetValue(parameterDescriptorNameLowerInvariant, out measureType))
                return measureType;

            // Fall back to a string comparison to catch any localized parameter descriptor names which either start or end with the key followed by a space and digits,
            // for example: "Length of Beam" or "Pipe Length 1"
            foreach (var kv in NameLowerInvariantToMeasureTypeMap)
            {
                var candidateMeasureType = kv.Value;

                // Skip the unknown measure type.
                if (candidateMeasureType == MeasureType.Unknown)
                    continue;

                var regex = MeasureTypeRegex[(int) candidateMeasureType];
                if (regex.IsMatch(parameterDescriptorNameLowerInvariant))
                    return candidateMeasureType;
            }

            return MeasureType.Unknown;
        }

        public static void GetQuantityOrDisplayValue(
            double? maybeQuantity,
            string displayValue,
            MeasureType measureType,
            out string outQuantityInFeetRvtOrDisplayValue,
            out string outQuantityInMetersOrDisplayValue)
        {
            outQuantityInFeetRvtOrDisplayValue = "";
            outQuantityInMetersOrDisplayValue = "";

            if (maybeQuantity == null)
            {
                // Use the display value.
                outQuantityInFeetRvtOrDisplayValue = outQuantityInMetersOrDisplayValue = displayValue;
                return;
            }

            var q = maybeQuantity.Value;

            // Use parsed quantity depending on the value
            switch (measureType)
            {
                case MeasureType.Angle:
                case MeasureType.Slope:
                    outQuantityInFeetRvtOrDisplayValue = outQuantityInMetersOrDisplayValue = (Units.RadiansToDegrees(q) ?? 0d).ToString(CultureInfo.InvariantCulture);
                    break;
                case MeasureType.Length:
                case MeasureType.Width:
                case MeasureType.Height:
                case MeasureType.Radius:
                case MeasureType.Diameter:
                case MeasureType.Thickness:
                case MeasureType.Elevation:
                case MeasureType.Offset:
                case MeasureType.Depth:
                case MeasureType.Size:
                case MeasureType.Distance:
                    outQuantityInFeetRvtOrDisplayValue = q.ToString(CultureInfo.InvariantCulture);
                    outQuantityInMetersOrDisplayValue = (Units.FeetToMeters(q) ?? 0d).ToString(CultureInfo.InvariantCulture);
                    break;
                case MeasureType.Area:
                    outQuantityInFeetRvtOrDisplayValue = q.ToString(CultureInfo.InvariantCulture);
                    outQuantityInMetersOrDisplayValue = (Units.SquareFeetToSquareMeters(q) ?? 0d).ToString(CultureInfo.InvariantCulture);
                    break;
                case MeasureType.Volume:
                    outQuantityInFeetRvtOrDisplayValue = q.ToString(CultureInfo.InvariantCulture);
                    outQuantityInMetersOrDisplayValue = (Units.CubicFeetToCubicMeters(q) ?? 0d).ToString(CultureInfo.InvariantCulture);
                    break;
                case MeasureType.Unknown:
                default:
                    // Use the display value.
                    outQuantityInFeetRvtOrDisplayValue = outQuantityInMetersOrDisplayValue = displayValue;
                    break;
            }
        }

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

            { "-1001000", MeasureType.Width }, // WALL_ATTR_WIDTH_PARAM, "Width"
            { "-1001301", MeasureType.Width }, // FAMILY_WIDTH_PARAM, "Width"
            // duplicate: "-1001301", // CASEWORK_WIDTH, "Width"
            // duplicate: "-1001301", // DOOR_WIDTH, "Width"
            // duplicate: "-1001301", // FURNITURE_WIDTH, "Width"
            // duplicate: "-1001301", // GENERIC_WIDTH, "Width"
            // duplicate: "-1001301", // WINDOW_WIDTH, "Width"
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

            
            {"-1001001", MeasureType.Height }, // WALL_ATTR_HEIGHT_PARAM, "Height"
            {"-1001135", MeasureType.Height }, // DPART_HEIGHT_COMPUTED, "Height"
            {"-1001300", MeasureType.Height }, // FAMILY_HEIGHT_PARAM, "Height"
            // duplicate: "-1001300", // CASEWORK_HEIGHT, "Height"
            // duplicate: "-1001300", // DOOR_HEIGHT, "Height"
            // duplicate: "-1001300", // FURNITURE_HEIGHT, "Height"
            // duplicate: "-1001300", // GENERIC_HEIGHT, "Height"
            // duplicate: "-1001300", // WINDOW_HEIGHT, "Height"
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

            { "-1004010", MeasureType.Radius }, // CURVE_ELEM_ARC_RADIUS, "Radius"
            { "-1004014", MeasureType.Radius }, // RADIAL_ARRAY_ARC_RADIUS, "Radius"
            { "-1007350", MeasureType.Radius }, // CIRC_MULLION_RADIUS, "Radius"
            { "-1008201", MeasureType.Radius }, // CALLOUT_CORNER_SHEET_RADIUS, "Corner Radius"
            { "-1010020", MeasureType.Radius }, // ELLIPSE_X_PARAM, "X-Radius Value for Ellipse (for Use with XAML Data Template example)"
            { "-1010021", MeasureType.Radius }, // ELLIPSE_Y_PARAM, "Y-Radius Value for Ellipse (for Use with XAML Data Template example)"
            { "-1012407", MeasureType.Radius }, // BOUNDARY_RADIUS, "Radius"
            { "-1012618", MeasureType.Radius }, // PROPERTY_SEGMENT_RADIUS, "Radius"
            { "-1017020", MeasureType.Radius }, // REBAR_BAR_MAXIMUM_BEND_RADIUS, "Maximum Bend Radius"
            { "-1133401", MeasureType.Radius }, // CONNECTOR_RADIUS, "Radius"
            { "-1140115", MeasureType.Radius }, // RBS_CABLETRAY_BENDRADIUS, "Bend Radius"
            { "-1140116", MeasureType.Radius }, // RBS_CONDUIT_BENDRADIUS, "Bend Radius"
            { "-1150338", MeasureType.Radius }, // CONTINUOUSRAIL_FILLET_RADIUS_PARAM, "Fillet Radius"
            { "-1151906", MeasureType.Radius }, // STAIRS_WINDERPATTERN_RADIUS_INTERIOR, "Fillet Radius": The fillet corner radius on the interior boundary
            { "-1155028", MeasureType.Radius }, // STEEL_ELEM_PARAM_RADIUS, "Radius"
            { "-1155059", MeasureType.Radius }, // STEEL_ELEM_PATTERN_RADIUS, "Radius"

            { "-1005504", MeasureType.Diameter }, // STRUCTURAL_SECTION_COMMON_DIAMETER, "Diameter"
            { "-1005539", MeasureType.Diameter }, // STRUCTURAL_SECTION_ISHAPE_BOLT_DIAMETER, "Bolt Diameter"
            { "-1005546", MeasureType.Diameter }, // STRUCTURAL_SECTION_LANGLE_BOLT_DIAMETER_LONGER_FLANGE, "Bolt Diameter Longer Flange"
            { "-1005547", MeasureType.Diameter }, // STRUCTURAL_SECTION_LANGLE_BOLT_DIAMETER_SHORTER_FLANGE, "Bolt Diameter Shorter Flange"
            { "-1017000", MeasureType.Diameter }, // REBAR_BAR_DIAMETER, "Bar Diameter"
            { "-1017010", MeasureType.Diameter }, // REBAR_STANDARD_BEND_DIAMETER, "Standard Bend Diameter"
            { "-1017019", MeasureType.Diameter }, // REBAR_BAR_STIRRUP_BEND_DIAMETER, "Stirrup/Tie Bend Diameter"
            { "-1017037", MeasureType.Diameter }, // REBAR_INSTANCE_BAR_DIAMETER, "Bar Diameter"
            { "-1017038", MeasureType.Diameter }, // REBAR_INSTANCE_BEND_DIAMETER, "Bend Diameter"
            { "-1017041", MeasureType.Diameter }, // REBAR_STANDARD_HOOK_BEND_DIAMETER, "Standard Hook Bend Diameter"
            { "-1017048", MeasureType.Diameter }, // REBAR_SHAPE_OUT_OF_PLANE_BEND_DIAMETER, "Out of Plane Bend Diameter"
            { "-1017601", MeasureType.Diameter }, // FABRIC_WIRE_DIAMETER, "Nominal Diameter": Nominal Diameter of Fabric Wire.
            { "-1017625", MeasureType.Diameter }, // FABRIC_BEND_DIAMETER, "Bend Diameter": Standard Bend Diameter of Fabric Wire.
            { "-1114103", MeasureType.Diameter }, // RBS_CURVE_DIAMETER_PARAM, "Diameter"
            { "-1114127", MeasureType.Diameter }, // RBS_EQ_DIAMETER_PARAM, "Equivalent Diameter"
            { "-1114129", MeasureType.Diameter }, // RBS_HYDRAULIC_DIAMETER_PARAM, "Hydraulic Diameter"
            { "-1133415", MeasureType.Diameter }, // CONNECTOR_DIAMETER, "Diameter"
            { "-1133416", MeasureType.Diameter }, // CONNECTOR_INSIDE_DIAMETER, "Inside Diameter"
            { "-1140123", MeasureType.Diameter }, // RBS_CONDUIT_DIAMETER_PARAM, "Diameter(Trade Size)"
            { "-1140126", MeasureType.Diameter }, // RBS_CONDUIT_INNER_DIAM_PARAM, "Inside Diameter"
            { "-1140127", MeasureType.Diameter }, // RBS_CONDUIT_OUTER_DIAM_PARAM, "Outside Diameter"
            { "-1140135", MeasureType.Diameter }, // RBS_CONDUITRUN_DIAMETER_PARAM, "Diameter(Trade Size)"
            { "-1140136", MeasureType.Diameter }, // RBS_CONDUITRUN_INNER_DIAM_PARAM, "Inside Diameter"
            { "-1140137", MeasureType.Diameter }, // RBS_CONDUITRUN_OUTER_DIAM_PARAM, "Outside Diameter"
            { "-1140212", MeasureType.Diameter }, // RBS_PIPE_INNER_DIAM_PARAM, "Inside Diameter"
            { "-1140225", MeasureType.Diameter }, // RBS_PIPE_DIAMETER_PARAM, "Diameter"
            { "-1140238", MeasureType.Diameter }, // RBS_PIPE_OUTER_DIAMETER, "Outside Diameter"
            { "-1140912", MeasureType.Diameter }, // FABRICATION_PART_DIAMETER_IN, "Main Primary Diameter"
            { "-1140933", MeasureType.Diameter }, // FABRICATION_PART_DIAMETER_OUT, "Main Secondary Diameter"
            { "-1140952", MeasureType.Diameter }, // FABRICATION_PART_DIAMETER_IN_OPTION, "Main Primary Diameter Option"
            { "-1140955", MeasureType.Diameter }, // FABRICATION_PART_DIAMETER_OUT_OPTION, "Main Secondary Diameter Option"
            { "-1150129", MeasureType.Diameter }, // FBX_LIGHT_EMIT_CIRCLE_DIAMETER, "Emit from Circle Diameter"
            { "-1150130", MeasureType.Diameter }, // FBX_LIGHT_SOURCE_DIAMETER, "Light Source Symbol Size"
            { "-1154651", MeasureType.Diameter }, // COUPLER_WIDTH, "External Diameter"
            { "-1155008", MeasureType.Diameter }, // STEEL_ELEM_BOLT_DIAMETER, "Diameter"
            { "-1155013", MeasureType.Diameter }, // STEEL_ELEM_ANCHOR_DIAMETER, "Diameter"
            { "-1155016", MeasureType.Diameter }, // STEEL_ELEM_SHEARSTUD_DIAMETER, "Diameter"
            { "-1155061", MeasureType.Diameter }, // STEEL_ELEM_HOLE_DIAMETER, "Diameter"
            { "-1155068", MeasureType.Diameter }, // STEEL_ELEM_HOLE_HEAD_DIAMETER, "Head diameter"
            { "-1155223", MeasureType.Diameter }, // REBAR_MODEL_BAR_DIAMETER, "Model Bar Diameter"
            { "-1155224", MeasureType.Diameter }, // REBAR_INSTANCE_BAR_MODEL_DIAMETER, "Model Bar Diameter"
            { "-1155248", MeasureType.Diameter }, // CIRCULAR_FRAMING_DIAMETER, "Circular Diameter"
            { "-1155299", MeasureType.Diameter }, // BENDING_DETAIL_TYPE_BEND_DIAMETER_DIMENSIONS_ENABLED, "Bend Diameter Dimensions"
            { "-1155301", MeasureType.Diameter }, // BENDING_DETAIL_TYPE_DIAMETER_DIMENSION_TYPE_ID, "Diameter Dimension Style"

            { "-1001134", MeasureType.Thickness }, // DPART_LAYER_WIDTH, "Thickness"
            { "-1001302", MeasureType.Thickness }, // FAMILY_THICKNESS_PARAM, "Thickness"
            // duplicate: "-1001302" // DOOR_THICKNESS, "Thickness"
            // duplicate: "-1001302" // FURNITURE_THICKNESS, "Thickness"
            // duplicate: "-1001302" // GENERIC_THICKNESS, "Thickness"
            // duplicate: "-1001302" // WINDOW_THICKNESS, "Thickness"
            { "-1001557", MeasureType.Thickness }, // STRUCTURAL_FOUNDATION_THICKNESS, "Foundation Thickness"
            { "-1001600", MeasureType.Thickness }, // ROOF_ATTR_DEFAULT_THICKNESS_PARAM, "Default Thickness"
            { "-1001601", MeasureType.Thickness }, // ROOF_ATTR_THICKNESS_PARAM, "Thickness"
            { "-1001656", MeasureType.Thickness }, // STRUCTURAL_FLOOR_CORE_THICKNESS, "Core Thickness"
            { "-1001900", MeasureType.Thickness }, // FLOOR_ATTR_THICKNESS_PARAM, "Thickness"
            { "-1001902", MeasureType.Thickness }, // FLOOR_ATTR_DEFAULT_THICKNESS_PARAM, "Default Thickness"
            { "-1002206", MeasureType.Thickness }, // CEILING_THICKNESS, "Thickness"
            { "-1002301", MeasureType.Thickness }, // CEILING_THICKNESS_PARAM, "Thickness"
            { "-1005505", MeasureType.Thickness }, // STRUCTURAL_SECTION_PIPESTANDARD_WALLNOMINALTHICKNESS, "Wall Nominal Thickness"
            { "-1005506", MeasureType.Thickness }, // STRUCTURAL_SECTION_PIPESTANDARD_WALLDESIGNTHICKNESS, "Wall Design Thickness"
            { "-1005524", MeasureType.Thickness }, // STRUCTURAL_SECTION_ISHAPE_FLANGETHICKNESS, "Flange Thickness"
            { "-1005525", MeasureType.Thickness }, // STRUCTURAL_SECTION_ISHAPE_WEBTHICKNESS, "Web Thickness"
            { "-1005531", MeasureType.Thickness }, // STRUCTURAL_SECTION_IWELDED_TOPFLANGETHICKNESS, "Top Flange Thickness"
            { "-1005533", MeasureType.Thickness }, // STRUCTURAL_SECTION_IWELDED_BOTTOMFLANGETHICKNESS, "Bottom Flange Thickness"
            { "-1005566", MeasureType.Thickness }, // STRUCTURAL_SECTION_ISHAPE_FLANGETHICKNESS_LOCATION, "Flange Thickness Location"
            { "-1005567", MeasureType.Thickness }, // STRUCTURAL_SECTION_ISHAPE_WEBTHICKNESS_LOCATION, "Web Thickness Location"
            { "-1007210", MeasureType.Thickness }, // STAIRS_ATTR_STRINGER_THICKNESS, "Stringer Thickness"
            { "-1007211", MeasureType.Thickness }, // STAIRS_ATTR_TREAD_THICKNESS, "Tread Thickness"
            { "-1007261", MeasureType.Thickness }, // STAIRS_ATTR_RISER_THICKNESS, "Riser Thickness"
            { "-1007304", MeasureType.Thickness }, // RECT_MULLION_THICK, "Thickness"
            { "-1007322", MeasureType.Thickness }, // CUST_MULLION_THICK, "Thickness"
            { "-1007737", MeasureType.Thickness }, // IMPORT_ADT_ENTITY_THICKNESS, "Thickness"
            { "-1008304", MeasureType.Thickness }, // RAMP_ATTR_THICKNESS, "Thickness"
            { "-1008604", MeasureType.Thickness }, // STAIRS_RAILING_THICKNESS, "Railing Thickness"
            { "-1010304", MeasureType.Thickness }, // CURTAIN_WALL_SYSPANEL_THICKNESS, "Thickness"
            { "-1012501", MeasureType.Thickness }, // BUILDINGPAD_THICKNESS, "Thickness"
            { "-1013455", MeasureType.Thickness }, // ANALYTICAL_PANEL_THICKNESS, "Thickness": Used for Analytical Panel
            { "-1114117", MeasureType.Thickness }, // RBS_INSULATION_THICKNESS, "Insulation Thickness"
            { "-1114118", MeasureType.Thickness }, // RBS_LINING_THICKNESS, "Lining Thickness"
            { "-1114358", MeasureType.Thickness }, // RBS_INSULATION_THICKNESS_FOR_DUCT, "Insulation Thickness"
            { "-1114359", MeasureType.Thickness }, // RBS_INSULATION_THICKNESS_FOR_PIPE, "Insulation Thickness"
            { "-1114360", MeasureType.Thickness }, // RBS_LINING_THICKNESS_FOR_DUCT, "Lining Thickness"
            { "-1114851", MeasureType.Thickness }, // THERMAL_MATERIAL_THICKNESS, "Thickness"
            { "-1140111", MeasureType.Thickness }, // RBS_CABLETRAY_THICKNESS, "Thickness"
            { "-1140241", MeasureType.Thickness }, // RBS_PIPE_INSULATION_THICKNESS, "Insulation Thickness"
            { "-1140972", MeasureType.Thickness }, // FABRICATION_PART_DOUBLEWALL_MATERIAL_THICKNESS, "Double Wall Material Thickness"
            { "-1140978", MeasureType.Thickness }, // FABRICATION_PART_MATERIAL_THICKNESS, "Part Material Thickness"
            { "-1141040", MeasureType.Thickness }, // RBS_PIPE_WALL_THICKNESS, "Wall Thickness"
            { "-1150431", MeasureType.Thickness }, // RBS_REFERENCE_INSULATION_THICKNESS, "Insulation Thickness": This parameter is obsolete. Use DUCT_INSULATION_THICKNESS and PIPE_INSULATION_THICKNESS.
            { "-1150433", MeasureType.Thickness }, // RBS_REFERENCE_LINING_THICKNESS, "Lining Thickness"
            { "-1150436", MeasureType.Thickness }, // DUCT_INSULATION_THICKNESS, "Insulation Thickness"
            { "-1150437", MeasureType.Thickness }, // PIPE_INSULATION_THICKNESS, "Insulation Thickness"
            { "-1151226", MeasureType.Thickness }, // STAIRSTYPE_NOTCH_THICKNESS, "Notch Thickness":
            { "-1151507", MeasureType.Thickness }, // STAIRS_LANDING_THICKNESS, "Total Thickness": Thickness
            { "-1151603", MeasureType.Thickness }, // STAIRS_LANDINGTYPE_THICKNESS, "Monolithic Thickness": Default thickness
            { "-1152152", MeasureType.Thickness }, // STAIRS_TRISERTYPE_TREAD_THICKNESS, "Tread Thickness"
            { "-1152160", MeasureType.Thickness }, // STAIRS_TRISERTYPE_RISER_THICKNESS, "Riser Thickness"
            { "-1155003", MeasureType.Thickness }, // STEEL_ELEM_PLATE_THICKNESS, "Thickness"
            { "-1155032", MeasureType.Thickness }, // STEEL_ELEM_WELD_MAIN_THICKNESS, "Main Thickness"
            { "-1155044", MeasureType.Thickness }, // STEEL_ELEM_WELD_DOUBLE_THICKNESS, "Double Thickness"
            { "-1155230", MeasureType.Thickness }, // LAYER_TYPE_THICKNESS, "Thickness": Thickness
            { "-1155235", MeasureType.Thickness }, // LAYER_ELEM_THICKNESS, "Thickness": Thickness
            { "-1155253", MeasureType.Thickness }, // TOPOSOLID_TYPE_DEFAULT_THICKNESS_PARAM, "Default Thickness"
            { "-1155254", MeasureType.Thickness }, // TOPOSOLID_ATTR_THICKNESS_PARAM, "Thickness"

            { "-1001320", MeasureType.Elevation }, // FAMILY_WPB_DEFAULT_ELEVATION, "Default Elevation"
            { "-1001360", MeasureType.Elevation }, // INSTANCE_ELEVATION_PARAM, "Elevation from Level"
            { "-1001397", MeasureType.Elevation }, // STRUCTURAL_ATTACHMENT_START_VALUE_ELEVATION, "Start Attachment Elevation"
            { "-1001398", MeasureType.Elevation }, // STRUCTURAL_ATTACHMENT_END_VALUE_ELEVATION, "End Attachment Elevation"
            { "-1001561", MeasureType.Elevation }, // STRUCTURAL_ELEVATION_AT_BOTTOM, "Elevation at Bottom"
            { "-1001571", MeasureType.Elevation }, // STRUCTURAL_BEAM_END0_ELEVATION, "Start Level Offset"
            { "-1001572", MeasureType.Elevation }, // STRUCTURAL_BEAM_END1_ELEVATION, "End Level Offset"
            { "-1001598", MeasureType.Elevation }, // STRUCTURAL_ELEVATION_AT_TOP, "Elevation at Top"
            { "-1001653", MeasureType.Elevation }, // STRUCTURAL_REFERENCE_LEVEL_ELEVATION, "Reference Level Elevation"
            { "-1001654", MeasureType.Elevation }, // STRUCTURAL_ELEVATION_AT_TOP_CORE, "Elevation at Top Core"
            { "-1001655", MeasureType.Elevation }, // STRUCTURAL_ELEVATION_AT_BOTTOM_CORE, "Elevation at Bottom Core"
            { "-1001657", MeasureType.Elevation }, // STRUCTURAL_ELEVATION_AT_TOP_SURVEY, "Elevation at Top Survey"
            { "-1001658", MeasureType.Elevation }, // STRUCTURAL_ELEVATION_AT_BOTTOM_SURVEY, "Elevation at Bottom Survey"
            { "-1005000", MeasureType.Elevation }, // VIEWER_EYE_ELEVATION, "Eye Elevation"
            { "-1005002", MeasureType.Elevation }, // VIEWER_TARGET_ELEVATION, "Target Elevation"
            { "-1005331", MeasureType.Elevation }, // VIEW_GRAPH_SCHED_LEVEL_RELATIVE_BASE_TYPE, "Elevation Base for Levels"
            { "-1006437", MeasureType.Elevation }, // SPOT_ELEV_BASE, "Elevation Base"
            { "-1006452", MeasureType.Elevation }, // SPOT_ELEV_IND_ELEVATION, "Elevation Indicator"
            { "-1006469", MeasureType.Elevation }, // SPOT_ELEV_DISPLAY_ELEVATIONS, "Display Elevations"
            { "-1006486", MeasureType.Elevation }, // SPOT_COORDINATE_ELEVATION_PREFIX, "Elevation Prefix"
            { "-1006487", MeasureType.Elevation }, // SPOT_COORDINATE_ELEVATION_SUFFIX, "Elevation Suffix"
            { "-1006488", MeasureType.Elevation }, // SPOT_COORDINATE_INCLUDE_ELEVATION, "Include Elevation"
            { "-1006489", MeasureType.Elevation }, // SPOT_ELEV_IND_TYPE_ELEVATION, "Elevation Indicator as Prefix/Suffix"
            { "-1007010", MeasureType.Elevation }, // TAG_ELEVATION_BASE, "Elevation Base"
            { "-1007102", MeasureType.Elevation }, // LEVEL_ELEV, "Elevation"
            { "-1007109", MeasureType.Elevation }, // LEVEL_RELATIVE_BASE_TYPE, "Elevation Base"
            { "-1007610", MeasureType.Elevation }, // ELEV_SYMBOL_ID, "Elevation Mark"
            { "-1008207", MeasureType.Elevation }, // ELEVATN_TAG, "Elevation Tag"
            { "-1012400", MeasureType.Elevation }, // POINT_ELEVATION, "Elevation"
            { "-1012401", MeasureType.Elevation }, // CONTOUR_ELEVATION, "Elevation"
            { "-1012402", MeasureType.Elevation }, // CONTOUR_ELEVATION_STEP, "Increment"
            { "-1012621", MeasureType.Elevation }, // CONTOUR_LABELS_ELEV_BASE_TYPE, "Elevation Base"
            { "-1012842", MeasureType.Elevation }, // FAMILY_IS_ELEVATION_MARK_BODY, "Elevation Mark Use"
            { "-1114002", MeasureType.Elevation }, // RBS_START_OFFSET_PARAM, "Start Middle Elevation"
            { "-1114003", MeasureType.Elevation }, // RBS_END_OFFSET_PARAM, "End Middle Elevation"
            { "-1114132", MeasureType.Elevation }, // RBS_OFFSET_PARAM, "Middle Elevation"
            { "-1140096", MeasureType.Elevation }, // RBS_ELEC_WIRE_ELEVATION, "Elevation"
            { "-1140124", MeasureType.Elevation }, // RBS_CTC_TOP_ELEVATION, "Upper End Top Elevation"
            { "-1140125", MeasureType.Elevation }, // RBS_CTC_BOTTOM_ELEVATION, "Lower End Bottom Elevation"
            { "-1140237", MeasureType.Elevation }, // RBS_PIPE_INVERT_ELEVATION, "Invert Elevation": This parameter is obsolete. It exists only for compatibility.
            { "-1140239", MeasureType.Elevation }, // RBS_DUCT_TOP_ELEVATION, "Upper End Top Elevation"
            { "-1140240", MeasureType.Elevation }, // RBS_DUCT_BOTTOM_ELEVATION, "Lower End Bottom Elevation"
            { "-1140707", MeasureType.Elevation }, // TRUSS_ELEMENT_END0_ELEVATION, "Start Level Offset"
            { "-1140708", MeasureType.Elevation }, // TRUSS_ELEMENT_END1_ELEVATION, "End Level Offset"
            { "-1140917", MeasureType.Elevation }, // FABRICATION_OFFSET_PARAM, "Middle Elevation"
            { "-1140918", MeasureType.Elevation }, // FABRICATION_TOP_OF_PART, "Upper End Top of Insulation Elevation"
            { "-1140919", MeasureType.Elevation }, // FABRICATION_BOTTOM_OF_PART, "Lower End Bottom of Insulation Elevation"
            { "-1140924", MeasureType.Elevation }, // FABRICATION_START_OFFSET_PARAM, "Start Middle Elevation"
            { "-1140925", MeasureType.Elevation }, // FABRICATION_END_OFFSET_PARAM, "End Middle Elevation"
            { "-1140984", MeasureType.Elevation }, // MEP_SPOT_TOP_ELEVATION, "Spot Top Elevation": used for both design and fabrication components
            // duplicate: "-1140984" // FABRICATION_SPOT_TOP_ELEVATION_OF_PART, "Spot Top Elevation": used for both design and fabrication components
            { "-1140985", MeasureType.Elevation }, // MEP_SPOT_TOP_ELEVATION_INCLUDE_INSULATION, "Spot Top of Insulation Elevation"
            // duplicate: "-1140985" // FABRICATION_SPOT_TOP_ELEVATION_INCLUDE_INSULATION_OF_PART, "Spot Top of Insulation Elevation"
            { "-1140986", MeasureType.Elevation }, // MEP_SPOT_BOTTOM_ELEVATION, "Spot Bottom Elevation": used for both design and fabrication components
            // duplicate:  "-1140986" // FABRICATION_SPOT_BOTTOM_ELEVATION_OF_PART, "Spot Bottom Elevation": used for both design and fabrication components
            { "-1140987", MeasureType.Elevation }, // MEP_SPOT_BOTTOM_ELEVATION_INCLUDE_INSULATION, "Spot Bottom of Insulation Elevation"
            // duplicate: "-1140987" // FABRICATION_SPOT_BOTTOM_ELEVATION_INCLUDE_INSULATION_OF_PART, "Spot Bottom of Insulation Elevation"
            { "-1140988", MeasureType.Elevation }, // MEP_SPOT_CENTERLINE_ELEVATION, "Spot Centerline Elevation": used for both design and fabrication components
            // duplicate: "-1140988" // FABRICATION_CENTERLINE_ELEVATION_OF_PART, "Spot Centerline Elevation": used for both design and fabrication components
            { "-1140989", MeasureType.Elevation }, // FABRICATION_TOP_ELEVATION_OF_PART, "Top Elevation"
            { "-1140990", MeasureType.Elevation }, // FABRICATION_TOP_ELEVATION_INCLUDE_INSULATION_OF_PART, "Top Elevation with Insulation"
            { "-1140991", MeasureType.Elevation }, // FABRICATION_BOTTOM_ELEVATION_OF_PART, "Bottom Elevation"
            { "-1140992", MeasureType.Elevation }, // FABRICATION_BOTTOM_ELEVATION_INCLUDE_INSULATION_OF_PART, "Bottom Elevation with Insulation"
            { "-1140993", MeasureType.Elevation }, // FABRICATION_PIPE_INVERT_ELEVATION, "Pipe Invert Elevation": This parameter is obsolete. It exists only for compatibility.
            { "-1141020", MeasureType.Elevation }, // MEP_UPPER_CENTERLINE_ELEVATION, "Upper End Centerline Elevation"
            { "-1141021", MeasureType.Elevation }, // MEP_LOWER_CENTERLINE_ELEVATION, "Lower End Centerline Elevation"
            { "-1141022", MeasureType.Elevation }, // MEP_UPPER_TOP_ELEVATION, "Upper End Top Elevation"
            { "-1141023", MeasureType.Elevation }, // MEP_UPPER_BOTTOM_ELEVATION, "Upper End Bottom Elevation"
            { "-1141024", MeasureType.Elevation }, // MEP_LOWER_TOP_ELEVATION, "Lower End Top Elevation"
            { "-1141025", MeasureType.Elevation }, // MEP_LOWER_BOTTOM_ELEVATION, "Lower End Bottom Elevation"
            { "-1141026", MeasureType.Elevation }, // MEP_UPPER_TOP_ELEVATION_INCLUDE_INSULATION, "Upper End Top of Insulation Elevation"
            { "-1141027", MeasureType.Elevation }, // MEP_UPPER_BOTTOM_ELEVATION_INCLUDE_INSULATION, "Upper End Bottom of Insulation Elevation"
            { "-1141028", MeasureType.Elevation }, // MEP_LOWER_TOP_ELEVATION_INCLUDE_INSULATION, "Lower End Top of Insulation Elevation"
            { "-1141029", MeasureType.Elevation }, // MEP_LOWER_BOTTOM_ELEVATION_INCLUDE_INSULATION, "Lower End Bottom of Insulation Elevation"
            { "-1141030", MeasureType.Elevation }, // MEP_PIPE_UPPER_OBVERT_ELEVATION, "Upper End Obvert Elevation"
            { "-1141031", MeasureType.Elevation }, // MEP_PIPE_LOWER_OBVERT_ELEVATION, "Lower End Obvert Elevation"
            { "-1141032", MeasureType.Elevation }, // MEP_PIPE_UPPER_INVERT_ELEVATION, "Upper End Invert Elevation"
            { "-1141033", MeasureType.Elevation }, // MEP_PIPE_LOWER_INVERT_ELEVATION, "Lower End Invert Elevation"
            { "-1150193", MeasureType.Elevation }, // BASEPOINT_ELEVATION_PARAM, "Elev"
            { "-1151301", MeasureType.Elevation }, // STAIRS_RUN_BOTTOM_ELEVATION, "Relative Base Height": Relative height to stairs bottom elevation
            { "-1151302", MeasureType.Elevation }, // STAIRS_RUN_TOP_ELEVATION, "Relative Top Height": Top height of run
            { "-1151501", MeasureType.Elevation }, // STAIRS_LANDING_BASE_ELEVATION, "Relative Height": Height
            { "-1154647", MeasureType.Elevation }, // FAMILY_FREEINST_DEFAULT_ELEVATION, "Default Elevation"
            { "-1155114", MeasureType.Elevation }, // RBS_PIPE_TOP_ELEVATION, "Upper End Top Elevation"
            { "-1155115", MeasureType.Elevation }, // RBS_PIPE_BOTTOM_ELEVATION, "Lower End Bottom Elevation"
            { "-1155260", MeasureType.Elevation }, // TOPOSOLID_ELEVATION_AT_BOTTOM, "Elevation at Bottom"
            { "-1155261", MeasureType.Elevation }, // TOPOSOLID_ELEVATION_AT_TOP, "Elevation at Top"
            { "-1155272", MeasureType.Elevation }, // SSE_POINT_ELEVATION, "Elevation"

            { "-1001108", MeasureType.Offset }, // WALL_BASE_OFFSET, "Base Offset"
            { "-1001109", MeasureType.Offset }, // WALL_TOP_OFFSET, "Top Offset"
            { "-1001123", MeasureType.Offset }, // WALL_LOCATION_LINE_OFFSET_PARAM, "Location Line Offset"
            { "-1001357", MeasureType.Offset }, // FAMILY_BASE_LEVEL_OFFSET_PARAM, "Base Offset"
            { "-1001358", MeasureType.Offset }, // FAMILY_TOP_LEVEL_OFFSET_PARAM, "Top Offset"
            { "-1001364", MeasureType.Offset }, // INSTANCE_FREE_HOST_OFFSET_PARAM, "Offset from Host"
            // duplicate: "-1001571" // STRUCTURAL_BEAM_END0_ELEVATION, "Start Level Offset"
            // duplicate: "-1001572" // STRUCTURAL_BEAM_END1_ELEVATION, "End Level Offset"
            { "-1001574", MeasureType.Offset }, // BEAM_V_JUSTIFICATION_OTHER_VALUE, "z-Direction Offset Value"
            { "-1001588", MeasureType.Offset }, // STRUCTURAL_ANALYTICAL_TESS_DEVIATION, "Maximum discretized offset"
            { "-1001652", MeasureType.Offset }, // ROOF_CONSTRAINT_OFFSET_PARAM, "Level Offset"
            { "-1001701", MeasureType.Offset }, // ROOF_LEVEL_OFFSET_PARAM, "Base Offset From Level"
            { "-1001703", MeasureType.Offset }, // ROOF_UPTO_LEVEL_OFFSET_PARAM, "Cutoff Offset"
            { "-1001706", MeasureType.Offset }, // CURVE_WALL_OFFSET, "Wall offset"
            { "-1001707", MeasureType.Offset }, // CURVE_WALL_OFFSET_ROOFS, "Overhang"
            { "-1001716", MeasureType.Offset }, // FACEROOF_OFFSET_PARAM, "Level Offset"
            { "-1001813", MeasureType.Offset }, // PROFILE_OFFSET_X, "Horizontal Profile Offset"
            { "-1001814", MeasureType.Offset }, // PROFILE_OFFSET_Y, "Vertical Profile Offset"
            { "-1001822", MeasureType.Offset }, // PROFILE1_OFFSET_X, "Horizontal Profile Offset"
            { "-1001823", MeasureType.Offset }, // PROFILE1_OFFSET_Y, "Vertical Profile Offset"
            { "-1001827", MeasureType.Offset }, // PROFILE2_OFFSET_X, "Horizontal Profile Offset"
            { "-1001828", MeasureType.Offset }, // PROFILE2_OFFSET_Y, "Vertical Profile Offset"
            { "-1001951", MeasureType.Offset }, // FLOOR_HEIGHTABOVELEVEL_PARAM, "Height Offset From Level"
            { "-1002065", MeasureType.Offset }, // SCHEDULE_BASE_LEVEL_OFFSET_PARAM, "Base Offset"
            { "-1002066", MeasureType.Offset }, // SCHEDULE_TOP_LEVEL_OFFSET_PARAM, "Top Offset"
            { "-1002300", MeasureType.Offset }, // CEILING_HEIGHTABOVELEVEL_PARAM, "Height Offset From Level"
            { "-1002400", MeasureType.Offset }, // SLOPE_START_HEIGHT, "Height Offset at Tail"
            { "-1002401", MeasureType.Offset }, // SLOPE_END_HEIGHT, "Height Offset at Head"
            { "-1002557", MeasureType.Offset }, // COLUMN_TOP_ATTACHMENT_OFFSET_PARAM, "Offset From Attachment At Top"
            { "-1002558", MeasureType.Offset }, // COLUMN_BASE_ATTACHMENT_OFFSET_PARAM, "Offset From Attachment At Base"
            { "-1002565", MeasureType.Offset }, // ASSOCIATED_LEVEL_OFFSET, "Associated Level Offset": The offset from the associated level.
            { "-1005100", MeasureType.Offset }, // VIEWER_BOUND_OFFSET_RIGHT, "Right Clip Offset"
            { "-1005101", MeasureType.Offset }, // VIEWER_BOUND_OFFSET_LEFT, "Left Clip Offset"
            { "-1005102", MeasureType.Offset }, // VIEWER_BOUND_OFFSET_TOP, "Top Clip Offset"
            { "-1005103", MeasureType.Offset }, // VIEWER_BOUND_OFFSET_BOTTOM, "Bottom Clip Offset"
            { "-1005104", MeasureType.Offset }, // VIEWER_BOUND_OFFSET_FAR, "Far Clip Offset"
            { "-1005105", MeasureType.Offset }, // VIEWER_BOUND_OFFSET_NEAR, "Near Clip Offset"
            { "-1006001", MeasureType.Offset }, // ROOF_CURVE_HEIGHT_OFFSET, "Offset From Roof Base"
            { "-1006005", MeasureType.Offset }, // ROOF_CURVE_HEIGHT_AT_WALL, "Plate Offset From Base"
            { "-1006008", MeasureType.Offset }, // CURVE_HEIGHT_OFFSET, "Offset From Base"
            { "-1006477", MeasureType.Offset }, // BASELINE_DIM_OFFSET, "Baseline Offset"
            { "-1006494", MeasureType.Offset }, // SPOT_SLOPE_OFFSET_FROM_REFERENCE, "Offset from Reference"
            { "-1006501", MeasureType.Offset }, // LEADER_OFFSET_SHEET, "Leader/Border Offset"
            { "-1006637", MeasureType.Offset }, // NUMBER_SYSTEM_JUSTIFY_OFFSET, "Justify Offset"
            { "-1006638", MeasureType.Offset }, // NUMBER_SYSTEM_REFERENCE_OFFSET, "Offset from Reference"
            { "-1006924", MeasureType.Offset }, // ROOM_UPPER_OFFSET, "Limit Offset"
            { "-1006925", MeasureType.Offset }, // ROOM_LOWER_OFFSET, "Base Offset"
            { "-1006927", MeasureType.Offset }, // ALWAYS_ZERO_LENGTH, "Base Offset"
            { "-1007218", MeasureType.Offset }, // STAIRS_BASE_OFFSET, "Base Offset"
            { "-1007219", MeasureType.Offset }, // STAIRS_TOP_OFFSET, "Top Offset"
            { "-1007238", MeasureType.Offset }, // STAIRS_ATTR_STRINGER_OFFSET, "Open Stringer Offset"
            { "-1007259", MeasureType.Offset }, // STAIRS_ATTR_STAIRS_CUT_OFFSET, "Extend Below Base"
            { "-1007351", MeasureType.Offset }, // MULLION_OFFSET, "Offset"
            { "-1007703", MeasureType.Offset }, // IMPORT_BASE_LEVEL_OFFSET, "Offset from Work Plane"
            { "-1007725", MeasureType.Offset }, // RVT_LEVEL_OFFSET, "Map Levels..."
            { "-1008203", MeasureType.Offset }, // CALLOUT_SYNCRONIZE_BOUND_OFFSET_FAR, "Far Clip Settings"
            { "-1008617", MeasureType.Offset }, // STAIRS_RAILING_RAIL_OFFSET, "Rail Offset"
            { "-1008619", MeasureType.Offset }, // STAIRS_RAILING_BALUSTER_OFFSET, "Baluster Offset"
            { "-1008621", MeasureType.Offset }, // STAIRS_RAILING_HEIGHT_OFFSET, "Base Offset"
            { "-1010302", MeasureType.Offset }, // CURTAIN_WALL_SYSPANEL_OFFSET, "Offset"
            { "-1012058", MeasureType.Offset }, // ENERGY_ANALYSIS_MASSZONE_COREOFFSET, "Perimeter Zone Depth"
            { "-1012502", MeasureType.Offset }, // BUILDINGPAD_HEIGHTABOVELEVEL_PARAM, "Height Offset From Level"
            { "-1012802", MeasureType.Offset }, // WALL_SWEEP_OFFSET_PARAM, "Offset From Level"
            { "-1012804", MeasureType.Offset }, // WALL_SWEEP_WALL_OFFSET_PARAM, "Offset From Wall"
            { "-1012825", MeasureType.Offset }, // SWEEP_BASE_OFFSET, "Horizontal Profile Offset"
            { "-1012827", MeasureType.Offset }, // SWEEP_BASE_VERT_OFFSET, "Vertical Profile Offset"
            { "-1013312", MeasureType.Offset }, // CURTAINGRID_ORIGIN_VERT, "Offset"
            { "-1013313", MeasureType.Offset }, // CURTAINGRID_ORIGIN_HORIZ, "Offset"
            { "-1013342", MeasureType.Offset }, // CURTAINGRID_ORIGIN_1, "Offset"
            { "-1013343", MeasureType.Offset }, // CURTAINGRID_ORIGIN_2, "Offset"
            { "-1013382", MeasureType.Offset }, // CURTAINGRID_ORIGIN_U, "Offset"
            { "-1013383", MeasureType.Offset }, // CURTAINGRID_ORIGIN_V, "Offset"
            { "-1013412", MeasureType.Offset }, // CURVE_SUPPORT_OFFSET, "Wall offset"
            { "-1013429", MeasureType.Offset }, // CURVE_EDGE_OFFSET, "Wall offset"
            { "-1016006", MeasureType.Offset }, // CWP_LEVEL_OFFSET, "Offset Level"
            { "-1017034", MeasureType.Offset }, // REBAR_SHAPE_START_HOOK_OFFSET, "Start Hook Offset Length"
            { "-1017036", MeasureType.Offset }, // REBAR_SHAPE_END_HOOK_OFFSET, "End Hook Offset Length"
            { "-1017708", MeasureType.Offset }, // FABRIC_PARAM_COVER_OFFSET, "Additional Cover Offset": Additional cover offset of the fabric distribution.
            { "-1017739", MeasureType.Offset }, // FABRIC_WIRE_OFFSET, "Offset along wire direction": Offset along wire direction
            { "-1018024", MeasureType.Offset }, // REBAR_SYSTEM_ADDL_TOP_OFFSET, "Additional Top Cover Offset"
            { "-1018025", MeasureType.Offset }, // REBAR_SYSTEM_ADDL_BOTTOM_OFFSET, "Additional Bottom Cover Offset"
            { "-1018026", MeasureType.Offset }, // REBAR_SYSTEM_ADDL_EXTERIOR_OFFSET, "Additional Exterior Cover Offset"
            { "-1018027", MeasureType.Offset }, // REBAR_SYSTEM_ADDL_INTERIOR_OFFSET, "Additional Interior Cover Offset"
            { "-1018321", MeasureType.Offset }, // PATH_REIN_ALT_OFFSET, "Alternating Bar - Offset"
            { "-1018322", MeasureType.Offset }, // PATH_REIN_ADDL_OFFSET, "Additional Offset"
            { "-1018360", MeasureType.Offset }, // PATH_REIN_SPANLENGTH_ALT_OFFSET, "Offset"
            // duplicate: "-1114002" // RBS_START_OFFSET_PARAM, "Start Middle Elevation"
            // duplicate: "-1114003" // RBS_END_OFFSET_PARAM, "End Middle Elevation"
            { "-1114105", MeasureType.Offset }, // RBS_CURVE_HOR_OFFSET_PARAM, "Horizontal Justification"
            { "-1114106", MeasureType.Offset }, // RBS_CURVE_VERT_OFFSET_PARAM, "Vertical Justification"
            // duplicate: "-1114132" // RBS_OFFSET_PARAM, "Middle Elevation"
            { "-1114211", MeasureType.Offset }, // RBS_FAMILY_CONTENT_OFFSET_WIDTH, "OffsetWidth"
            { "-1114212", MeasureType.Offset }, // RBS_FAMILY_CONTENT_OFFSET_HEIGHT, "OffsetHeight"
            { "-1114237", MeasureType.Offset }, // RBS_CONNECTOR_OFFSET_OBSOLETE, "Connector Offset"
            { "-1114706", MeasureType.Offset }, // ZONE_LEVEL_OFFSET, "Level Offset"
            { "-1114707", MeasureType.Offset }, // ZONE_LEVEL_OFFSET_TOP, "Top Offset"
            { "-1133501", MeasureType.Offset }, // GROUP_OFFSET_FROM_LEVEL, "Origin Level Offset"
            { "-1140175", MeasureType.Offset }, // RBS_ELEC_CIRCUIT_PATH_OFFSET_PARAM, "Offset"
            { "-1140270", MeasureType.Offset }, // RBS_PARALLELCONDUITS_HORIZONTAL_OFFSET_VALUE, "Horizontal Offset value for parallel conduits"
            { "-1140271", MeasureType.Offset }, // RBS_PARALLELCONDUITS_VERTICAL_OFFSET_VALUE, "Vertical Offset value for parallel conduits"
            { "-1140274", MeasureType.Offset }, // RBS_PARALLELPIPES_HORIZONTAL_OFFSET_VALUE, "Horizontal Offset value for parallel pipes"
            { "-1140275", MeasureType.Offset }, // RBS_PARALLELPIPES_VERTICAL_OFFSET_VALUE, "Vertical Offset value for parallel pipes"
            // duplicate: "-1140707" // TRUSS_ELEMENT_END0_ELEVATION, "Start Level Offset"
            // duplicate: "-1140708" // TRUSS_ELEMENT_END1_ELEVATION, "End Level Offset"
            { "-1140717", MeasureType.Offset }, // TRUSS_NON_BEARING_OFFSET_PARAM, "Non Bearing Offset"
            { "-1140751", MeasureType.Offset }, // MATCHLINE_TOP_OFFSET, "Top Offset"
            { "-1140752", MeasureType.Offset }, // MATCHLINE_BOTTOM_OFFSET, "Bottom Offset"
            // duplicate: "-1140917" // FABRICATION_OFFSET_PARAM, "Middle Elevation"
            // duplicate: "-1140924" // FABRICATION_START_OFFSET_PARAM, "Start Middle Elevation"
            // duplicate: "-1140925" // FABRICATION_END_OFFSET_PARAM, "End Middle Elevation"
            { "-1150055", MeasureType.Offset }, // DIVIDED_SURFACE_OFFSET_FROM_SURFACE, "Offset from surface"
            { "-1150144", MeasureType.Offset }, // POINT_ELEMENT_OFFSET, "Offset"
            { "-1150162", MeasureType.Offset }, // LOCKED_TOP_OFFSET, "Positive Offset"
            { "-1150163", MeasureType.Offset }, // LOCKED_BASE_OFFSET, "Negative Offset"
            { "-1150164", MeasureType.Offset }, // LOCKED_START_OFFSET, "Negative Offset"
            { "-1150165", MeasureType.Offset }, // LOCKED_END_OFFSET, "Positive Offset"
            { "-1150170", MeasureType.Offset }, // VIEW_SLANTED_COLUMN_SYMBOL_OFFSET, "Column Symbolic Offset"
            { "-1150332", MeasureType.Offset }, // RAILING_SYSTEM_HANDRAILS_LATTERAL_OFFSET, "Lateral Offset"
            { "-1150336", MeasureType.Offset }, // RAILING_SYSTEM_SECONDARY_HANDRAILS_LATTERAL_OFFSET, "Lateral Offset"
            { "-1150624", MeasureType.Offset }, // PART_MAKER_DIVISION_PROFILE_OFFSET, "Profile Offset"
            { "-1151235", MeasureType.Offset }, // STAIRSTYPE_RIGHT_SUPPORT_LATERAL_OFFSET, "Right Lateral Offset":
            { "-1151236", MeasureType.Offset }, // STAIRSTYPE_LEFT_SUPPORT_LATERAL_OFFSET, "Left Lateral Offset":
            { "-1151701", MeasureType.Offset }, // STAIRS_SUPPORT_HORIZONTAL_OFFSET, "Lateral Offset": Distance from center or edge of boundary
            { "-1151702", MeasureType.Offset }, // STAIRS_SUPPORT_VERTICAL_OFFSET, "Vertical Offset": Distance of top plane of edge stringer relative to the plane connecting tread nosing
            { "-1151904", MeasureType.Offset }, // STAIRS_WINDERPATTERN_STAIR_PATH_OFFSET, "Inside Walk Line Offset": The offset from inside walk line to interior boundary
            { "-1152300", MeasureType.Offset }, // STAIRS_RAILING_PLACEMENT_OFFSET, "Offset from Path"
            { "-1152363", MeasureType.Offset }, // Y_OFFSET_VALUE, "y Offset Value"
            { "-1152365", MeasureType.Offset }, // Z_OFFSET_VALUE, "z Offset Value"
            { "-1152367", MeasureType.Offset }, // START_Y_OFFSET_VALUE, "Start y Offset Value"
            { "-1152369", MeasureType.Offset }, // START_Z_OFFSET_VALUE, "Start z Offset Value"
            { "-1152371", MeasureType.Offset }, // END_Y_OFFSET_VALUE, "End y Offset Value"
            { "-1152373", MeasureType.Offset }, // END_Z_OFFSET_VALUE, "End z Offset Value"
            { "-1155233", MeasureType.Offset }, // LAYER_ELEM_OFFSET_FROM_HOST, "Offset From Host"
            { "-1155252", MeasureType.Offset }, // OFFSET_FROM_REFERENCE_BASE, "Offset from Reference Base"
            { "-1155255", MeasureType.Offset }, // TOPOSOLID_HEIGHTABOVELEVEL_PARAM, "Height Offset From Level"
            { "-1155271", MeasureType.Offset }, // SSE_POINT_OFFSET_FROM_SURFACE, "Offset from Surface"
            { "-1155285", MeasureType.Offset }, // SSE_POINT_OFFSET_FROM_SNAPS, "Offset from Snaps"
            { "-1155292", MeasureType.Offset }, // BENDING_DETAIL_TYPE_SEGMENT_LENGTH_DIMENSIONS_OFFSET, "Dimension Offset"
            { "-1155298", MeasureType.Offset }, // BENDING_DETAIL_TYPE_ANGULAR_DIMENSION_OFFSET, "Angular Dimension Offset"
            { "-1180409", MeasureType.Offset }, // REBAR_STAGGER_OFFSET_AT_START, "Offset At Start"
            { "-1180410", MeasureType.Offset }, // REBAR_STAGGER_OFFSET_AT_END, "Offset At End"

            { "-1001711", MeasureType.Depth }, // FASCIA_DEPTH_PARAM, "Fascia Depth"
            { "-1001799", MeasureType.Depth }, // EXTRUSION_DEPTH_PARAM, "Depth"
            { "-1001812", MeasureType.Depth }, // EXTRUSION_LENGTH, "Depth"
            { "-1005154", MeasureType.Depth }, // VIEW_DEPTH, "View Depth"
            { "-1005181", MeasureType.Depth }, // VIEW_BACK_CLIPPING, "Depth Clipping"
            { "-1007203", MeasureType.Depth }, // STAIRS_ATTR_MINIMUM_TREAD_DEPTH, "Minimum Tread Depth"
            { "-1007250", MeasureType.Depth }, // STAIRS_ACTUAL_TREAD_DEPTH, "Actual Tread Depth"
            { "-1007356", MeasureType.Depth }, // MULLION_DEPTH, "Depth"
            { "-1007357", MeasureType.Depth }, // MULLION_DEPTH1, "Depth 1"
            { "-1007358", MeasureType.Depth }, // MULLION_DEPTH2, "Depth 2"
            { "-1010003", MeasureType.Depth }, // GENERIC_DEPTH, "Depth"
            // duplicate: "-1010003" // CASEWORK_DEPTH, "Depth"
            { "-1012041", MeasureType.Depth }, // MASS_DATA_SHADE_DEPTH, "Shade Depth"
            { "-1012044", MeasureType.Depth }, // MASS_DATA_SKYLIGHT_WIDTH, "Skylight Width & Depth"
            { "-1012055", MeasureType.Depth }, // ENERGY_ANALYSIS_SKYLIGHT_WIDTH, "Skylight Width & Depth"
            { "-1012057", MeasureType.Depth }, // ENERGY_ANALYSIS_SHADE_DEPTH, "Shade Depth"
            // duplicate: "-1012058" // ENERGY_ANALYSIS_MASSZONE_COREOFFSET, "Perimeter Zone Depth"
            { "-1140930", MeasureType.Depth }, // FABRICATION_PART_DEPTH_IN, "Main Primary Depth"
            { "-1140935", MeasureType.Depth }, // FABRICATION_PART_DEPTH_OUT, "Main Secondary Depth"
            { "-1140951", MeasureType.Depth }, // FABRICATION_PART_DEPTH_IN_OPTION, "Main Primary Depth Option"
            { "-1140957", MeasureType.Depth }, // FABRICATION_PART_DEPTH_OUT_OPTION, "Main Secondary Depth Option"
            { "-1141002", MeasureType.Depth }, // DISPLACEMENT_PATH_DEPTH, "Depth"
            { "-1150361", MeasureType.Depth }, // CONTINUOUSRAIL_PLUS_TREAD_DEPTH_PARAM, "Plus Tread Depth"
            { "-1151117", MeasureType.Depth }, // STAIRS_MIN_AUTOMATIC_LANDING_DEPTH, "Minimum Automatic Landing Depth": The minimum depth of automatic landing
            { "-1151204", MeasureType.Depth }, // STAIRSTYPE_MINIMUM_TREAD_DEPTH, "Min. Tread Depth": Minimum Tread Depth
            { "-1151205", MeasureType.Depth }, // STAIRSTYPE_MINIMUM_TREAD_WIDTH_INSIDE_BOUNDARY, "Min. Tread Depth on Winder Inner Boundary": Minimum Tread Width on Inside Boundary
            { "-1151308", MeasureType.Depth }, // STAIRS_RUN_ACTUAL_TREAD_DEPTH, "Actual Tread Depth": Actual Tread Depth
            { "-1151404", MeasureType.Depth }, // STAIRS_RUNTYPE_STRUCTURAL_DEPTH, "Structural Depth": Structural Depth
            { "-1151405", MeasureType.Depth }, // STAIRS_RUNTYPE_TOTAL_DEPTH, "Total Depth": Total Depth
            { "-1151805", MeasureType.Depth }, // STAIRS_SUPPORTTYPE_STRUCTURAL_DEPTH, "Structural Depth": Structural Depth
            { "-1151806", MeasureType.Depth }, // STAIRS_SUPPORTTYPE_TOTAL_DEPTH, "Total Depth": Total Depth
            { "-1151809", MeasureType.Depth }, // STAIRS_SUPPORTTYPE_STRUCTURAL_DEPTH_ON_RUN, "Structural Depth On Run": Structural Depth
            { "-1151810", MeasureType.Depth }, // STAIRS_SUPPORTTYPE_STRUCTURAL_DEPTH_ON_LANDING, "Structural Depth On Landing": Structural Depth
            { "-1154634", MeasureType.Depth }, // MULTISTORY_STAIRS_ACTUAL_TREAD_DEPTH, "Actual Tread Depth"
            { "-1155042", MeasureType.Depth }, // STEEL_ELEM_WELD_MAIN_PREPDEPTH, "Main Preparation Depth"
            { "-1155050", MeasureType.Depth }, // STEEL_ELEM_WELD_DOUBLE_PREPDEPTH, "Double Preparation Depth"
            { "-1155065", MeasureType.Depth }, // STEEL_ELEM_HOLE_DEPTH, "Hole Depth"
            { "-1155072", MeasureType.Depth }, // STEEL_ELEM_HOLE_DEPTH_OF_BOLT_HEAD, "Depth of bolt head"
            { "-1155314", MeasureType.Depth }, // BLEND_DEPTH_PARAM, "Depth"

            { "-1001530", MeasureType.Size }, // STRUCTURAL_CAMBER, "Camber Size"
            { "-1006407", MeasureType.Size }, // CENTER_MARK_SIZE, "Center Mark Size"
            { "-1013316", MeasureType.Size }, // CURTAINGRID_ADJUST_BORDER_VERT, "Adjust for Mullion Size"
            { "-1013317", MeasureType.Size }, // CURTAINGRID_ADJUST_BORDER_HORIZ, "Adjust for Mullion Size"
            { "-1013346", MeasureType.Size }, // CURTAINGRID_ADJUST_BORDER_1, "Adjust for Mullion Size"
            { "-1013347", MeasureType.Size }, // CURTAINGRID_ADJUST_BORDER_2, "Adjust for Mullion Size"
            { "-1013386", MeasureType.Size }, // CURTAINGRID_ADJUST_BORDER_U, "Adjust for Mullion Size"
            { "-1013387", MeasureType.Size }, // CURTAINGRID_ADJUST_BORDER_V, "Adjust for Mullion Size"
            { "-1114143", MeasureType.Size }, // RBS_DUCT_SIZE_FORMATTED_PARAM, "Size"
            { "-1114144", MeasureType.Size }, // RBS_PIPE_SIZE_FORMATTED_PARAM, "Size"
            { "-1114240", MeasureType.Size }, // RBS_CALCULATED_SIZE, "Size"
            { "-1140022", MeasureType.Size }, // RBS_WIRE_NEUTRAL_MODE_PARAM, "Neutral Size"
            { "-1140025", MeasureType.Size }, // RBS_WIRE_MAX_CONDUCTOR_SIZE_PARAM, "Max Size"
            { "-1140037", MeasureType.Size }, // RBS_ELEC_CIRCUIT_WIRE_SIZE_PARAM, "Wire Size"
            { "-1140266", MeasureType.Size }, // RBS_FP_SPRINKLER_ORIFICE_SIZE_PARAM, "Orifice Size"
            { "-1140283", MeasureType.Size }, // RBS_PIPE_SIZE_MINIMUM, "Minimum Size"
            { "-1140284", MeasureType.Size }, // RBS_PIPE_SIZE_MAXIMUM, "Maximum Size"
            { "-1141010", MeasureType.Size }, // FABRICATION_PRIMARY_SIZE, "Size of Primary End"
            // duplicate: "-1141010" // FABRICATION_PRI_SIZE, "Size of Primary End"
            { "-1141011", MeasureType.Size }, // FABRICATION_SECONDARY_SIZE, "Size of Secondary End"
            // duplicate: "-1141011" // FABRICATION_SEC_SIZE, "Size of Secondary End"
            { "-1141012", MeasureType.Size }, // FABRICATION_BRANCH_SIZE, "Size of Primary Branch End"
            { "-1141013", MeasureType.Size }, // FABRICATION_END_SIZE, "Size of Connector End"
            { "-1150233", MeasureType.Size }, // RBS_ENERGY_ANALYSIS_BUILDING_ENVELOPE_ANALYTICAL_GRID_CELL_SIZE, "Analytical Grid Cell Size"
            { "-1150426", MeasureType.Size }, // RBS_DUCT_CALCULATED_SIZE, "Duct Size"
            { "-1150427", MeasureType.Size }, // RBS_PIPE_CALCULATED_SIZE, "Pipe Size"
            { "-1150434", MeasureType.Size }, // RBS_REFERENCE_OVERALLSIZE, "Overall Size"
            { "-1150435", MeasureType.Size }, // RBS_REFERENCE_FREESIZE, "Free Size"
            { "-1154639", MeasureType.Size }, // COUPLER_MAIN_BAR_SIZE, "Bar Size 1"
            { "-1154640", MeasureType.Size }, // COUPLER_COUPLED_BAR_SIZE, "Bar Size 2"

            { "-1001387", MeasureType.Distance}, // STRUCTURAL_ATTACHMENT_START_VALUE_DISTANCE, "Start Attachment Distance"
            { "-1001388", MeasureType.Distance}, // STRUCTURAL_ATTACHMENT_END_VALUE_DISTANCE, "End Attachment Distance"
            { "-1001559", MeasureType.Distance}, // STRUCTURAL_COPING_DISTANCE, "Coping Distance"
            { "-1006446", MeasureType.Distance}, // DIM_STYLE_DIM_LINE_SNAP_DIST, "Dimension Line Snap Distance"
            { "-1006595", MeasureType.Distance}, // ALIGNMENT_STATION_LABEL_DISTANCE, "Distance": Currently not in use
            { "-1006617", MeasureType.Distance}, // CUT_LINE_DISTANCE, "Cut Line Distance"
            { "-1006625", MeasureType.Distance}, // DISTANCE_TO_CUT_MARK, "Distance to Cut Mark"
            { "-1012403", MeasureType.Distance}, // BOUNDARY_DISTANCE, "Distance"
            { "-1012614", MeasureType.Distance}, // PROPERTY_SEGMENT_DISTANCE, "Distance"
            { "-1012828", MeasureType.Distance}, // WALL_TOP_EXTENSION_DIST_PARAM, "Top Extension Distance"
            { "-1012829", MeasureType.Distance}, // WALL_BOTTOM_EXTENSION_DIST_PARAM, "Base Extension Distance"
            { "-1013318", MeasureType.Distance}, // CURTAINGRID_USE_CURVE_DIST_VERT, "Use Curve Distance"
            { "-1013319", MeasureType.Distance}, // CURTAINGRID_USE_CURVE_DIST_HORIZ, "Use Curve Distance"
            { "-1013348", MeasureType.Distance}, // CURTAINGRID_USE_CURVE_DIST_1, "Use Curve Distance"
            { "-1013349", MeasureType.Distance}, // CURTAINGRID_USE_CURVE_DIST_2, "Use Curve Distance"
            { "-1013354", MeasureType.Distance}, // CURTAINGRID_USE_CURVE_DIST, "Use Curve Distance"
            { "-1013372", MeasureType.Distance}, // SPACING_LENGTH_U, "Distance"
            { "-1013373", MeasureType.Distance}, // SPACING_LENGTH_V, "Distance"
            { "-1017738", MeasureType.Distance}, // FABRIC_WIRE_DISTANCE, "Wire distance": The distance between wires
            { "-1050427", MeasureType.Distance}, // DIVIDEDPATH_DISTANCE, "Distance"
            { "-1050430", MeasureType.Distance}, // DIVIDEDPATH_MIN_DISTANCE, "Minimum Distance"
            { "-1050431", MeasureType.Distance}, // DIVIDEDPATH_MAX_DISTANCE, "Maximum Distance"
            { "-1114397", MeasureType.Distance}, // GRID_BANK_ROW_HEIGHT, "Row Distance"
            { "-1114398", MeasureType.Distance}, // GRID_BANK_COL_WIDTH, "Column Distance"
            { "-1150177", MeasureType.Distance}, // STRUCTURAL_ATTACHMENT_BASE_DISTANCE, "Base Attachment Distance"
            { "-1150181", MeasureType.Distance}, // STRUCTURAL_ATTACHMENT_TOP_DISTANCE, "Top Attachment Distance"
            { "-1150216", MeasureType.Distance}, // STRUCTURAL_BEAM_START_ATTACHMENT_DISTANCE, "Start Attachment Distance"
            { "-1150217", MeasureType.Distance}, // STRUCTURAL_BEAM_END_ATTACHMENT_DISTANCE, "End Attachment Distance"
            { "-1151228", MeasureType.Distance}, // STAIRSTYPE_NOTCH_HORIZONTAL_GAP, "Horizontal Gap Distance":
            { "-1151229", MeasureType.Distance}, // STAIRSTYPE_NOTCH_VERTICAL_GAP, "Vertical Gap Distance":
            // duplicate: "-1151701" // STAIRS_SUPPORT_HORIZONTAL_OFFSET, "Lateral Offset": Distance from center or edge of boundary
            // duplicate: "-1151702" // STAIRS_SUPPORT_VERTICAL_OFFSET, "Vertical Offset": Distance of top plane of edge stringer relative to the plane connecting tread nosing
            { "-1155026", MeasureType.Distance}, // STEEL_ELEM_CONTOUR_SIDE1DIST, "Boundary distance 1"
            { "-1155027", MeasureType.Distance}, // STEEL_ELEM_CONTOUR_SIDE2DIST, "Boundary distance 2"
            { "-1155055", MeasureType.Distance}, // STEEL_ELEM_PATTERN_INTERMEDIATE_DISTANCE_X, "Intermediate distance on side 1"
            { "-1155056", MeasureType.Distance}, // STEEL_ELEM_PATTERN_INTERMEDIATE_DISTANCE_Y, "Intermediate distance on side 2"
            { "-1155057", MeasureType.Distance}, // STEEL_ELEM_PATTERN_EDGE_DISTANCE_X, "Edge distance on side 1"
            { "-1155058", MeasureType.Distance}, // STEEL_ELEM_PATTERN_EDGE_DISTANCE_Y, "Edge distance on side 2"
            { "-1155079", MeasureType.Distance}, // STEEL_ELEM_X_DISTANCE, "Side 1"
            { "-1155080", MeasureType.Distance}, // STEEL_ELEM_Y_DISTANCE, "Side 2"
            { "-1155082", MeasureType.Distance}, // STEEL_ELEM_COPE_DISTANCE_AXIS, "Distance from axis"
            { "-1155238", MeasureType.Distance}, // LAYER_ELEM_TOP_EXTENSION_DIS, "Top Extension Distance"
            { "-1155239", MeasureType.Distance}, // LAYER_ELEM_BASE_EXTENSION_DIS, "Base Extension Distance"
        };
    }
}
