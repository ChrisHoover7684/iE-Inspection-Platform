import type { B313ThicknessInput, B313ThicknessResult, InspectionReport } from '../../types';
import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type B31CircuitSourceMetadata = {
  pressure: { value: number | null; source: string; isManual: boolean };
  temperature: { value: number | null; source: string; isManual: boolean };
};

export type B31CircuitSharedInputs = {
  pressurePsi: number;
  temperatureF: number;
  wFactor?: number;
  yOverride?: number | null;
  eOverride?: number | null;
  defaultJointType: string;
  defaultJointQualityKey: string;
  sourceMetadata: B31CircuitSourceMetadata;
};

export type B31CircuitRowInput = {
  id: string;
  nps: string;
  materialPreset?: string;
  schedule?: string;
  outsideDiameterIn?: number | null;
  spec: string;
  grade: string;
  productForm: string;
  unsNo?: string;
  classConditionTemper?: string;
  materialCategory: string;
  jointType?: string;
  jointQualityKey?: string;
  note?: string;
};

export type B31MaterialPresetKey = 'A106_GR_B_SEAMLESS' | 'A53_GR_B_ERW_EB' | 'CUSTOM';

export type B31MaterialPreset = {
  key: B31MaterialPresetKey;
  label: string;
  spec: string;
  grade: string;
  productForm: string;
  materialCategory: string;
  jointType: string;
  jointQualityKey: string;
};

export const B31_MATERIAL_PRESETS: B31MaterialPreset[] = [
  { key: 'A106_GR_B_SEAMLESS', label: 'A106 Gr B seamless', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless' },
  { key: 'A53_GR_B_ERW_EB', label: 'A53 Gr B ERW / E-B', spec: 'A53', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'ERW', jointQualityKey: 'E/B' },
  { key: 'CUSTOM', label: 'Custom', spec: '', grade: '', productForm: '', materialCategory: '', jointType: '', jointQualityKey: '' }
];

export function applyMaterialPreset(row: B31CircuitRowInput, presetKey: B31MaterialPresetKey): B31CircuitRowInput {
  const preset = B31_MATERIAL_PRESETS.find((candidate) => candidate.key === presetKey);
  if (!preset) return row;
  return {
    ...row,
    materialPreset: preset.key,
    spec: preset.spec,
    grade: preset.grade,
    productForm: preset.productForm,
    materialCategory: preset.materialCategory,
    jointType: preset.jointType,
    jointQualityKey: preset.jointQualityKey
  };
}

export type B31CircuitRowResult = {
  rowId: string;
  nps: string;
  materialPreset?: string;
  schedule?: string;
  spec?: string;
  grade?: string;
  productForm?: string;
  materialCategory?: string;
  jointType?: string;
  jointQualityKey?: string;
  note?: string;
  material: string;
  outsideDiameterIn: number | null;
  success: boolean;
  message: string;
  allowableStressPsi: number | null;
  eFactor: number | null;
  yCoefficient: number | null;
  wFactor: number | null;
  requiredThicknessIn: number | null;
  input: B313ThicknessInput | null;
  warnings: string[];
};

export type B31CircuitBatchOutputs = {
  rows: B31CircuitRowResult[];
  successfulRows: number;
  failedRows: number;
  governingRequiredThicknessIn: number | null;
};

export const NPS_OD_LOOKUP: Record<string, number> = {
  '0.5': 0.84,
  '0.75': 1.05,
  '1': 1.315,
  '1.5': 1.9,
  '2': 2.375,
  '3': 3.5,
  '4': 4.5,
  '6': 6.625,
  '8': 8.625,
  '10': 10.75,
  '12': 12.75
};

const SOURCE_HINTS = {
  pressure: ['design pressure', 'operating pressure', 'evaluation pressure', 'pressure psig'],
  temperature: ['design temperature', 'operating temperature', 'evaluation temperature', 'temperature f']
};

export function resolveCircuitConditionsFromReport(report: InspectionReport | null): B31CircuitSourceMetadata {
  const defaultMeta: B31CircuitSourceMetadata = {
    pressure: { value: null, source: 'Not found', isManual: false },
    temperature: { value: null, source: 'Not found', isManual: false }
  };
  if (!report) return defaultMeta;
  const answers = report.sections.flatMap((section) => section.answers.map((answer) => ({ section: section.sectionTitle, answer })));
  const match = (hints: string[]) => {
    const found = answers.find(({ answer }) => {
      const label = `${answer.label} ${answer.fieldId}`.toLowerCase();
      return hints.some((h) => label.includes(h));
    });
    if (!found) return { value: null, source: 'Not found' };
    const raw = `${found.answer.value ?? found.answer.comment ?? ''}`;
    const parsed = Number(raw.replace(/[^0-9.+-]/g, ''));
    return { value: Number.isFinite(parsed) ? parsed : null, source: found.section };
  };
  const p = match(SOURCE_HINTS.pressure);
  const t = match(SOURCE_HINTS.temperature);
  return {
    pressure: { value: p.value, source: p.source, isManual: false },
    temperature: { value: t.value, source: t.source, isManual: false }
  };
}

export function buildCircuitBatchSnapshot(
  shared: B31CircuitSharedInputs,
  rows: B31CircuitRowResult[],
): EngineeringCalculationResult<{ shared: B31CircuitSharedInputs; rows: B31CircuitRowInput[] }, B31CircuitBatchOutputs> {
  const calculatedAt = new Date().toISOString();
  const warnings: EngineeringCalculationWarning[] = [];
  if (shared.sourceMetadata.pressure.value == null || shared.sourceMetadata.temperature.value == null) {
    warnings.push({ code: 'CIRCUIT_PRESSURE_OR_TEMPERATURE_MISSING', message: 'Circuit pressure and/or temperature were not found in report context.', severity: 'warning' });
  }
  if (shared.sourceMetadata.pressure.isManual || shared.sourceMetadata.temperature.isManual) {
    warnings.push({ code: 'CIRCUIT_PRESSURE_OR_TEMPERATURE_MANUAL_OVERRIDE', message: 'Circuit pressure and/or temperature were manually supplied.', severity: 'warning' });
  }
  rows.forEach((r) => {
    r.warnings.forEach((w) => warnings.push({ code: 'ROW_WARNING', message: `${r.nps}: ${w}`, severity: 'warning' }));
    if (!r.success) warnings.push({ code: 'B313_ROW_FAILED', message: `${r.nps}: ${r.message}`, severity: 'error' });
  });
  const required = rows.map((r) => r.requiredThicknessIn ?? 0);
  const governing = required.length > 0 ? Math.max(...required) : null;
  const failed = rows.filter((r) => !r.success).length;
  return {
    id: `b31-3-piping-circuit-batch-${calculatedAt}`,
    calculationType: 'b31-3-piping-circuit-batch',
    displayName: 'B31.3 Circuit Tmin Batch',
    formulaVersion: 'asme-b31.3-circuit-batch-v1',
    standardReferences: [
      { standard: 'ASME B31.3', note: 'Required thickness evaluation for piping.' },
      { standard: 'API 570', note: 'Inspection and fitness-for-service workflow context.' }
    ],
    inputs: { shared, rows: rows.map((r) => ({ id: r.rowId, nps: r.nps, materialPreset: r.materialPreset, schedule: r.schedule, outsideDiameterIn: r.outsideDiameterIn, spec: r.spec ?? '', grade: r.grade ?? '', productForm: r.productForm ?? '', materialCategory: r.materialCategory ?? '', jointType: r.jointType ?? '', jointQualityKey: r.jointQualityKey ?? '', note: r.note })) },
    outputs: { rows, successfulRows: rows.length - failed, failedRows: failed, governingRequiredThicknessIn: governing },
    warnings,
    calculatedAt,
    insertLabel: `B31.3 circuit batch: ${rows.length} rows, ${failed} failed, governing Tmin ${governing?.toFixed(4) ?? 'N/A'} in`
  };
}

export type B31CircuitResultRowDisplay = {
  nps: string;
  material: string;
  outsideDiameterIn: number | null;
  allowableStressPsi: number | null;
  eFactor: number | null;
  yCoefficient: number | null;
  wFactor: number | null;
  requiredThicknessIn: number | null;
  statusMessage: string;
};

export function toResultDisplayRows(rows: B31CircuitRowResult[]): B31CircuitResultRowDisplay[] {
  return rows.map((row) => ({
    nps: row.nps,
    material: row.material,
    outsideDiameterIn: row.outsideDiameterIn,
    allowableStressPsi: row.allowableStressPsi,
    eFactor: row.eFactor,
    yCoefficient: row.yCoefficient,
    wFactor: row.wFactor,
    requiredThicknessIn: row.requiredThicknessIn,
    statusMessage: row.success ? 'OK' : row.message
  }));
}

export function formatCircuitBatchSummary(snapshot: EngineeringCalculationResult<{ shared: B31CircuitSharedInputs; rows: B31CircuitRowInput[] }, B31CircuitBatchOutputs>): string {
  const pressure = snapshot.inputs.shared.pressurePsi;
  const temperature = snapshot.inputs.shared.temperatureF;
  const lines = [`B31.3 circuit Tmin batch at ${pressure} psig / ${temperature}°F:`];
  snapshot.outputs.rows.forEach((row) => {
    if (row.success && row.requiredThicknessIn != null) {
      lines.push(`- ${row.nps} in ${row.material}: Tmin ${row.requiredThicknessIn.toFixed(4)} in`);
    }
  });
  const failedRows = snapshot.outputs.rows.filter((row) => !row.success).map((row) => `${row.nps} in ${row.material}: ${row.message}`);
  if (failedRows.length > 0) lines.push(`Failed rows: ${failedRows.join('; ')}`);
  return lines.join('\n');
}

export function mapB313RowResult(rowId: string, row: B31CircuitRowInput, input: B313ThicknessInput | null, result: B313ThicknessResult | null, warnings: string[]): B31CircuitRowResult {
  return {
    rowId,
    nps: row.nps,
    materialPreset: row.materialPreset,
    schedule: row.schedule,
    spec: row.spec,
    grade: row.grade,
    productForm: row.productForm,
    materialCategory: row.materialCategory,
    jointType: row.jointType,
    jointQualityKey: row.jointQualityKey,
    note: row.note,
    material: `${row.spec} ${row.grade}`.trim(),
    outsideDiameterIn: row.outsideDiameterIn ?? input?.outsideDiameterIn ?? null,
    success: result?.success ?? false,
    message: result?.message ?? 'Input preparation failed.',
    allowableStressPsi: result?.allowableStressPsi ?? null,
    eFactor: result?.eFactor ?? null,
    yCoefficient: result?.yCoefficient ?? null,
    wFactor: result?.wFactor ?? null,
    requiredThicknessIn: result?.requiredThicknessIn ?? null,
    input,
    warnings
  };
}
