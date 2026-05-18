import { cleanup, render, screen } from '@testing-library/react';
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

describe('ApiInspectionReportStartWizard', () => {
  it('renders hierarchy on /reports including API 510 external groups and no internal options', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    expect(await screen.findByRole('heading', { name: 'Start New API Inspection Report' })).toBeInTheDocument();
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

  it('shows shell-and-tube design conditions and component preview when selected', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    screen.getByLabelText('Shell and Tube Exchanger External').click();
    expect(await screen.findByText('Shell Side Design Pressure')).toBeInTheDocument();
    expect(screen.getByText('Tube Side Design Temperature')).toBeInTheDocument();
    expect(screen.getByText('Channel / Channel Head')).toBeInTheDocument();
  });

  it('shows drum/vessel design conditions and component preview when selected', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    screen.getByLabelText('Horizontal Drum External').click();
    expect(await screen.findByText('Vessel Design Pressure')).toBeInTheDocument();
    expect(screen.getByText('Vessel Design Temperature')).toBeInTheDocument();
    expect(screen.getByText('Head Type')).toBeInTheDocument();
    expect(screen.getByText('Supports')).toBeInTheDocument();
  });

  it('existing API 570 and API 510 workspace routes still render', () => {
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
