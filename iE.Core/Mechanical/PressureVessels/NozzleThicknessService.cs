namespace iE.Core.Mechanical.PressureVessels;

public sealed record NozzleThicknessInput(
    double DesignPressurePsi,
    double AllowableStressPsi,
    double JointEfficiency,
    double InsideRadiusIn,
    double CorrosionAllowanceIn,
    double ProvidedThicknessIn,
    double ShellOrHeadRequiredThicknessIn,
    double ShellOrHeadExternalRequiredThicknessIn,
    double Ug45TableMinimumThicknessIn,
    double Ug16MinimumThicknessIn = 0.0625);

public sealed record NozzleThicknessResult(
    double TaRequiredThicknessIn,
    double Tb1RequiredThicknessIn,
    double Tb2RequiredThicknessIn,
    double Tb3RequiredThicknessIn,
    double TbRequiredThicknessIn,
    double GoverningRequiredThicknessIn,
    double RequiredWithCorrosionAllowanceIn,
    double MarginIn,
    bool IsAcceptable);

public sealed class NozzleThicknessService
{
    public NozzleThicknessResult Calculate(NozzleThicknessInput input)
    {
        var ta = (input.DesignPressurePsi * input.InsideRadiusIn) /
                 ((input.AllowableStressPsi * input.JointEfficiency) - (0.6 * input.DesignPressurePsi));

        var tb1 = Math.Max(input.ShellOrHeadRequiredThicknessIn, input.Ug16MinimumThicknessIn) + input.CorrosionAllowanceIn;
        var tb2 = Math.Max(input.ShellOrHeadExternalRequiredThicknessIn, input.Ug16MinimumThicknessIn) + input.CorrosionAllowanceIn;
        var tb3 = input.Ug45TableMinimumThicknessIn + input.CorrosionAllowanceIn;

        var tb = Math.Max(Math.Min(tb1, tb3), Math.Min(tb2, tb3));
        var governing = Math.Max(ta, tb);
        var requiredWithCa = governing + input.CorrosionAllowanceIn;
        var margin = input.ProvidedThicknessIn - governing;

        return new NozzleThicknessResult(
            ta, tb1, tb2, tb3, tb, governing, requiredWithCa, margin,
            input.ProvidedThicknessIn >= requiredWithCa);
    }
}
