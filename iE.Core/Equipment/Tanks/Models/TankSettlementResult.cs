namespace iE.Core.Equipment.Tanks.Models
{
    public class TankSettlementResult
    {
        public double MaxSettlement { get; set; }
        public double MinSettlement { get; set; }
        public double DifferentialSettlement { get; set; }
        public double OutOfPlaneLimit { get; set; }

        public bool IsOutOfPlaneAcceptable { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
