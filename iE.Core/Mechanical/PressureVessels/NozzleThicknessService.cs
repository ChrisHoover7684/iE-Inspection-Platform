namespace iE.Core.Mechanical.PressureVessels;

public enum NozzleType { PipeNozzle, ForgedNozzle, LongWeldNeck, FittingNozzle }
public enum AttachmentLocation { Shell, Head }
public enum CodeEra { Pre1999, Post1999 }

public sealed record NozzleThicknessInput(
    string DesignCode,
    double DesignPressurePsi,
    double ExternalPressurePsi,
    double DesignTemperatureF,
    double JointEfficiency,
    double CorrosionAllowanceIn,
    bool ManualAllowableStress,
    double AllowableStressPsi,
    string MaterialSpec,
    string MaterialGrade,
    string MaterialProductForm,
    CodeEra CodeEra,
    AttachmentLocation AttachmentLocation,
    double ShellOrHeadRequiredThicknessIn,
    double ShellOrHeadExternalRequiredThicknessIn,
    double Ug16MinimumThicknessIn,
    NozzleType NozzleType,
    bool UseOdForTa,
    bool UseIdForTa,
    double OutsideDiameterIn,
    double InsideDiameterIn,
    double NominalThicknessIn,
    double OriginalThicknessIn,
    string NominalPipeSize,
    double Ug45TableMinimumThicknessIn)
{
    public static NozzleThicknessInput Default() => new("ASME Section VIII 1999",150,0,100,1.0,0.125,false,20000,"SA-106","B","Seamless Pipe",CodeEra.Post1999,AttachmentLocation.Shell,0,0,0.0625,NozzleType.PipeNozzle,true,false,4.5,4.026,0.237,0.237,"1",0.116);
}

public sealed record NozzleThicknessResult(
    bool IsValid,
    string ErrorMessage,
    double JointEfficiencyUsed,
    double InsideRadiusUsed,
    double TaRequiredThicknessIn,
    double Tb1RequiredThicknessIn,
    double Tb2RequiredThicknessIn,
    double Tb3TableThicknessIn,
    double Tb3PlusCorrosionAllowanceIn,
    double TbRequiredThicknessIn,
    double PressureRequiredThicknessIn,
    double GoverningRequiredThicknessIn,
    double MarginIn,
    double ProvidedThicknessIn,
    double CorrodedThicknessIn,
    double RequiredThicknessPlusCorrosionAllowanceIn,
    bool IsAcceptable,
    IReadOnlyList<string> Warnings);

public sealed class NozzleThicknessService
{
    public static double GetJointEfficiency(NozzleType nozzleType, double userEfficiency) => nozzleType switch
    {
        NozzleType.PipeNozzle or NozzleType.ForgedNozzle or NozzleType.LongWeldNeck => Math.Min(userEfficiency, 1.0),
        NozzleType.FittingNozzle => Math.Min(userEfficiency, 0.85),
        _ => userEfficiency
    };

    public NozzleThicknessResult Calculate(NozzleThicknessInput input)
    {
        var warnings = new List<string>();
        if (input.DesignPressurePsi <= 0) return Invalid("Design pressure must be greater than 0.");
        if (input.JointEfficiency <= 0 || input.JointEfficiency > 1) return Invalid("Joint efficiency must be between 0 and 1.");
        if (input.CorrosionAllowanceIn < 0) return Invalid("Corrosion allowance cannot be negative.");
        if (input.ShellOrHeadRequiredThicknessIn < 0) return Invalid("Shell/Head required thickness cannot be negative.");
        if (input.ShellOrHeadExternalRequiredThicknessIn < 0) return Invalid("External required thickness cannot be negative.");
        if (input.AllowableStressPsi <= 0) return Invalid("Allowable stress must be greater than 0.");

        var (resolvedOd, resolvedId, resolvedT, ok) = ResolveDimensions(input.OutsideDiameterIn, input.InsideDiameterIn, input.NominalThicknessIn);
        if (!ok) return Invalid("Unable to resolve dimensions. Please provide at least two of: OD, ID, or Thickness.");

        var jointEfficiencyForTa = GetJointEfficiency(input.NozzleType, input.JointEfficiency);
        var radius = input.UseOdForTa ? resolvedOd / 2.0 : resolvedId / 2.0;

        var denominator = input.AllowableStressPsi * jointEfficiencyForTa - 0.6 * input.DesignPressurePsi;
        if (denominator <= 0) return Invalid("Denominator in formula is zero or negative. Check allowable stress and pressure.");

        var taPressure = (input.DesignPressurePsi * radius) / denominator;
        var tAWithCa = taPressure + input.CorrosionAllowanceIn;

        var tb1 = Math.Max(input.ShellOrHeadRequiredThicknessIn, input.Ug16MinimumThicknessIn) + input.CorrosionAllowanceIn;
        if (input.ShellOrHeadRequiredThicknessIn <= 0)
            warnings.Add($"Required {(input.AttachmentLocation == AttachmentLocation.Shell ? "shell" : "head")} thickness at nozzle location not provided. UG-45 t_b1 may be unconservative.");

        double tb2;
        if (input.ShellOrHeadExternalRequiredThicknessIn > 0)
        {
            tb2 = Math.Max(input.ShellOrHeadExternalRequiredThicknessIn, input.Ug16MinimumThicknessIn) + input.CorrosionAllowanceIn;
        }
        else
        {
            tb2 = input.Ug16MinimumThicknessIn + input.CorrosionAllowanceIn;
            if (input.ExternalPressurePsi > 0) warnings.Add("External pressure exists but external required thickness not provided. UG-45 t_b2 may be unconservative.");
        }

        var tb3 = input.Ug45TableMinimumThicknessIn;
        var tb3PlusCa = tb3 + input.CorrosionAllowanceIn;
        var tb = Math.Max(Math.Min(tb1, tb3PlusCa), Math.Min(tb2, tb3PlusCa));
        var governing = Math.Max(tAWithCa, tb);

        var margin = input.OriginalThicknessIn - governing;
        var acceptable = margin >= -0.001;
        if (input.ManualAllowableStress) warnings.Add("Manual allowable stress override is active.");
        if (!acceptable) warnings.Add($"Provided thickness ({input.OriginalThicknessIn:F4}\") is {Math.Abs(margin):F4}\" less than required thickness ({governing:F4}\").");
        warnings.Add("UG-37 reinforcement calculation not yet included.");

        return new NozzleThicknessResult(true, string.Empty, jointEfficiencyForTa, radius, taPressure, tb1, tb2, tb3, tb3PlusCa, tb, taPressure, governing, margin, input.OriginalThicknessIn, input.OriginalThicknessIn - input.CorrosionAllowanceIn, governing, acceptable, warnings);
    }

    private static (double od, double id, double t, bool ok) ResolveDimensions(double outsideDiameterIn, double insideDiameterIn, double nominalThicknessIn)
    {
        var od = outsideDiameterIn; var id = insideDiameterIn; var t = nominalThicknessIn;
        var validCount = (od > 0 ? 1 : 0) + (id > 0 ? 1 : 0) + (t > 0 ? 1 : 0);
        if (validCount >= 2)
        {
            if (od > 0 && id > 0 && t <= 0) t = (od - id) / 2.0;
            else if (od > 0 && t > 0 && id <= 0) id = od - 2.0 * t;
            else if (id > 0 && t > 0 && od <= 0) od = id + 2.0 * t;
        }
        return (od, id, t, od > 0 && id > 0 && t > 0);
    }

    private static NozzleThicknessResult Invalid(string message) =>
        new(false, message, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, Array.Empty<string>());
}


public static class Ug45Table
{
    private static readonly Dictionary<string, double> MinimumThickness = new()
    {
        { "1/8", 0.060 },{ "1/4", 0.077 },{ "3/8", 0.080 },{ "1/2", 0.095 },{ "3/4", 0.099 },{ "1", 0.116 },{ "1-1/4", 0.123 },{ "1-1/2", 0.127 },{ "2", 0.135 },{ "2-1/2", 0.178 },{ "3", 0.189 },{ "3-1/2", 0.198 },{ "4", 0.207 },{ "5", 0.226 },{ "6", 0.245 },{ "8", 0.282 },{ "10", 0.319 },{ "12", 0.328 }
    };
    public static double GetMinimumThickness(string nps) => !string.IsNullOrEmpty(nps) && MinimumThickness.TryGetValue(nps, out var v) ? v : 0.116;
}
