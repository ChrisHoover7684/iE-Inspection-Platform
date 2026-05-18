import { fireEvent, render, screen } from '@testing-library/react';
import { describe, expect, it, vi } from 'vitest';
import { Api510DrumVesselCalculationWorkspace } from '../reporting/Api510DrumVesselCalculationWorkspace';
import { executeApi510ComponentCalculation } from '../reporting/componentCalculationExecution';

vi.mock('../reporting/componentCalculationExecution', async () => {
  const actual = await vi.importActual<typeof import('../reporting/componentCalculationExecution')>('../reporting/componentCalculationExecution');
  return { ...actual, executeApi510ComponentCalculation: vi.fn() };
});

describe('Api510DrumVesselCalculationWorkspace', () => {
  it('workspace renders subtype selector', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    expect(screen.getByLabelText('Equipment Subtype')).toBeInTheDocument();
  });

  it('Shell shows UG-27 inputs', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    expect(screen.getByLabelText('insideDiameterIn')).toBeInTheDocument();
    expect(screen.getByLabelText('outsideDiameterIn')).toBeInTheDocument();
  });

  it('Heads show UG-32 inputs and head type geometry switches', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'heads' } });
    expect(screen.getByLabelText('Head Type')).toBeInTheDocument();
    expect(screen.getByLabelText('effectiveInsideDiameterIn')).toBeInTheDocument();
    fireEvent.change(screen.getByLabelText('Head Type'), { target: { value: 'Hemispherical' } });
    expect(screen.getByLabelText('effectiveInsideRadiusIn')).toBeInTheDocument();
    expect(screen.queryByLabelText('effectiveInsideDiameterIn')).not.toBeInTheDocument();
  });

  it('Nozzles show UG-45 and parent/nozzle fields', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'nozzles' } });
    expect(screen.getByLabelText('Parent Component')).toBeInTheDocument();
    expect(screen.getByLabelText('Nozzle Location')).toBeInTheDocument();
    expect(screen.getByLabelText('nominalPipeSize')).toBeInTheDocument();
  });

  it('review-only components do not execute', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'supports' } });
    expect(screen.getByRole('button', { name: 'Run Calculation' })).toBeDisabled();
  });

  it('successful calculation displays snapshot preview and save disabled without report context', async () => {
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shared', inputsUsed: {}, resultSummary: 'ok', warnings: [], snapshotReadyPayload: { id: 'snap' } } as any);
    render(<Api510DrumVesselCalculationWorkspace fieldValues={{ 'api510.external.drum-vessel.design-pressure': 100, 'api510.external.drum-vessel.design-temperature': 200 }} />);
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    expect(await screen.findByText(/Snapshot Preview: available/i)).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Save Calculation Snapshot' })).toBeDisabled();
  });

  it('finding draft includes equipment subtype/component context', async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shared', inputsUsed: {}, resultSummary: 'ok', warnings: [], snapshotReadyPayload: { id: 's1', inputs: { prefill: { equipmentSubtype: 'Vertical Drum' } } } } as any);
    render(<Api510DrumVesselCalculationWorkspace onCreateFindingDraft={onCreate} fieldValues={{ 'api510.external.drum-vessel.design-pressure': 100, 'api510.external.drum-vessel.design-temperature': 200 }} />);
    fireEvent.change(screen.getByLabelText('Equipment Subtype'), { target: { value: 'Vertical Drum' } });
    fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(screen.getByRole('button', { name: 'Create Finding from Calculation' }));
    expect(onCreate).toHaveBeenCalledWith(expect.objectContaining({ equipmentSubtype: 'Vertical Drum', componentKey: 'shell' }));
  });
});
