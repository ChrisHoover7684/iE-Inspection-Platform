namespace iE.Core.Equipment.Tanks.Models
{
    public class TankShellResult
    {
        public List<CourseResult> Courses { get; set; } = new();
        public double MaxFillHeight { get; set; }
    }

    public class CourseResult
    {
        public int CourseNumber { get; set; }
        public double RequiredThickness { get; set; }
        public double ActualThickness { get; set; }
        public double DesignStress { get; set; }
        public double HydroTestStress { get; set; }
        public double HydroTestHeight { get; set; }
        public double MaxProductHeight { get; set; }
        public string Status { get; set; } = "";
        public double CalculatedThickness { get; set; }
        public double GoverningRequiredThickness { get; set; }
        public bool IsGovernedByLowerCourse { get; set; }
        public string GoverningNote { get; set; } = "";
        public string CourseStressGroup { get; set; } = "";
        public double YieldFactor { get; set; }
        public double TensileFactor { get; set; }
        public double YieldBasedStress { get; set; }
        public double TensileBasedStress { get; set; }
        public string StressBasis { get; set; } = "";
    }
}