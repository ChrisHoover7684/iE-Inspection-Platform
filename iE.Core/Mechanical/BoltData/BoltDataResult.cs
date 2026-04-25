namespace iE.Core.Mechanical.BoltData
{
    public class BoltDataResult
    {
        public string FlangeSize { get; set; } = "";
        public string FlangeRating { get; set; } = "";

        public int NumberOfBolts { get; set; }
        public string BoltDiameter { get; set; } = "";
        public string StudBoltLengthRF { get; set; } = "";
        public string StudBoltLengthMFTG { get; set; } = "";
        public string StudBoltLengthRJ { get; set; } = "";
        public string BoltCircleDiameter { get; set; } = "";
        public string FlangeDiameter { get; set; } = "";
        public string BoltHoleDiameter { get; set; } = "";

        public string Display { get; set; } = "";
    }
}