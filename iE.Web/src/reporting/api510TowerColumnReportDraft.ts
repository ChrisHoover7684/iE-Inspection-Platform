import type { ApiInspectionDraftSetup } from './componentCalculationPrefill';
import type {
  Api510TowerColumnExternalChecklistField,
  Api510TowerColumnExternalComponentState,
  Api510TowerColumnExternalDraftPayload,
  Api510TowerColumnExternalMaterialsDraft,
  Api510TowerColumnExternalReportHeaderDraft,
  Api510TowerColumnExternalReportTypeId,
  Api510TowerColumnExternalSummaryFieldsDraft
} from '../types';

export const API510_TOWER_COLUMN_EXTERNAL_REPORT_LOCAL_STORAGE_KEY = 'ie.api510.towerColumnExternal.reportDraft.v1';
export const API510_TOWER_COLUMN_EXTERNAL_REPORT_TYPE_PREFIX = 'api510.external.tower-column.' as const;

export const towerColumnSubtypeByReportTypeId: Record<Api510TowerColumnExternalReportTypeId, string> = {
  'api510.external.tower-column.distillation': 'Distillation Tower',
  'api510.external.tower-column.absorber-stripper': 'Absorber / Stripper',
  'api510.external.tower-column.packed-column': 'Packed Column',
  'api510.external.tower-column.tray-column': 'Tray Column'
};

const fieldValue = (header: Record<string, string> | undefined, key: string) => header?.[key] ?? '';

export const isApi510TowerColumnExternalReportType = (reportTypeId: string | undefined): reportTypeId is Api510TowerColumnExternalReportTypeId =>
  Boolean(reportTypeId?.startsWith(API510_TOWER_COLUMN_EXTERNAL_REPORT_TYPE_PREFIX));

export const buildApi510TowerColumnExternalReportHeader = (
  header: Record<string, string> | undefined
): Api510TowerColumnExternalReportHeaderDraft => ({
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

export const buildApi510TowerColumnExternalDesignConditions = (
  header: Record<string, string> | undefined
) => ({
  vesselDesignPressure: fieldValue(header, 'vesselDesignPressure'),
  vesselDesignTemperature: fieldValue(header, 'vesselDesignTemperature')
});

export const buildApi510TowerColumnExternalMaterials = (
  header: Record<string, string> | undefined
): Api510TowerColumnExternalMaterialsDraft => ({
  shellCourseMaterialSpec: fieldValue(header, 'shellCourseMaterialSpec'),
  shellCourseMaterialGrade: fieldValue(header, 'shellCourseMaterialGrade'),
  shellCourseProductForm: fieldValue(header, 'shellCourseProductForm')
});

export const countApi510TowerColumnExternalChecklistSelections = (
  componentStates: Record<string, Api510TowerColumnExternalComponentState>,
  checklistFields: readonly Api510TowerColumnExternalChecklistField[]
): Record<Api510TowerColumnExternalChecklistField, number> => Object.fromEntries(
  checklistFields.map((field) => [
    field,
    Object.values(componentStates).filter((state) => state.checklist[field]).length
  ])
) as Record<Api510TowerColumnExternalChecklistField, number>;

export type BuildApi510TowerColumnExternalDraftPayloadInput = {
  sourceStartWizardDraft: ApiInspectionDraftSetup | undefined;
  sourceStartWizardDraftStorageKey: string;
  components: string[];
  componentStates: Record<string, Api510TowerColumnExternalComponentState>;
  checklistFields: readonly Api510TowerColumnExternalChecklistField[];
  summaryFields: Api510TowerColumnExternalSummaryFieldsDraft;
  photos: Api510TowerColumnExternalDraftPayload['photos'];
  ndeTesting: Api510TowerColumnExternalDraftPayload['ndeTesting'];
  recommendations: Api510TowerColumnExternalDraftPayload['recommendations'];
  returnToService: Api510TowerColumnExternalDraftPayload['returnToService'];
  savedAt: string;
};

export const buildApi510TowerColumnExternalDraftPayload = ({
  sourceStartWizardDraft,
  sourceStartWizardDraftStorageKey,
  components,
  componentStates,
  checklistFields,
  summaryFields,
  photos,
  ndeTesting,
  recommendations,
  returnToService,
  savedAt
}: BuildApi510TowerColumnExternalDraftPayloadInput): Api510TowerColumnExternalDraftPayload => {
  const header = sourceStartWizardDraft?.header;
  const reportTypeId = isApi510TowerColumnExternalReportType(sourceStartWizardDraft?.reportTypeId)
    ? sourceStartWizardDraft.reportTypeId
    : 'api510.external.tower-column.distillation';
  return {
    reportTypeId,
    reportTypeLabel: sourceStartWizardDraft?.reportTypeLabel ?? 'Distillation Tower External',
    equipmentSubtype: towerColumnSubtypeByReportTypeId[reportTypeId],
    sourceStartWizardDraft: {
      storageKey: sourceStartWizardDraftStorageKey,
      draft: sourceStartWizardDraft
    },
    reportHeader: buildApi510TowerColumnExternalReportHeader(header),
    designConditions: buildApi510TowerColumnExternalDesignConditions(header),
    materials: buildApi510TowerColumnExternalMaterials(header),
    componentStates: Object.fromEntries(components.map((component) => [component, componentStates[component]])),
    checklistCounts: countApi510TowerColumnExternalChecklistSelections(componentStates, checklistFields),
    summaryFields,
    photos,
    ndeTesting,
    recommendations,
    returnToService,
    savedAt
  };
};

export const readApi510TowerColumnExternalDraftPayload = (): Api510TowerColumnExternalDraftPayload | undefined => {
  if (typeof window === 'undefined') return undefined;

  const saved = window.localStorage.getItem(API510_TOWER_COLUMN_EXTERNAL_REPORT_LOCAL_STORAGE_KEY);
  if (!saved) return undefined;

  try {
    const parsed = JSON.parse(saved) as Partial<Api510TowerColumnExternalDraftPayload>;
    if (!isApi510TowerColumnExternalReportType(parsed.reportTypeId) || !parsed.savedAt) return undefined;
    return parsed as Api510TowerColumnExternalDraftPayload;
  } catch {
    return undefined;
  }
};
