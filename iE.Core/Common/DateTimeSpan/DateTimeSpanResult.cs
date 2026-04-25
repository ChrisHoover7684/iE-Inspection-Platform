namespace iE.Core.Common.DateTimeSpan
{
    public class DateTimeSpanResult
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool EndIsBeforeStart { get; set; }

        public double TotalDays { get; set; }
        public double TotalHours { get; set; }
        public double TotalMinutes { get; set; }
        public double TotalSeconds { get; set; }

        public int CalendarDays { get; set; }
        public int WorkDaysExclusiveEnd { get; set; }
        public int WorkDaysInclusive { get; set; }
        public double ApproxWorkWeeks { get; set; }

        public int ElapsedDays { get; set; }
        public int ElapsedHours { get; set; }
        public int ElapsedMinutes { get; set; }
        public int ElapsedSeconds { get; set; }

        public string Display { get; set; } = "";
        public int HolidayCountExcluded { get; set; }
        public int NetWorkDaysExclusiveEndWithHolidays { get; set; }
        public int NetWorkDaysInclusiveWithHolidays { get; set; }
        public double ApproxWorkWeeksWithHolidays { get; set; }

    }
}