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

export const API570_EXTERNAL_COMPONENT_PRESETS = [
  'Valve','Control Valve / Control Loop','Flange Pair','Bolting / Gasket','Support','Spring Can / Hanger','Guide / Anchor','Shoe / Saddle','Bellows / Expansion Joint','Small Bore Connection','Branch Connection','Deadleg','Injection Point / Mix Point','Insulation / Jacketing Area','CUI Area','Other Component'
];

// Frontend catalog is the temporary source of truth for #276 external catalog review export.
export const externalInspectionFieldSets: InspectionFieldSet[] = [
  {
    id: 'api-570-external-piping', name: 'API 570 External Piping', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', componentPresets: API570_EXTERNAL_COMPONENT_PRESETS,
    fields: [
      mkField({ fieldTag: 'api570.external.piping.component.type', label: 'Component Type', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Component Section', componentType: 'Component', dataType: 'select', options: API570_EXTERNAL_COMPONENT_PRESETS, required: true, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 10 }),
      mkField({ fieldTag: 'api570.external.piping.component.tag-name', label: 'Component Tag / Name', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Component Section', componentType: 'Component', dataType: 'text', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 20 }),
      mkField({ fieldTag: 'api570.external.piping.component.location', label: 'Location', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Component Section', componentType: 'Component', dataType: 'text', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 30 }),
      mkField({ fieldTag: 'api570.external.piping.component.condition', label: 'Condition', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Component Section', componentType: 'Component', dataType: 'select', options: ['Acceptable', 'Monitor', 'Issue'], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 40 }),
      mkField({ fieldTag: 'api570.external.piping.component.finding-notes', label: 'Finding Notes', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Findings', componentType: 'Component', dataType: 'textarea', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 50 }),
      mkField({ fieldTag: 'api570.external.piping.component.recommendation-text', label: 'Recommendation Text', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Recommendations', componentType: 'Component', dataType: 'textarea', options: [], required: false, supportsFinding: false, supportsRecommendation: true, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 60 }),
      mkField({ fieldTag: 'api570.external.piping.component.photo-tag', label: 'Photo Reference / Picture Tag', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Photos', componentType: 'Component', dataType: 'text', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: true, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 70 }),
      mkField({ fieldTag: 'api570.external.piping.component.create-finding', label: 'Create Finding', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Actions', componentType: 'Component', dataType: 'boolean', options: [], required: false, supportsFinding: true, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 80 }),
      mkField({ fieldTag: 'api570.external.piping.component.recommendation-required', label: 'Recommendation Required', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Actions', componentType: 'Component', dataType: 'boolean', options: [], required: false, supportsFinding: false, supportsRecommendation: true, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 90 }),
      mkField({ fieldTag: 'api570.external.piping.component.repair-required', label: 'Repair Required', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Actions', componentType: 'Component', dataType: 'boolean', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: true, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 100 }),
      mkField({ fieldTag: 'api570.external.piping.component.photo-required', label: 'Photo Required', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Actions', componentType: 'Component', dataType: 'boolean', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: true, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 110 }),
      mkField({ fieldTag: 'api570.external.piping.component.nde-required', label: 'NDE Required', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Actions', componentType: 'Component', dataType: 'boolean', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: true, defaultLayoutOrder: 120 }),
      mkField({ fieldTag: 'api570.external.piping.component.add-to-summary', label: 'Add to Summary', standard: 'API 570', inspectionScope: 'External', equipmentFamily: 'Piping', equipmentSubtype: 'General', sectionGroup: 'Actions', componentType: 'Component', dataType: 'boolean', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: true, supportsNdeRequest: false, defaultLayoutOrder: 130 })
    ]
  }
];

export const futureOnlyApi510InternalFields: InspectionFieldDefinition[] = [
  mkField({ fieldTag: 'api510.internal.future.shell.condition', label: 'Internal Shell Condition', standard: 'API 510', inspectionScope: 'Internal', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Vessel', sectionGroup: 'Internal Inspection', componentType: 'Internal Shell', dataType: 'textarea', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 1, futureOnly: true, reviewNotes: 'Future-only internal inspection field. Excluded from MVP export.' })
];

export const getMvpExternalInspectionFields = () => externalInspectionFieldSets.flatMap((s) => s.fields);
