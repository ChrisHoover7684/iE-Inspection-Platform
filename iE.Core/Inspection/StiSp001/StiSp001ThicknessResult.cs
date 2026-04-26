namespace iE.Core.Inspection.StiSp001
{
    public class StiSp001ThicknessResult
    {
        public bool AreaRequired { get; set; }
        public bool RepairRequired { get; set; }
        public int EffectiveCategory { get; set; }
        public string Result { get; set; } = string.Empty;
        public string ResultColor { get; set; } = "Black";
    }
}
