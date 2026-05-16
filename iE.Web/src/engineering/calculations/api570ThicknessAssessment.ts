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
  monitorMarginThresholdIn: number;
  cmlReadings: Api570CmlReadingInput[];
};
export type Api570ThicknessAssessmentOutputs = { rows: Api570ThicknessAssessmentRow[] };

const norm = (v?: string) => (v ?? '').trim().toLowerCase();
const DEFAULT_MONITOR_MARGIN_THRESHOLD_IN = 0.02;

export function normalizeNpsValue(value?: string): string {
  const raw = (value ?? '').trim().toLowerCase();
  if (!raw) return '';
  const stripped = raw
    .replace(/^nps\s*/i, '')
    .replace(/in(ch|ches)?$/i, '')
    .replace(/"/g, '')
    .trim();
  const numeric = Number(stripped);
  if (!Number.isFinite(numeric)) return stripped;
  return `${numeric}`;
}

export function assessApi570Thickness(
  inputs: Api570ThicknessAssessmentInputs,
  b31Rows: B31CircuitRowResult[]
): EngineeringCalculationResult<Api570ThicknessAssessmentInputs, Api570ThicknessAssessmentOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();
  const rows = inputs.cmlReadings.map((reading) => {
    const normalizedReadingNps = normalizeNpsValue(reading.nps);
    const npsMatches = b31Rows.filter((r) => normalizeNpsValue(r.nps) === normalizedReadingNps && r.requiredThicknessIn != null);
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
    const monitorMarginThresholdIn = Number.isFinite(inputs.monitorMarginThresholdIn) ? inputs.monitorMarginThresholdIn : DEFAULT_MONITOR_MARGIN_THRESHOLD_IN;
    let status: Api570ThicknessStatus = margin < 0 ? 'Below Tmin' : margin <= monitorMarginThresholdIn ? 'Monitor' : 'Acceptable';
    let recommendedAction = status === 'Below Tmin'
      ? 'Below Tmin: mark related report item as Issue and Recommendation Required; evaluate repair/mitigation immediately.'
      : status === 'Monitor'
        ? 'Near Tmin: monitor at increased frequency and validate trending.'
        : 'Thickness acceptable versus Tmin; continue routine interval.';

    let corrosionRateInPerYear: number | null = null;
    let remainingLifeYears: number | null = null;
    if (!reading.currentInspectionDate) {
      warnings.push({ code: 'MISSING_CURRENT_DATE', message: `${reading.cmlId}: current inspection date is required`, severity: 'warning' });
    }
    if (reading.currentThicknessIn <= 0) {
      warnings.push({ code: 'CURRENT_THICKNESS_NON_POSITIVE', message: `${reading.cmlId}: current thickness must be greater than zero`, severity: 'warning' });
    }
    if (reading.priorThicknessIn != null && reading.priorInspectionDate && reading.currentInspectionDate) {
      const currentDateMs = new Date(reading.currentInspectionDate).getTime();
      const priorDateMs = new Date(reading.priorInspectionDate).getTime();
      const years = (currentDateMs - priorDateMs) / (1000 * 60 * 60 * 24 * 365.25);
      const isPriorDateAfterCurrent = priorDateMs > currentDateMs;
      if (isPriorDateAfterCurrent) {
        warnings.push({ code: 'PRIOR_DATE_AFTER_CURRENT', message: `${reading.cmlId}: prior inspection date is after current inspection date`, severity: 'warning' });
      }

      const hasInvalidDateInterval = !Number.isFinite(years) || years <= 0;
      if (hasInvalidDateInterval) {
        warnings.push({ code: 'INVALID_DATE_INTERVAL', message: `${reading.cmlId}: prior/current inspection dates produce a zero or invalid interval`, severity: 'warning' });
      }

      if (!isPriorDateAfterCurrent && !hasInvalidDateInterval) {
        const corr = calculateCorrosionRate({
          initialThicknessInches: reading.priorThicknessIn,
          finalThicknessInches: reading.currentThicknessIn,
          exposureTimeYears: years,
          inspectionFactor: 0.5,
          currentThicknessInches: reading.currentThicknessIn,
          tminInches: tmin
        });
        corr.warnings.forEach((w) => warnings.push({ ...w, message: `${reading.cmlId}: ${w.message}` }));
        corrosionRateInPerYear = corr.outputs.corrosionRateInchesPerYear;
        remainingLifeYears = corr.outputs.remainingLifeYears;
      }
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


export type Api570FindingDraftSeverity = 'High' | 'Critical';

export type Api570FindingDraft = {
  assessmentSnapshotId: string;
  cmlId: string;
  location: string;
  nps: string;
  currentThicknessIn: number;
  tminIn: number;
  marginToTminIn: number;
  matchedB31RowId?: string;
  recommendation: string;
  severity: Api570FindingDraftSeverity;
};

const CRITICAL_MARGIN_THRESHOLD_IN = -0.05;

export function toApi570FindingDraftsFromAssessment(
  snapshot: EngineeringCalculationResult<Api570ThicknessAssessmentInputs, Api570ThicknessAssessmentOutputs>,
  options?: { includeMonitorRows?: boolean }
): Api570FindingDraft[] {
  const includeMonitorRows = options?.includeMonitorRows ?? false;
  return snapshot.outputs.rows
    .filter((row) => row.status === 'Below Tmin' || (includeMonitorRows && row.status === 'Monitor'))
    .filter((row): row is Api570ThicknessAssessmentRow & { tminIn: number; marginToTminIn: number } => row.tminIn != null && row.marginToTminIn != null)
    .map((row) => {
      const severity: Api570FindingDraftSeverity = row.marginToTminIn <= CRITICAL_MARGIN_THRESHOLD_IN ? 'Critical' : 'High';
      return {
        assessmentSnapshotId: snapshot.id,
        cmlId: row.cmlId,
        location: row.location,
        nps: row.nps,
        currentThicknessIn: row.currentThicknessIn,
        tminIn: row.tminIn,
        marginToTminIn: row.marginToTminIn,
        matchedB31RowId: row.matchedB31RowId,
        recommendation: row.recommendedAction,
        severity
      };
    });
}

export function formatApi570AssessmentSummary(snapshot: EngineeringCalculationResult<Api570ThicknessAssessmentInputs, Api570ThicknessAssessmentOutputs>): string {
  return snapshot.outputs.rows.map((r) => `${r.cmlId || r.location} (${r.nps}") ${r.status}; t=${r.currentThicknessIn.toFixed(3)} in, Tmin=${r.tminIn?.toFixed(3) ?? 'N/A'} in, margin=${r.marginToTminIn?.toFixed(3) ?? 'N/A'} in. ${r.recommendedAction}`).join('\n');
}
