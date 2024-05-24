using NUnit.Framework;
using System;
using System.Collections.Generic;

using static Vim.Util.Cron;

namespace Vim.Util.Tests;

[TestFixture]
public static class CronTests
{
    public record GTC(DayOfWeek[] Days, TimeSpan TimeOfDay, string Expected);

    public static IEnumerable<GTC> GenerateTestCases() => new[]
    {
        new GTC(Array.Empty<DayOfWeek>(), default, "0 0 0 ? * "),
        new GTC(new[] { DayOfWeek.Sunday }, new TimeSpan(23, 59, 58), "58 59 23 ? * 0"),
        new GTC(new [] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }, new TimeSpan(19, 0, 0), "0 0 19 ? * 1,3,5")
    };

    [TestCaseSource(nameof(GenerateTestCases))]
    public static void TestGenerate(GTC c)
    {
        var s = GenerateCronExpression(c.Days, c.TimeOfDay);
        Assert.AreEqual(c.Expected, s);
    }

    public record PTC(string CronExpression, bool ExpectedSuccess, DayOfWeek[] ExpectedDays, TimeSpan ExpectedTimeOfDay);

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
            new [] { DayOfWeek.Sunday },
            new TimeSpan(23, 59, 58)),
        new PTC("0 0 19 ? * 1,3,5",
            true,
            new [] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday },
            new TimeSpan(19, 0, 0)),
        new PTC("0 0 7 ? * 4-6",
            true,
            new [] { DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday},
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * *",
            true,
            new [] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday},
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * 7", // Test that we wrap around using modulo 7 (7 % 7 = 0 = Sunday)
            true,
            new [] { DayOfWeek.Sunday },
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * 14", // Test that we wrap around using modulo 7 (14 % 7 = 0 = Sunday)
            true,
            new [] { DayOfWeek.Sunday },
            new TimeSpan(7, 0, 0)),
        new PTC("0 0 7 ? * 13", // Test that we wrap around using modulo 7 (13 % 7 = 6 = Saturday)
            true,
            new [] { DayOfWeek.Saturday },
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

    public record TNO(string CronExpression, DateTime RelativeTo, DateTime ExpectedDateTime, bool ExpectedSuccess);

    public static IEnumerable<TNO> NextOccurrenceTestCases() => new[]
    {
        new TNO("", default, default, false),
        new TNO(null, default, default, false),
        
        // Mon/Wed/Fri at 7pm (19h)
        new TNO("0 0 19 ? * 1,3,5", new DateTime(2024, 7, 27), new DateTime(2024, 7, 29, 19, 0, 0), true),
        
        // Every day 7am (relative DateTime is midnight)
        new TNO("0 0 7 ? * *", new DateTime(2024, 10, 1), new DateTime(2024, 10, 1, 7, 0, 0), true),

        // Every day 7am (relative DateTime is 7:01am, so next run should be the day after)
        new TNO("0 0 7 ? * *", new DateTime(2024, 10, 1, 7, 0, 1), new DateTime(2024, 10, 2, 7, 0, 0), true),

        // Every day 7:01am (relative DateTime is 7:01am, so next run should be immediate)
        new TNO("1 0 7 ? * *", new DateTime(2024, 10, 1, 7, 0, 1), new DateTime(2024, 10, 1, 7, 0, 1), true),

        // Every Saturday at midnight
        new TNO("0 0 0 ? * 6", new DateTime(2024, 7, 11), new DateTime(2024, 7, 13), true),

        // Every Sunday at 5pm (17h)
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 19, 17, 0, 1), new DateTime(2024, 5, 26, 17, 0, 0), true),
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 20), new DateTime(2024, 5, 26, 17, 0, 0), true),
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 21), new DateTime(2024, 5, 26, 17, 0, 0), true),
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 22), new DateTime(2024, 5, 26, 17, 0, 0), true),
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 23), new DateTime(2024, 5, 26, 17, 0, 0), true),
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 24), new DateTime(2024, 5, 26, 17, 0, 0), true),
        new TNO("0 0 17 ? * 0", new DateTime(2024, 5, 25), new DateTime(2024, 5, 26, 17, 0, 0), true),
    };

    [TestCaseSource(nameof(NextOccurrenceTestCases))]
    public static void TestNextOccurrence(TNO c)
    {
        var success = TryParseNextOccurrence(c.CronExpression, c.RelativeTo, out var dateTime, out var delta);
        Assert.AreEqual(c.ExpectedSuccess, success);
        Assert.AreEqual(c.ExpectedDateTime, dateTime);
        Assert.GreaterOrEqual(delta.Ticks, 0);
    }
}
