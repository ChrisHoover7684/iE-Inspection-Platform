import { cleanup, fireEvent, render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY } from '../reporting/StiApi653TankExternalReportPage';
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

const draft = {
  reportTypeId: 'sti653.external.tank',
  reportTypeLabel: 'Tank External',
  header: {
    inspectionDate: '2026-05-18',
    inspector: 'T. Inspector',
    clientFacility: 'Tank Farm',
    unitArea: 'North Yard',
    equipmentIdentifier: 'TK-101',
    service: 'Diesel',
    inspectionReason: 'Routine external',
    operatingStatus: 'In service',
    inspectionScope: 'External tank inspection',
    tankId: 'TK-101',
    tankService: 'Diesel',
    tankConstruction: 'API 650'
  },
  components: ['Tank Shell', 'Roof', 'Nozzles / Appurtenances']
};

const storeDraft = () => window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify(draft));
const renderRoute = (route = '/reports/sti-api-653-tank-external') => render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);

beforeEach(() => window.localStorage.clear());
afterEach(() => { cleanup(); window.localStorage.clear(); vi.useRealTimers(); });

describe('StiApi653TankExternalReportPage', () => {
  it('Tank External page renders from draft with correct default sections', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'STI / API 653 Tank External Report' })).toBeInTheDocument();
    expect(screen.getByText('Tank ID')).toBeInTheDocument();
    expect(screen.getAllByText('TK-101').length).toBeGreaterThan(0);
    for (const section of ['Foundation', 'Shell', 'Roof', 'Shell-to-Bottom / Chime Area', 'Nozzles / Appurtenances', 'Vents', 'Coating', 'Containment / Release Prevention', 'Overfill / Spill Prevention', 'Anchor Bolts / Grounding', 'Stairs / Platforms / Ladders', 'External Leaks / Staining']) {
      expect(screen.getByRole('heading', { name: section })).toBeInTheDocument();
    }
  });

  it('shows required layout, tank checklist fields, local save payload, and draft JSON', () => {
    vi.useFakeTimers();
    vi.setSystemTime(new Date('2026-05-18T12:34:56.000Z'));
    storeDraft();
    renderRoute();

    expect(screen.getByLabelText('Sticky report header')).toBeInTheDocument();
    expect(screen.getByLabelText('Report sidebar')).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Actions' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'iE Assist' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Summary Preview' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Summary Warnings' })).toBeInTheDocument();
    expect(within(screen.getByLabelText('Report sections')).getByRole('link', { name: 'Shell-to-Bottom / Chime Area' })).toHaveAttribute('href', '#shell-to-bottom-chime-area');

    for (const label of ['Foundation Condition', 'Foundation Location', 'Foundation Photo Tag', 'Foundation Create Finding', 'Foundation Recommendation Required', 'Foundation Repair Required', 'Foundation Photo Required', 'Foundation NDE Required', 'Foundation Add to Summary']) {
      expect(screen.getByLabelText(label)).toBeInTheDocument();
    }
    expect(screen.getAllByLabelText('Finding Notes').length).toBeGreaterThan(0);
    expect(screen.getAllByLabelText('Recommendation Text').length).toBeGreaterThan(0);

    fireEvent.click(screen.getByLabelText('Foundation Create Finding'));
    fireEvent.change(screen.getByLabelText('Foundation Location'), { target: { value: 'North side' } });
    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);
    const saved = JSON.parse(window.localStorage.getItem(STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) ?? '{}');
    expect(saved).toMatchObject({ reportTypeId: 'sti653.external.tank', savedAt: '2026-05-18T12:34:56.000Z', tankDetails: { tankId: 'TK-101', tankService: 'Diesel' }, componentStates: { Foundation: { location: 'North side', checklist: { 'Create Finding': true } } } });

    fireEvent.click(screen.getByText('View Draft Payload'));
    expect(screen.getByLabelText('STI API 653 tank draft payload')).toHaveTextContent('sti653.external.tank');
  });

  it('Start Wizard opens Tank External route and does not expose API 510 Internal', () => {
    renderRoute('/reports');
    fireEvent.click(screen.getByLabelText('Tank External'));
    fireEvent.click(screen.getByRole('button', { name: 'Start Draft Report' }));

    expect(screen.getByRole('button', { name: 'Open Tank External Report' })).toBeInTheDocument();
    expect(screen.queryByText(/API 510 Internal/i)).not.toBeInTheDocument();
  });
});
