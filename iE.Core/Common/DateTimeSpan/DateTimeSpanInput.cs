namespace iE.Core.Common.DateTimeSpan
{
    public class DateTimeSpanInput
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<DateTime> Holidays { get; set; } = new();
    }
}