using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;
using iE.Core.Mechanical.PressureVessels;

namespace iE.Tests.Mechanical;

public class MaterialStressIntegrationTests
{
    private static MaterialStressService BuildService()
    {
        var lib = new MaterialStressLibrary();
        lib.AddRecord(new MaterialStressRecord
        {
            Dataset = new StressDataset { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", DesignMargin = 1 },
            Material = new MaterialIdentity { SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate" },
            WeldEfficiency = 1.0,
            AllowableStressByTemperature = new SortedDictionary<double, double?> { [300] = 20, [400] = 19 }
        });
        return new MaterialStressService(lib);
    }

    private static StressLookupResult Lookup(MaterialStressService s, double t) => s.GetAllowableStress(new StressLookupRequest { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate", Temperature = t });

    [Fact] public void ExactTemperature_Returns20000Psi_AndNotInterpolated() { var r = Lookup(BuildService(), 300); Assert.Equal(20000, r.AllowableStressPsi); Assert.False(r.WasInterpolated); }
    [Fact] public void T325_InterpolatesTo19750Psi() { var r = Lookup(BuildService(), 325); Assert.Equal(19750, r.AllowableStressPsi); Assert.True(r.WasInterpolated); }
    [Fact] public void T350_InterpolatesTo19500Psi() { var r = Lookup(BuildService(), 350); Assert.Equal(19500, r.AllowableStressPsi); Assert.True(r.WasInterpolated); }
    [Fact] public void T375_InterpolatesTo19250Psi() { var r = Lookup(BuildService(), 375); Assert.Equal(19250, r.AllowableStressPsi); Assert.True(r.WasInterpolated); }
    [Fact] public void PressureVesselFormula_UsesPsiNotDoubleConverted() { var t = ShellThicknessService.CylindricalCircumferential(150, 24, 20000, 1.0); Assert.InRange(t, 0.17, 0.19); }
    [Fact] public void OutOfRange_FailsWithoutExtrapolation() { var r = BuildService().GetAllowableStress(new StressLookupRequest { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate", Temperature = 500, AllowExtrapolation = false }); Assert.False(r.Found); }
    [Fact] public void Resolver_UsesManualOverride() { var r = new PressureVesselAllowableStressResolver(BuildService()).Resolve(new PressureVesselMaterialStressInput("ASME_VIII_DIV1", "From1999Onward", 300, "SA-TEST", "A", "Plate", "", "", true, 12345)); Assert.True(r.IsValid); Assert.Equal(12345, r.AllowableStressPsi); }
}
