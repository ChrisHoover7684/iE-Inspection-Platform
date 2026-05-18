import { API510_EXTERNAL_COMPONENT_DEFINITIONS, type PressureBoundarySide } from './inspectionComponentCatalog';
import { resolveComponentCalculationContext } from './componentCalculationContext';

export type CalculationFieldValuesMap = Record<string, string | number | null | undefined>;

export const API_INSPECTION_DRAFT_SETUP_STORAGE_KEY = 'apiInspectionDraftSetup';

export type ApiInspectionDraftSetup = {
  reportTypeId: string;
  reportTypeLabel?: string;
  header: Record<string, string>;
  components: string[];
};

export type DraftCalculationWorkspaceContext = {
  fieldValues: CalculationFieldValuesMap;
  selectedComponents: string[];
  reportTypeId: string;
  reportTypeLabel?: string;
  equipmentSubtype?: string;
  headType?: string;
};

const drumVesselSubtypeByReportTypeId: Record<string, string> = {
  'api510.external.drum-vessel.horizontal-drum': 'Horizontal Drum',
  'api510.external.drum-vessel.vertical-drum': 'Vertical Drum',
  'api510.external.drum-vessel.separator-ko-drum': 'Separator / KO Drum',
  'api510.external.drum-vessel.accumulator-receiver': 'Accumulator / Receiver',
  'api510.external.drum-vessel.generic-pressure-vessel': 'Generic Pressure Vessel'
};

export const readApiInspectionDraftSetup = (): ApiInspectionDraftSetup | undefined => {
  if (typeof window === 'undefined') return undefined;

  const storedDraft = window.localStorage.getItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
  if (!storedDraft) return undefined;

  try {
    const draft = JSON.parse(storedDraft) as Partial<ApiInspectionDraftSetup>;
    if (!draft.reportTypeId || !draft.header || !Array.isArray(draft.components)) return undefined;
    return {
      reportTypeId: draft.reportTypeId,
      reportTypeLabel: draft.reportTypeLabel,
      header: draft.header,
      components: draft.components
    };
  } catch {
    return undefined;
  }
};

export const buildShellTubeDraftWorkspaceContext = (
  draft: ApiInspectionDraftSetup | undefined
): DraftCalculationWorkspaceContext | undefined => {
  if (draft?.reportTypeId !== 'api510.external.exchanger.shell-tube') return undefined;

  return {
    reportTypeId: draft.reportTypeId,
    reportTypeLabel: draft.reportTypeLabel,
    selectedComponents: draft.components,
    equipmentSubtype: 'Shell and Tube Exchanger',
    fieldValues: {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': draft.header.shellSideDesignPressure,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': draft.header.shellSideDesignTemperature,
      'api510.external.exchanger.shell-tube.tube-side.design-pressure': draft.header.tubeSideDesignPressure,
      'api510.external.exchanger.shell-tube.tube-side.design-temperature': draft.header.tubeSideDesignTemperature
    }
  };
};

export const buildDrumVesselDraftWorkspaceContext = (
  draft: ApiInspectionDraftSetup | undefined
): DraftCalculationWorkspaceContext | undefined => {
  if (!draft?.reportTypeId.startsWith('api510.external.drum-vessel.')) return undefined;

  return {
    reportTypeId: draft.reportTypeId,
    reportTypeLabel: draft.reportTypeLabel,
    selectedComponents: draft.components,
    equipmentSubtype: drumVesselSubtypeByReportTypeId[draft.reportTypeId],
    headType: draft.header.headType,
    fieldValues: {
      'api510.external.drum-vessel.design-pressure': draft.header.vesselDesignPressure,
      'api510.external.drum-vessel.design-temperature': draft.header.vesselDesignTemperature
    }
  };
};

export type ComponentCalculationPrefill = {
  equipmentSubtype: string;
  componentKey: string;
  componentLabel: string;
  selectedParentComponent?: string;
  selectedNozzleLocation?: string;
  resolvedPressureSide: PressureBoundarySide;
  designPressureFieldTag?: string;
  designPressureValue?: string | number;
  designTemperatureFieldTag?: string;
  designTemperatureValue?: string | number;
  supportsTminCalculation: boolean;
  calculationMethodLabel?: string;
  supportsUg45: boolean;
  parentThicknessSourceOptions: string[];
  missingRequiredInputWarnings: string[];
  notes: string[];
};

export const buildComponentCalculationPrefill = (
  equipmentSubtype: string,
  componentKey: string,
  selectedPressureSide: PressureBoundarySide | undefined,
  selectedParentComponent: string | undefined,
  selectedNozzleLocation: string | undefined,
  fieldValues: CalculationFieldValuesMap
): ComponentCalculationPrefill | undefined => {
  const componentDefinition = API510_EXTERNAL_COMPONENT_DEFINITIONS.find(
    (d) => d.equipmentSubtype === equipmentSubtype && d.componentKey === componentKey
  );

  if (!componentDefinition || componentDefinition.standard !== 'API 510' || componentDefinition.inspectionScope !== 'External') {
    return undefined;
  }

  const context = resolveComponentCalculationContext(
    componentDefinition,
    selectedPressureSide,
    selectedParentComponent,
    selectedNozzleLocation
  );

  const shellTubeDesignTagBySide = {
    'shell-side': {
      pressure: 'api510.external.exchanger.shell-tube.shell-side.design-pressure',
      temperature: 'api510.external.exchanger.shell-tube.shell-side.design-temperature'
    },
    'tube-side': {
      pressure: 'api510.external.exchanger.shell-tube.tube-side.design-pressure',
      temperature: 'api510.external.exchanger.shell-tube.tube-side.design-temperature'
    }
  } as const;

  const fallbackShellTubeTagSet = equipmentSubtype === 'Shell and Tube Exchanger'
    && (context.pressureBoundarySide === 'shell-side' || context.pressureBoundarySide === 'tube-side')
      ? shellTubeDesignTagBySide[context.pressureBoundarySide]
      : undefined;
  const designPressureFieldTag = context.designPressureFieldTag ?? fallbackShellTubeTagSet?.pressure;
  const designTemperatureFieldTag = context.designTemperatureFieldTag ?? fallbackShellTubeTagSet?.temperature;

  const notes: string[] = [];
  if (equipmentSubtype === 'Shell and Tube Exchanger' && selectedPressureSide === 'channel-side') {
    notes.push('Channel-side was normalized to tube-side for shell-and-tube exchanger design conditions.');
  }
  if (equipmentSubtype === 'Shell and Tube Exchanger' && context.componentKey === 'channel-channel-head') {
    notes.push('Channel / Channel Head resolves to tube-side design pressure and temperature context.');
  }
  if (equipmentSubtype === 'Shell and Tube Exchanger' && (context.componentKey === 'channel-cover' || context.componentKey === 'channel-head-dollar-plate' || context.componentKey === 'bonnet-head')) {
    notes.push('Tube-side design pressure and temperature apply for channel/channel-head family components.');
  }

  return {
    equipmentSubtype: context.equipmentSubtype,
    componentKey: context.componentKey,
    componentLabel: context.componentLabel,
    selectedParentComponent: context.selectedParentComponent,
    selectedNozzleLocation: context.selectedNozzleLocation,
    resolvedPressureSide: context.pressureBoundarySide,
    designPressureFieldTag,
    designPressureValue: designPressureFieldTag ? fieldValues[designPressureFieldTag] ?? undefined : undefined,
    designTemperatureFieldTag,
    designTemperatureValue: designTemperatureFieldTag ? fieldValues[designTemperatureFieldTag] ?? undefined : undefined,
    supportsTminCalculation: context.tminCalculationSupported,
    calculationMethodLabel: context.tminCalculationMethod,
    supportsUg45: context.ug45Supported,
    parentThicknessSourceOptions: context.parentThicknessSourceOptions,
    missingRequiredInputWarnings: context.validationWarnings,
    notes
  };
};
