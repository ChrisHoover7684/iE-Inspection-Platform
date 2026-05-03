using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;
using iE.Core.Mechanical.PressureVessels;

namespace iE.Tests.Mechanical;

public class MaterialStressIntegrationTests
{
    private static MaterialStressService BuildService()
    {
        var lib = new MaterialStressLibrary();
        var record = new MaterialStressRecord
        {
            Dataset = new StressDataset { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", DesignMargin = 1 },
            Material = new MaterialIdentity { SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate" },
            WeldEfficiency = 1.0,
            AllowableStressByTemperature = new SortedDictionary<double, double?> { [300] = 20, [350] = 19.5, [400] = 19 }
        };
        lib.AddRecord(record);
        return new MaterialStressService(lib);
    }

    [Fact]
    public void ExactKsi20_ReturnsPsi20000()
    {
        var s = BuildService();
        var r = s.GetAllowableStress(new StressLookupRequest { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate", Temperature = 300 });
        Assert.True(r.Found);
        Assert.Equal(20000, r.AllowableStressPsi);
    }

    [Fact]
    public void Interpolation_ReturnsPsi19500()
    {
        var s = BuildService();
        var r = s.GetAllowableStress(new StressLookupRequest { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate", Temperature = 350 });
        Assert.True(r.Found);
        Assert.Equal(19500, r.AllowableStressPsi);
    }

    [Fact]
    public void PressureVesselFormula_UsesPsiNotKsiOrDoubleConverted()
    {
        var t = ShellThicknessService.CylindricalCircumferential(150, 24, 20000, 1.0);
        Assert.InRange(t, 0.17, 0.19);
    }

    [Fact]
    public void OutOfRange_FailsWithoutExtrapolation()
    {
        var s = BuildService();
        var r = s.GetAllowableStress(new StressLookupRequest { Era = StressEra.From1999Onward, DesignCode = "ASME_VIII_DIV1", CalculationFamily = "PressureVessel", SpecNo = "SA-TEST", TypeGrade = "A", ProductForm = "Plate", Temperature = 500, AllowExtrapolation = false });
        Assert.False(r.Found);
    }

    [Fact]
    public void Resolver_UsesManualOverride()
    {
        var resolver = new PressureVesselAllowableStressResolver(BuildService());
        var r = resolver.Resolve(new PressureVesselMaterialStressInput("ASME_VIII_DIV1", "From1999Onward", 300, "SA-TEST", "A", "Plate", "", "", true, 12345));
        Assert.True(r.IsValid);
        Assert.Equal(12345, r.AllowableStressPsi);
    }
}
