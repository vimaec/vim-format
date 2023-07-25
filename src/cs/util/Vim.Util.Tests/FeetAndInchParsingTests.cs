using NUnit.Framework;
using System.Collections.Generic;

namespace Vim.Util.Tests
{
    [TestFixture]
    public static class FeetAndInchParsingTests
    {
        public record TestCase(string Input, bool ExpectSuccess, double? ExpectedDecimalInches = null)
        {
            public override string ToString()
                => $"Input: {Input}, ExpectSuccess: {ExpectSuccess}, DecimalInches: {ExpectedDecimalInches}";
        }

        // Test cases inspired from question here: https://stackoverflow.com/q/22794466
        // Implementation follows from question answer: https://stackoverflow.com/a/22819580
        public static IEnumerable<TestCase> TestCases = new TestCase[]
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
            new ("12' 11-1/2\"", true, 155.5),
            new ("12'   11     1/2\"", true, 155.5),
            new ("121103", true, 155.1875),
            new ("-11'11\"", true, -143.0),
        };

        [TestCaseSource(nameof(TestCases))]
        public static void Test(TestCase args)
        {
            var success = StringFormatting.TryParseFeetAndInchesToDecimalInches(args.Input, out var decimalInches);
            Assert.AreEqual(args.ExpectSuccess, success);

            if (args.ExpectSuccess == false)
                return;

            Assert.AreEqual(args.ExpectedDecimalInches, decimalInches);
        }
    }
}
