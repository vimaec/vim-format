using System.Collections.Generic;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Convenience class which extracts angle/slope/length/width/height/area/volume from the Element's parameters.
    /// </summary>
    public class ElementMeasureInfo : IElementIndex
    {
        /// <summary>
        /// The element.
        /// </summary>
        public Element Element { get; }

        /// <summary>
        /// Returns the element index.
        /// </summary>
        public int GetElementIndexOrNone()
            => Element.IndexOrDefault();

        public double? AngleOrSlopeInDegrees { get; set; }

        public static readonly HashSet<string> AngleOrSlopeBuiltInIds = new HashSet<string>()
        {
            "-1001817", // PROFILE_ANGLE, "Angle"
            "-1001826", // PROFILE1_ANGLE, "Angle"
            "-1001831", // PROFILE2_ANGLE, "Angle"
            "-1004006", // CURVE_ELEM_LINE_ANGLE, "Angle"
            "-1007007", // TAG_ANGLE_PARAM, "Angle"
            "-1007363", // MULLION_ANGLE, "Angle"
            "-1013309", // CURTAINGRID_ANGLE_VERT, "Angle"
            "-1013310", // CURTAINGRID_ANGLE_HORIZ, "Angle"
            "-1013339", // CURTAINGRID_ANGLE_1, "Angle"
            "-1013340", // CURTAINGRID_ANGLE_2, "Angle"
            "-1133409", // CONNECTOR_ANGLE, "Angle"
            "-1140724", // TRUSS_FAMILY_VERT_WEB_ANGLE_PARAM, "Angle"
            "-1140734", // TRUSS_FAMILY_DIAG_WEB_ANGLE_PARAM, "Angle"
            "-1140744", // TRUSS_FAMILY_TOP_CHORD_ANGLE_PARAM, "Angle"
            "-1140764", // TRUSS_FAMILY_BOTTOM_CHORD_ANGLE_PARAM, "Angle"
            "-1140911", // FABRICATION_PART_ANGLE, "Angle"
            "-1150226", // POINT_ELEMENT_ANGLE, "Angle"
            "-1155075", // STEEL_ELEM_PLATE_SHORTEN_ANGLE, "Angle"
            "-1006016", // ROOF_SLOPE, "Slope"
            "-1140254", // RBS_CURVE_UTSLOPE, "Slope"
            "-1140255", // RBS_DUCT_SLOPE, "Slope"
            "-1140256", // RBS_PIPE_SLOPE, "Slope"
            "-1140923", // FABRICATION_SLOPE_PARAM, "Slope"
        };

        public double? LengthInFeet { get; }

        public double? LengthInMeters
            => Units.FeetToMeters(LengthInFeet);

        // NOTE: the values are correlated to the parameters named "Length" in the BuiltInParameter enumerations https://www.revitapidocs.com/2025/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm
        public static readonly HashSet<string> LengthBuiltInIds = new HashSet<string>()
        {
            "-1001136", // DPART_LENGTH_COMPUTED, "Length"
            "-1001306", // FAMILY_LINE_LENGTH_PARAM, "Length"
            "-1001375", // INSTANCE_LENGTH_PARAM, "Length"
            "-1001567", // CONTINUOUS_FOOTING_LENGTH, "Length"
            "-1001569", // STRUCTURAL_FOUNDATION_LENGTH, "Length"
            "-1004005", // CURVE_ELEM_LENGTH, "Length"
            "-1007736", // IMPORT_ADT_ENTITY_LENGTH, "Length"
            "-1013434", // COVER_TYPE_LENGTH, "Length"
            "-1015043", // LOAD_LINEAR_LENGTH, "Length"
            "-1017608", // FABRIC_SHEET_LENGTH, "Length"
            "-1140039", // RBS_ELEC_CIRCUIT_LENGTH_PARAM, "Length"
            "-1140132", // RBS_CABLETRAYCONDUITRUN_LENGTH_PARAM, "Length"
            "-1140337", // CONNECTOR_LENGTH, "Length"
            "-1140944", // FABRICATION_PART_LENGTH, "Length"
            "-1150348", // CONTINUOUSRAIL_EXTENSION_LENGTH_PARAM, "Length"
            "-1150350", // CONTINUOUSRAIL_END_EXTENSION_LENGTH_PARAM, "Length"
            "-1150360", // CONTINUOUSRAIL_LENGTH_PARAM, "Length"
            "-1150461", // ANALYTICAL_MODEL_LENGTH, "Length"
            "-1153545", // RBS_ELEC_ANALYTICAL_FEEDER_LENGTH, "Length"
            "-1155017", // STEEL_ELEM_ANCHOR_LENGTH, "Length"
            "-1155019", // STEEL_ELEM_SHEARSTUD_LENGTH, "Length"
            "-1155020", // STEEL_ELEM_SHORTEN_REFLENGTH, "Length"
            "-1155033", // STEEL_ELEM_WELD_LENGTH, "Length"
            "-1155137", // STEEL_ELEM_PLATE_LENGTH, "Length"
            "-1155147", // STEEL_ELEM_PROFILE_LENGTH, "Length"
            "-1155247", // LINEAR_FRAMING_LENGTH, "Length"
        };

        public double? WidthInFeet { get; }

        public double? WidthInMeters
            => Units.FeetToMeters(WidthInFeet);

        public static readonly HashSet<string> WidthBuiltInIds = new HashSet<string>()
        {
            "-1001000", // WALL_ATTR_WIDTH_PARAM, "Width"
            "-1001301", // FAMILY_WIDTH_PARAM, "Width"
            // duplicate: "-1001301", // CASEWORK_WIDTH, "Width"
            // duplicate: "-1001301", // DOOR_WIDTH, "Width"
            // duplicate: "-1001301", // FURNITURE_WIDTH, "Width"
            // duplicate: "-1001301", // GENERIC_WIDTH, "Width"
            // duplicate: "-1001301", // WINDOW_WIDTH, "Width"
            "-1001558", // CONTINUOUS_FOOTING_WIDTH, "Width"
            "-1001562", // CONTINUOUS_FOOTING_BEARING_WIDTH, "Width"
            "-1001568", // STRUCTURAL_FOUNDATION_WIDTH, "Width"
            "-1005502", // STRUCTURAL_SECTION_COMMON_WIDTH, "Width"
            "-1007204", // STAIRS_ATTR_TREAD_WIDTH, "Width"
            "-1007600", // ELEV_WIDTH, "Width"
            "-1007735", // IMPORT_ADT_ENTITY_WIDTH, "Width"
            "-1007750", // RASTER_SHEETWIDTH, "Width"
            "-1007765", // RASTER_SYMBOL_WIDTH, "Width"
            "-1010301", // CURTAIN_WALL_PANELS_WIDTH, "Width"
            "-1012814", // DECAL_WIDTH, "Width"
            "-1017615", // FABRIC_SHEET_WIDTH, "Width": Width
            "-1114101", // RBS_CURVE_WIDTH_PARAM, "Width"
            "-1133403", // CONNECTOR_WIDTH, "Width"
            "-1140122", // RBS_CABLETRAY_WIDTH_PARAM, "Width"
            "-1140134", // RBS_CABLETRAYRUN_WIDTH_PARAM, "Width"
            "-1150623", // DIVISION_PROFILE_WIDTH, "Width": Width
            "-1151807", // STAIRS_SUPPORTTYPE_WIDTH, "Width": Width
            "-1155138", // STEEL_ELEM_PLATE_WIDTH, "Width"
            "-1155317", // BENDING_DETAIL_TYPE_SCHEMATIC_WIDTH, "Width"
        };

        public double? HeightInFeet { get; }

        public double? HeightInMeters
            => Units.FeetToMeters(HeightInFeet);

        public static readonly HashSet<string> HeightBuiltInIds = new HashSet<string>()
        {
            "-1001001", // WALL_ATTR_HEIGHT_PARAM, "Height"
            "-1001135", // DPART_HEIGHT_COMPUTED, "Height"
            "-1001300", // FAMILY_HEIGHT_PARAM, "Height"
            // duplicate: "-1001300", // CASEWORK_HEIGHT, "Height"
            // duplicate: "-1001300", // DOOR_HEIGHT, "Height"
            // duplicate: "-1001300", // FURNITURE_HEIGHT, "Height"
            // duplicate: "-1001300", // GENERIC_HEIGHT, "Height"
            // duplicate: "-1001300", // WINDOW_HEIGHT, "Height"
            "-1005193", // RENDER_PLANT_HEIGHT, "Height"
            "-1005503", // STRUCTURAL_SECTION_COMMON_HEIGHT, "Height"
            "-1007734", // IMPORT_ADT_ENTITY_HEIGHT, "Height"
            "-1007751", // RASTER_SHEETHEIGHT, "Height"
            "-1007766", // RASTER_SYMBOL_HEIGHT, "Height"
            "-1010300", // CURTAIN_WALL_PANELS_HEIGHT, "Height"
            "-1012114", // VOLUME_OF_INTEREST_HEIGHT, "Height"
            "-1012815", // DECAL_HEIGHT, "Height"
            "-1017043", // REBAR_SHAPE_SPIRAL_HEIGHT, "Height"
            "-1114102", // RBS_CURVE_HEIGHT_PARAM, "Height"
            "-1133404", // CONNECTOR_HEIGHT, "Height"
            "-1140121", // RBS_CABLETRAY_HEIGHT_PARAM, "Height"
            "-1140133", // RBS_CABLETRAYRUN_HEIGHT_PARAM, "Height"
            "-1150328", // RAILING_SYSTEM_TOP_RAIL_HEIGHT_PARAM, "Height"
            "-1150331", // RAILING_SYSTEM_HANDRAILS_HEIGHT_PARAM, "Height"
            "-1150335", // RAILING_SYSTEM_SECONDARY_HANDRAILS_HEIGHT_PARAM, "Height"
            "-1150340", // HANDRAIL_HEIGHT_PARAM, "Height"
            "-1152166", // SUPPORT_HEIGHT, "Height"
            "-1155318", // BENDING_DETAIL_TYPE_SCHEMATIC_HEIGHT, "Height"
        };

        public double? AreaInSquareFeet { get; }

        public double? AreaInSquareMeters
            => Units.SquareFeetToSquareMeters(AreaInSquareFeet);

        public static readonly HashSet<string> AreaBuiltInIds = new HashSet<string>()
        {
            "-1001133", // DPART_AREA_COMPUTED, "Area"
            "-1006902", // ROOM_AREA, "Area"
            "-1012600", // PROPERTY_AREA, "Area"
            "-1012606", // PROPERTY_AREA_OPEN, "Area"
            "-1012805", // HOST_AREA_COMPUTED, "Area"
            "-1015069", // LOAD_AREA_AREA, "Area"
            "-1114120", // RBS_CURVE_SURFACE_AREA, "Area"
            "-1114247", // RBS_GBXML_SURFACE_AREA, "Area"
            "-1114820", // SPACE_AREA, "Area"
            "-1140360", // MATERIAL_AREA, "Area"
            "-1150462", // ANALYTICAL_MODEL_AREA, "Area": The Area of Analytical Model
            "-1153551", // RBS_ELEC_ANALYTICAL_AREA, "Area"
            "-1155139", // STEEL_ELEM_PLATE_AREA, "Area"
            "-1155234", // LAYER_ELEM_AREA_COMPUTED, "Area"
        };

        public double? VolumeInCubicFeet { get; set; }

        public double? VolumeInCubicMeters
            => Units.CubicFeetToCubicMeters(VolumeInCubicFeet);

        public static readonly HashSet<string> VolumeBuiltInIds = new HashSet<string>()
        {
            "-1001129", // DPART_VOLUME_COMPUTED, "Volume"
            "-1006921", // ROOM_VOLUME, "Volume"
            "-1012007", // MASS_GROSS_VOLUME, "Gross Volume"
            "-1012012", // LEVEL_DATA_VOLUME, "Floor Volume"
            "-1012021", // MASS_ZONE_VOLUME, "Mass Zone Volume"
            "-1012806", // HOST_VOLUME_COMPUTED, "Volume"
            "-1018502", // REIN_EST_BAR_VOLUME, "Estimated Reinforcement Volume"
            "-1018503", // REINFORCEMENT_VOLUME, "Reinforcement Volume"
            "-1114303", // ZONE_VOLUME, "Occupied Volume"
            "-1114331", // ZONE_VOLUME_GROSS, "Gross Volume"
            "-1114821", // SPACE_VOLUME, "Volume"
            "-1140253", // RBS_PIPE_VOLUME_PARAM, "Volume"
            "-1140361", // MATERIAL_VOLUME, "Volume"
            "-1150425", // RBS_INSULATION_LINING_VOLUME, "Volume"
            "-1155140", // STEEL_ELEM_PLATE_VOLUME, "Volume"
            "-1155148", // STEEL_ELEM_PROFILE_VOLUME, "Volume"
            "-1155232", // LAYER_ELEM_VOLUME_COMPUTED, "Volume"
            "-1180303", // INDIVIDUAL_EXCAVATION_VOLUME, "Individual Excavation Volume"
            "-1180306", // EXCAVATION_VOLUME, "Excavation Volume"
            "-1180307", // TOTAL_EXCAVATION_VOLUME, "Total Excavation Volume"
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementMeasureInfo(Element element, ParameterTable parameterTable, ElementIndexMaps elementIndexMaps)
        {
            Element = element;

            var elementIndex = GetElementIndexOrNone();

            var elementParameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);

            foreach (var parameterIndex in elementParameterIndices)
            {
                var p = parameterTable.Get(parameterIndex);
                var (nativeValue, _) = p.Values;
                var d = p.ParameterDescriptor;

                // NOTE: Guid is either the built-in ID (if the parameter is built-in), or a guid (if the parameter is shared).
                var paramNameLowerInvariant = d.Name.ToLowerInvariant();
                var builtInId = d.Guid;

                if (AngleOrSlopeInDegrees == null && TryGetAngleOrSlopeValue(nativeValue, paramNameLowerInvariant, builtInId, out var radians))
                {
                    AngleOrSlopeInDegrees = Units.RadiansToDegrees(radians);
                }
                else if (LengthInFeet == null && TryGetLengthValue(nativeValue, paramNameLowerInvariant, builtInId, out var length))
                {
                    LengthInFeet = length;
                }
                else if (WidthInFeet == null && TryGetWidthValue(nativeValue, paramNameLowerInvariant, builtInId, out var width))
                {
                    WidthInFeet = width;
                }
                else if (HeightInFeet == null && TryGetHeightValue(nativeValue, paramNameLowerInvariant, builtInId, out var height))
                {
                    HeightInFeet = height;
                }
                else if (AreaInSquareFeet == null && TryGetAreaValue(nativeValue, paramNameLowerInvariant, builtInId, out var area))
                {
                    AreaInSquareFeet = area;
                }
                else if (VolumeInCubicFeet == null && TryGetVolumeValue(nativeValue, paramNameLowerInvariant, builtInId, out var volume))
                {
                    VolumeInCubicFeet = volume;
                }
            }
        }

        private static bool TryGetAngleOrSlopeValue(string nativeValue, string paramNameLowerInvariant, string builtInId, out double radians)
        {
            radians = 0d;

            if (paramNameLowerInvariant is "angle" ||
                paramNameLowerInvariant is "slope" ||
                AngleOrSlopeBuiltInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsDouble(nativeValue, out radians);
            }

            return false;
        }

        private static bool TryGetLengthValue(string nativeValue, string paramNameLowerInvariant, string builtInId, out double length)
        {
            length = 0d;

            if (paramNameLowerInvariant is "length" ||
                LengthBuiltInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsDouble(nativeValue, out length);
            }

            return false;
        }

        private static bool TryGetWidthValue(string nativeValue, string paramNameLowerInvariant, string builtInId, out double width)
        {
            width = 0d;

            if (paramNameLowerInvariant is "length" ||
                WidthBuiltInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsDouble(nativeValue, out width);
            }

            return false;
        }

        private static bool TryGetHeightValue(string nativeValue, string paramNameLowerInvariant, string builtInId, out double height)
        {
            height = 0d;

            if (paramNameLowerInvariant is "height" ||
                HeightBuiltInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsDouble(nativeValue, out height);
            }

            return false;
        }

        private static bool TryGetAreaValue(string nativeValue, string paramNameLowerInvariant, string builtInId, out double area)
        {
            area = 0d;

            if (paramNameLowerInvariant is "area" ||
                AreaBuiltInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsDouble(nativeValue, out area);
            }

            return false;
        }

        private static bool TryGetVolumeValue(string nativeValue, string paramNameLowerInvariant, string builtInId, out double volume)
        {
            volume = 0d;

            if (paramNameLowerInvariant is "volume" ||
                VolumeBuiltInIds.Contains(builtInId))
            {
                return Parameter.TryParseNativeValueAsDouble(nativeValue, out volume);
            }

            return false;
        }
    }
}
