using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    public static class Units
    {
        public const double MetersToFeetRatio = 3.280839895d;
        public const double FeetToMetersRatio = 1.0d / MetersToFeetRatio;

        public const double SquareMetersToSquareFeetRatio = 10.7639d;
        public const double SquareFeetToSquareMetersRatio = 1.0d / SquareMetersToSquareFeetRatio;

        public const double CubicMetersToCubicFeetRatio = 35.3147d;
        public const double CubicFeetToCubicMetersRatio = 1.0d / CubicMetersToCubicFeetRatio;

        public const double RadiansToDegreesRatio = 180d / Math.PI;
        public const double DegreesToRadiansRatio = 1.0d / RadiansToDegreesRatio;

        public static double? ConvertMeasure(double? source, double sourceToDestinationRatio, int digitRounding = -1)
        {
            if (source == null)
                return null;

            var destination = sourceToDestinationRatio * source.Value;

            return digitRounding < 0
                ? destination
                : Math.Round(destination, digitRounding);
        }

        public static double? FeetToMeters(double? feet, int digitRounding = -1)
            => ConvertMeasure(feet, FeetToMetersRatio, digitRounding);

        public static double? SquareFeetToSquareMeters(double? squareFeet, int digitRounding = -1)
            => ConvertMeasure(squareFeet, SquareFeetToSquareMetersRatio, digitRounding);

        public static double? CubicFeetToCubicMeters(double? cubicFeet, int digitRounding = -1)
            => ConvertMeasure(cubicFeet, CubicFeetToCubicMetersRatio, digitRounding);

        public static double? RadiansToDegrees(double? radians, int digitRounding = -1)
            => ConvertMeasure(radians, RadiansToDegreesRatio, digitRounding);

        public static double? DegreesToRadians(double? degrees, int digitRounding = -1)
            => ConvertMeasure(degrees, DegreesToRadiansRatio, digitRounding);

        public static string ToFeetAndFractionalInchesString(
            double? feet,
            string feetFormatString = "",
            string positivePrefix = "",
            string negativePrefix = "-")
            => feet.HasValue
                ? FormatAsFractionalFeetAndInches(feet.Value, feetFormatString, positivePrefix, negativePrefix)
                : "";

        public static string ToPaddedString(
            double? inValue,
            string suffix = "",
            string formatString = "",
            string positiveSign = "",
            string negativeSign = "-")
        {
            if (!inValue.HasValue)
                return "";

            var value = inValue.Value;
            var absValue = Math.Abs(value);

            var sb = new StringBuilder();

            sb.Append(value < 0 ? negativeSign : positiveSign);
            sb.Append(absValue.ToString(formatString));
            sb.Append(suffix);

            return sb.ToString();
        }

        public static string ToDecimalFeetString(
            double? feet,
            string formatString = "",
            string positiveSign = "",
            string negativeSign = "-")
            => ToPaddedString(feet, "ft", formatString, positiveSign, negativeSign);

        public static string ToMetersString(
            double? meters,
            string formatString = "",
            string positiveSign = "",
            string negativeSign = "-")
            => ToPaddedString(meters, "m", formatString, positiveSign, negativeSign);

        /// <summary>
        /// Converts a value to fractional feet and inches.
        /// Examples:
        ///  12.1667 converts to 12' 2".
        ///  4 converts to 4'.
        ///  0.1667 converts to 2".
        /// </summary>
        public static string FormatAsFractionalFeetAndInches(
            double value,
            string feetFormatString = "",
            string positiveSign = "",
            string negativeSign = "-",
            bool skipRounding = false,
            double decimalPlaces = FractionConverter.OneSixteenth)
        {
            if (value == 0)
                return $"{positiveSign}0'";

            var absValue = Math.Abs(value);

            var feet = Math.Floor(absValue);
            var inchesDecimal = Math.Round((absValue - feet) * 12, 2);

            var sb = new StringBuilder();

            sb.Append(value < 0 ? negativeSign : positiveSign);

            if (feet != 0)
                sb.Append($"{feet.ToString(feetFormatString)}'");

            if (inchesDecimal != 0)
            {
                if (feet != 0)
                    sb.Append(" ");

                sb.Append($"{FractionConverter.ToFractionString(inchesDecimal, skipRounding, decimalPlaces)}\"");
            }

            var result = sb.ToString().TrimEnd();

            return string.IsNullOrWhiteSpace(result)
                ? $"{positiveSign}0'"
                : result;
        }

        // https://stackoverflow.com/questions/22794466/parsing-all-possible-types-of-varying-architectural-dimension-input
        // https://stackoverflow.com/questions/6157865/c-sharp-function-to-convert-text-input-of-feet-inches-meters-centimeters-millime

        public static Regex FeetAndInchesRegex
            = new Regex(
                "^\\s*(?<minus>-)?\\s*(((?<feet>\\d+)(?<inch>\\d{2})(?<sixt>\\d{2}))|((?<feet>[\\d.]+)')?[\\s-]*((?<inch>\\d+)?[\\s-]*((?<numer>\\d+)/(?<denom>\\d+))?\")?)\\s*$",
                RegexOptions.Compiled);

        public static Regex NumberAndUnitsRegex
            = new Regex("^\\s*(?<number>-?[0-9]*[.]?[0-9]+)([\\s]+(?<units>[a-zA-Z°²³$£%/*^@#`]*))?\\s*$", RegexOptions.Compiled);

        /// <summary>
        /// Parses the input string and outputs the value in decimal inches upon success
        /// </summary>
        public static bool TryParseFeetAndInchesToDecimalInches(string input, out double value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var m = FeetAndInchesRegex.Match(input);
            if (!m.Success)
                return false;

            var sign = m.Groups["minus"].Success ? -1 : 1;
            var feet = m.Groups["feet"].Success ? double.Parse(m.Groups["feet"].Value, CultureInfo.InvariantCulture) : 0;
            var inch = m.Groups["inch"].Success ? Convert.ToInt32(m.Groups["inch"].Value) : 0;
            var sixt = m.Groups["sixt"].Success ? Convert.ToInt32(m.Groups["sixt"].Value) : 0;
            var numer = m.Groups["numer"].Success ? Convert.ToInt32(m.Groups["numer"].Value) : 0;
            var denom = m.Groups["denom"].Success ? Convert.ToInt32(m.Groups["denom"].Value) : 1;
            value = sign * (feet * 12 + inch + sixt / 16.0 + numer / Convert.ToDouble(denom));
            return true;
        }
    }
}
