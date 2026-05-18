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

  it('Shell component shows UG-27 inputs', () => {
    render(<Api510ShellTubeCalculationWorkspace />);
    expect(screen.getByLabelText('insideDiameterIn')).toBeInTheDocument();
    expect(screen.queryByLabelText('shellOrHeadRequiredThicknessIn')).not.toBeInTheDocument();
  });

  it('Nozzle component shows UG-45 inputs and parent/nozzle fields', () => {
    render(<Api510ShellTubeCalculationWorkspace />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'nozzles' } });
    expect(screen.getByLabelText('Parent Component')).toBeInTheDocument();
    expect(screen.getByLabelText('Nozzle Location')).toBeInTheDocument();
    expect(screen.getByLabelText('shellOrHeadRequiredThicknessIn')).toBeInTheDocument();
  });

  it('prefill warnings are displayed', () => {
    render(<Api510ShellTubeCalculationWorkspace fieldValues={{}} />);
    expect(screen.getAllByText(/warning:/i).length).toBeGreaterThan(0);
  });

  it('execution success displays snapshot preview', async () => {
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'ok', warnings: [], snapshotReadyPayload: { id: 'x' } } as any);
    render(<Api510ShellTubeCalculationWorkspace fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    expect(await screen.findByText(/Snapshot Preview: available/i)).toBeInTheDocument();
  });

  it('execution failure displays warnings', async () => {
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: false, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shell-side', inputsUsed: {}, resultSummary: 'failed', warnings: ['bad input'] } as any);
    render(<Api510ShellTubeCalculationWorkspace fieldValues={{ 'api510.external.exchanger.shell-tube.shell-side.design-pressure': 100, 'api510.external.exchanger.shell-tube.shell-side.design-temperature': 100 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    expect(await screen.findByText(/exec warning: bad input/i)).toBeInTheDocument();
  });

  it('Save Snapshot disabled when no report context exists', () => {
    render(<Api510ShellTubeCalculationWorkspace hasReportContext={false} />);
    expect(screen.getByRole('button', { name: 'Save Calculation Snapshot' })).toBeDisabled();
  });
});
