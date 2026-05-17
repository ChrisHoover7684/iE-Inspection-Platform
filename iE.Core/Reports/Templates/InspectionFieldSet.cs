namespace iE.Core.Reports.Templates;

public class InspectionFieldSet
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Standard { get; init; } = string.Empty;
    public string InspectionScope { get; init; } = "External";
    public string EquipmentFamily { get; init; } = string.Empty;
    public string EquipmentSubtype { get; init; } = string.Empty;
    public IReadOnlyList<string> ComponentPresets { get; init; } = [];
    public IReadOnlyList<InspectionFieldDefinition> Fields { get; init; } = [];
}
