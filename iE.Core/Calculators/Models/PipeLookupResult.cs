namespace iE.Core.Calculators.Models;

public class PipeLookupResult
{
    public string Nps { get; set; } = string.Empty;
    public string Schedule { get; set; } = string.Empty;
    public double OutsideDiameterInches { get; set; }
    public double NominalWallThicknessInches { get; set; }
    public double InsideDiameterInches { get; set; }
}
