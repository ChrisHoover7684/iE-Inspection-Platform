using iE.Core.Reports.Templates;

namespace iE.Tests.Reports;

public class InspectionComponentCatalogTests
{
    [Fact]
    public void Api510ExternalComponentDefinitions_ExposeNozzleParentSideTminUg45Metadata()
    {
        var nozzle = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "nozzles");

        Assert.True(nozzle.ParentComponentRequired);
        Assert.Contains("shell", nozzle.AllowedParentComponentKeys);
        Assert.Equal("shared", nozzle.PressureBoundarySide);
        Assert.Equal("api510.external.exchanger.shell-tube.shell-side.design-pressure", nozzle.DesignPressureFieldTag);
        Assert.Equal("api510.external.exchanger.shell-tube.shell-side.design-temperature", nozzle.DesignTemperatureFieldTag);
        Assert.True(nozzle.SupportsTminCalculation);
        Assert.NotNull(nozzle.TminCalculationMethod);
        Assert.True(nozzle.SupportsNozzleUg45);
    }
}
