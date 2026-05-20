import { cleanup, fireEvent, render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, describe, expect, it, vi } from 'vitest';
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

const renderCreatePage = async () => {
  render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);
  expect(await screen.findByRole('heading', { level: 2, name: 'Create Inspection Report' })).toBeInTheDocument();
};

afterEach(() => {
  window.localStorage.removeItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
});

describe('ApiInspectionReportStartWizard launcher flow', () => {
  it('/reports renders dashboard-only sections', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    expect(await screen.findByRole('button', { name: 'Create Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Total Reports' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Draft Reports' })).toBeInTheDocument();
    expect(screen.getByRole('table')).toBeInTheDocument();
    expect(screen.queryByText('Step 1: Choose Inspection Standard')).not.toBeInTheDocument();
  });

  it('/reports/new renders launcher-only controls', async () => {
    await renderCreatePage();
    expect(screen.getByRole('link', { name: 'Back to Reports Dashboard' })).toBeInTheDocument();
    expect(screen.getByText('Step 1: Choose Inspection Standard')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'API 570 Piping' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'API 510 Pressure Equipment' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'STI / API 653 Tanks' })).toBeInTheDocument();
    expect(screen.getByText('Step 2: Choose Report Type')).toBeInTheDocument();
    expect(screen.getByText('Step 3: Start Inspection Report')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Start Inspection Report' })).toBeInTheDocument();

    expect(screen.queryByRole('region', { name: 'Report Header' })).not.toBeInTheDocument();
    expect(screen.queryByRole('region', { name: 'Design Conditions' })).not.toBeInTheDocument();
    expect(screen.queryByRole('region', { name: 'Materials' })).not.toBeInTheDocument();
    expect(screen.queryByRole('region', { name: 'Components' })).not.toBeInTheDocument();
    expect(screen.queryByText('Default / Minimum')).not.toBeInTheDocument();
    expect(screen.queryByText('Optional')).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Start Draft Report' })).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Open External Inspection Report' })).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Open Tank External Report' })).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Open Engineering Tools' })).not.toBeInTheDocument();
    expect(screen.queryByText(/Allowable stress/i)).not.toBeInTheDocument();
  });

  it('API 570 piping selections navigate to external and CUI routes', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 570 Piping' }));
    fireEvent.click(screen.getByRole('button', { name: 'Piping External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByText(/Loading API 570 Piping External report/i)).toBeInTheDocument();
    cleanup();

    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 570 Piping' }));
    fireEvent.click(screen.getByRole('button', { name: 'Piping CUI External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByText(/Loading API 570 Piping External report/i)).toBeInTheDocument();
  });

  it('API 510 selections navigate to shell-tube, drum-vessel, tower-column, and exchanger routes', async () => {
    const expectations: Array<[string, string]> = [
      ['Shell and Tube Exchanger External', 'API 510 Shell-and-Tube External Report'],
      ['Horizontal Drum External', 'API 510 Drum/Vessel External Report'],
      ['Distillation Tower External', 'API 510 Tower/Column External Report'],
      ['Plate and Frame Exchanger External', 'API 510 Remaining Exchanger External Report']
    ];

    for (const [typeLabel, expectedHeading] of expectations) {
      await renderCreatePage();
      fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
      fireEvent.click(screen.getByRole('button', { name: typeLabel }));
      fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
      expect(await screen.findByRole('heading', { name: expectedHeading })).toBeInTheDocument();
      cleanup();
    }
  });

  it('STI/API 653 tank selection navigates to tank page', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'STI / API 653 Tanks' }));
    fireEvent.click(screen.getByRole('button', { name: 'Tank External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByRole('heading', { name: 'STI / API 653 Tank External Report' })).toBeInTheDocument();
  });

  it('stores minimal selected report context in localStorage and hides API 510 internal options', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
    fireEvent.click(screen.getByRole('button', { name: 'Shell and Tube Exchanger External' }));

    const step3 = screen.getByRole('region', { name: 'Selected report summary' });
    expect(within(step3).getByText(/Selected Standard:/)).toBeInTheDocument();
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));

    const storedDraft = JSON.parse(window.localStorage.getItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY) ?? '{}');
    expect(Object.keys(storedDraft)).toEqual(expect.arrayContaining([
      'selectedStandard',
      'selectedReportTypeId',
      'selectedReportTypeLabel',
      'equipmentFamily',
      'equipmentSubtype',
      'startedAt'
    ]));
    expect(storedDraft).toMatchObject({
      selectedStandard: 'API 510 Pressure Equipment',
      selectedReportTypeId: 'api510.external.exchanger.shell-tube',
      selectedReportTypeLabel: 'Shell and Tube Exchanger External',
      equipmentFamily: 'Pressure Equipment',
      equipmentSubtype: 'Shell and Tube Exchanger'
    });
    expect(screen.queryByText(/API 510 Internal/i)).not.toBeInTheDocument();
  });

  it('existing report-entry and calculation routes still render', async () => {
    for (const [route, assertFn] of [
      ['/reports/api-570-piping-external', async () => expect(await screen.findByText(/Loading API 570 Piping External report/i)).toBeInTheDocument()],
      ['/reports/api-510-shell-tube-workspace', async () => expect(await screen.findByLabelText('Component')).toBeInTheDocument()],
      ['/reports/api-510-drum-vessel-workspace', async () => expect(await screen.findByLabelText('Component')).toBeInTheDocument()],
      ['/reports/api-510-shell-tube-external', async () => expect(await screen.findByRole('heading', { name: 'API 510 Shell-and-Tube External Report' })).toBeInTheDocument()],
      ['/reports/api-510-drum-vessel-external', async () => expect(await screen.findByRole('heading', { name: 'API 510 Drum/Vessel External Report' })).toBeInTheDocument()],
      ['/reports/api-510-tower-column-external', async () => expect(await screen.findByRole('heading', { name: 'API 510 Tower/Column External Report' })).toBeInTheDocument()],
      ['/reports/api-510-exchanger-external', async () => expect(await screen.findByText(/External Exchanger report/i)).toBeInTheDocument()],
      ['/reports/sti-api-653-tank-external', async () => expect(await screen.findByRole('heading', { name: 'STI / API 653 Tank External Report' })).toBeInTheDocument()]
    ] as const) {
      render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);
      await assertFn();
      cleanup();
    }
  });
});
