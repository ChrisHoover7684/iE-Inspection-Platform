namespace iE.Core.Reports.Templates;

public static class InspectionFieldCatalog
{
    public static IReadOnlyList<InspectionFieldSet> ExternalMvpFieldSets { get; } =
    [
        new()
        {
            Id = "api-570-external-piping-fields", Name = "API 570 External Piping", Standard = "API 570", InspectionScope = "External", EquipmentFamily = "Piping", EquipmentSubtype = "General",
            Fields = [
                Field("component-type","Component Type","API 570","External","Piping","General","Component Section","Component","select",false,5,options:["Valve","Other Component"]),
                Field("finding-notes","Finding Notes","API 570","External","Piping","General","Component Section","Component","textarea",false,6,supportsFinding:true,supportsSummary:true),
                Field("recommendation-text","Recommendation Text","API 570","External","Piping","General","Component Section","Component","textarea",false,7,supportsRecommendation:true),
                Field("photo-tag","Picture / Photo Tag","API 570","External","Piping","General","Component Section","Component","text",false,8,supportsPhotoTag:true),
                Field("create-finding","Create Finding","API 570","External","Piping","General","Component Section","Component","boolean",false,9,supportsFinding:true),
                Field("repair-required","Repair Required","API 570","External","Piping","General","Repairs","Component","boolean",false,10,supportsRepairRequired:true),
                Field("nde-required","NDE Required","API 570","External","Piping","General","NDE / Testing","Component","boolean",false,11,supportsNdeRequest:true),
                Field("add-to-summary","Add to Summary","API 570","External","Piping","General","Findings","Component","boolean",false,12,supportsSummary:true),
            ]
        },
        new(){ Id="api-510-external-exchangers-fields",Name="API 510 External Exchangers",Standard="API 510",EquipmentFamily="Pressure Equipment",EquipmentSubtype="Exchanger",InspectionScope="External",ComponentPresets=["Shell","Shell Cover","Channel Head","Channel Cover","Bonnet Head","Nozzles","Tubesheet Area","Flanges / Gaskets / Bolting","Saddles / Supports","Expansion Joint","Coating / Insulation Area","Platform / Ladder / Access","Nameplate / Markings","CML Locations","Other Component"],Fields=[Field("external-condition","External Condition","API 510","External","Pressure Equipment","Exchanger","External Condition","Equipment","select",true,1)]},
        new(){ Id="api-510-external-drum-vessel-fields",Name="API 510 External Drums / Pressure Vessels",Standard="API 510",EquipmentFamily="Pressure Equipment",EquipmentSubtype="Drum / Vessel",InspectionScope="External",ComponentPresets=["Shell","Heads","Nozzles","Manways","Flanges / Gaskets / Bolting","Saddle / Skirt / Legs","Supports / Foundation","Platforms / Ladders / Handrails","Coating","Insulation","Nameplate / Markings","CML Locations","Other Component"],Fields=[Field("vessel-condition","Vessel Condition","API 510","External","Pressure Equipment","Drum / Vessel","External Condition","Equipment","select",true,1)]},
        new(){ Id="api-510-external-tower-column-fields",Name="API 510 External Towers / Columns",Standard="API 510",EquipmentFamily="Pressure Equipment",EquipmentSubtype="Tower / Column",InspectionScope="External",ComponentPresets=["Shell Courses","Heads","Nozzles","Manways","Skirt","Base Ring / Anchor Bolts","Platforms / Ladders / Handrails","Insulation / Jacketing","Coating","Nameplate / Markings","CML Locations","Other Component"],Fields=[Field("tower-condition","Tower Condition","API 510","External","Pressure Equipment","Tower / Column","External Condition","Equipment","select",true,1)]},
        new(){ Id="api-653-sti-external-tanks-fields",Name="API 653 / STI External Tanks",Standard="API 653 / STI",EquipmentFamily="Storage Tank",EquipmentSubtype="AST",InspectionScope="External",Fields=[Field("tank-condition","Tank Condition","API 653 / STI","External","Storage Tank","AST","External Inspection","Equipment","textarea",false,1)]}
    ];

    public static IReadOnlyList<InspectionFieldDefinition> FutureOnlyApi510InternalFields { get; } =
    [
        Field("future-internal-shell","Internal Shell Condition","API 510","Internal","Pressure Equipment","Vessel","Internal Inspection","Shell","textarea",false,1,isFutureOnly:true,reviewNotes:"Future-only. Do not include in MVP export.")
    ];

    private static InspectionFieldDefinition Field(string tag,string label,string standard,string scope,string family,string subtype,string group,string component,string type,bool required,int order,IReadOnlyList<string>? options=null,bool supportsFinding=false,bool supportsRecommendation=false,bool supportsRepairRequired=false,bool supportsPhotoTag=false,bool supportsSummary=false,bool supportsNdeRequest=false,bool isFutureOnly=false,string? reviewNotes=null)
        => new(){FieldTag=tag,Label=label,Standard=standard,InspectionScope=scope,EquipmentFamily=family,EquipmentSubtype=subtype,SectionGroup=group,ComponentType=component,DataType=type,Required=required,DefaultLayoutOrder=order,Options=options??[],SupportsFinding=supportsFinding,SupportsRecommendation=supportsRecommendation,SupportsRepairRequired=supportsRepairRequired,SupportsPhotoTag=supportsPhotoTag,SupportsSummary=supportsSummary,SupportsNdeRequest=supportsNdeRequest,WordExportGroup=$"{standard} | {scope} | {family} | {subtype}",ReviewNotes=reviewNotes,IsFutureOnly=isFutureOnly};
}
