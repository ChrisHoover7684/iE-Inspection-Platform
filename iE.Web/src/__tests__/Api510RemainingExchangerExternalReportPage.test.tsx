import { cleanup, fireEvent, render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY } from '../reporting/Api510RemainingExchangerExternalReportPage';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from '../reporting/componentCalculationPrefill';

vi.mock('../api', async () => {
  const actual = await vi.importActual<typeof import('../api')>('../api');
  return {
    ...actual,
    reportingApi: { ...actual.reportingApi, getInstances: vi.fn().mockResolvedValue([]), getTemplateById: vi.fn().mockResolvedValue({ sections: [] }), getInstanceById: vi.fn().mockRejectedValue(new Error('none')), createInstanceFromTemplate: vi.fn(), saveInstance: vi.fn(), inlineSuggestions: vi.fn(), getAlerts: vi.fn().mockResolvedValue([]) },
    pipeLookupApi: { getNps: vi.fn().mockResolvedValue([]), getSchedules: vi.fn().mockResolvedValue([]), getDimensions: vi.fn().mockResolvedValue(null) },
    b313Api: { calculateThickness: vi.fn().mockResolvedValue({ success: true, requiredThicknessIn: 0.1, warnings: [] }) }
  };
});

const route = '/reports/api-510-exchanger-external';
const renderRoute = () => render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);

const storeDraft = (reportTypeId: string, reportTypeLabel: string, header: Record<string, string>, components: string[]) => {
  window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify({ reportTypeId, reportTypeLabel, header: { inspectionDate: '2026-05-18', inspector: 'A. Inspector', clientFacility: 'Demo Refinery', unitArea: 'Utilities', equipmentIdentifier: 'E-200', service: 'Cooling water', inspectionReason: 'Routine external', operatingStatus: 'Online', inspectionScope: 'Accessible external inspection', ...header }, components }));
};

beforeEach(() => window.localStorage.clear());
afterEach(() => { cleanup(); window.localStorage.clear(); vi.useRealTimers(); });

describe('Api510RemainingExchangerExternalReportPage', () => {
  it('Plate and Frame page renders from draft with correct default components and review-only calculations', () => {
    storeDraft('api510.external.exchanger.plate-frame', 'Plate and Frame Exchanger External', { hotSideDesignPressure: '150', hotSideDesignTemperature: '300', coldSideDesignPressure: '100', coldSideDesignTemperature: '200' }, ['Frame Head', 'Pressure Plate', 'Plate Pack External', 'Ports / Nozzles', 'Tie Bolts', 'Gaskets / Seals']);
    renderRoute();

    expect(screen.getByRole('heading', { name: 'API 510 Plate and Frame Exchanger External Report' })).toBeInTheDocument();
    expect(screen.getByText('Hot Side Design Pressure')).toBeInTheDocument();
    expect(screen.getByText('Cold Side Design Temperature')).toBeInTheDocument();
    for (const component of ['Frame Head', 'Pressure Plate', 'Plate Pack External', 'Ports / Nozzles', 'Tie Bolts']) {
      expect(screen.getByRole('heading', { name: component })).toBeInTheDocument();
    }
    expect(screen.getAllByText('Calculation workspace coming soon for this exchanger type.').length).toBeGreaterThan(0);
  });

  it('Double Pipe page renders from draft with correct default components', () => {
    storeDraft('api510.external.exchanger.double-pipe', 'Double Pipe Exchanger External', { innerPipeSideDesignPressure: '250', innerPipeSideDesignTemperature: '500', annulusOuterPipeSideDesignPressure: '125', annulusOuterPipeSideDesignTemperature: '350' }, ['Inner Pipe External', 'Outer Pipe', 'Return Bends', 'Nozzles']);
    renderRoute();

    expect(screen.getByRole('heading', { name: 'API 510 Double Pipe Exchanger External Report' })).toBeInTheDocument();
    expect(screen.getByText('Inner Pipe Side Design Pressure')).toBeInTheDocument();
    expect(screen.getByText('Annulus / Outer Pipe Side Design Temperature')).toBeInTheDocument();
    for (const component of ['Inner Pipe External', 'Outer Pipe', 'Return Bends', 'Nozzles']) {
      expect(screen.getByRole('heading', { name: component })).toBeInTheDocument();
    }
  });

  it('Air Cooler page renders from draft with correct default components', () => {
    storeDraft('api510.external.exchanger.air-cooler', 'Air Cooler / Fin Fan External', { tubeSideDesignPressure: '275', tubeSideDesignTemperature: '625', headerBoxDesignPressure: '300', headerBoxDesignTemperature: '650' }, ['Header Box', 'Tube Bundle External', 'Nozzles', 'Frame / Supports']);
    renderRoute();

    expect(screen.getByRole('heading', { name: 'API 510 Air Cooler / Fin Fan External Report' })).toBeInTheDocument();
    expect(screen.getByText('Tube Side Design Pressure')).toBeInTheDocument();
    expect(screen.getByText('Header Box Design Temperature')).toBeInTheDocument();
    for (const component of ['Header Box', 'Tube Bundle External', 'Nozzles', 'Frame / Supports']) {
      expect(screen.getByRole('heading', { name: component })).toBeInTheDocument();
    }
  });

  it('shows required layout sections, local save stores structured payload, and debug panel shows JSON', () => {
    vi.useFakeTimers();
    vi.setSystemTime(new Date('2026-05-18T12:34:56.000Z'));
    storeDraft('api510.external.exchanger.plate-frame', 'Plate and Frame Exchanger External', { hotSideDesignPressure: '150' }, ['Frame Head', 'Pressure Plate', 'Plate Pack External', 'Ports / Nozzles', 'Tie Bolts']);
    renderRoute();

    expect(screen.getByLabelText('Sticky report header')).toBeInTheDocument();
    expect(screen.getByLabelText('Report sidebar')).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Actions' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'iE Assist' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Summary Preview' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Summary Warnings' })).toBeInTheDocument();
    expect(within(screen.getByLabelText('Report sections')).getByRole('link', { name: 'Calculations / Review Notes' })).toHaveAttribute('href', '#calculations-review-notes');

    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);
    const saved = JSON.parse(window.localStorage.getItem(API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) ?? '{}');
    expect(saved).toMatchObject({ reportTypeId: 'api510.external.exchanger.plate-frame', savedAt: '2026-05-18T12:34:56.000Z', designConditions: { hotSideDesignPressure: '150' }, calculationReview: { status: 'review-only' } });

    fireEvent.click(screen.getByText('View Draft Payload'));
    expect(screen.getByLabelText('API 510 remaining exchanger draft payload')).toHaveTextContent('api510.external.exchanger.plate-frame');
  });


  it('Start Wizard opens remaining exchanger report route for Plate and Frame, Double Pipe, and Air Cooler', () => {
    for (const label of ['Plate and Frame Exchanger External', 'Double Pipe Exchanger External', 'Air Cooler / Fin Fan External']) {
      render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);
      fireEvent.click(screen.getByLabelText(label));
      fireEvent.click(screen.getByRole('button', { name: 'Start Draft Report' }));

      expect(screen.getByRole('button', { name: 'Open External Inspection Report' })).toBeInTheDocument();
      expect(screen.queryByText(/API 510 Internal/i)).not.toBeInTheDocument();
      cleanup();
      window.localStorage.clear();
    }
  });

  it('preserves API 570 and API 510 external routes while API 510 Internal does not appear', () => {
    render(<MemoryRouter initialEntries={['/reports/api-570-piping-external']}><App /></MemoryRouter>);
    expect(screen.getByText(/Loading API 570 Piping External report/i)).toBeInTheDocument();
    cleanup();

    for (const [route, heading] of [['/reports/api-510-shell-tube-external', 'API 510 Shell-and-Tube External Report'], ['/reports/api-510-drum-vessel-external', 'API 510 Drum/Vessel External Report'], ['/reports/api-510-tower-column-external', 'API 510 Tower/Column External Report']] as const) {
      render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);
      expect(screen.getByRole('heading', { name: heading })).toBeInTheDocument();
      cleanup();
    }

    render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);
    expect(screen.queryByText(/API 510 Internal/i)).not.toBeInTheDocument();
  });

  it('warning count updates for remaining exchanger assist panel', () => {
    storeDraft('api510.external.exchanger.plate-frame', 'Plate and Frame Exchanger External', { hotSideDesignPressure: '150' }, ['Frame Head']);
    renderRoute();
    expect(screen.getAllByText('iE Assist (Rule-Based)').length).toBeGreaterThan(0);
    const assistCard = screen.getByLabelText('iE Assist');
    fireEvent.click(screen.getByLabelText('Frame Head Photo Required'));
    expect(within(assistCard).getByTestId('total-assist-warnings-count')).toHaveTextContent('1');
  });
});
