using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vim.Format.Utils.Tests
{
    [TestFixture]
    public static class PipeSeparatedStringsTests
    {
        public record TestCase(string[] Parts, string Expected)
        {
            public override string ToString()
                => $"Parts: {(Parts == null ? "null" : "{ " + string.Join(", ", Parts.Select(p => p == null ? "null" : $"\"{p}\""))) + " }"} ~ Expected: {(Expected == null ? "null" : $"\"{Expected}\"")}";
        };

        public static IEnumerable<TestCase> TestCases = new TestCase[]
        {
            new (null, null),
            new (new string[] {}, null),
            new (new string[] { null }, ""),
            new (new [] { "" }, @""),
            new (new [] { "" }, @""),
            new (new [] { "", "" }, @"|"),
            new (new [] { "", "", "" }, @"||"),
            new (new [] { "", "|" }, @"|\|"),
            new (new [] { "|", "" }, @"\||"),
            new (new [] { "|", "|" }, @"\||\|"),
            new (new [] { "|", "|", "|" }, @"\||\||\|"),
            new (new [] { "|", "", "|" }, @"\|||\|"),
            new (new [] { @"\" }, @"\\"),
            new (new [] { @"\", "" }, @"\\|"),
            new (new [] { @"", @"\" }, @"|\\"),
            new (new [] { @"\", @"\" }, @"\\|\\"),
            new (new [] { @"\\", @"\" }, @"\\\\|\\"),
            new (new [] {"beep" }, @"beep"),
            new (new [] {"beep", "boop" }, @"beep|boop"),
            new (new [] {"be|ep", "boop" }, @"be\|ep|boop"),
            new (new [] {"|b|e|e|p|", "|b|o|o|p|" }, @"\|b\|e\|e\|p\||\|b\|o\|o\|p\|"),
            new (new [] {@"\b\e\e\p\", @"\b\o\o\p\" }, @"\\b\\e\\e\\p\\|\\b\\o\\o\\p\\"),
            new (new [] {@"\|b\|e\|e\|p\|", @"\|b\|o\|o\|p\|" }, @"\\\|b\\\|e\\\|e\\\|p\\\||\\\|b\\\|o\\\|o\\\|p\\\|"),
            new (new [] {"beep", "boop", "doot" }, @"beep|boop|doot"),
            new (new [] {"beep|", @"boop\|", @"|doot" }, @"beep\||boop\\\||\|doot"),
        };

        [TestCaseSource(nameof(TestCases))]
        public static void Test(TestCase args)
        {
            var joined = PipeSeparatedStrings.Join(args.Parts);
            Assert.AreEqual(args.Expected, joined, "Joined is not equal to expected");

            var split = PipeSeparatedStrings.Split(joined);

            // Checking the new parser is legit
            var parser = new PipeSeparatedStrings.Parser(100);
            parser.Parse(joined);
            var parsed = parser.GetValues();
            Assert.AreEqual(split, parsed);

            if (joined == null)
            {
                Assert.IsNull(parsed, "Split is not null");
            }
            else
            {
                var expected = args.Parts.Select(p => string.IsNullOrEmpty(p) ? "" : p);
                Assert.AreEqual(expected, parsed, $"Split ({string.Join(", ", parsed.Select(s => $"\"{s}\""))}) is not equal to original parts ({string.Join(", ", expected.Select(s => $"\"{s}\""))})");
            }
        }

        [Test]
        public static void Test_Parser_DoesntThrow_OnExtraValues()
        {
            var parser = new PipeSeparatedStrings.Parser(2);
            parser.Parse(@"beep|boop|doot");
            Assert.AreEqual(parser.GetCount(), 2);
            Assert.AreEqual(parser.GetValue(0), "beep");
            Assert.AreEqual(parser.GetValue(1), "boop|doot");
        }

        [Test]
        public static void Test_Parser_Throws_OnGetValueOutOfRange()
        {
            var parser = new PipeSeparatedStrings.Parser(2);
            parser.Parse(@"beep|boop");
            Assert.Throws<ArgumentOutOfRangeException>(() => parser.GetValue(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => parser.GetValue(2));
        }

        [Test]
        public static void Test_Parser_ReturnsNull_OnUndefinedValues()
        {
            var parser = new PipeSeparatedStrings.Parser(3);
            parser.Parse(@"beep|boop");
            Assert.AreEqual(parser.GetValue(2), null);
        }

    }
}
