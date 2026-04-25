using System;
using System.Linq;
using System.Text;

namespace iE.Core.Common.DateTimeSpan
{
    public class DateTimeSpanService
    {
        public DateTimeSpanResult Calculate(DateTimeSpanInput input)
        {
            DateTime start = input.StartDate;
            DateTime end = input.EndDate;

            bool isNegative = end < start;

            DateTime earlier = start <= end ? start : end;
            DateTime later = start <= end ? end : start;

            TimeSpan absSpan = later - earlier;

            var holidays = input.Holidays
                .Select(d => d.Date)
                .ToHashSet();

            int calendarDays = (later.Date - earlier.Date).Days;

            int workDaysExclusiveEnd = CountNetworkDays(
                earlier.Date,
                later.Date,
                new HashSet<DateTime>(),
                includeEndDate: false);

            int workDaysInclusive = CountNetworkDays(
                earlier.Date,
                later.Date,
                new HashSet<DateTime>(),
                includeEndDate: true);

            int networkDaysExclusiveWithHolidays = CountNetworkDays(
                earlier.Date,
                later.Date,
                holidays,
                includeEndDate: false);

            int networkDaysInclusiveWithHolidays = CountNetworkDays(
                earlier.Date,
                later.Date,
                holidays,
                includeEndDate: true);

            int holidaysExcluded = holidays.Count(h =>
                h >= earlier.Date &&
                h <= later.Date &&
                IsWeekday(h));

            double approxWorkWeeks = workDaysInclusive / 5.0;
            double approxWorkWeeksWithHolidays = networkDaysInclusiveWithHolidays / 5.0;

            var result = new DateTimeSpanResult
            {
                StartDate = start,
                EndDate = end,
                EndIsBeforeStart = isNegative,

                TotalDays = Math.Round(absSpan.TotalDays, 6),
                TotalHours = Math.Round(absSpan.TotalHours, 6),
                TotalMinutes = Math.Round(absSpan.TotalMinutes, 6),
                TotalSeconds = Math.Round(absSpan.TotalSeconds, 6),

                CalendarDays = calendarDays,

                WorkDaysExclusiveEnd = workDaysExclusiveEnd,
                WorkDaysInclusive = workDaysInclusive,
                ApproxWorkWeeks = Math.Round(approxWorkWeeks, 6),

                HolidayCountExcluded = holidaysExcluded,
                NetWorkDaysExclusiveEndWithHolidays = networkDaysExclusiveWithHolidays,
                NetWorkDaysInclusiveWithHolidays = networkDaysInclusiveWithHolidays,
                ApproxWorkWeeksWithHolidays = Math.Round(approxWorkWeeksWithHolidays, 6),

                ElapsedDays = absSpan.Days,
                ElapsedHours = absSpan.Hours,
                ElapsedMinutes = absSpan.Minutes,
                ElapsedSeconds = absSpan.Seconds
            };

            result.Display = BuildDisplay(result);
            return result;
        }

        private int CountNetworkDays(
            DateTime startDate,
            DateTime endDate,
            HashSet<DateTime> holidays,
            bool includeEndDate)
        {
            if (endDate < startDate)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }

            int count = 0;
            DateTime current = startDate.Date;
            DateTime end = endDate.Date;

            while (includeEndDate ? current <= end : current < end)
            {
                if (IsWeekday(current) && !IsHoliday(current, holidays))
                    count++;

                current = current.AddDays(1);
            }

            return count;
        }

        private bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday &&
                   date.DayOfWeek != DayOfWeek.Sunday;
        }

        private bool IsHoliday(DateTime date, HashSet<DateTime> holidays)
        {
            return holidays.Contains(date.Date);
        }

        private string BuildDisplay(DateTimeSpanResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Start: {result.StartDate:yyyy-MM-dd hh:mm:ss tt}");
            sb.AppendLine($"End: {result.EndDate:yyyy-MM-dd hh:mm:ss tt}");
            sb.AppendLine(result.EndIsBeforeStart ? "Direction: End is BEFORE Start" : "Direction: End is AFTER Start");
            sb.AppendLine();

            sb.AppendLine($"Total Days: {result.TotalDays}");
            sb.AppendLine($"Total Hours: {result.TotalHours}");
            sb.AppendLine($"Total Minutes: {result.TotalMinutes}");
            sb.AppendLine($"Total Seconds: {result.TotalSeconds}");
            sb.AppendLine();

            sb.AppendLine($"Calendar Days (date difference): {result.CalendarDays}");
            sb.AppendLine($"Work Days (Mon-Fri, end excluded): {result.WorkDaysExclusiveEnd}");
            sb.AppendLine($"Work Days Inclusive (Mon-Fri): {result.WorkDaysInclusive}");
            sb.AppendLine($"Approx. Work Weeks (5-day): {result.ApproxWorkWeeks}");
            sb.AppendLine();

            sb.AppendLine($"Selected Holidays Excluded: {result.HolidayCountExcluded}");
            sb.AppendLine($"Network Days with Holidays Excluded (end excluded): {result.NetWorkDaysExclusiveEndWithHolidays}");
            sb.AppendLine($"Network Days with Holidays Excluded Inclusive: {result.NetWorkDaysInclusiveWithHolidays}");
            sb.AppendLine($"Approx. Work Weeks with Holidays Excluded: {result.ApproxWorkWeeksWithHolidays}");
            sb.AppendLine();

            sb.AppendLine($"Elapsed: {result.ElapsedDays} days, {result.ElapsedHours} hours, {result.ElapsedMinutes} minutes, {result.ElapsedSeconds} seconds");

            return sb.ToString();
        }
    }
}