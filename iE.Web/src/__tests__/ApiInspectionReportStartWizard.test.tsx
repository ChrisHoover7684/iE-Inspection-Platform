import { fireEvent, render, screen } from '@testing-library/react';
import { MemoryRouter, useLocation } from 'react-router-dom';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from '../reporting/componentCalculationPrefill';

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

function LocationProbe() {
  const location = useLocation();
  return <output data-testid="location">{location.pathname}{location.search}</output>;
}

const renderCreatePage = async () => {
  render(<MemoryRouter initialEntries={['/reports/new']}><App /><LocationProbe /></MemoryRouter>);
  expect(await screen.findByRole('heading', { level: 2, name: 'Create Inspection Report' })).toBeInTheDocument();
};

beforeEach(() => {
  localStorage.clear();
  vi.clearAllMocks();
});

describe('ApiInspectionReportStartWizard launcher flow', () => {
  it('/reports renders dashboard-only sections', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    expect(await screen.findByRole('button', { name: 'Create Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Total Reports' })).toBeInTheDocument();
    expect(screen.getByRole('table')).toBeInTheDocument();
    expect(screen.queryByText('Step 1: Choose Inspection Standard')).not.toBeInTheDocument();
  });

  it('/reports/new renders only launcher controls', async () => {
    await renderCreatePage();
    expect(screen.getByRole('link', { name: 'Back to Reports Dashboard' })).toBeInTheDocument();
    expect(screen.getByText('Step 1: Choose Inspection Standard')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'API 570 Piping' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'API 510 Pressure Equipment' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'STI / API 653 Tanks' })).toBeInTheDocument();
    expect(screen.getByText('Step 2: Choose Report Type')).toBeInTheDocument();
    expect(screen.getByText('Step 3: Start Inspection Report')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /^start inspection report$/i })).toBeInTheDocument();

    expect(screen.queryByText(/Report Header/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Design Conditions/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Materials/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Components/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Default \/ Minimum/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Optional/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Start Draft Report/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Open External Inspection Report/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Open Tank External Report/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Open Engineering Tools/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Allowable stress/i)).not.toBeInTheDocument();
  });

  it('navigates API 570 piping external', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 570 Piping' }));
    fireEvent.click(screen.getByRole('button', { name: 'Piping External' }));
    fireEvent.click(screen.getByRole('button', { name: /^start inspection report$/i }));
    expect(screen.getByTestId('location')).toHaveTextContent('/reports/api-570-piping-external');
  });

  it('navigates API 570 piping CUI external', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 570 Piping' }));
    fireEvent.click(screen.getByRole('button', { name: 'Piping CUI External' }));
    fireEvent.click(screen.getByRole('button', { name: /^start inspection report$/i }));
    expect(screen.getByTestId('location')).toHaveTextContent('/reports/api-570-piping-external?type=cui');
  });

  it('navigates API 510 selections to expected routes', async () => {
    const cases = [
      ['Shell and Tube Exchanger External', '/reports/api-510-shell-tube-external'],
      ['Horizontal Drum External', '/reports/api-510-drum-vessel-external'],
      ['Distillation Tower External', '/reports/api-510-tower-column-external'],
      ['Plate and Frame Exchanger External', '/reports/api-510-exchanger-external']
    ] as const;

    for (const [reportType, expectedPath] of cases) {
      await renderCreatePage();
      fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
      fireEvent.click(screen.getByRole('button', { name: reportType }));
      fireEvent.click(screen.getByRole('button', { name: /^start inspection report$/i }));
      expect(screen.getByTestId('location')).toHaveTextContent(expectedPath);
    }
  });

  it('navigates STI/API 653 tank external', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'STI / API 653 Tanks' }));
    fireEvent.click(screen.getByRole('button', { name: 'Tank External' }));
    fireEvent.click(screen.getByRole('button', { name: /^start inspection report$/i }));
    expect(screen.getByTestId('location')).toHaveTextContent('/reports/sti-api-653-tank-external');
  });

  it('stores minimal selected context only and hides API 510 internal options', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
    fireEvent.click(screen.getByRole('button', { name: 'Shell and Tube Exchanger External' }));
    fireEvent.click(screen.getByRole('button', { name: /^start inspection report$/i }));

    const storedDraft = JSON.parse(window.localStorage.getItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY) ?? '{}');
    expect(storedDraft).toMatchObject({
      selectedStandard: 'API 510 Pressure Equipment',
      selectedReportTypeId: 'api510.external.exchanger.shell-tube',
      selectedReportTypeLabel: 'Shell and Tube Exchanger External',
      equipmentFamily: 'Pressure Equipment',
      equipmentSubtype: 'Shell and Tube Exchanger'
    });
    expect(storedDraft.startedAt).toEqual(expect.any(String));
    expect(storedDraft.header).toEqual({});
    expect(storedDraft.components).toEqual([]);
    expect(screen.queryByText(/Internal/i)).not.toBeInTheDocument();
  });
});
