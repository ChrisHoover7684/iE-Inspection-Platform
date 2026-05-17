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
        Assert.Equal("api510.external.exchanger.shell-tube.tube-side.design-temperature", nozzle.DesignTemperatureFieldTagsByPressureBoundarySide["tube-side"]);
        Assert.DoesNotContain("channel-side", nozzle.DesignConditionSourceOptions);
        Assert.True(nozzle.SupportsNozzleUg45);
        Assert.Contains("tube-side", nozzle.ReviewNotes);
    }

    [Fact]
    public void Api510ExternalComponentDefinitions_ExposeShellTubeTminEligibilityMetadata()
    {
        var shell = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "shell");
        var shellCover = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "shell-cover");
        var channelHead = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "channel-channel-head");
        var channelCover = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "channel-cover");
        var channelHeadDollarPlate = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "channel-head-dollar-plate");
        var bonnetHead = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "bonnet-head");
        var tubesheetArea = InspectionComponentCatalog.Api510ExternalComponentDefinitions
            .Single(d => d.EquipmentSubtype == "Shell and Tube Exchanger" && d.ComponentKey == "tubesheet-area");

        Assert.True(shell.SupportsTminCalculation);
        Assert.Equal("UG-27 cylindrical shell", shell.TminCalculationMethod);
        Assert.Equal("shell-side", shell.PressureBoundarySide);

        Assert.True(shellCover.SupportsTminCalculation);
        Assert.Equal("shell-side", shellCover.PressureBoundarySide);

        Assert.True(channelHead.SupportsTminCalculation);
        Assert.Equal("tube-side", channelHead.PressureBoundarySide);

        Assert.True(channelCover.SupportsTminCalculation);
        Assert.Equal("flat cover / channel cover method placeholder", channelCover.TminCalculationMethod);
        Assert.Equal("tube-side", channelCover.PressureBoundarySide);

        Assert.True(channelHeadDollarPlate.SupportsTminCalculation);
        Assert.Equal("flat cover / channel cover method placeholder", channelHeadDollarPlate.TminCalculationMethod);
        Assert.Equal("tube-side", channelHeadDollarPlate.PressureBoundarySide);

        Assert.True(bonnetHead.SupportsTminCalculation);
        Assert.Equal("UG-32 formed head", bonnetHead.TminCalculationMethod);
        Assert.Equal("tube-side", bonnetHead.PressureBoundarySide);

        Assert.False(tubesheetArea.SupportsTminCalculation);
        Assert.Equal("review only unless calculation method is selected", tubesheetArea.TminCalculationMethod);
        Assert.Equal("shared", tubesheetArea.PressureBoundarySide);
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
