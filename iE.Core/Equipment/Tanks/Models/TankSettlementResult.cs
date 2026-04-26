namespace iE.Core.Equipment.Tanks.Models
{
    public class TankSettlementResult
    {
        public int PointCount { get; set; }
        public double ArcLength { get; set; }

        public double MaxElevation { get; set; }
        public double MinElevation { get; set; }
        public double MaxDifferential { get; set; }
        public double AllowableDifferentialSettlement { get; set; }

        public double TiltRadians { get; set; }
        public double TiltDegrees { get; set; }

        public double EdgeSettlement { get; set; }
        public double AllowableEdgeSettlement { get; set; }

        public double CenterSettlement { get; set; }
        public double AllowableCenterSettlement { get; set; }

        public bool IsDifferentialAcceptable { get; set; }
        public bool IsEdgeAcceptable { get; set; }
        public bool IsCenterAcceptable { get; set; }
        public bool IsAcceptable { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
