namespace iE.Core.Equipment.Tanks.Models
{
    public class TankShellInput
    {
        public double Diameter { get; set; }          // ft
        public double SpecificGravity { get; set; }
        public double CorrosionAllowance { get; set; } // inches

        public string Material { get; set; } = "A516-70";

        public string ApiEdition { get; set; } = "7th and Later (1980-Present)";

        public List<TankShellCourse> Courses { get; set; } = new();
        
    }

    public class TankShellCourse
    {
        public double Height { get; set; }
        public double JointEfficiency { get; set; } = 1.0;
        public double ActualThickness { get; set; } // inches
    }
}