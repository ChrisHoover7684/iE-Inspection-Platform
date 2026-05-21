import { cleanup, fireEvent, render, screen, waitFor } from '@testing-library/react';
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
    expect(screen.queryByText(/Start Draft Report/i)).not.toBeInTheDocument();
  });

  it('maps launcher selections to expected routes', async () => {
    const cases = [
      ['API 570 Piping', 'Piping External', '/reports/api-570-piping-external'],
      ['API 570 Piping', 'Piping CUI External', '/reports/api-570-piping-external?type=cui'],
      ['API 510 Pressure Equipment', 'Shell and Tube Exchanger External', '/reports/api-510-shell-tube-external'],
      ['API 510 Pressure Equipment', 'Horizontal Drum External', '/reports/api-510-drum-vessel-external'],
      ['API 510 Pressure Equipment', 'Distillation Tower External', '/reports/api-510-tower-column-external'],
      ['API 510 Pressure Equipment', 'Plate and Frame Exchanger External', '/reports/api-510-exchanger-external'],
      ['STI / API 653 Tanks', 'Tank External', '/reports/sti-api-653-tank-external']
    ] as const;

    for (const [standard, reportType, expectedPath] of cases) {
      await renderCreatePage();
      fireEvent.click(screen.getByRole('button', { name: standard }));
      fireEvent.click(screen.getByRole('button', { name: reportType }));
      const startButton = screen.getByRole('button', { name: /^start inspection report$/i });
      fireEvent.click(startButton);
      await waitFor(() => expect(screen.getByTestId('location')).toHaveTextContent(expectedPath));
      cleanup();
    }
  });

  it('stores minimal selected context only', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
    fireEvent.click(screen.getByRole('button', { name: 'Shell and Tube Exchanger External' }));
    fireEvent.click(screen.getByRole('button', { name: /^start inspection report$/i }));

    const raw = window.localStorage.getItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
    expect(raw).toBeTruthy();
    const context = JSON.parse(raw ?? '{}');
    expect(context).toMatchObject({
      selectedStandard: expect.any(String),
      selectedReportTypeId: expect.any(String),
      selectedReportTypeLabel: expect.any(String),
      equipmentFamily: expect.any(String),
      equipmentSubtype: expect.any(String)
    });
    expect(context.startedAt).toEqual(expect.any(String));
    expect(context).not.toHaveProperty('reportHeader');
    expect(context).not.toHaveProperty('designConditions');
    expect(context).not.toHaveProperty('materials');
  });
});
