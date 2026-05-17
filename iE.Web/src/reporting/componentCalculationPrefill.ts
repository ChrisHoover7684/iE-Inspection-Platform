import { API510_EXTERNAL_COMPONENT_DEFINITIONS, type PressureBoundarySide } from './inspectionComponentCatalog';
import { resolveComponentCalculationContext } from './componentCalculationContext';

export type CalculationFieldValuesMap = Record<string, string | number | null | undefined>;

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
