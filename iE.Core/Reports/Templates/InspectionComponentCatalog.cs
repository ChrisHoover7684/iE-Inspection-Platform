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
            DesignPressureFieldTag = "api510.external.exchanger.shell-tube.shell-side.design-pressure",
            DesignTemperatureFieldTag = "api510.external.exchanger.shell-tube.shell-side.design-temperature",
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
            DesignPressureFieldTagsByPressureBoundarySide = new Dictionary<string, string>
            {
                ["shell-side"] = "api510.external.exchanger.shell-tube.shell-side.design-pressure",
                ["tube-side"] = "api510.external.exchanger.shell-tube.tube-side.design-pressure",
                ["channel-side"] = "api510.external.exchanger.shell-tube.channel-side.design-pressure"
            },
            DesignTemperatureFieldTagsByPressureBoundarySide = new Dictionary<string, string>
            {
                ["shell-side"] = "api510.external.exchanger.shell-tube.shell-side.design-temperature",
                ["tube-side"] = "api510.external.exchanger.shell-tube.tube-side.design-temperature",
                ["channel-side"] = "api510.external.exchanger.shell-tube.channel-side.design-temperature"
            },
            DesignConditionSourceOptions = ["shell-side", "tube-side", "channel-side"],
            ParentThicknessSourceOptions = ["selected-parent", "manual-entry"],
            NozzleLocationRequired = true,
            NozzleLocationOptions = ["shell", "channel-channel-head", "bonnet-head", "tubesheet-area"],
            SupportsTminCalculation = true,
            TminCalculationMethod = "UG-27/UG-32 with selected parent thickness",
            SupportsNozzleUg45 = true,
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
            EquipmentSubtype = "Horizontal Drum",
            RequirementLevel = "minimum",
            DefaultSelected = true,
            FieldTagPrefix = "api510.external.drum-vessel.horizontal-drum.nozzles",
            ParentComponentRequired = true,
            AllowedParentComponentKeys = ["shell", "heads"],
            NozzleLocationRequired = true,
            NozzleLocationOptions = ["shell", "heads"],
            DesignConditionSourceOptions = ["vessel-side"],
            DesignPressureFieldTag = "api510.external.drum-vessel.design-pressure",
            DesignTemperatureFieldTag = "api510.external.drum-vessel.design-temperature",
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
            EquipmentSubtype = "Distillation Tower",
            RequirementLevel = "minimum",
            DefaultSelected = true,
            FieldTagPrefix = "api510.external.tower-column.distillation.nozzles",
            ParentComponentRequired = true,
            AllowedParentComponentKeys = ["shell-courses", "heads"],
            NozzleLocationRequired = true,
            NozzleLocationOptions = ["shell-courses", "heads"],
            DesignConditionSourceOptions = ["tower-side"],
            DesignPressureFieldTag = "api510.external.tower-column.design-pressure",
            DesignTemperatureFieldTag = "api510.external.tower-column.design-temperature",
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
            EquipmentSubtype = "Air Cooler / Fin Fan",
            RequirementLevel = "minimum",
            DefaultSelected = true,
            FieldTagPrefix = "api510.external.exchanger.air-cooler.nozzles",
            ParentComponentRequired = true,
            AllowedParentComponentKeys = ["header-box"],
            NozzleLocationRequired = true,
            NozzleLocationOptions = ["header-box"],
            DesignConditionSourceOptions = ["tube-side", "header-box"],
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
            EquipmentSubtype = "Double Pipe Exchanger",
            RequirementLevel = "minimum",
            DefaultSelected = true,
            FieldTagPrefix = "api510.external.exchanger.double-pipe.nozzles",
            ParentComponentRequired = true,
            AllowedParentComponentKeys = ["inner-pipe-external", "outer-pipe", "return-bends"],
            NozzleLocationRequired = true,
            NozzleLocationOptions = ["inner-pipe-external", "outer-pipe", "return-bends"],
            DesignConditionSourceOptions = ["inner-pipe-side", "annulus-side"],
            SupportsFinding = true,
            SupportsRecommendation = true,
            SupportsRepairRequired = true,
            SupportsPhotoTag = true,
            SupportsSummary = true,
            SupportsNdeRequest = true
        }
    ];
}
