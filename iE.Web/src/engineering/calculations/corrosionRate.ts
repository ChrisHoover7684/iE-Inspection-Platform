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
  const thicknessLossInches = inputs.initialThicknessInches - inputs.finalThicknessInches;
  const corrosionRateInchesPerYear = inputs.exposureTimeYears > 0 ? thicknessLossInches / inputs.exposureTimeYears : 0;

  if (inputs.exposureTimeYears <= 0) warnings.push({ code: 'EXPOSURE_TIME_NON_POSITIVE', message: 'Exposure time must be greater than zero.' });
  if (corrosionRateInchesPerYear <= 0) warnings.push({ code: 'NON_POSITIVE_CORROSION_RATE', message: 'Calculated corrosion rate is non-positive.' });

  const remainingLifeYears = corrosionRateInchesPerYear > 0
    ? (inputs.currentThicknessInches - inputs.tminInches) / corrosionRateInchesPerYear
    : null;

  if (remainingLifeYears !== null && remainingLifeYears < 0) {
    warnings.push({ code: 'BELOW_TMIN', message: 'Current thickness is below Tmin.' });
  }

  return {
    calculationType: 'corrosion-rate',
    formulaVersion: 'api-570-foundation-v1',
    inputs,
    outputs: {
      thicknessLossInches,
      corrosionRateInchesPerYear,
      corrosionRateMpy: corrosionRateInchesPerYear * 1000,
      corrosionRateMmPerYear: corrosionRateInchesPerYear * 25.4,
      remainingLifeYears,
      nextInspectionYears: remainingLifeYears !== null ? remainingLifeYears * inputs.inspectionFactor : null
    },
    warnings
  };
}
