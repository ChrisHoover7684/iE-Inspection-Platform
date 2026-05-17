namespace iE.Core.Reports.Templates;

public static class InspectionComponentCatalog
{
    public static IReadOnlyList<InspectionComponentDefinition> Api510ExternalComponentDefinitions { get; } =
    [
        new()
        {
            ComponentKey = "shell",
            Label = "Shell",
            Standard = "API 510",
            InspectionScope = "External",
            EquipmentFamily = "Pressure Equipment",
            EquipmentSubtype = "Shell and Tube Exchanger",
            RequirementLevel = "minimum",
            DefaultSelected = true,
            FieldTagPrefix = "api510.external.exchanger.shell-tube.shell",
            PressureBoundarySide = "shell-side",
            SupportsFinding = true,
            SupportsRecommendation = true,
            SupportsRepairRequired = true,
            SupportsPhotoTag = true,
            SupportsSummary = true,
            SupportsNdeRequest = true
        },
        new()
        {
            ComponentKey = "nozzles",
            Label = "Nozzles",
            Standard = "API 510",
            InspectionScope = "External",
            EquipmentFamily = "Pressure Equipment",
            EquipmentSubtype = "Shell and Tube Exchanger",
            RequirementLevel = "minimum",
            DefaultSelected = true,
            FieldTagPrefix = "api510.external.exchanger.shell-tube.nozzles",
            ParentComponentRequired = true,
            AllowedParentComponentKeys = ["shell", "channel-channel-head", "shell-cover", "channel-cover", "bonnet-head", "tubesheet-area", "other-component"],
            PressureBoundarySide = "shared",
            DesignPressureFieldTag = "api510.external.exchanger.shell-tube.shell-side.design-pressure",
            DesignTemperatureFieldTag = "api510.external.exchanger.shell-tube.shell-side.design-temperature",
            SupportsTminCalculation = true,
            TminCalculationMethod = "UG-27/UG-32 with selected parent thickness",
            SupportsNozzleUg45 = true,
            SupportsFinding = true,
            SupportsRecommendation = true,
            SupportsRepairRequired = true,
            SupportsPhotoTag = true,
            SupportsSummary = true,
            SupportsNdeRequest = true
        }
    ];
}
