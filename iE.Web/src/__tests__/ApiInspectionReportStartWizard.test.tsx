import { cleanup, fireEvent, render, screen } from '@testing-library/react';
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
  expect(await screen.findByRole('heading', { name: 'Create Inspection Report' })).toBeInTheDocument();
};

afterEach(() => {
  window.localStorage.removeItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
});

describe('ApiInspectionReportStartWizard', () => {
  it('reports dashboard shows dashboard-only sections and no wizard steps', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);
    expect(await screen.findByRole('button', { name: 'Create Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Total Reports' })).toBeInTheDocument();
    expect(screen.getByRole('table')).toBeInTheDocument();
    expect(screen.queryByText('Step 1: Choose Inspection Standard')).not.toBeInTheDocument();
  });

  it('renders simplified create report launcher and hides setup forms', async () => {
    await renderCreatePage();
    expect(screen.getByText('Step 1: Choose Inspection Standard')).toBeInTheDocument();
    expect(screen.getByText('Step 2: Choose Report Type')).toBeInTheDocument();
    expect(screen.getByText('Step 3: Start Inspection Report')).toBeInTheDocument();

    expect(screen.queryByText('Report Header')).not.toBeInTheDocument();
    expect(screen.queryByText('Design Conditions')).not.toBeInTheDocument();
    expect(screen.queryByText('Materials')).not.toBeInTheDocument();
    expect(screen.queryByText('Components')).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Open Engineering Tools' })).not.toBeInTheDocument();
  });

  it('selects API 570 Piping External and starts report route', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 570 Piping' }));
    fireEvent.click(screen.getByRole('button', { name: 'Piping External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByText(/Loading API 570 Piping External report/i)).toBeInTheDocument();
  });

  it('selects API 510 shell and tube and starts shell-tube page', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
    fireEvent.click(screen.getByRole('button', { name: 'Shell and Tube Exchanger External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByRole('heading', { name: 'API 510 Shell-and-Tube External Report' })).toBeInTheDocument();
  });

  it('selects STI/API 653 tank and starts tank page', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'STI / API 653 Tanks' }));
    fireEvent.click(screen.getByRole('button', { name: 'Tank External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByRole('heading', { name: 'STI / API 653 Tank External Report' })).toBeInTheDocument();
  });

  it('stores minimal selected report context in localStorage and excludes API 510 internal options', async () => {
    await renderCreatePage();
    fireEvent.click(screen.getByRole('button', { name: 'API 510 Pressure Equipment' }));
    fireEvent.click(screen.getByRole('button', { name: 'Shell and Tube Exchanger External' }));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));

    const storedDraft = JSON.parse(window.localStorage.getItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY) ?? '{}');
    expect(storedDraft).toMatchObject({
      selectedStandard: 'API 510 Pressure Equipment',
      selectedReportTypeId: 'api510.external.exchanger.shell-tube',
      selectedReportTypeLabel: 'Shell and Tube Exchanger External',
      equipmentFamily: 'Pressure Equipment',
      equipmentSubtype: 'Shell and Tube Exchanger'
    });
    expect(storedDraft.startedAt).toEqual(expect.any(String));
    expect(screen.queryByText(/Internal/i)).not.toBeInTheDocument();
  });

  it('existing report and workspace routes still render', () => {
    for (const [route, text] of [
      ['/reports/api-570-piping-external', /Loading API 570 Piping External report/i],
      ['/reports/api-510-shell-tube-workspace', 'Component'],
      ['/reports/api-510-drum-vessel-workspace', 'Component'],
      ['/reports/api-510-drum-vessel-external', 'API 510 Drum/Vessel External Report'],
      ['/reports/api-510-tower-column-external', 'API 510 Tower/Column External Report'],
      ['/reports/api-510-exchanger-external', 'API 510 Remaining Exchanger External Report']
    ] as const) {
      render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);
      if (text instanceof RegExp) expect(screen.getByText(text)).toBeInTheDocument();
      else expect(screen.getByText(text)).toBeInTheDocument();
      cleanup();
    }
  });
});
