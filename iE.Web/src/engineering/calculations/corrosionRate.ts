import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type CorrosionRateInputs = {
  initialThicknessInches: number;
  finalThicknessInches: number;
  exposureTimeYears: number;
  inspectionFactor: number;
  currentThicknessInches: number;
  tminInches: number;
};

export type CorrosionRateOutputs = {
  thicknessLossInches: number;
  corrosionRateInchesPerYear: number;
  corrosionRateMpy: number;
  corrosionRateMmPerYear: number;
  remainingLifeYears: number | null;
  nextInspectionYears: number | null;
};

export function calculateCorrosionRate(inputs: CorrosionRateInputs): EngineeringCalculationResult<CorrosionRateInputs, CorrosionRateOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();
  const standardReferences = [
    { standard: 'API 570', note: 'Foundation corrosion-rate workflow; owner/operator validation required before report use.' }
  ];
  const thicknessLossInches = inputs.initialThicknessInches - inputs.finalThicknessInches;
  const canCalculateRate = inputs.initialThicknessInches > 0
    && inputs.finalThicknessInches > 0
    && inputs.initialThicknessInches > inputs.finalThicknessInches
    && inputs.exposureTimeYears > 0;
  const corrosionRateInchesPerYear = canCalculateRate ? thicknessLossInches / inputs.exposureTimeYears : 0;

  if (inputs.initialThicknessInches <= 0) warnings.push({ code: 'INITIAL_THICKNESS_NON_POSITIVE', message: 'Initial thickness must be greater than zero.', severity: 'error' });
  if (inputs.finalThicknessInches <= 0) warnings.push({ code: 'FINAL_THICKNESS_NON_POSITIVE', message: 'Final thickness must be greater than zero.', severity: 'error' });
  if (inputs.initialThicknessInches <= inputs.finalThicknessInches) warnings.push({ code: 'INITIAL_NOT_GREATER_THAN_FINAL', message: 'Initial thickness should be greater than final thickness to calculate corrosion rate.', severity: 'warning' });
  if (inputs.exposureTimeYears <= 0) warnings.push({ code: 'EXPOSURE_TIME_NON_POSITIVE', message: 'Exposure time must be greater than zero.', severity: 'error' });
  if (inputs.currentThicknessInches <= 0) warnings.push({ code: 'CURRENT_THICKNESS_NON_POSITIVE', message: 'Current thickness must be greater than zero.', severity: 'error' });
  if (inputs.tminInches <= 0) warnings.push({ code: 'TMIN_NON_POSITIVE', message: 'Tmin must be greater than zero.', severity: 'error' });
  if (inputs.inspectionFactor <= 0) warnings.push({ code: 'INSPECTION_FACTOR_NON_POSITIVE', message: 'Inspection factor must be greater than zero.', severity: 'warning' });
  if (corrosionRateInchesPerYear <= 0) warnings.push({ code: 'NON_POSITIVE_CORROSION_RATE', message: 'Calculated corrosion rate is non-positive.', severity: 'warning' });

  if (inputs.currentThicknessInches <= inputs.tminInches) {
    warnings.push({ code: 'CURRENT_AT_OR_BELOW_TMIN', message: 'Current thickness is at or below Tmin. Remaining life cannot be projected.', severity: 'error' });
  }

  const remainingLifeYears = corrosionRateInchesPerYear > 0 && inputs.currentThicknessInches > inputs.tminInches
    ? (inputs.currentThicknessInches - inputs.tminInches) / corrosionRateInchesPerYear
    : null;
  const normalizedRemainingLifeYears = remainingLifeYears !== null ? Math.max(remainingLifeYears, 0) : null;
  const nextInspectionYears = normalizedRemainingLifeYears !== null && inputs.inspectionFactor > 0
    ? Math.max(normalizedRemainingLifeYears * inputs.inspectionFactor, 0)
    : null;
  const insertLabel = `Corrosion Rate (${corrosionRateInchesPerYear.toFixed(4)} in/yr), Remaining Life (${normalizedRemainingLifeYears?.toFixed(2) ?? 'N/A'} years)`;

  return {
    id: `corrosion-rate-${calculatedAt}`,
    calculationType: 'corrosion-rate',
    displayName: 'Corrosion Rate Foundation Helper',
    formulaVersion: 'api-570-foundation-v1',
    standardReferences,
    inputs,
    outputs: {
      thicknessLossInches,
      corrosionRateInchesPerYear,
      corrosionRateMpy: corrosionRateInchesPerYear * 1000,
      corrosionRateMmPerYear: corrosionRateInchesPerYear * 25.4,
      remainingLifeYears: normalizedRemainingLifeYears,
      nextInspectionYears
    },
    warnings,
    calculatedAt,
    insertLabel
  };
}
