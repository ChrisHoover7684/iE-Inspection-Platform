import type { ApiInspectionDraftSetup } from './componentCalculationPrefill';
import type {
  StiApi653TankExternalChecklistField,
  StiApi653TankExternalComponentState,
  StiApi653TankExternalDraftPayload,
  StiApi653TankExternalSummaryFieldsDraft
} from '../types';

export const STI_API653_TANK_EXTERNAL_REPORT_TYPE_ID = 'sti653.external.tank' as const;
export const STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY = 'ie.stiApi653.tankExternal.reportDraft.v1';

const fieldValue = (header: Record<string, string> | undefined, key: string) => header?.[key] ?? '';

export const buildStiApi653TankExternalReportHeader = (header: Record<string, string> | undefined) => ({
  inspectionDate: fieldValue(header, 'inspectionDate'),
  inspector: fieldValue(header, 'inspector'),
  clientFacility: fieldValue(header, 'clientFacility'),
  unitArea: fieldValue(header, 'unitArea'),
  equipmentIdentifier: fieldValue(header, 'equipmentIdentifier'),
  service: fieldValue(header, 'service'),
  inspectionReason: fieldValue(header, 'inspectionReason'),
  operatingStatus: fieldValue(header, 'operatingStatus'),
  inspectionScope: fieldValue(header, 'inspectionScope')
});

export const buildStiApi653TankExternalDetails = (header: Record<string, string> | undefined) => ({
  tankId: fieldValue(header, 'tankId'),
  tankService: fieldValue(header, 'tankService'),
  tankConstruction: fieldValue(header, 'tankConstruction')
});

export const countStiApi653TankExternalChecklistSelections = (
  componentStates: Record<string, StiApi653TankExternalComponentState>,
  checklistFields: readonly StiApi653TankExternalChecklistField[]
): Record<StiApi653TankExternalChecklistField, number> => Object.fromEntries(
  checklistFields.map((field) => [field, Object.values(componentStates).filter((state) => state.checklist[field]).length])
) as Record<StiApi653TankExternalChecklistField, number>;

export type BuildStiApi653TankExternalDraftPayloadInput = {
  sourceStartWizardDraft: ApiInspectionDraftSetup | undefined;
  sourceStartWizardDraftStorageKey: string;
  sections: string[];
  componentStates: Record<string, StiApi653TankExternalComponentState>;
  checklistFields: readonly StiApi653TankExternalChecklistField[];
  summaryFields: StiApi653TankExternalSummaryFieldsDraft;
  photos: StiApi653TankExternalDraftPayload['photos'];
  ndeTesting: StiApi653TankExternalDraftPayload['ndeTesting'];
  recommendations: StiApi653TankExternalDraftPayload['recommendations'];
  returnToService: StiApi653TankExternalDraftPayload['returnToService'];
  savedAt: string;
};

export const buildStiApi653TankExternalDraftPayload = ({
  sourceStartWizardDraft,
  sourceStartWizardDraftStorageKey,
  sections,
  componentStates,
  checklistFields,
  summaryFields,
  photos,
  ndeTesting,
  recommendations,
  returnToService,
  savedAt
}: BuildStiApi653TankExternalDraftPayloadInput): StiApi653TankExternalDraftPayload => {
  const header = sourceStartWizardDraft?.header;
  return {
    reportTypeId: STI_API653_TANK_EXTERNAL_REPORT_TYPE_ID,
    reportTypeLabel: sourceStartWizardDraft?.reportTypeLabel ?? 'Tank External',
    sourceStartWizardDraft: { storageKey: sourceStartWizardDraftStorageKey, draft: sourceStartWizardDraft },
    reportHeader: buildStiApi653TankExternalReportHeader(header),
    tankDetails: buildStiApi653TankExternalDetails(header),
    sections,
    componentStates: Object.fromEntries(sections.map((section) => [section, componentStates[section]])),
    checklistCounts: countStiApi653TankExternalChecklistSelections(componentStates, checklistFields),
    summaryFields,
    photos,
    ndeTesting,
    recommendations,
    returnToService,
    savedAt
  };
};

export const readStiApi653TankExternalDraftPayload = (): StiApi653TankExternalDraftPayload | undefined => {
  if (typeof window === 'undefined') return undefined;
  const saved = window.localStorage.getItem(STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY);
  if (!saved) return undefined;

  try {
    const parsed = JSON.parse(saved) as Partial<StiApi653TankExternalDraftPayload>;
    if (parsed.reportTypeId !== STI_API653_TANK_EXTERNAL_REPORT_TYPE_ID || !parsed.savedAt) return undefined;
    return parsed as StiApi653TankExternalDraftPayload;
  } catch {
    return undefined;
  }
};
