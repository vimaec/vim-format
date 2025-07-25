using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    public static class Units
    {
        public const double MetersToFeetRatio = 3.280839895d;

        public const double FeetToMetersRatio = 1.0d / MetersToFeetRatio;
        
        public static double? FeetToMeters(double? feet)
            => FeetToMetersRatio * feet;

        public static string ToFeetAndFractionalInchesString(double? feet)
            => feet.HasValue
                ? FormatAsFractionalFeetAndInches(feet.Value)
                : "";


        /// <summary>
        /// Converts a value to fractional feet and inches.
        /// Examples:
        ///  12.1667 converts to 12' 2".
        ///  4 converts to 4'.
        ///  0.1667 converts to 2".
        /// </summary>
        /// <param name="value"></param>
        /// <param name="skipRounding"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static string FormatAsFractionalFeetAndInches(double value, bool skipRounding = false, double decimalPlaces = FractionConverter.OneSixteenth)
        {
            if (value == 0)
                return "0'";

            var absValue = Math.Abs(value);

            var feetDecimal = Math.Floor(absValue);
            var inchesDecimal = Math.Round((absValue - feetDecimal) * 12, 2);

            var sb = new StringBuilder();

            if (value < 0)
                sb.Append("-");

            if (feetDecimal != 0)
                sb.Append($"{feetDecimal}'");

            if (inchesDecimal != 0)
            {
                if (feetDecimal != 0)
                    sb.Append(" ");

                sb.Append($"{FractionConverter.ToFractionString(inchesDecimal, skipRounding, decimalPlaces)}\"");
            }

            var result = sb.ToString().Trim();

            return string.IsNullOrWhiteSpace(result)
                ? "0'"
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
