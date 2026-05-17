import type { InspectionComponentDefinition, PressureBoundarySide } from './inspectionComponentCatalog';

export type ComponentCalculationContext = {
  componentKey: string;
  componentLabel: string;
  equipmentSubtype: string;
  parentComponentRequired: boolean;
  allowedParentComponentKeys: string[];
  selectedParentComponent?: string;
  nozzleLocationRequired: boolean;
  allowedNozzleLocationOptions: string[];
  selectedNozzleLocation?: string;
  pressureBoundarySide: PressureBoundarySide;
  designPressureFieldTag?: string;
  designTemperatureFieldTag?: string;
  tminCalculationSupported: boolean;
  tminCalculationMethod?: string;
  ug45Supported: boolean;
  parentThicknessSourceOptions: string[];
  validationWarnings: string[];
};

const normalizeKey = (value?: string) => value === 'channel-head' ? 'channel-channel-head' : value;

export const resolveComponentCalculationContext = (
  componentDefinition: InspectionComponentDefinition,
  selectedPressureBoundarySide?: PressureBoundarySide,
  selectedParentComponent?: string,
  selectedNozzleLocation?: string
): ComponentCalculationContext => {
  const side = selectedPressureBoundarySide ?? componentDefinition.pressureBoundarySide;
  const normalizedParent = normalizeKey(selectedParentComponent);
  const normalizedLocation = normalizeKey(selectedNozzleLocation);
  const warnings: string[] = [];

  if (componentDefinition.parentComponentRequired && !normalizedParent) {
    warnings.push('Parent component is required but was not selected.');
  }

  if (componentDefinition.nozzleLocationRequired && !normalizedLocation) {
    warnings.push('Nozzle location is required but was not selected.');
  }

  const designPressureFieldTag = componentDefinition.designPressureFieldTagsByPressureBoundarySide?.[side]
    ?? componentDefinition.designPressureFieldTag;
  const designTemperatureFieldTag = componentDefinition.designTemperatureFieldTagsByPressureBoundarySide?.[side]
    ?? componentDefinition.designTemperatureFieldTag;

  return {
    componentKey: componentDefinition.componentKey,
    componentLabel: componentDefinition.label,
    equipmentSubtype: componentDefinition.equipmentSubtype,
    parentComponentRequired: componentDefinition.parentComponentRequired,
    allowedParentComponentKeys: componentDefinition.allowedParentComponentKeys.map((v) => normalizeKey(v) ?? v),
    selectedParentComponent: normalizedParent,
    nozzleLocationRequired: componentDefinition.nozzleLocationRequired ?? false,
    allowedNozzleLocationOptions: (componentDefinition.nozzleLocationOptions ?? []).map((v) => normalizeKey(v) ?? v),
    selectedNozzleLocation: normalizedLocation,
    pressureBoundarySide: side,
    designPressureFieldTag,
    designTemperatureFieldTag,
    tminCalculationSupported: componentDefinition.supportsTminCalculation,
    tminCalculationMethod: componentDefinition.tminCalculationMethod,
    ug45Supported: componentDefinition.supportsNozzleUg45,
    parentThicknessSourceOptions: componentDefinition.parentThicknessSourceOptions ?? [],
    validationWarnings: warnings
  };
};
