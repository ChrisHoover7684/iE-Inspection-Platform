using iE.Core.Reports.Templates;

namespace iE.Tests.Reports;

public class InspectionComponentCatalogTests
{
    [Fact]
    public void Api510ExternalComponentDefinitions_ExposeShellTubeNozzleSideSpecificMappings()
    {
        var nozzle = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "nozzles");

        Assert.True(nozzle.ParentComponentRequired);
        Assert.Contains("shell", nozzle.AllowedParentComponentKeys);
        Assert.Contains("channel-channel-head", nozzle.NozzleLocationOptions);
        Assert.DoesNotContain("channel-head", nozzle.NozzleLocationOptions);
        Assert.Equal("api510.external.exchanger.shell-tube.tube-side.design-pressure", nozzle.DesignPressureFieldTagsByPressureBoundarySide["tube-side"]);
        Assert.Equal("api510.external.exchanger.shell-tube.channel-side.design-temperature", nozzle.DesignTemperatureFieldTagsByPressureBoundarySide["channel-side"]);
        Assert.True(nozzle.SupportsNozzleUg45);
    }

    [Theory]
    [InlineData("Horizontal Drum", new[] { "shell", "heads" })]
    [InlineData("Distillation Tower", new[] { "shell-courses", "heads" })]
    [InlineData("Air Cooler / Fin Fan", new[] { "header-box" })]
    [InlineData("Double Pipe Exchanger", new[] { "inner-pipe-external", "outer-pipe", "return-bends" })]
    public void Api510ExternalComponentDefinitions_ExposeNozzleParentOptionsBySubtype(string subtype, string[] expectedParents)
    {
        var nozzle = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == subtype && d.ComponentKey == "nozzles");

        Assert.True(nozzle.ParentComponentRequired);
        Assert.Equal(expectedParents, nozzle.AllowedParentComponentKeys);
    }
}
