import type { InspectionCalculationSnapshot } from '../engineering/types';
import type { ComponentCalculationExecutionResult } from './componentCalculationExecution';

export type Api510FindingDraft = {
  equipmentSubtype: string;
  componentKey: string;
  componentLabel: string;
  calculationType: string;
  pressureSide: string;
  designPressureUsed?: number;
  designTemperatureUsed?: number;
  parentComponent?: string;
  nozzleLocation?: string;
  resultSummary: string;
  warnings: string[];
  linkedCalculationSnapshotId?: string;
  linkedSnapshotPendingSave: boolean;
  recommendationDraft?: string;
};

const shouldCreateRecommendation = (execution: ComponentCalculationExecutionResult, forceRecommendation: boolean) => {
  if (forceRecommendation) return true;
  if (!execution.success) return true;
  if (execution.warnings.some((w) => /missing|required|unsupported|not acceptable/i.test(w))) return true;
  const json = JSON.stringify(execution.snapshotReadyPayload?.outputs ?? {});
  return /"isAcceptable"\s*:\s*false/i.test(json) || /"isValid"\s*:\s*false/i.test(json);
};

export function buildApi510FindingDraft(
  execution: ComponentCalculationExecutionResult,
  snapshot: InspectionCalculationSnapshot | undefined,
  forceRecommendation: boolean
): Api510FindingDraft {
  const prefill = (snapshot?.inputs as { prefill?: { equipmentSubtype?: string; selectedParentComponent?: string; selectedNozzleLocation?: string } } | undefined)?.prefill;
  const pending = !snapshot;
  const recommendationRequired = shouldCreateRecommendation(execution, forceRecommendation);
  return {
    equipmentSubtype: prefill?.equipmentSubtype ?? 'Shell and Tube Exchanger',
    componentKey: execution.componentKey,
    componentLabel: execution.componentLabel,
    calculationType: execution.calculationType,
    pressureSide: execution.pressureSide,
    designPressureUsed: execution.designPressureUsed,
    designTemperatureUsed: execution.designTemperatureUsed,
    parentComponent: prefill?.selectedParentComponent,
    nozzleLocation: prefill?.selectedNozzleLocation,
    resultSummary: execution.resultSummary,
    warnings: execution.warnings,
    linkedCalculationSnapshotId: snapshot?.id,
    linkedSnapshotPendingSave: pending,
    recommendationDraft: recommendationRequired ? `Recommendation: review ${execution.componentLabel} condition and plan corrective action. ${pending ? 'Linked snapshot pending save.' : `Linked snapshot: ${snapshot.id}.`}` : undefined
  };
}
