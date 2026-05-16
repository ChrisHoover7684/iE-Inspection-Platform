import { calculateCorrosionRate } from './corrosionRate';
import type { B31CircuitBatchOutputs, B31CircuitRowResult } from './b31_3CircuitBatch';
import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type Api570CmlReadingInput = {
  id: string;
  cmlId: string;
  location: string;
  nps: string;
  material?: string;
  spec?: string;
  grade?: string;
  currentThicknessIn: number;
  priorThicknessIn?: number | null;
  priorInspectionDate?: string | null;
  currentInspectionDate: string;
};

export type Api570ThicknessStatus = 'Acceptable' | 'Monitor' | 'Below Tmin' | 'Missing Tmin';

export type Api570ThicknessAssessmentRow = {
  cmlId: string;
  location: string;
  nps: string;
  currentThicknessIn: number;
  tminIn: number | null;
  marginToTminIn: number | null;
  corrosionRateInPerYear: number | null;
  remainingLifeYears: number | null;
  status: Api570ThicknessStatus;
  recommendedAction: string;
  matchedB31RowId?: string;
};

export type Api570ThicknessAssessmentInputs = {
  b31SnapshotId: string;
  cmlReadings: Api570CmlReadingInput[];
};
export type Api570ThicknessAssessmentOutputs = { rows: Api570ThicknessAssessmentRow[] };

const norm = (v?: string) => (v ?? '').trim().toLowerCase();

export function assessApi570Thickness(
  inputs: Api570ThicknessAssessmentInputs,
  b31Rows: B31CircuitRowResult[]
): EngineeringCalculationResult<Api570ThicknessAssessmentInputs, Api570ThicknessAssessmentOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();
  const rows = inputs.cmlReadings.map((reading) => {
    const npsMatches = b31Rows.filter((r) => r.nps === reading.nps && r.requiredThicknessIn != null);
    const matched = npsMatches.find((r) => {
      if (!reading.spec && !reading.grade) return true;
      return (!reading.spec || norm(r.spec) === norm(reading.spec)) && (!reading.grade || norm(r.grade) === norm(reading.grade));
    }) ?? npsMatches[0];

    if (!matched || matched.requiredThicknessIn == null) {
      warnings.push({ code: 'MISSING_TMIN', message: `${reading.cmlId}: no matching B31.3 Tmin for NPS ${reading.nps}`, severity: 'warning' });
      return { cmlId: reading.cmlId, location: reading.location, nps: reading.nps, currentThicknessIn: reading.currentThicknessIn, tminIn: null, marginToTminIn: null, corrosionRateInPerYear: null, remainingLifeYears: null, status: 'Missing Tmin' as const, recommendedAction: 'Add/verify B31.3 Tmin row for this CML before final disposition.' };
    }

    const tmin = matched.requiredThicknessIn;
    const margin = reading.currentThicknessIn - tmin;
    let status: Api570ThicknessStatus = margin < 0 ? 'Below Tmin' : margin <= 0.02 ? 'Monitor' : 'Acceptable';
    let recommendedAction = status === 'Below Tmin'
      ? 'Below Tmin: mark related report item as Issue and Recommendation Required; evaluate repair/mitigation immediately.'
      : status === 'Monitor'
        ? 'Near Tmin: monitor at increased frequency and validate trending.'
        : 'Thickness acceptable versus Tmin; continue routine interval.';

    let corrosionRateInPerYear: number | null = null;
    let remainingLifeYears: number | null = null;
    if (reading.priorThicknessIn != null && reading.priorInspectionDate) {
      const years = Math.abs((new Date(reading.currentInspectionDate).getTime() - new Date(reading.priorInspectionDate).getTime()) / (1000 * 60 * 60 * 24 * 365.25));
      const corr = calculateCorrosionRate({
        initialThicknessInches: reading.priorThicknessIn,
        finalThicknessInches: reading.currentThicknessIn,
        exposureTimeYears: years,
        inspectionFactor: 0.5,
        currentThicknessInches: reading.currentThicknessIn,
        tminInches: tmin
      });
      corrosionRateInPerYear = corr.outputs.corrosionRateInchesPerYear;
      remainingLifeYears = corr.outputs.remainingLifeYears;
    } else {
      warnings.push({ code: 'MISSING_PRIOR_DATA', message: `${reading.cmlId}: prior thickness/date missing; corrosion rate not calculated`, severity: 'info' });
    }
    if (status === 'Below Tmin') warnings.push({ code: 'BELOW_TMIN', message: `${reading.cmlId}: current thickness ${reading.currentThicknessIn.toFixed(4)} in below Tmin ${tmin.toFixed(4)} in`, severity: 'error' });
    return { cmlId: reading.cmlId, location: reading.location, nps: reading.nps, currentThicknessIn: reading.currentThicknessIn, tminIn: tmin, marginToTminIn: margin, corrosionRateInPerYear, remainingLifeYears, status, recommendedAction, matchedB31RowId: matched.rowId };
  });

  return {
    id: `api570-thickness-assessment-${calculatedAt}`,
    calculationType: 'api-570-thickness-assessment',
    displayName: 'API 570 CML Thickness Assessment',
    formulaVersion: 'api570-thickness-assessment-v1',
    standardReferences: [{ standard: 'API 570', note: 'CML thickness assessment and disposition guidance.' }, { standard: 'ASME B31.3', note: 'Saved Tmin basis.' }],
    inputs,
    outputs: { rows },
    warnings,
    calculatedAt,
    insertLabel: `API 570 thickness assessment: ${rows.length} CML rows, ${rows.filter((r)=>r.status==='Below Tmin').length} below Tmin`
  };
}

export function formatApi570AssessmentSummary(snapshot: EngineeringCalculationResult<Api570ThicknessAssessmentInputs, Api570ThicknessAssessmentOutputs>): string {
  return snapshot.outputs.rows.map((r) => `${r.cmlId || r.location} (${r.nps}") ${r.status}; t=${r.currentThicknessIn.toFixed(3)} in, Tmin=${r.tminIn?.toFixed(3) ?? 'N/A'} in, margin=${r.marginToTminIn?.toFixed(3) ?? 'N/A'} in. ${r.recommendedAction}`).join('\n');
}
