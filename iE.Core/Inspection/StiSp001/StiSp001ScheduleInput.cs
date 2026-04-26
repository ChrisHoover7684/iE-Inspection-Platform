namespace iE.Core.Inspection.StiSp001
{
    public class StiSp001ScheduleInput
    {
        public bool IsPortableContainer { get; set; }
        public string PortableMaterial { get; set; } = "Plastic";

        public bool UseDirectCapacity { get; set; } = true;
        public double? Capacity { get; set; }
        public string CapacityUnit { get; set; } = "Gallons";

        public double? DiameterFeet { get; set; }
        public double? HeightFeet { get; set; }

        public int SpillControlCount { get; set; }
        public int CrdmCount { get; set; }

        public DateTime LastExternalDate { get; set; } = DateTime.Now;
        public DateTime LastInternalDate { get; set; } = DateTime.Now;
        public DateTime LastLeakTestDate { get; set; } = DateTime.Now;
    }
}
