namespace iE.Core.Inspection.StiSp001
{
    public class StiSp001ScheduleResult
    {
        public double CapacityGallons { get; set; }
        public int? Category { get; set; }
        public string DeterminedCategory { get; set; } = string.Empty;
        public bool ShowRegulatoryWarning { get; set; }
        public string RawSchedule { get; set; } = string.Empty;
        public string FormattedSchedule { get; set; } = string.Empty;
        public string ResultColor { get; set; } = "Default";
        public string? ValidationWarning { get; set; }
    }
}
