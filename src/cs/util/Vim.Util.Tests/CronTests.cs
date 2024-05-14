using NUnit.Framework;
using System;
using System.Collections.Generic;

using static Vim.Util.Cron;

namespace Vim.Util.Tests;

[TestFixture]
public static class CronTests
{
    public record GTC(Days[] Days, TimeSpan TimeOfDay, string Expected);

    public static IEnumerable<GTC> GenerateTestCases() => new[]
    {
        new GTC(Array.Empty<Days>(), default, "0 0 0 ? * "),
        new GTC(new[] { Days.SUN }, new TimeSpan(23, 59, 58), "58 59 23 ? * 0"),
        new GTC(new [] { Days.MON, Days.WED, Days.FRI }, new TimeSpan(19, 0, 0), "0 0 19 ? * 1,3,5")
    };

    [TestCaseSource(nameof(GenerateTestCases))]
    public static void TestGenerate(GTC c)
    {
        var s = GenerateCronExpression(c.Days, c.TimeOfDay);
        Assert.AreEqual(c.Expected, s);
    }

    public record PTC(string CronExpression, bool ExpectedSuccess, Days[] ExpectedDays, TimeSpan ExpectedTimeOfDay);

    public static IEnumerable<PTC> ParseTestCases() => new[]
    {
        // You can test these cron expressions manually here: https://crontab.cronhub.io/
        new PTC(null, false, null, default),
        new PTC("", false, null, default),
        new PTC(" ", false, null, default),
        new PTC("0-5", false, null, default),
        new PTC("0 0 0 ? *", false, null, default),
        new PTC("58 59 23 ? * 0",
            true, 
            new [] { Days.SUN },
            new TimeSpan(23, 59, 58)),
        new PTC("0 0 19 ? * 1,3,5",
            true,
            new [] { Days.MON, Days.WED, Days.FRI },
            new TimeSpan(19, 0, 0)),
        new PTC("0 0 7 ? * 4-6",
            true,
            new [] { Days.THU, Days.FRI, Days.SAT},
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * *",
            true,
            new [] { Days.SUN, Days.MON, Days.TUE, Days.WED, Days.THU, Days.FRI, Days.SAT},
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * 7", // Test that we wrap around using modulo 7 (7 % 7 = 0 = SUN)
            true,
            new [] { Days.SUN },
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * 14", // Test that we wrap around using modulo 7 (14 % 7 = 0 = SUN)
            true,
            new [] { Days.SUN },
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * 13", // Test that we wrap around using modulo 7 (13 % 7 = 6 = SAT)
            true,
            new [] { Days.SAT },
            new TimeSpan(7, 0, 0)),
    };

    [TestCaseSource(nameof(ParseTestCases))]
    public static void TestParse(PTC c)
    {
        var success = TryParseCronExpression(c.CronExpression, out var d, out var t);
        Assert.AreEqual(c.ExpectedSuccess, success);
        Assert.AreEqual(c.ExpectedDays, d);
        Assert.AreEqual(c.ExpectedTimeOfDay, t);
    }
}
