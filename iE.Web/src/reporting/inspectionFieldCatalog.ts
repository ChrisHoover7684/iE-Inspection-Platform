export type InspectionFieldDefinition = {
  fieldTag: string;
  label: string;
  standard: string;
  inspectionScope: string;
  equipmentFamily: string;
  equipmentSubtype: string;
  sectionGroup: string;
  componentType: string;
  dataType: string;
  options: string[];
  required: boolean;
  defaultValue?: string | null;
  helpText?: string | null;
  supportsFinding: boolean;
  supportsRecommendation: boolean;
  supportsRepairRequired: boolean;
  supportsPhotoTag: boolean;
  supportsSummary: boolean;
  supportsNdeRequest: boolean;
  wordExportGroup: string;
  defaultLayoutOrder: number;
  reviewNotes?: string | null;
  futureOnly?: boolean;
};

export type InspectionFieldSet = {
  id: string;
  name: string;
  standard: string;
  inspectionScope: string;
  equipmentFamily: string;
  equipmentSubtype: string;
  componentPresets: string[];
  fields: InspectionFieldDefinition[];
};

const mkField = (input: Omit<InspectionFieldDefinition, 'wordExportGroup'>): InspectionFieldDefinition => ({
  ...input,
  wordExportGroup: `${input.standard} | ${input.inspectionScope} | ${input.equipmentFamily} | ${input.equipmentSubtype}`
});

const api510CoreFields = (subtype: string, prefix: string): InspectionFieldDefinition[] => [
  mkField({ fieldTag: `${prefix}.external-condition.summary`, label: 'External Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'External Condition', componentType: 'Equipment', dataType: 'textarea', options: [], required: true, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 10 }),
  mkField({ fieldTag: `${prefix}.leakage-staining.summary`, label: 'Leakage / Staining', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Leakage / Staining', componentType: 'Equipment', dataType: 'textarea', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: true, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 20 }),
  mkField({ fieldTag: `${prefix}.coating.condition`, label: 'Coating Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Coating / Insulation', componentType: 'Equipment', dataType: 'select', options: ['Acceptable', 'Monitor', 'Finding'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 30 }),
  mkField({ fieldTag: `${prefix}.insulation.condition`, label: 'Insulation Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Coating / Insulation', componentType: 'Equipment', dataType: 'select', options: ['Acceptable', 'Monitor', 'Finding', 'Not Applicable'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 40 }),
  mkField({ fieldTag: `${prefix}.supports-foundation.condition`, label: 'Supports / Foundation Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Supports / Attachments', componentType: 'Equipment', dataType: 'textarea', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 50 }),
  mkField({ fieldTag: `${prefix}.cml-thickness.review-summary`, label: 'CML / Thickness Review Summary', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'CML / Thickness Review', componentType: 'Equipment', dataType: 'textarea', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: true, defaultLayoutOrder: 60 }),
  mkField({ fieldTag: `${prefix}.component.condition`, label: 'Component Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Component Condition', componentType: 'Component', dataType: 'select', options: ['Acceptable', 'Monitor', 'Issue'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 70 }),
  mkField({ fieldTag: `${prefix}.finding.notes`, label: 'Finding Notes', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Findings', componentType: 'Component', dataType: 'textarea', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 80 }),
  mkField({ fieldTag: `${prefix}.recommendation.text`, label: 'Recommendation Text', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Recommendations', componentType: 'Component', dataType: 'textarea', options: [], required: false, supportsFinding: false, supportsRecommendation: true, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 90 }),
  mkField({ fieldTag: `${prefix}.photo.reference-tag`, label: 'Photo Reference / Picture Tag', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Photos', componentType: 'Component', dataType: 'text', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: true, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 100 }),
  mkField({ fieldTag: `${prefix}.external.summary`, label: 'External Summary', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype, sectionGroup: 'Return to Service', componentType: 'Equipment', dataType: 'textarea', options: [], required: true, supportsFinding: false, supportsRecommendation: true, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 110 })
];

export const externalInspectionFieldSets: InspectionFieldSet[] = [
  {
    id: 'api-570-external-piping', name: 'API 570 External Piping', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', componentPresets: [],
    fields: [
      mkField({ fieldTag: 'api570.external.piping.component.type', label: 'Component Type', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Component Section', componentType: 'Component', dataType: 'select', options: ['Valve', 'Other Component'], required: true, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 10 }),
      mkField({ fieldTag: 'api570.external.piping.component.finding-notes', label: 'Finding Notes', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Findings', componentType: 'Component', dataType: 'textarea', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 20 }),
      mkField({ fieldTag: 'api570.external.piping.component.recommendation-text', label: 'Recommendation Text', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Recommendations', componentType: 'Component', dataType: 'textarea', options: [], required: false, supportsFinding: false, supportsRecommendation: true, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 30 }),
      mkField({ fieldTag: 'api570.external.piping.component.photo-tag', label: 'Photo Reference / Picture Tag', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Photos', componentType: 'Component', dataType: 'text', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: true, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 40 })
    ]
  },
  { id: 'api-510-external-exchangers', name: 'API 510 External Exchangers', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Exchanger', componentPresets: ['Shell','Shell Cover','Channel Head','Channel Cover','Bonnet Head','Nozzles','Tubesheet Area','Flanges / Gaskets / Bolting','Saddles / Supports','Expansion Joint','Coating / Insulation Area','Platform / Ladder / Access','Nameplate / Markings','CML Locations','Other Component'], fields: [
    ...api510CoreFields('Exchanger','api510.external.exchanger'),
    mkField({ fieldTag: 'api510.external.exchanger.shell-tube.shell.condition', label: 'Shell Condition (Shell & Tube)', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Exchanger', sectionGroup: 'Component Condition', componentType: 'Shell', dataType: 'select', options: ['Acceptable','Monitor','Issue'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 120 }),
    mkField({ fieldTag: 'api510.external.exchanger.shell-tube.channel-head.condition', label: 'Channel Head Condition (Shell & Tube)', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Exchanger', sectionGroup: 'Component Condition', componentType: 'Channel Head', dataType: 'select', options: ['Acceptable','Monitor','Issue'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 130 })
  ] },
  { id: 'api-510-external-drum-vessel', name: 'API 510 External Drums / Pressure Vessels', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Drum / Vessel', componentPresets: ['Shell','Heads','Nozzles','Manways','Flanges / Gaskets / Bolting','Saddle / Skirt / Legs','Supports / Foundation','Platforms / Ladders / Handrails','Coating','Insulation','Nameplate / Markings','CML Locations','Other Component'], fields: [...api510CoreFields('Drum / Vessel','api510.external.drum-vessel'), mkField({ fieldTag: 'api510.external.drum-vessel.shell.condition', label: 'Shell Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Drum / Vessel', sectionGroup: 'Component Condition', componentType: 'Shell', dataType: 'select', options: ['Acceptable','Monitor','Issue'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 120 })] },
  { id: 'api-510-external-tower-column', name: 'API 510 External Towers / Columns', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Tower / Column', componentPresets: ['Shell Courses','Heads','Nozzles','Manways','Skirt','Base Ring / Anchor Bolts','Platforms / Ladders / Handrails','Insulation / Jacketing','Coating','Nameplate / Markings','CML Locations','Other Component'], fields: [...api510CoreFields('Tower / Column','api510.external.tower-column'), mkField({ fieldTag: 'api510.external.tower-column.shell-courses.condition', label: 'Shell Courses Condition', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Tower / Column', sectionGroup: 'Component Condition', componentType: 'Shell Courses', dataType: 'select', options: ['Acceptable','Monitor','Issue'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 120 })] }
];

export const futureOnlyApi510InternalFields: InspectionFieldDefinition[] = [
  mkField({ fieldTag: 'api510.internal.future.shell.condition', label: 'Internal Shell Condition', standard: 'API 510', inspectionScope: 'Internal', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Vessel', sectionGroup: 'Internal Inspection', componentType: 'Internal Shell', dataType: 'textarea', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 1, futureOnly: true, reviewNotes: 'Future-only internal inspection field. Excluded from MVP export.' })
];

export const getMvpExternalInspectionFields = () => externalInspectionFieldSets.flatMap((s) => s.fields);
