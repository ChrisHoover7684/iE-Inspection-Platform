namespace iE.Core.Equipment.Tanks.Models
{
    public class TankMrtResult
    {
        public double MRT { get; set; }
        public double RequiredMRT { get; set; }
        public bool Passes { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
