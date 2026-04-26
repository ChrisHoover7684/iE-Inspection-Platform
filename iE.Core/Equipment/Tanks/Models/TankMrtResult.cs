namespace iE.Core.Equipment.Tanks.Models
{
    public class TankMrtResult
    {
        public double Mrt { get; set; }
        public double RequiredMrt { get; set; }
        public bool Passes { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
