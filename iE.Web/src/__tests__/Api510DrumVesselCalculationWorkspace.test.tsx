import { fireEvent, render, screen } from '@testing-library/react';
import { afterEach, describe, expect, it, vi } from 'vitest';
import { Api510DrumVesselCalculationWorkspace } from '../reporting/Api510DrumVesselCalculationWorkspace';
import { executeApi510ComponentCalculation } from '../reporting/componentCalculationExecution';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from '../reporting/componentCalculationPrefill';

vi.mock('../reporting/componentCalculationExecution', async () => {
  const actual = await vi.importActual<typeof import('../reporting/componentCalculationExecution')>('../reporting/componentCalculationExecution');
  return { ...actual, executeApi510ComponentCalculation: vi.fn() };
});

afterEach(() => {
  window.localStorage.removeItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
});

describe('Api510DrumVesselCalculationWorkspace', () => {
  it('workspace renders subtype selector', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    expect(screen.getByLabelText('Equipment Subtype')).toBeInTheDocument();
  });

  it('reads draft vessel design conditions, subtype, selected components, and head type', () => {
    window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify({
      reportTypeId: 'api510.external.drum-vessel.vertical-drum',
      reportTypeLabel: 'Vertical Drum External',
      header: {
        vesselDesignPressure: '300',
        vesselDesignTemperature: '650',
        headType: 'Hemispherical'
      },
      components: ['Shell', 'Heads', 'Nozzles', 'Supports', 'Insulation']
    }));

    render(<Api510DrumVesselCalculationWorkspace />);

    expect(screen.getByRole('status')).toHaveTextContent('Using draft setup from Start Wizard');
    expect(screen.getByLabelText('Equipment Subtype')).toHaveValue('Vertical Drum');
    expect(screen.getByText('Design Pressure: api510.external.drum-vessel.design-pressure = 300')).toBeInTheDocument();
    expect(screen.getByText('Design Temperature: api510.external.drum-vessel.design-temperature = 650')).toBeInTheDocument();
    expect(screen.getByRole('region', { name: 'Draft Context' })).toHaveTextContent('Insulation');
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'heads' } });
    expect(screen.getByLabelText('Head Type')).toHaveValue('Hemispherical');
    expect(screen.getByLabelText('Effective Inside Radius (in.)')).toBeInTheDocument();
  });



  it('shows user-facing API 510 drum/vessel labels, cards, dropdowns, and helper text', () => {
    render(<Api510DrumVesselCalculationWorkspace />);

    expect(screen.getByRole('group', { name: 'Component Selection' })).toBeInTheDocument();
    expect(screen.getByRole('region', { name: 'Resolved Design Conditions' })).toBeInTheDocument();
    expect(screen.getByRole('group', { name: 'Material / Allowable Stress' })).toBeInTheDocument();
    expect(screen.getByRole('group', { name: 'Calculation Inputs' })).toBeInTheDocument();
    expect(screen.getByRole('region', { name: 'Results / Snapshot' })).toBeInTheDocument();
    expect(screen.getByText('Allowable stress should be resolved from material tables. Manual override requires a reason.')).toBeInTheDocument();
    expect(screen.getByText('Save Snapshot is enabled only when this workspace is opened from a report context.')).toBeInTheDocument();
    expect(screen.getByLabelText('Joint Efficiency')).toBeInTheDocument();
    expect(screen.getByLabelText('Inside Diameter (in.)')).toBeInTheDocument();
    expect(screen.getByLabelText('Outside Diameter (in.)')).toBeInTheDocument();
    expect(screen.getByLabelText('Original/Nominal Thickness (in.)')).toBeInTheDocument();
    expect(screen.getByLabelText('Current / Provided Thickness (in.)')).toBeInTheDocument();
    expect(screen.getByLabelText('Corrosion Allowance (in.)')).toBeInTheDocument();
    expect(screen.queryByText('jointEfficiency')).not.toBeInTheDocument();
    expect(screen.queryByText('insideDiameterIn')).not.toBeInTheDocument();
    expect(screen.queryByText('outsideDiameterIn')).not.toBeInTheDocument();
  });

  it('Shell shows UG-27 inputs', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    expect(screen.getByLabelText('Inside Diameter (in.)')).toBeInTheDocument();
    expect(screen.getByLabelText('Outside Diameter (in.)')).toBeInTheDocument();
  });

  it('Heads show UG-32 inputs and head type geometry switches', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'heads' } });
    expect(screen.getByLabelText('Head Type')).toBeInTheDocument();
    expect(screen.getByLabelText('Effective Inside Diameter (in.)')).toBeInTheDocument();
    fireEvent.change(screen.getByLabelText('Head Type'), { target: { value: 'Hemispherical' } });
    expect(screen.getByLabelText('Effective Inside Radius (in.)')).toBeInTheDocument();
    expect(screen.queryByLabelText('Effective Inside Diameter (in.)')).not.toBeInTheDocument();
  });

  it('Nozzles show UG-45 and parent/nozzle fields', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    fireEvent.change(screen.getByLabelText('Component'), { target: { value: 'nozzles' } });
    expect(screen.getByLabelText('Parent Component')).toBeInTheDocument();
    expect(screen.getByLabelText('Nozzle Location')).toBeInTheDocument();
    expect(screen.getByLabelText('Nominal Pipe Size')).toBeInTheDocument();
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

  it('standalone workspace does not show draft banner without draft setup', () => {
    render(<Api510DrumVesselCalculationWorkspace />);
    expect(screen.queryByText('Using draft setup from Start Wizard')).not.toBeInTheDocument();
    expect(screen.getByLabelText('Vessel Design Pressure')).toBeInTheDocument();
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


it('standalone vessel design conditions are passed via panel', async () => {
  vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({ success: true, calculationType: 'ug-27-shell-tmin', componentKey: 'shell', componentLabel: 'Shell', pressureSide: 'shared', inputsUsed: {}, resultSummary: 'ok', warnings: [] } as any);
  render(<Api510DrumVesselCalculationWorkspace hasReportContext={false} />);
  fireEvent.change(screen.getByLabelText('Vessel Design Pressure'), { target: { value: '200' } });
  fireEvent.change(screen.getByLabelText('Vessel Design Temperature'), { target: { value: '600' } });
  fireEvent.change(screen.getByLabelText('Resolved Allowable Stress (psi)'), { target: { value: '16000' } });
  fireEvent.change(screen.getByLabelText('Joint Efficiency'), { target: { value: '1' } });
  fireEvent.change(screen.getByLabelText('Inside Diameter (in.)'), { target: { value: '48' } });
  fireEvent.change(screen.getByLabelText('Outside Diameter (in.)'), { target: { value: '48.5' } });
  fireEvent.change(screen.getByLabelText('Original/Nominal Thickness (in.)'), { target: { value: '0.5' } });
  fireEvent.change(screen.getByLabelText('Current / Provided Thickness (in.)'), { target: { value: '0.5' } });
  fireEvent.change(screen.getByLabelText('Corrosion Allowance (in.)'), { target: { value: '0' } });
  fireEvent.click(screen.getByRole('button', { name: 'Run Calculation' }));
  expect(vi.mocked(executeApi510ComponentCalculation)).toHaveBeenCalledWith(expect.objectContaining({ prefill: expect.objectContaining({ designPressureValue: '200', designTemperatureValue: '600' }) }));
});
