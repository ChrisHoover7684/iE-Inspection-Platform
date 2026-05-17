import { API510_EXTERNAL_COMPONENT_DEFINITIONS, API510_EXCHANGER_DESIGN_CONDITION_FIELDS, API510_VESSEL_TOWER_DESIGN_CONDITION_FIELDS } from './inspectionComponentCatalog';
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

const API510_EXTERNAL_SECTION_GROUPS = [
  'Report Header','Inspection Context','Scope / Preparation','External Condition','Component Condition','Coating / Insulation','Leakage / Staining','Supports / Attachments','CML / Thickness Review','Findings','Recommendations','Photos','Return to Service'
] as const;

const API510_CONDITION_OPTIONS = ['Acceptable', 'Monitor', 'Issue'];
const API510_STATUS_OPTIONS = ['Yes', 'No', 'N/A'];

const buildApi510ExternalFields = (equipmentFamily: string, equipmentSubtype: string, baseTag: string, componentMap: Array<{ key: string; label: string }>) => {
  const baseMeta = { standard: 'API 510', inspectionScope: 'External', equipmentFamily, equipmentSubtype };
  const headerAndContext = API510_EXTERNAL_SECTION_GROUPS.map((group, index) => mkField({
    fieldTag: `${baseTag}.section.${group.toLowerCase().replace(/\s*\/\s*/g, '-').replace(/\s+/g, '-')}.status`,
    label: `${group} Status`,
    ...baseMeta,
    sectionGroup: group,
    componentType: 'General',
    dataType: 'select',
    options: API510_STATUS_OPTIONS,
    required: false,
    supportsFinding: group === 'Findings',
    supportsRecommendation: group === 'Recommendations',
    supportsRepairRequired: group === 'Return to Service',
    supportsPhotoTag: group === 'Photos',
    supportsSummary: group === 'Findings',
    supportsNdeRequest: group === 'CML / Thickness Review',
    defaultLayoutOrder: (index + 1) * 10
  }));

  const componentConditionFields = componentMap.map((component, index) => mkField({
    fieldTag: `${baseTag}.${component.key}.condition`,
    label: `${component.label} Condition`,
    ...baseMeta,
    sectionGroup: 'Component Condition',
    componentType: component.label,
    dataType: 'select',
    options: API510_CONDITION_OPTIONS,
    required: false,
    supportsFinding: true,
    supportsRecommendation: false,
    supportsRepairRequired: false,
    supportsPhotoTag: false,
    supportsSummary: true,
    supportsNdeRequest: false,
    defaultLayoutOrder: 200 + ((index + 1) * 10)
  }));

  const designConditionFieldTags = baseTag === 'api510.external.exchanger'
    ? API510_EXCHANGER_DESIGN_CONDITION_FIELDS
    : (baseTag === 'api510.external.drum-vessel' || baseTag === 'api510.external.tower-column')
      ? API510_VESSEL_TOWER_DESIGN_CONDITION_FIELDS.filter((tag) => tag.startsWith(baseTag))
      : [];

  const exchangerDesignFields = designConditionFieldTags.map((tag, index) => mkField({
      fieldTag: tag,
      label: `${tag.split('.').slice(-2).join(' ').replace('-', ' ').replace(/\b\w/g, (m) => m.toUpperCase())}`,
      ...baseMeta,
      sectionGroup: 'Inspection Context',
      componentType: 'Design Conditions',
      dataType: 'number',
      options: [],
      required: false,
      supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false,
      defaultLayoutOrder: 150 + index
    }));

  const nozzleDetailFields = baseTag === 'api510.external.exchanger'
    ? [
      'nozzle-id-tag','nozzle-service','parent-component','nozzle-location','pressure-boundary-side','nozzle-nps-diameter','ug45-calculation-required','ug45-parent-thickness-source','design-pressure-source','design-temperature-source'
    ].map((k, index) => mkField({
      fieldTag: `api510.external.exchanger.shell-tube.nozzles.${k}`,
      label: `Shell and Tube Nozzle ${k.replace(/-/g, ' ').replace(/\b\w/g, (m) => m.toUpperCase())}`,
      ...baseMeta, sectionGroup: 'Component Condition', componentType: 'Nozzles',
      dataType: ['ug45-calculation-required'].includes(k) ? 'boolean' : (['pressure-boundary-side','parent-component'].includes(k) ? 'select' : 'text'),
      options: k === 'pressure-boundary-side' ? ['shell-side','tube-side','channel-side','shared','unknown'] : (k === 'parent-component' ? ['shell','channel-channel-head','shell-cover','channel-cover','bonnet-head','tubesheet-area','other-component'] : []),
      required: ['parent-component','pressure-boundary-side'].includes(k),
      supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false,
      defaultLayoutOrder: 450 + index
    }))
    : [];

  return [...headerAndContext, ...exchangerDesignFields, ...componentConditionFields, ...nozzleDetailFields];
};

export const API570_EXTERNAL_COMPONENT_PRESETS = [
  'Valve','Control Valve / Control Loop','Flange Pair','Bolting / Gasket','Support','Spring Can / Hanger','Guide / Anchor','Shoe / Saddle','Bellows / Expansion Joint','Small Bore Connection','Branch Connection','Deadleg','Injection Point / Mix Point','Insulation / Jacketing Area','CUI Area','Other Component'
];

export const API510_EXTERNAL_EXCHANGER_COMPONENT_PRESETS = ['Shell and Tube Exchanger', 'Plate and Frame Exchanger', 'Double Pipe Exchanger', 'Air Cooler / Fin Fan'];
export const API510_EXTERNAL_DRUM_VESSEL_COMPONENT_PRESETS = ['Horizontal Drum', 'Vertical Drum', 'Separator / KO Drum', 'Accumulator / Receiver', 'Generic Pressure Vessel'];
export const API510_EXTERNAL_TOWER_COLUMN_COMPONENT_PRESETS = ['Distillation Tower', 'Absorber / Stripper', 'Packed Column', 'Tray Column'];

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
  },
  {
    id: 'api-510-external-exchanger', name: 'API 510 External Exchanger', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Exchanger', componentPresets: API510_EXTERNAL_EXCHANGER_COMPONENT_PRESETS,
    fields: buildApi510ExternalFields('Pressure Equipment', 'Exchanger', 'api510.external.exchanger', API510_EXTERNAL_COMPONENT_DEFINITIONS.filter((d) => d.fieldTagPrefix.startsWith('api510.external.exchanger.')).map((d) => ({ key: d.fieldTagPrefix.replace('api510.external.exchanger.', ''), label: d.label })))
  },
  {
    id: 'api-510-external-drum-vessel', name: 'API 510 External Drums / Pressure Vessels', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Drum / Vessel', componentPresets: API510_EXTERNAL_DRUM_VESSEL_COMPONENT_PRESETS,
    fields: buildApi510ExternalFields('Pressure Equipment', 'Drum / Vessel', 'api510.external.drum-vessel', API510_EXTERNAL_COMPONENT_DEFINITIONS.filter((d) => d.fieldTagPrefix.startsWith('api510.external.drum-vessel.')).map((d) => ({ key: d.fieldTagPrefix.replace('api510.external.drum-vessel.', ''), label: d.label })))
  },
  {
    id: 'api-510-external-tower-column', name: 'API 510 External Towers / Columns', standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Tower / Column', componentPresets: API510_EXTERNAL_TOWER_COLUMN_COMPONENT_PRESETS,
    fields: buildApi510ExternalFields('Pressure Equipment', 'Tower / Column', 'api510.external.tower-column', API510_EXTERNAL_COMPONENT_DEFINITIONS.filter((d) => d.fieldTagPrefix.startsWith('api510.external.tower-column.')).map((d) => ({ key: d.fieldTagPrefix.replace('api510.external.tower-column.', ''), label: d.label })))
  }
];

export const futureOnlyApi510InternalFields: InspectionFieldDefinition[] = [
  mkField({ fieldTag: 'api510.internal.future.shell.condition', label: 'Internal Shell Condition', standard: 'API 510', inspectionScope: 'Internal', equipmentFamily: 'Pressure Equipment', equipmentSubtype: 'Vessel', sectionGroup: 'Internal Inspection', componentType: 'Internal Shell', dataType: 'textarea', options: [], required: false, supportsFinding: false, supportsRecommendation: false, supportsRepairRequired: false, supportsPhotoTag: false, supportsSummary: false, supportsNdeRequest: false, defaultLayoutOrder: 1, futureOnly: true, reviewNotes: 'Future-only internal inspection field. Excluded from MVP export.' })
];

export const getMvpExternalInspectionFields = () => externalInspectionFieldSets.flatMap((s) => s.fields);
