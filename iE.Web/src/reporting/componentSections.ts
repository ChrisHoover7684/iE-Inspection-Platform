import type { InspectionFinding, InspectionReport, InspectionReportAnswer, InspectionReportSection } from '../types';

export const API570_COMPONENT_PRESETS = ['Valve','Control Valve / Control Loop','Flange Pair','Bolting / Gasket','Support','Spring Can / Hanger','Guide / Anchor','Shoe / Saddle','Bellows / Expansion Joint','Small Bore Connection','Branch Connection','Deadleg','Injection Point / Mix Point','Insulation / Jacketing Area','CUI Area','Other Component'] as const;
export const API510_EXTERNAL_COMPONENT_PRESETS = ['Shell','Shell Cover','Channel Head','Bonnet Head','Tube Bundle','Tubesheet','Nozzle','Manway','Saddle / Skirt / Leg','Platform / Ladder / Handrail','Other Component'] as const;
export const API510_INTERNAL_COMPONENT_PRESETS = ['Internal Shell','Internal Head','Baffles','Impingement Plate','Demister / Internals'] as const;
export const API653_EXTERNAL_COMPONENT_PRESETS = ['Shell','Roof','Bottom','Nozzle','Manway','Stairs / Platforms','Other Component'] as const;

const COMPONENT_SECTION_ID = 'component-section';
const COMPONENT_SECTION_TITLE = 'Component Section';
const answer = (fieldId: string, label: string, dataType = 'text', value = ''): InspectionReportAnswer => ({ fieldId, label, dataType, value, values: [] });
const read = (section: InspectionReportSection, fieldId: string) => section.answers.find((a) => a.fieldId === fieldId)?.value ?? '';
const checked = (section: InspectionReportSection, fieldId: string) => read(section, fieldId).toLowerCase() === 'true';

export const createComponentSection = (instanceNumber: number, componentType = 'Valve'): InspectionReportSection => ({
  sectionId: COMPONENT_SECTION_ID, sectionTitle: COMPONENT_SECTION_TITLE, order: 5, isRepeatable: true, instanceNumber,
  answers: [
    answer('component-type', 'Component Type', 'select', componentType), answer('component-tag', 'Component Name / Tag'), answer('component-location', 'Location'), answer('component-condition', 'Condition', 'select', 'Acceptable'),
    answer('finding-notes', 'Finding Notes', 'textarea'), answer('recommendation-text', 'Recommendation Text', 'textarea'), answer('photo-tag', 'Picture / Photo Tag', 'text'), answer('create-finding', 'Create Finding', 'boolean', 'false'),
    answer('recommendation-required', 'Recommendation Required', 'boolean', 'false'), answer('repair-required', 'Repair Required', 'boolean', 'false'), answer('photo-required', 'Photo Required', 'boolean', 'false'), answer('transfer-to-component-section', 'Transfer to Component Section', 'boolean', 'false'),
    answer('nde-required', 'NDE Required', 'boolean', 'false'), answer('add-to-summary', 'Add to Summary', 'boolean', 'false')
  ]
});

export const buildComponentFindingDedupeKey = (section: InspectionReportSection) => [section.sectionId, section.instanceNumber ?? 0, read(section, 'component-tag').trim().toLowerCase(), read(section, 'component-type').trim().toLowerCase()].join('|');

export const upsertFindingFromComponentSection = (report: InspectionReport, section: InspectionReportSection): InspectionReport => {
  if (!checked(section, 'create-finding')) return report;
  const dedupeKey = buildComponentFindingDedupeKey(section);
  const idx = (report.findings ?? []).findIndex((f) => f.associatedChecklistItem === dedupeKey);
  const finding: InspectionFinding = {
    id: idx >= 0 ? report.findings[idx].id : crypto.randomUUID(), location: read(section, 'component-location') || read(section, 'component-tag'), componentType: read(section, 'component-type') || 'Other Component',
    findingType: checked(section, 'nde-required') ? 'Component Finding - NDE Required' : 'Component Finding', associatedChecklistItem: dedupeKey, description: read(section, 'finding-notes'),
    severity: read(section, 'component-condition') === 'Issue' ? 'High' : read(section, 'component-condition') === 'Monitor' ? 'Medium' : 'Low', repairRequired: checked(section, 'repair-required'),
    repairRecommendation: read(section, 'recommendation-text') || undefined, photoIds: read(section, 'photo-tag') ? [read(section, 'photo-tag')] : undefined, addToSummary: checked(section, 'add-to-summary'), summaryText: checked(section, 'add-to-summary') ? read(section, 'finding-notes') : undefined
  };
  const next = structuredClone(report);
  if (idx >= 0) next.findings[idx] = finding; else next.findings = [...(next.findings ?? []), finding];
  return next;
};

export const canCreateFindingFromComponentSection = (section: InspectionReportSection) => checked(section, 'create-finding');

export const createAndAddComponentSection = (report: InspectionReport, componentType: string): InspectionReport => {
  const next = structuredClone(report); const instances = next.sections.filter((s) => s.sectionId === COMPONENT_SECTION_ID).map((s) => s.instanceNumber ?? 0); const instanceNumber = (instances.length ? Math.max(...instances) : 0) + 1;
  next.sections = [...next.sections, createComponentSection(instanceNumber, componentType)];
  return next;
};
