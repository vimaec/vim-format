using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Vim.Util
{
    public static class FractionConverter
    {
        // Adapted from: https://www.codeproject.com/Tips/623477/Convert-Decimal-to-Fraction-and-Vice-Versa-in-Csha

        public const double OneSixteenth = 0.0625d;
        private const double Tolerance = 0.0000001d;

        /// <summary>
        /// Convert and round to 1/16
        /// </summary>
        public static string ToFractionString(double inValue, bool skipRounding = false, double decimalPlaces = OneSixteenth)
        {
            var value = inValue;

            if (!skipRounding)
                value = RoundTo(inValue, decimalPlaces);

            // get the whole value of the fraction
            var wholePart = Math.Truncate(value);

            if (Math.Abs(wholePart - value) < Tolerance)
                return value.ToString(CultureInfo.InvariantCulture);

            // get the fractional value
            var fractionPart = value - wholePart;

            // initialize a numerator and denomintar
            uint numerator = 0;
            uint denominator = 1;

            // ensure that there is actual a fraction
            if (fractionPart > 0d)
            {
                // convert the value to a string so that 
                // you can count the number of decimal places there are
                var strFraction = fractionPart
                    .ToString(CultureInfo.InvariantCulture)
                    .Remove(0, 2);

                // store the number of decimal places
                var intFractLength = (uint)strFraction.Length;

                // set the numerator to have the proper amount of zeros
                numerator = (uint)Math.Pow(10, intFractLength);

                // parse the fraction value to an integer that equals 
                // [fraction value] * 10^[number of decimal places]
                uint.TryParse(strFraction, out denominator);

                // get the greatest common divisor for both numbers
                var gcd = GreatestCommonDivisor(denominator, numerator);

                // divide the numerator and the denominator by the greatest common divisor
                numerator = numerator / gcd;
                denominator = denominator / gcd;
            }

            // create a string builder
            var stringBuilder = new StringBuilder();

            // add the whole number if it's greater than 0
            if (wholePart > 0d)
            {
                stringBuilder.Append(wholePart);
            }

            // add the fraction if it's greater than 0
            if (fractionPart > 0d)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(" ");
                }

                stringBuilder.Append(denominator);
                stringBuilder.Append("/");
                stringBuilder.Append(numerator);
            }

            return stringBuilder.ToString();
        }

        // Converts fraction to decimal. 
        // There are two formats a fraction greater than 1 can consist of
        // which this function will work for:
        //  Example: 4-1/2 or 4 1/2
        // Fractions less than 1 are in the format of 1/2, etc..
        public static double ParseFraction(string value)
        {
            string[] dparse;
            string[] fparse;
            var whole = "0";
            var dec = "";
            double result = 0;

            // check for '-' or ' ' separator between whole number and fraction
            dparse = value.Contains('-') ? value.Split('-') : value.Split(' ');
            var pcount = dparse.Count();

            // fraction greater than one.
            if (pcount == 2)
            {
                whole = dparse[0];
                dec = dparse[1];
            }
            // whole number or fraction less than 1.
            else if (pcount == 1)
            {
                dec = dparse[0];
            }

            // split out fractional part of value passed in.
            fparse = dec.Split('/');

            // check for fraction.
            if (fparse.Count() == 2)
            {
                try
                {
                    var d0 = Convert.ToDouble(fparse[0]); // convert numerator
                    var d1 = Convert.ToDouble(fparse[1]); // convert denominator
                    result = d0 / d1; // divide the fraction (converts to decimal)
                    var dWhole = Convert.ToDouble(whole); // convert whole number part to decimal.

                    result = dWhole + result; // add whole number 
                                              // and fractional part and we're done.
                }
                catch (Exception e)
                {
                    result = 0;
                }
            }
            else
            // there is no fractional part of the input.
            {
                try
                {
                    result = Convert.ToDouble(whole + dec);
                }
                catch (Exception e)
                {
                    // bad input so return 0.
                    result = 0;
                }
            }

            return result;
        }

        private static uint GreatestCommonDivisor(uint valA, uint valB)
        {
            // return 0 if both values are 0 (no GSD)
            if (valA == 0 &&
              valB == 0)
            {
                return 0;
            }
            // return value b if only a == 0
            else if (valA == 0 &&
                  valB != 0)
            {
                return valB;
            }
            // return value a if only b == 0
            else if (valA != 0 && valB == 0)
            {
                return valA;
            }
            // actually find the GSD
            else
            {
                var first = valA;
                var second = valB;

                while (first != second)
                {
                    if (first > second)
                    {
                        first = first - second;
                    }
                    else
                    {
                        second = second - first;
                    }
                }

                return first;
            }

        }

        // Rounds a number to the nearest decimal.
        // For instance, carpenters do not want to see a number like 4/5.
        // That means nothing to them
        // and you'll have an angry carpenter on your hands
        // if you ask them cut a 2x4 to 36 and 4/5 inches.
        // So, we would want to convert to the nearest 1/16 of an inch.
        // Example: RoundTo(0.8, 0.0625) Rounds 4/5 to 13/16 or 0.8125.
        private static double RoundTo(double val, double places)
        {
            var sPlaces = ToFractionString(places, true);
            var s = sPlaces.Split('/');

            if (s.Count() == 2)
            {
                var nPlaces = System.Convert.ToInt32(s[1]);
                var d = Math.Round(val * nPlaces);
                return d / nPlaces;
            }

            return val;
        }
    }
}
