import type { ApiInspectionDraftSetup } from './componentCalculationPrefill';
import type {
  Api510RemainingExchangerExternalChecklistField,
  Api510RemainingExchangerExternalComponentState,
  Api510RemainingExchangerExternalDraftPayload,
  Api510RemainingExchangerExternalReportTypeId,
  Api510RemainingExchangerExternalSummaryFieldsDraft
} from '../types';

export const API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_TYPE_IDS = [
  'api510.external.exchanger.plate-frame',
  'api510.external.exchanger.double-pipe',
  'api510.external.exchanger.air-cooler'
] as const;

export const API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY = 'ie.api510.remainingExchangerExternal.reportDraft.v1';

const fieldValue = (header: Record<string, string> | undefined, key: string) => header?.[key] ?? '';

export const isApi510RemainingExchangerExternalReportTypeId = (reportTypeId: string | undefined): reportTypeId is Api510RemainingExchangerExternalReportTypeId =>
  API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_TYPE_IDS.includes(reportTypeId as Api510RemainingExchangerExternalReportTypeId);

export const buildApi510RemainingExchangerExternalReportHeader = (header: Record<string, string> | undefined) => ({
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

export const buildApi510RemainingExchangerExternalDesignConditions = (reportTypeId: Api510RemainingExchangerExternalReportTypeId, header: Record<string, string> | undefined) => {
  if (reportTypeId === 'api510.external.exchanger.double-pipe') {
    return {
      innerPipeSideDesignPressure: fieldValue(header, 'innerPipeSideDesignPressure'),
      innerPipeSideDesignTemperature: fieldValue(header, 'innerPipeSideDesignTemperature'),
      annulusOuterPipeSideDesignPressure: fieldValue(header, 'annulusOuterPipeSideDesignPressure'),
      annulusOuterPipeSideDesignTemperature: fieldValue(header, 'annulusOuterPipeSideDesignTemperature')
    };
  }

  if (reportTypeId === 'api510.external.exchanger.air-cooler') {
    return {
      tubeSideDesignPressure: fieldValue(header, 'tubeSideDesignPressure'),
      tubeSideDesignTemperature: fieldValue(header, 'tubeSideDesignTemperature'),
      headerBoxDesignPressure: fieldValue(header, 'headerBoxDesignPressure'),
      headerBoxDesignTemperature: fieldValue(header, 'headerBoxDesignTemperature')
    };
  }

  return {
    hotSideDesignPressure: fieldValue(header, 'hotSideDesignPressure'),
    hotSideDesignTemperature: fieldValue(header, 'hotSideDesignTemperature'),
    coldSideDesignPressure: fieldValue(header, 'coldSideDesignPressure'),
    coldSideDesignTemperature: fieldValue(header, 'coldSideDesignTemperature')
  };
};

export const buildApi510RemainingExchangerExternalMaterials = (header: Record<string, string> | undefined) => ({
  primaryMaterialSpec: fieldValue(header, 'primaryMaterialSpec'),
  primaryMaterialGrade: fieldValue(header, 'primaryMaterialGrade'),
  primaryProductForm: fieldValue(header, 'primaryProductForm'),
  secondaryMaterialSpec: fieldValue(header, 'secondaryMaterialSpec'),
  secondaryMaterialGrade: fieldValue(header, 'secondaryMaterialGrade'),
  secondaryProductForm: fieldValue(header, 'secondaryProductForm')
});

export const countApi510RemainingExchangerExternalChecklistSelections = (
  componentStates: Record<string, Api510RemainingExchangerExternalComponentState>,
  checklistFields: readonly Api510RemainingExchangerExternalChecklistField[]
): Record<Api510RemainingExchangerExternalChecklistField, number> => Object.fromEntries(
  checklistFields.map((field) => [field, Object.values(componentStates).filter((state) => state.checklist[field]).length])
) as Record<Api510RemainingExchangerExternalChecklistField, number>;

export type BuildApi510RemainingExchangerExternalDraftPayloadInput = {
  reportTypeId: Api510RemainingExchangerExternalReportTypeId;
  sourceStartWizardDraft: ApiInspectionDraftSetup | undefined;
  sourceStartWizardDraftStorageKey: string;
  components: string[];
  componentStates: Record<string, Api510RemainingExchangerExternalComponentState>;
  checklistFields: readonly Api510RemainingExchangerExternalChecklistField[];
  summaryFields: Api510RemainingExchangerExternalSummaryFieldsDraft;
  photos: Api510RemainingExchangerExternalDraftPayload['photos'];
  ndeTesting: Api510RemainingExchangerExternalDraftPayload['ndeTesting'];
  recommendations: Api510RemainingExchangerExternalDraftPayload['recommendations'];
  returnToService: Api510RemainingExchangerExternalDraftPayload['returnToService'];
  savedAt: string;
};

export const buildApi510RemainingExchangerExternalDraftPayload = ({
  reportTypeId,
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
}: BuildApi510RemainingExchangerExternalDraftPayloadInput): Api510RemainingExchangerExternalDraftPayload => {
  const header = sourceStartWizardDraft?.header;
  return {
    reportTypeId,
    reportTypeLabel: sourceStartWizardDraft?.reportTypeLabel ?? 'API 510 Exchanger External',
    sourceStartWizardDraft: { storageKey: sourceStartWizardDraftStorageKey, draft: sourceStartWizardDraft },
    reportHeader: buildApi510RemainingExchangerExternalReportHeader(header),
    designConditions: buildApi510RemainingExchangerExternalDesignConditions(reportTypeId, header),
    materials: buildApi510RemainingExchangerExternalMaterials(header),
    componentStates: Object.fromEntries(components.map((component) => [component, componentStates[component]])),
    checklistCounts: countApi510RemainingExchangerExternalChecklistSelections(componentStates, checklistFields),
    calculationReview: { status: 'review-only', notes: 'Calculation workspace coming soon for this exchanger type.' },
    summaryFields,
    photos,
    ndeTesting,
    recommendations,
    returnToService,
    savedAt
  };
};

export const readApi510RemainingExchangerExternalDraftPayload = (): Api510RemainingExchangerExternalDraftPayload | undefined => {
  if (typeof window === 'undefined') return undefined;
  const saved = window.localStorage.getItem(API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY);
  if (!saved) return undefined;

  try {
    const parsed = JSON.parse(saved) as Partial<Api510RemainingExchangerExternalDraftPayload>;
    if (!isApi510RemainingExchangerExternalReportTypeId(parsed.reportTypeId) || !parsed.savedAt) return undefined;
    return parsed as Api510RemainingExchangerExternalDraftPayload;
  } catch {
    return undefined;
  }
};
