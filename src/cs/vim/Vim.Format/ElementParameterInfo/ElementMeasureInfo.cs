using Vim.Format.api_v2;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Convenience class which extracts angle/slope/length/width/height/area/volume from the Element's parameters.
    /// </summary>
    public class ElementMeasureInfo : ObjectModel.IElementIndex
    {
        /// <summary>
        /// The element.
        /// </summary>
        public ObjectModel.Element Element { get; }

        /// <summary>
        /// Returns the element index.
        /// </summary>
        public int GetElementIndexOrNone()
            => ObjectModel.EntityRelation.IndexOrDefault(Element);

        public double? Angle { get; }

        public double? Slope { get; }

        public double? Length { get; }

        public double? Width { get; }

        public double? Height { get; }

        public double? Area { get; }

        public double? Volume { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementMeasureInfo(
            ObjectModel.Element element,
            ParameterTable parameterTable,
            MeasureType[] parameterMeasureInfos,
            VimElementIndexMaps elementIndexMaps)
        {
            Element = element;

            var elementIndex = GetElementIndexOrNone();

            var elementParameterIndices = elementIndexMaps.GetParameterIndicesFromElementIndex(elementIndex);

            foreach (var parameterIndex in elementParameterIndices)
            {
                var mt = parameterMeasureInfos[parameterIndex];

                var (nativeValue, _) = ObjectModel.Parameter.SplitValues(parameterTable.Column_Value[parameterIndex]);
                var parsed = ObjectModel.Parameter.ParseNativeValueAsDouble(nativeValue);

                switch (mt)
                {
                    case MeasureType.Angle:
                        Angle = parsed;
                        break;
                    case MeasureType.Slope:
                        Slope = parsed;
                        break;
                    case MeasureType.Length:
                        Length = parsed;
                        break;
                    case MeasureType.Width:
                        Width = parsed;
                        break;
                    case MeasureType.Height:
                        Height = parsed;
                        break;
                    case MeasureType.Area:
                        Area = parsed;
                        break;
                    case MeasureType.Volume:
                        Volume = parsed;
                        break;
                    case MeasureType.Unknown:
                    default:
                        break; // not recognized
                }
            }
        }
    }

    /// <summary>
    /// A wrapper around an ElementMeasureInfo which interprets the values as though they were coming from Revit and provides the values in feet and meters.
    /// </summary>
    public class RevitElementMeasureInfo
    {
        public ElementMeasureInfo ElementMeasureInfo { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RevitElementMeasureInfo(ElementMeasureInfo elementMeasureInfo)
        {
            ElementMeasureInfo = elementMeasureInfo;
        }

        // When reading from Revit, angle parameters are always stored in radians.
        public double? AngleInRadians
            => ElementMeasureInfo.Angle;

        public double? AngleInDegrees
            => Units.RadiansToDegrees(AngleInRadians);

        // When reading from Revit, slope parameters are always stored in radians
        public double? SlopeInRadians
            => ElementMeasureInfo.Slope;

        public double? SlopeInDegrees
            => Units.RadiansToDegrees(SlopeInRadians);

        // When reading from Revit, length parameters are always stored in feet.
        public double? LengthInFeet
            => ElementMeasureInfo.Length;

        public double? LengthInMeters
            => Units.FeetToMeters(LengthInFeet);

        // When reading from Revit, width parameters are always stored in feet.
        public double? WidthInFeet
            => ElementMeasureInfo.Width;

        public double? WidthInMeters
            => Units.FeetToMeters(WidthInFeet);

        // When reading from Revit, height parameters are always stored in feet.
        public double? HeightInFeet
            => ElementMeasureInfo.Height;

        public double? HeightInMeters
            => Units.FeetToMeters(HeightInFeet);

        // When reading from Revit, area parameters are always stored in square feet.
        public double? AreaInSquareFeet
            => ElementMeasureInfo.Area;

        public double? AreaInSquareMeters
            => Units.SquareFeetToSquareMeters(AreaInSquareFeet);

        // When reading from Revit, volume parameters are always stored in cubic feet.
        public double? VolumeInCubicFeet
            => ElementMeasureInfo.Volume;

        public double? VolumeInCubicMeters
            => Units.CubicFeetToCubicMeters(VolumeInCubicFeet);
    }
}
