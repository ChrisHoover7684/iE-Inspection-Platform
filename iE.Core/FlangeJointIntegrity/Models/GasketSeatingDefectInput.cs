namespace iE.Core.FlangeJointIntegrity.Models
{
    public class GasketSeatingDefectInput
    {
        public GasketType GasketType { get; set; }
        public double? DefectDepthInches { get; set; }
        public double? DefectWidthAcrossFaceInches { get; set; }
        public double? DefectLengthCircumferentialInches { get; set; }
        public DefectLocation DefectLocation { get; set; }
        public double? SeatingWidthInches { get; set; }
        public string? Notes { get; set; }
    }
}
