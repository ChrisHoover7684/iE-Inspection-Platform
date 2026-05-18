import { cleanup, fireEvent, render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it, vi } from 'vitest';
import App from '../App';

vi.mock('../api', async () => {
  const actual = await vi.importActual<typeof import('../api')>('../api');
  return {
    ...actual,
    reportingApi: {
      ...actual.reportingApi,
      getInstances: vi.fn().mockResolvedValue([])
    }
  };
});

const renderReportsPage = async () => {
  render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
  expect(await screen.findByRole('heading', { name: 'Start New API Inspection Report' })).toBeInTheDocument();
};

describe('ApiInspectionReportStartWizard', () => {
  it('renders hierarchy on /reports including API 510 external groups and no internal options', async () => {
    await renderReportsPage();
    expect(screen.getByRole('heading', { name: 'Exchangers' })).toBeInTheDocument();
    expect(screen.getByLabelText('Shell and Tube Exchanger External')).toBeInTheDocument();
    expect(screen.getByLabelText('Plate and Frame Exchanger External')).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Drums / Pressure Vessels' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Towers / Columns' })).toBeInTheDocument();
    expect(screen.queryByText(/Internal/i)).not.toBeInTheDocument();
  });

  it('renders hierarchy when report instance fetch fails', async () => {
    const { reportingApi } = await import('../api');
    vi.mocked(reportingApi.getInstances).mockRejectedValueOnce(new Error('403 Forbidden'));
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    expect(await screen.findByRole('heading', { name: 'Start New API Inspection Report' })).toBeInTheDocument();
    expect(await screen.findByText(/Report list error:/i)).toBeInTheDocument();
  });

  it('selecting Shell and Tube shows editable shell/tube fields', async () => {
    await renderReportsPage();
    fireEvent.click(screen.getByLabelText('Shell and Tube Exchanger External'));

    expect(await screen.findByLabelText('Shell Side Design Pressure')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Side Design Temperature')).toBeInTheDocument();
    expect(screen.getByLabelText('Tube Side Design Pressure')).toBeInTheDocument();
    expect(screen.getByLabelText('Tube Side Design Temperature')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Material Spec')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Material Grade')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Product Form')).toBeInTheDocument();
    expect(screen.getByLabelText('Tube/Channel Material Spec')).toBeInTheDocument();
    expect(screen.getByLabelText('Tube/Channel Material Grade')).toBeInTheDocument();
    expect(screen.getByLabelText('Tube/Channel Product Form')).toBeInTheDocument();
  });

  it('selecting Horizontal Drum shows editable vessel/head fields', async () => {
    await renderReportsPage();
    fireEvent.click(screen.getByLabelText('Horizontal Drum External'));

    expect(await screen.findByLabelText('Vessel Design Pressure')).toBeInTheDocument();
    expect(screen.getByLabelText('Vessel Design Temperature')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Material Spec')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Material Grade')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Product Form')).toBeInTheDocument();
    expect(screen.getByLabelText('Head Material Spec')).toBeInTheDocument();
    expect(screen.getByLabelText('Head Material Grade')).toBeInTheDocument();
    expect(screen.getByLabelText('Head Product Form')).toBeInTheDocument();
    expect(screen.getByLabelText('Head Type')).toBeInTheDocument();
  });

  it('selecting Tower shows editable tower fields', async () => {
    await renderReportsPage();
    fireEvent.click(screen.getByLabelText('Distillation Tower External'));

    expect(await screen.findByLabelText('Vessel Design Pressure')).toBeInTheDocument();
    expect(screen.getByLabelText('Vessel Design Temperature')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Course Material Spec')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Course Material Grade')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Course Product Form')).toBeInTheDocument();
  });

  it('optional components are checkboxes and default components are selected', async () => {
    await renderReportsPage();
    fireEvent.click(screen.getByLabelText('Shell and Tube Exchanger External'));

    const defaultComponent = await screen.findByLabelText('Shell') as HTMLInputElement;
    const optionalComponent = screen.getByLabelText('Shell Cover') as HTMLInputElement;
    expect(defaultComponent.type).toBe('checkbox');
    expect(defaultComponent.checked).toBe(true);
    expect(defaultComponent.disabled).toBe(true);
    expect(optionalComponent.type).toBe('checkbox');
    expect(optionalComponent.checked).toBe(false);

    fireEvent.click(optionalComponent);
    expect(optionalComponent.checked).toBe(true);
    expect(screen.getByText('4 components selected.')).toBeInTheDocument();
  });

  it('Start Draft Report shows draft setup prepared message', async () => {
    await renderReportsPage();
    fireEvent.change(screen.getByLabelText('Inspector'), { target: { value: 'A. Inspector' } });
    fireEvent.click(screen.getByRole('button', { name: 'Start Draft Report' }));

    expect(await screen.findByRole('status')).toHaveTextContent('Draft setup prepared. Full report creation will be connected next.');
  });


  it('enables calculation workspace for supported API 510 setup types and disables it for unsupported types', async () => {
    await renderReportsPage();
    fireEvent.click(screen.getByLabelText('Shell and Tube Exchanger External'));
    expect(await screen.findByRole('button', { name: 'Open Calculation Workspace' })).toBeEnabled();

    fireEvent.click(screen.getByLabelText('Horizontal Drum External'));
    expect(screen.getByRole('button', { name: 'Open Calculation Workspace' })).toBeEnabled();

    fireEvent.click(screen.getByLabelText('Distillation Tower External'));
    expect(screen.getByRole('button', { name: 'Open Calculation Workspace' })).toBeDisabled();
    expect(screen.getByText('Calculation workspace coming soon')).toBeInTheDocument();
  });

  it('Allowable Stress is not present as a normal input', async () => {
    await renderReportsPage();
    fireEvent.click(screen.getByLabelText('Shell and Tube Exchanger External'));

    expect(screen.queryByLabelText(/Allowable Stress/i)).not.toBeInTheDocument();
    expect(screen.getByText('Allowable stress will be resolved from material tables during calculations.')).toBeInTheDocument();
  });

  it('shows API 570 editable setup fields by default', async () => {
    await renderReportsPage();
    expect(screen.getByLabelText('Inspection Date')).toBeInTheDocument();
    expect(screen.getByLabelText('Inspector')).toBeInTheDocument();
    expect(screen.getByLabelText('Design Pressure')).toBeInTheDocument();
    expect(screen.getByLabelText('Design Temperature')).toBeInTheDocument();
    expect(screen.getByLabelText('Circuit ID')).toBeInTheDocument();
    expect(screen.getByLabelText('Line Number(s)')).toBeInTheDocument();
    expect(screen.getByLabelText('Piping Class')).toBeInTheDocument();
    expect(screen.getByLabelText('Pipe size/material rows')).toBeInTheDocument();
  });

  it('existing workspace routes still render', () => {
    render(<MemoryRouter initialEntries={['/reports/api-570-piping-external']}><App /></MemoryRouter>);
    expect(screen.getByText(/Loading API 570 Piping External report/i)).toBeInTheDocument();
    cleanup();

    render(<MemoryRouter initialEntries={['/reports/api-510-shell-tube-workspace']}><App /></MemoryRouter>);
    expect(screen.getByLabelText('Component')).toBeInTheDocument();
    cleanup();

    render(<MemoryRouter initialEntries={['/reports/api-510-drum-vessel-workspace']}><App /></MemoryRouter>);
    expect(screen.getByLabelText('Component')).toBeInTheDocument();
  });
});
