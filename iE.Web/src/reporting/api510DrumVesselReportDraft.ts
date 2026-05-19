import type { InspectionCalculationSnapshot } from '../engineering/types';
import type { Api510FindingDraft } from './api510CalculationFindings';
import type { ApiInspectionDraftSetup } from './componentCalculationPrefill';
import type {
  Api510DrumVesselExternalChecklistField,
  Api510DrumVesselExternalComponentState,
  Api510DrumVesselExternalDraftPayload,
  Api510DrumVesselExternalMaterialsDraft,
  Api510DrumVesselExternalReportHeaderDraft,
  Api510DrumVesselExternalReportTypeId,
  Api510DrumVesselExternalSummaryFieldsDraft
} from '../types';

export const API510_DRUM_VESSEL_EXTERNAL_REPORT_LOCAL_STORAGE_KEY = 'ie.api510.drumVesselExternal.reportDraft.v1';
export const API510_DRUM_VESSEL_EXTERNAL_REPORT_TYPE_PREFIX = 'api510.external.drum-vessel.' as const;

export const drumVesselSubtypeByReportTypeId: Record<Api510DrumVesselExternalReportTypeId, string> = {
  'api510.external.drum-vessel.horizontal-drum': 'Horizontal Drum',
  'api510.external.drum-vessel.vertical-drum': 'Vertical Drum',
  'api510.external.drum-vessel.separator-ko-drum': 'Separator / KO Drum',
  'api510.external.drum-vessel.accumulator-receiver': 'Accumulator / Receiver',
  'api510.external.drum-vessel.generic-pressure-vessel': 'Generic Pressure Vessel'
};

const fieldValue = (header: Record<string, string> | undefined, key: string) => header?.[key] ?? '';

export const isApi510DrumVesselExternalReportType = (reportTypeId: string | undefined): reportTypeId is Api510DrumVesselExternalReportTypeId =>
  Boolean(reportTypeId?.startsWith(API510_DRUM_VESSEL_EXTERNAL_REPORT_TYPE_PREFIX));

export const buildApi510DrumVesselExternalReportHeader = (
  header: Record<string, string> | undefined
): Api510DrumVesselExternalReportHeaderDraft => ({
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

export const buildApi510DrumVesselExternalDesignConditions = (
  header: Record<string, string> | undefined
) => ({
  vesselDesignPressure: fieldValue(header, 'vesselDesignPressure'),
  vesselDesignTemperature: fieldValue(header, 'vesselDesignTemperature')
});

export const buildApi510DrumVesselExternalMaterials = (
  header: Record<string, string> | undefined
): Api510DrumVesselExternalMaterialsDraft => ({
  shellMaterialSpec: fieldValue(header, 'shellMaterialSpec'),
  shellMaterialGrade: fieldValue(header, 'shellMaterialGrade'),
  shellProductForm: fieldValue(header, 'shellProductForm'),
  headMaterialSpec: fieldValue(header, 'headMaterialSpec'),
  headMaterialGrade: fieldValue(header, 'headMaterialGrade'),
  headProductForm: fieldValue(header, 'headProductForm'),
  headType: fieldValue(header, 'headType')
});

export const countApi510DrumVesselExternalChecklistSelections = (
  componentStates: Record<string, Api510DrumVesselExternalComponentState>,
  checklistFields: readonly Api510DrumVesselExternalChecklistField[]
): Record<Api510DrumVesselExternalChecklistField, number> => Object.fromEntries(
  checklistFields.map((field) => [
    field,
    Object.values(componentStates).filter((state) => state.checklist[field]).length
  ])
) as Record<Api510DrumVesselExternalChecklistField, number>;

export type BuildApi510DrumVesselExternalDraftPayloadInput = {
  sourceStartWizardDraft: ApiInspectionDraftSetup | undefined;
  sourceStartWizardDraftStorageKey: string;
  components: string[];
  componentStates: Record<string, Api510DrumVesselExternalComponentState>;
  checklistFields: readonly Api510DrumVesselExternalChecklistField[];
  calculationSnapshots: InspectionCalculationSnapshot[];
  findingDrafts: Api510FindingDraft[];
  summaryFields: Api510DrumVesselExternalSummaryFieldsDraft;
  photos: Api510DrumVesselExternalDraftPayload['photos'];
  ndeTesting: Api510DrumVesselExternalDraftPayload['ndeTesting'];
  recommendations: Api510DrumVesselExternalDraftPayload['recommendations'];
  returnToService: Api510DrumVesselExternalDraftPayload['returnToService'];
  savedAt: string;
};

export const buildApi510DrumVesselExternalDraftPayload = ({
  sourceStartWizardDraft,
  sourceStartWizardDraftStorageKey,
  components,
  componentStates,
  checklistFields,
  calculationSnapshots,
  findingDrafts,
  summaryFields,
  photos,
  ndeTesting,
  recommendations,
  returnToService,
  savedAt
}: BuildApi510DrumVesselExternalDraftPayloadInput): Api510DrumVesselExternalDraftPayload => {
  const header = sourceStartWizardDraft?.header;
  const reportTypeId = isApi510DrumVesselExternalReportType(sourceStartWizardDraft?.reportTypeId)
    ? sourceStartWizardDraft.reportTypeId
    : 'api510.external.drum-vessel.generic-pressure-vessel';
  return {
    reportTypeId,
    reportTypeLabel: sourceStartWizardDraft?.reportTypeLabel ?? 'Generic Pressure Vessel External',
    equipmentSubtype: drumVesselSubtypeByReportTypeId[reportTypeId],
    sourceStartWizardDraft: {
      storageKey: sourceStartWizardDraftStorageKey,
      draft: sourceStartWizardDraft
    },
    reportHeader: buildApi510DrumVesselExternalReportHeader(header),
    designConditions: buildApi510DrumVesselExternalDesignConditions(header),
    materials: buildApi510DrumVesselExternalMaterials(header),
    componentStates: Object.fromEntries(components.map((component) => [component, componentStates[component]])),
    checklistCounts: countApi510DrumVesselExternalChecklistSelections(componentStates, checklistFields),
    calculationSnapshots,
    findingDrafts,
    summaryFields,
    photos,
    ndeTesting,
    recommendations,
    returnToService,
    savedAt
  };
};

export const readApi510DrumVesselExternalDraftPayload = (): Api510DrumVesselExternalDraftPayload | undefined => {
  if (typeof window === 'undefined') return undefined;

  const saved = window.localStorage.getItem(API510_DRUM_VESSEL_EXTERNAL_REPORT_LOCAL_STORAGE_KEY);
  if (!saved) return undefined;

  try {
    const parsed = JSON.parse(saved) as Partial<Api510DrumVesselExternalDraftPayload>;
    if (!isApi510DrumVesselExternalReportType(parsed.reportTypeId) || !parsed.savedAt) return undefined;
    return parsed as Api510DrumVesselExternalDraftPayload;
  } catch {
    return undefined;
  }
};
