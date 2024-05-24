using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Vim.Util
{
    public static class Cron
    {
        public static string GenerateCronExpression(IEnumerable<DayOfWeek> cronDaysOfTheWeek, TimeSpan timeOfDay)
            => $"{timeOfDay.Seconds} {timeOfDay.Minutes} {timeOfDay.Hours} ? * {string.Join(",", cronDaysOfTheWeek.Select(d => (int)d))}";

        public static bool TryParseCronExpression(string cronExpression, out DayOfWeek[] daysOfWeek, out TimeSpan timeOfDay)
        {
            daysOfWeek = null;
            timeOfDay = default;

            if (string.IsNullOrWhiteSpace(cronExpression))
                return false;

            try
            {
                // Split the cron expression into its components
                var cronComponents = cronExpression.Split(' ');
                if (cronComponents.Length < 6)
                    return false;

                // Extract the time of day components (hour and minute)
                var second = int.Parse(cronComponents[0]);
                var minute = int.Parse(cronComponents[1]);
                var hour = int.Parse(cronComponents[2]);

                // Convert hour and minute to TimeSpan representing the time of day
                timeOfDay = new TimeSpan(hour, minute, second);

                // Extract the day of week component (0-indexed)
                var dayOfWeekComponent = cronComponents[5];

                // Convert the day of week component to a list of integers representing selected days
                daysOfWeek = ParseDayOfWeekComponent(dayOfWeekComponent);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryParseNextOccurrence(string cronExpression, DateTime relativeTo, out DateTime dateTime, out TimeSpan delay)
        {
            dateTime = default;
            delay = default;

            if (!TryParseCronExpression(cronExpression, out var daysOfWeek, out var timeOfDay))
                return false;

            if (daysOfWeek.Length == 0)
                return false;

            var now = relativeTo;

            // Find the minimum time between now and the next.
            var thisDayOfTheWeek = now.DayOfWeek;

            var tuples = daysOfWeek
                .Select(dayOfWeek =>
                {
                    var daysUntilNext = ((int)dayOfWeek - (int)thisDayOfTheWeek + 7) % 7;

                    var nextOccurrence = now.Date.AddDays(daysUntilNext).Add(timeOfDay);

                    var _delta = nextOccurrence - now;
                    if (_delta.Ticks < 0 && daysUntilNext == 0)
                    {
                        // Corner case: this happened previously (today), so the next occurrence should be in 7 days.
                        nextOccurrence = now.Date.AddDays(7).Add(timeOfDay);
                        _delta = nextOccurrence - now;
                    }

                    Debug.Assert(_delta.Ticks >= 0, "Delay is less than zero");

                    return (nextOccurrence, delay: _delta);
                })
                .ToArray();

            (dateTime, delay) = tuples.Minimize(TimeSpan.MaxValue, t => t.delay);

            return dateTime != default;
        }

        private static DayOfWeek[] ParseDayOfWeekComponent(string dayOfWeekComponent)
        {
            var daysOfWeek = new List<DayOfWeek>();

            var allDaysAsInts = Enum.GetValues(typeof(DayOfWeek)).Cast<int>().ToArray();
            var minDay = allDaysAsInts.Min();
            var maxDay = allDaysAsInts.Max();

            void AddDay(int day)
            {
                if (day < minDay)
                    return;

                day %= (maxDay + 1); // 7 (also SUN) becomes 0 (SUN)

                daysOfWeek.Add((DayOfWeek) day);
            }

            if (dayOfWeekComponent.Contains(","))
            {
                // Handle comma-separated list of days
                var parts = dayOfWeekComponent.Split(',');
                foreach (var part in parts)
                {
                    if (int.TryParse(part, out var day))
                    {
                        AddDay(day);
                    }
                }
            }
            else if (dayOfWeekComponent.Contains("-"))
            {
                // Handle range of days
                var range = dayOfWeekComponent.Split('-');
                if (range.Length == 2 &&
                    int.TryParse(range[0], out var start) &&
                    int.TryParse(range[1], out var end))
                {
                    for (var i = start; i <= end; i++)
                    {
                        AddDay(i);
                    }
                }
            }
            else if (dayOfWeekComponent.Equals("*"))
            {
                // Handle all days of the week
                daysOfWeek.AddRange(Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>());
            }
            else if (int.TryParse(dayOfWeekComponent, out var day))
            {
                // Handle single day
                AddDay(day);
            }

            return daysOfWeek.ToArray();
        }
    }
}
