import { fireEvent, render, screen } from '@testing-library/react';
import { describe, expect, it, vi } from 'vitest';
import { Api510ShellTubeCalculationWorkspace } from '../reporting/Api510ShellTubeCalculationWorkspace';
import { executeApi510ComponentCalculation } from '../reporting/componentCalculationExecution';

vi.mock('../reporting/componentCalculationExecution', async () => {
  const actual = await vi.importActual<typeof import('../reporting/componentCalculationExecution')>('../reporting/componentCalculationExecution');
  return { ...actual, executeApi510ComponentCalculation: vi.fn() };
});

describe('Api510ShellTubeCalculationWorkspace', () => {
  it('workspace renders component selector', () => {
    render(<Api510ShellTubeCalculationWorkspace />);
    expect(screen.getByLabelText('Component')).toBeInTheDocument();
  });

  it('Save Snapshot enabled only with report context and snapshot payload; callback receives snapshot', async () => {
    const onSave = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'ok', warnings: [], snapshotReadyPayload: { id: 'snap-1' } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext onSaveSnapshot={onSave} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    const save = screen.getByRole('button', { name: 'Save Calculation Snapshot' });
    expect(save).toBeDisabled();
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    expect(await screen.findByText(/Snapshot Preview: available/i)).toBeInTheDocument();
    expect(save).toBeEnabled();
    fireEvent.click(save);
    expect(onSave).toHaveBeenCalledWith(expect.objectContaining({ id: 'snap-1' }));
  });

  it('duplicate snapshot save is prevented', async () => {
    const onSave = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'ok', warnings: [], snapshotReadyPayload: { id: 'snap-dup' } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext onSaveSnapshot={onSave} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Snapshot Preview: available/i);
    const save = screen.getByRole('button', { name: 'Save Calculation Snapshot' });
    fireEvent.click(save);
    fireEvent.click(save);
    expect(onSave).toHaveBeenCalledTimes(1);
  });

  it('finding before saving marks linkedSnapshotPendingSave true', async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', designPressureUsed: 150, designTemperatureUsed: 400, inputsUsed: {}, resultSummary: 'UG-27 calculation executed.', warnings: [], snapshotReadyPayload: { id: 's1', inputs: { prefill: { equipmentSubtype: 'Shell and Tube Exchanger' } } } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext onCreateFindingDraft={onCreate} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(screen.getByRole('button', { name: 'Create Finding from Calculation' }));
    expect(onCreate).toHaveBeenCalledWith(expect.objectContaining({ componentKey: 'shell', linkedCalculationSnapshotId: undefined, linkedSnapshotPendingSave: true }));
  });

  it('finding after saving includes linkedCalculationSnapshotId', async () => {
    const onCreate = vi.fn();
    const onSave = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', designPressureUsed: 150, designTemperatureUsed: 400, inputsUsed: {}, resultSummary: 'UG-27 calculation executed.', warnings: [], snapshotReadyPayload: { id: 's-saved', inputs: { prefill: { equipmentSubtype: 'Shell and Tube Exchanger' } } } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext onSaveSnapshot={onSave} onCreateFindingDraft={onCreate} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(screen.getByRole('button', { name: 'Save Calculation Snapshot' }));
    fireEvent.click(screen.getByRole('button', { name: 'Create Finding from Calculation' }));
    expect(onCreate).toHaveBeenCalledWith(expect.objectContaining({ linkedCalculationSnapshotId: 's-saved', linkedSnapshotPendingSave: false }));
  });

  it('finding with existing reportCalculations snapshot includes linkedCalculationSnapshotId', async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', designPressureUsed: 150, designTemperatureUsed: 400, inputsUsed: {}, resultSummary: 'UG-27 calculation executed.', warnings: [], snapshotReadyPayload: { id: 's-existing', inputs: { prefill: { equipmentSubtype: 'Shell and Tube Exchanger' } } } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext reportCalculations={[{ id: 's-existing' } as any]} onCreateFindingDraft={onCreate} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(screen.getByRole('button', { name: 'Create Finding from Calculation' }));
    expect(onCreate).toHaveBeenCalledWith(expect.objectContaining({ linkedCalculationSnapshotId: 's-existing', linkedSnapshotPendingSave: false }));
  });

  it('UG-45 finding includes parent component and nozzle location', async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-45-nozzle-neck-tmin', componentKey: 'nozzles', componentLabel: 'Nozzles', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'UG-45 calculation executed.', warnings: [], snapshotReadyPayload: { id: 's2', inputs: { prefill: { selectedParentComponent: 'shell', selectedNozzleLocation: 'shell' } } } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext onCreateFindingDraft={onCreate} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'nozzles' } });
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(screen.getByRole('button', { name: 'Create Finding from Calculation' }));
    expect(onCreate).toHaveBeenCalledWith(expect.objectContaining({ parentComponent: 'shell', nozzleLocation: 'shell' }));
  });

  it('failed calculation creates recommendation text', async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: false, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'failed', warnings: ['missing required context'] } as any);
    render(<Api510ShellTubeCalculationWorkspace onCreateFindingDraft={onCreate} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Execution: failure/i);
    fireEvent.click(screen.getByRole('button', { name: 'Create Recommendation' }));
    expect(onCreate).toHaveBeenCalledWith(expect.objectContaining({ recommendationDraft: expect.stringContaining('Recommendation:') }));
  });

  it('standalone workspace still works without report context', async () => {
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'ok', warnings: [], snapshotReadyPayload: { id: 'x' } } as any);
    render(<Api510ShellTubeCalculationWorkspace hasReportContext={false} fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    expect(await screen.findByText(/Snapshot Preview: available/i)).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Save Calculation Snapshot' })).toBeDisabled();
  });
});
