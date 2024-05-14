using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Util
{
    public static class Cron
    {
        public enum Days
        {
            SUN = 0,
            MON = 1,
            TUE = 2,
            WED = 3,
            THU = 4,
            FRI = 5,
            SAT = 6,
        }

        public static IReadOnlyList<string> DayNames => Enum.GetNames(typeof(Days));

        public static string GenerateCronExpression(IEnumerable<Days> cronDaysOfTheWeek, TimeSpan timeOfDay)
            => $"{timeOfDay.Seconds} {timeOfDay.Minutes} {timeOfDay.Hours} ? * {string.Join(",", cronDaysOfTheWeek.Select(d => (int)d))}";

        public static bool TryParseCronExpression(string cronExpression, out Days[] daysOfWeek, out TimeSpan timeOfDay)
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

        private static Days[] ParseDayOfWeekComponent(string dayOfWeekComponent)
        {
            var daysOfWeek = new List<Days>();

            var allDaysAsInts = Enum.GetValues(typeof(Days)).Cast<int>().ToArray();
            var minDay = allDaysAsInts.Min();
            var maxDay = allDaysAsInts.Max();

            void AddDay(int day)
            {
                if (day < minDay)
                    return;

                day %= (maxDay + 1); // 7 (also SUN) becomes 0 (SUN)

                daysOfWeek.Add((Days) day);
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
                daysOfWeek.AddRange(Enum.GetValues(typeof(Days)).Cast<Days>());
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
