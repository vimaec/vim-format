using NUnit.Framework;
using System.Collections.Generic;

namespace Vim.Util.Tests;

[TestFixture]
public static class UnitsTests
{
    public record DecimalInchParsingTestCase(string Input, bool ExpectSuccess, double? ExpectedDecimalInches = null)
    {
        public override string ToString()
            => $"Input: {Input}, ExpectSuccess: {ExpectSuccess}, DecimalInches: {ExpectedDecimalInches}";
    }

    // Test cases inspired from question here: https://stackoverflow.com/q/22794466
    // Implementation follows from question answer: https://stackoverflow.com/a/22819580
    public static IEnumerable<DecimalInchParsingTestCase> DecimalInchParsingTestCases = new DecimalInchParsingTestCase[]
    {
        new (null, false),
        new ("", false),
        new (" ", false),
        new ("abc", false),
        new ("0", false),
        new ("12.5'", true, 150.0),
        new ("11\"", true, 11.0),
        new ("3/16\"", true, 0.1875),
        new ("11' 11\"", true, 143.0),
        new ("11'11\"", true, 143.0),
        new ("12'-11\"", true, 155.0),
        new ("12' 11 3/16\"", true, 155.1875),
        new ("-12' 11 3/16\"", true, -(12d * 12d + 11d + 3d/16d)),
        new ("12' 11-1/2\"", true, 155.5),
        new ("12'   11     1/2\"", true, 155.5),
        new ("121103", true, 155.1875),
        new ("-11'11\"", true, -143.0),
    };

    [TestCaseSource(nameof(DecimalInchParsingTestCases))]
    public static void TestParseToDecimalInches(DecimalInchParsingTestCase args)
    {
        var success = Units.TryParseFeetAndInchesToDecimalInches(args.Input, out var decimalInches);
        Assert.AreEqual(args.ExpectSuccess, success);

        if (args.ExpectSuccess == false)
            return;

        Assert.AreEqual(args.ExpectedDecimalInches, decimalInches);
    }

    //-----------------------------------------------------------------------------

    public record FeetAndFractionalInchTestCase(
        double? Feet,
        string Expected,
        string FeetFormatting = "",
        string PositivePrefix = "",
        string NegativePrefix = "-");

    public static IEnumerable<FeetAndFractionalInchTestCase> FeetAndFractionalInchTestCases = new FeetAndFractionalInchTestCase[]
    {
        new (null, ""),
        new (0d, "0'"),
        new (0d, "+0'", PositivePrefix: "+"),
        new (1d, "1'"),
        new (1d, "0001'", "0000"),
        new (-1d, "-1'"),
        new (-1d, "-0001'", "0000"),
        new (1.5d, "1' 6\""),
        new (-1.5d, "-1' 6\""),
        new (150d, "150'"),
        new (-150d, "-150'"),
        new (11d / 12d, "11\""),
        new (-11d / 12d, "-11\""),
        new (3d / 16d / 12d, "3/16\""),
        new (-3d / 16d / 12d, "-3/16\""),
        new (11d + 11d / 12d, "11' 11\""),
        new (-(11d + 11d / 12d), "-11' 11\""),
        new (1d/16d/12d, "1/16\""),
        new (-1d/16d/12d, "-1/16\""),
        new (99d + 0d / 12d + 1d/16d/12d, "99' 1/16\""),
        new (-(99d + 0d / 12d + 1d/16d/12d), "-99' 1/16\""),
        new (12d + 11d / 12d + 3d/16d/12d, "12' 11 3/16\""),
        new (12d + 11d / 12d + 3d/16d/12d, "00012' 11 3/16\"", "00000"),
        new (12d + 11d / 12d + 3d/16d/12d, " 00012' 11 3/16\"", "00000", " "),
        new (12d + 11d / 12d + 3d/16d/12d, "+00012' 11 3/16\"", "00000", "+"),
        new (120000d + 11d / 12d + 3d/16d/12d, "+120000' 11 3/16\"", "00000", "+"),
        new (-(12d + 11d / 12d + 3d/16d/12d), "-12' 11 3/16\""),
        new (-(12d + 11d / 12d + 3d/16d/12d), "-00012' 11 3/16\"", "00000"),
        new (-(12d + 11d / 12d + 3d/16d/12d), "~00012' 11 3/16\"", "00000", "+", "~"),
    };

    [TestCaseSource(nameof(FeetAndFractionalInchTestCases))]
    public static void ValidateToFeetAndFractionalInchesString(FeetAndFractionalInchTestCase testCase)
    {
        Assert.AreEqual(testCase.Expected, Units.ToFeetAndFractionalInchesString(testCase.Feet, testCase.FeetFormatting, testCase.PositivePrefix, testCase.NegativePrefix));
    }

    //-----------------------------------------------------------------------------

    public record MetersTestCase(
        double? Meters,
        string Expected,
        string MetersFormatting = "",
        string PositivePrefix = "",
        string NegativePrefix = "-");

    public static IEnumerable<MetersTestCase> ToMetersStringTestCases = new MetersTestCase[]
    {
        new (null, ""),
        new (0d, "0m"),
        new (0d, "+0m", PositivePrefix: "+"),
        new (1d, "1m"),
        new (1d, "+1m", PositivePrefix: "+"),
        new (-1d, "-1m"),
        new (10d, "10m"),
        new (10d, "+10m", PositivePrefix: "+"),
        new (10d, "+0010.00m", MetersFormatting: "0000.00", "+"),
        new (10.12345d, "+0010.12m", MetersFormatting: "0000.00", "+"),
        new (-10.12345d, "-0010.12m", MetersFormatting: "0000.00", "+"),
        new (-10.12345d, "~0010.12m", MetersFormatting: "0000.00", "+", "~"),
        new (999999.12345d, "+999999.12345m", MetersFormatting: "0000.00000", "+"),
    };

    [TestCaseSource(nameof(ToMetersStringTestCases))]
    public static void ValidateToMetersString(MetersTestCase testCase)
    {
        Assert.AreEqual(testCase.Expected, Units.ToMetersString(testCase.Meters, testCase.MetersFormatting, testCase.PositivePrefix, testCase.NegativePrefix));
    }
}
