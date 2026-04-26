namespace iE.Core.Equipment.Tanks.Models
{
    public class TankMrtInput
    {
        public double RTbc { get; set; }
        public double RTip { get; set; }
        public double Or { get; set; }
        public double StPr { get; set; }
        public double UPr { get; set; }
        public string BottomProtectionType { get; set; } = string.Empty;
        public bool BottomCoated { get; set; }
        public double CoatingLife { get; set; }
    }
}
