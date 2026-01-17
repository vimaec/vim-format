using System.Collections.Generic;
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
            => EntityRelation.IndexOrDefault(Element);

        public double? Angle { get; set; }

        public double? Slope { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public double? Area { get; set; }

        public double? Volume { get; set; }

        public double? Depth { get; set; }

        public double? Diameter { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementMeasureInfo(
            Element element,
            ElementTable elementTable,
            ParameterTable parameterTable,
            MeasureType[] parameterMeasureInfos)
        {
            Element = element;

            var elementIndex = GetElementIndexOrNone();
            if (elementIndex < 0)
                return;

            // First supplement with family parameter info
            var familyParameterIndices = elementTable.GetFamilyParameterIndices(elementIndex);
            ReadParameters(parameterTable, parameterMeasureInfos, familyParameterIndices);

            // Then supplement with family type parameter info
            var familyTypeParameterIndices = elementTable.GetFamilyTypeParameterIndices(elementIndex);
            ReadParameters(parameterTable, parameterMeasureInfos, familyTypeParameterIndices);

            // Lastly read this element's parameters (includes the case of this element being a FamilyInstance)
            var elementParameterIndices = elementTable.GetParameterIndices(elementIndex);
            ReadParameters(parameterTable, parameterMeasureInfos, elementParameterIndices);
        }

        private void ReadParameters(
            ParameterTable parameterTable,
            MeasureType[] parameterMeasureInfos,
            IReadOnlyList<int> parameterIndices)
        {
            foreach (var parameterIndex in parameterIndices)
            {
                var mt = parameterMeasureInfos[parameterIndex];

                var (nativeValue, _) = Parameter.SplitValues(parameterTable.Column_Value[parameterIndex]);
                var parsed = Parameter.ParseNativeValueAsDouble(nativeValue);

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
                    case MeasureType.Depth:
                        Depth = parsed;
                        break;
                    case MeasureType.Diameter:
                        Diameter = parsed;
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

        // When reading from Revit, depth parameters are always stored in feet.
        public double? DepthInFeet
            => ElementMeasureInfo.Depth;

        public double? DepthInMeters
            => Units.FeetToMeters(DepthInFeet);

        // When reading from Revit, diameter parameters are always stored in feet.
        public double? DiameterInFeet
            => ElementMeasureInfo.Diameter;

        public double? DiameterInMeters
            => Units.FeetToMeters(DiameterInFeet);

    }
}
