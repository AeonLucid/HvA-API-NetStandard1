using System;
using HvA.API.Extensions;

namespace HvA.API.Util
{
    public static class DateTimeHelper
    {

        private static readonly DateTime PosixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly int CurrentYear = DateTime.Now.Year;

        // Thanks to http://stackoverflow.com/a/34727270
        internal static DateTime FirstDateOfWeek(int weekOfYear)
        {
            var newYear = new DateTime(CurrentYear, 1, 1);
            var weekNumber = newYear.GetIso8601WeekOfYear();

            DateTime firstWeekDate;

            if (weekNumber != 1)
            {
                var dayNumber = (int)newYear.DayOfWeek;
                firstWeekDate = newYear.AddDays(7 - dayNumber + 1);
            }
            else
            {
                var dayNumber = (int)newYear.DayOfWeek;
                firstWeekDate = newYear.AddDays(-dayNumber + 1);
            }

            if (weekOfYear == 1)
            {
                return firstWeekDate;
            }

            return firstWeekDate.AddDays(7 * (weekOfYear - 1));
        }

        /// <summary>
        ///     Converts a long timestamp type to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="timestampMilliseconds">The timestamp in milliseconds./</param>
        /// <returns>Returns a <see cref="DateTime"/>.</returns>
        public static DateTime LongDateToDateTime(long timestampMilliseconds)
        {
            return PosixTime.AddMilliseconds(timestampMilliseconds).ToLocalTime();
        }

    }
}
