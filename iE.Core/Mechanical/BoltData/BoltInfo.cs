namespace iE.Core.Mechanical.BoltData
{
    public class BoltInfo
    {
        public int NumberOfBolts { get; set; }
        public string BoltDiameter { get; set; } = "";
        public string StudBoltLengthRF { get; set; } = "";
        public string StudBoltLengthMFTG { get; set; } = "";
        public string StudBoltLengthRJ { get; set; } = "";
        public string BoltCircleDiameter { get; set; } = "";
        public string FlangeDiameter { get; set; } = "";
        public string BoltHoleDiameter { get; set; } = "";

        public BoltInfo(
            int numberOfBolts,
            string boltDiameter,
            string studBoltLength_RF,
            string studBoltLength_MF_TG,
            string studBoltLength_RJ,
            string boltCircleDiameter,
            string flangeDiameter,
            string boltHoleDiameter)
        {
            NumberOfBolts = numberOfBolts;
            BoltDiameter = boltDiameter;
            StudBoltLengthRF = studBoltLength_RF;
            StudBoltLengthMFTG = studBoltLength_MF_TG;
            StudBoltLengthRJ = studBoltLength_RJ;
            BoltCircleDiameter = boltCircleDiameter;
            FlangeDiameter = flangeDiameter;
            BoltHoleDiameter = boltHoleDiameter;
        }
    }
}