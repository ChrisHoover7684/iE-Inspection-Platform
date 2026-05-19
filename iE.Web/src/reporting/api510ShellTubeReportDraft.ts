import type { InspectionCalculationSnapshot } from '../engineering/types';
import type { Api510FindingDraft } from './api510CalculationFindings';
import type { ApiInspectionDraftSetup } from './componentCalculationPrefill';
import type {
  Api510ShellTubeExternalChecklistField,
  Api510ShellTubeExternalComponentState,
  Api510ShellTubeExternalDraftPayload,
  Api510ShellTubeExternalMaterialsDraft,
  Api510ShellTubeExternalReportHeaderDraft,
  Api510ShellTubeExternalSummaryFieldsDraft
} from '../types';

export const API510_SHELL_TUBE_EXTERNAL_REPORT_TYPE_ID = 'api510.external.exchanger.shell-tube' as const;
export const API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY = 'ie.api510.shellTubeExternal.reportDraft.v1';

const fieldValue = (header: Record<string, string> | undefined, key: string) => header?.[key] ?? '';

export const buildApi510ShellTubeExternalReportHeader = (
  header: Record<string, string> | undefined
): Api510ShellTubeExternalReportHeaderDraft => ({
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

export const buildApi510ShellTubeExternalDesignConditions = (
  header: Record<string, string> | undefined
) => ({
  shellSideDesignPressure: fieldValue(header, 'shellSideDesignPressure'),
  shellSideDesignTemperature: fieldValue(header, 'shellSideDesignTemperature'),
  tubeSideDesignPressure: fieldValue(header, 'tubeSideDesignPressure'),
  tubeSideDesignTemperature: fieldValue(header, 'tubeSideDesignTemperature')
});

export const buildApi510ShellTubeExternalMaterials = (
  header: Record<string, string> | undefined
): Api510ShellTubeExternalMaterialsDraft => ({
  shellMaterialSpec: fieldValue(header, 'shellMaterialSpec'),
  shellMaterialGrade: fieldValue(header, 'shellMaterialGrade'),
  shellProductForm: fieldValue(header, 'shellProductForm'),
  tubeChannelMaterialSpec: fieldValue(header, 'tubeChannelMaterialSpec'),
  tubeChannelMaterialGrade: fieldValue(header, 'tubeChannelMaterialGrade'),
  tubeChannelProductForm: fieldValue(header, 'tubeChannelProductForm')
});

export const countApi510ShellTubeExternalChecklistSelections = (
  componentStates: Record<string, Api510ShellTubeExternalComponentState>,
  checklistFields: readonly Api510ShellTubeExternalChecklistField[]
): Record<Api510ShellTubeExternalChecklistField, number> => Object.fromEntries(
  checklistFields.map((field) => [
    field,
    Object.values(componentStates).filter((state) => state.checklist[field]).length
  ])
) as Record<Api510ShellTubeExternalChecklistField, number>;

export type BuildApi510ShellTubeExternalDraftPayloadInput = {
  sourceStartWizardDraft: ApiInspectionDraftSetup | undefined;
  sourceStartWizardDraftStorageKey: string;
  components: string[];
  componentStates: Record<string, Api510ShellTubeExternalComponentState>;
  checklistFields: readonly Api510ShellTubeExternalChecklistField[];
  calculationSnapshots: InspectionCalculationSnapshot[];
  findingDrafts: Api510FindingDraft[];
  summaryFields: Api510ShellTubeExternalSummaryFieldsDraft;
  photos: Api510ShellTubeExternalDraftPayload['photos'];
  ndeTesting: Api510ShellTubeExternalDraftPayload['ndeTesting'];
  recommendations: Api510ShellTubeExternalDraftPayload['recommendations'];
  returnToService: Api510ShellTubeExternalDraftPayload['returnToService'];
  savedAt: string;
};

export const buildApi510ShellTubeExternalDraftPayload = ({
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
}: BuildApi510ShellTubeExternalDraftPayloadInput): Api510ShellTubeExternalDraftPayload => {
  const header = sourceStartWizardDraft?.header;
  return {
    reportTypeId: API510_SHELL_TUBE_EXTERNAL_REPORT_TYPE_ID,
    reportTypeLabel: sourceStartWizardDraft?.reportTypeLabel ?? 'Shell and Tube Exchanger External',
    sourceStartWizardDraft: {
      storageKey: sourceStartWizardDraftStorageKey,
      draft: sourceStartWizardDraft
    },
    reportHeader: buildApi510ShellTubeExternalReportHeader(header),
    designConditions: buildApi510ShellTubeExternalDesignConditions(header),
    materials: buildApi510ShellTubeExternalMaterials(header),
    componentStates: Object.fromEntries(components.map((component) => [component, componentStates[component]])),
    checklistCounts: countApi510ShellTubeExternalChecklistSelections(componentStates, checklistFields),
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

export const readApi510ShellTubeExternalDraftPayload = (): Api510ShellTubeExternalDraftPayload | undefined => {
  if (typeof window === 'undefined') return undefined;

  const saved = window.localStorage.getItem(API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY);
  if (!saved) return undefined;

  try {
    const parsed = JSON.parse(saved) as Partial<Api510ShellTubeExternalDraftPayload>;
    if (parsed.reportTypeId !== API510_SHELL_TUBE_EXTERNAL_REPORT_TYPE_ID || !parsed.savedAt) return undefined;
    return parsed as Api510ShellTubeExternalDraftPayload;
  } catch {
    return undefined;
  }
};
