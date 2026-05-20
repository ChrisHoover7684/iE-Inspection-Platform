import { cleanup, fireEvent, render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { API510_TOWER_COLUMN_EXTERNAL_REPORT_LOCAL_STORAGE_KEY } from '../reporting/Api510TowerColumnExternalReportPage';
import { readApi510TowerColumnExternalDraftPayload } from '../reporting/api510TowerColumnReportDraft';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from '../reporting/componentCalculationPrefill';

vi.mock('../api', async () => {
  const actual = await vi.importActual<typeof import('../api')>('../api');
  return {
    ...actual,
    reportingApi: {
      ...actual.reportingApi,
      getInstances: vi.fn().mockResolvedValue([]),
      getTemplateById: vi.fn().mockResolvedValue({ id: 'api-570-piping-external', name: 'API 570 Piping External', sections: [] }),
      getInstanceById: vi.fn().mockRejectedValue(new Error('no active report')),
      createInstanceFromTemplate: vi.fn().mockResolvedValue({ id: 'rpt-1', templateId: 'api-570-piping-external', sections: [], answers: [], findings: [], status: 'Draft' }),
      saveInstance: vi.fn().mockResolvedValue({ id: 'rpt-1', templateId: 'api-570-piping-external', sections: [], answers: [], findings: [], status: 'Draft' }),
      inlineSuggestions: vi.fn().mockResolvedValue({ suggestions: [] }),
      getAlerts: vi.fn().mockResolvedValue([])
    },
    pipeLookupApi: {
      getNps: vi.fn().mockResolvedValue([]),
      getSchedules: vi.fn().mockResolvedValue([]),
      getDimensions: vi.fn().mockResolvedValue(null)
    },
    b313Api: {
      calculateThickness: vi.fn().mockResolvedValue({ success: true, requiredThicknessIn: 0.1, warnings: [] })
    }
  };
});

const baseDraft = {
  reportTypeId: 'api510.external.tower-column.distillation',
  reportTypeLabel: 'Distillation Tower External',
  header: {
    inspectionDate: '2026-05-18',
    inspector: 'A. Inspector',
    clientFacility: 'Demo Refinery',
    unitArea: 'Crude Unit',
    equipmentIdentifier: 'T-101',
    service: 'Fractionator service',
    inspectionReason: 'Routine external',
    operatingStatus: 'Online',
    inspectionScope: 'Accessible external inspection',
    vesselDesignPressure: '150',
    vesselDesignTemperature: '450',
    shellCourseMaterialSpec: 'SA-516',
    shellCourseMaterialGrade: '70',
    shellCourseProductForm: 'Plate'
  },
  components: [
    'Shell Courses',
    'Nozzles',
    'Manways',
    'Skirt',
    'Base Ring / Anchor Bolts',
    'Platforms / Ladders / Handrails',
    'Tray Manways',
    'Draw Nozzles',
    'Reboiler / Condenser Connections'
  ]
};

const draftWith = (reportTypeId: string, reportTypeLabel: string, components: string[]) => ({
  ...baseDraft,
  reportTypeId,
  reportTypeLabel,
  components: [...baseDraft.components.slice(0, 6), ...components]
});

const storeDraft = (draft = baseDraft) => {
  window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify(draft));
};

const renderRoute = (route = '/reports/api-510-tower-column-external') => render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);

beforeEach(() => {
  window.localStorage.clear();
});

afterEach(() => {
  cleanup();
  vi.useRealTimers();
  window.localStorage.clear();
});

describe('Api510TowerColumnExternalReportPage', () => {
  it('route renders', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'API 510 Tower/Column External Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Header' })).toBeInTheDocument();
  });

  it('wrong draft type shows warning', () => {
    storeDraft({ ...baseDraft, reportTypeId: 'api570.piping-external', reportTypeLabel: 'Piping External' });
    renderRoute();

    expect(screen.getByRole('alert')).toHaveTextContent('The saved draft setup is not for an API 510 External Tower/Column report.');
    expect(screen.getByRole('link', { name: 'Return to Start Wizard' })).toHaveAttribute('href', '/reports');
  });

  it('draft header/design/material fields appear', () => {
    storeDraft();
    renderRoute();

    expect(within(screen.getByLabelText('Report Header')).getByText('A. Inspector')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Report Header')).getByText('T-101')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Design Conditions')).getByText('150')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Design Conditions')).getByText('450')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Materials')).getByText('SA-516')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Materials')).getByText('70')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Materials')).getByText('Plate')).toBeInTheDocument();
  });

  it('Distillation default components appear', () => {
    storeDraft();
    renderRoute();

    for (const component of ['Shell Courses', 'Nozzles', 'Manways', 'Skirt', 'Base Ring / Anchor Bolts', 'Platforms / Ladders / Handrails']) {
      expect(screen.getByRole('heading', { name: component })).toBeInTheDocument();
    }
    expect(screen.getByRole('heading', { name: 'Tray Manways' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Draw Nozzles' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Reboiler / Condenser Connections' })).toBeInTheDocument();
  });

  it('Absorber/Stripper optional components appear', () => {
    storeDraft(draftWith('api510.external.tower-column.absorber-stripper', 'Absorber / Stripper External', ['Packing Access Manways', 'Distributor Nozzles', 'Reboiler / Overhead Connections']));
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Packing Access Manways' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Distributor Nozzles' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Reboiler / Overhead Connections' })).toBeInTheDocument();
  });

  it('Packed Column optional components appear', () => {
    storeDraft(draftWith('api510.external.tower-column.packed-column', 'Packed Column External', ['Packing Support Access', 'Distributor Access', 'Mist Eliminator Access']));
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Packing Support Access' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Distributor Access' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Mist Eliminator Access' })).toBeInTheDocument();
  });

  it('Tray Column optional components appear', () => {
    storeDraft(draftWith('api510.external.tower-column.tray-column', 'Tray Column External', ['Tray Access Manways', 'Downcomer / Tray Access Locations', 'Draw Pan Connections']));
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Tray Access Manways' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Downcomer / Tray Access Locations' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Draw Pan Connections' })).toBeInTheDocument();
  });

  it('component checklist fields appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByLabelText('Shell Courses Condition')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Location / Elevation')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Finding Notes')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Recommendation Text')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Photo Tag')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Create Finding')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Recommendation Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Repair Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Photo Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses NDE Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Courses Add to Summary')).toBeInTheDocument();
  });

  it('calculation section shows coming soon/review-only message', () => {
    storeDraft();
    renderRoute();

    const calculations = screen.getByLabelText('Calculations / Review Notes');
    expect(within(calculations).getByText('Tower/Column calculation workspace coming soon. Shell course/nozzle calculations will follow the Drum/Vessel pattern.')).toBeInTheDocument();
  });

  it('local save stores structured payload', () => {
    storeDraft();
    vi.useFakeTimers();
    vi.setSystemTime(new Date('2026-05-18T12:34:56.000Z'));
    renderRoute();

    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = JSON.parse(window.localStorage.getItem(API510_TOWER_COLUMN_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) as string);
    expect(saved).toMatchObject({
      reportTypeId: 'api510.external.tower-column.distillation',
      reportTypeLabel: 'Distillation Tower External',
      equipmentSubtype: 'Distillation Tower',
      savedAt: '2026-05-18T12:34:56.000Z',
      reportHeader: { inspector: 'A. Inspector', equipmentIdentifier: 'T-101' },
      designConditions: { vesselDesignPressure: '150', vesselDesignTemperature: '450' },
      materials: { shellCourseMaterialSpec: 'SA-516', shellCourseMaterialGrade: '70', shellCourseProductForm: 'Plate' }
    });
    expect(readApi510TowerColumnExternalDraftPayload()?.reportHeader.equipmentIdentifier).toBe('T-101');
  });

  it('payload includes component states and checklist counts', () => {
    storeDraft();
    renderRoute();

    fireEvent.change(screen.getByLabelText('Shell Courses Condition'), { target: { value: 'Requires Evaluation' } });
    fireEvent.click(screen.getByLabelText('Shell Courses Repair Required'));
    fireEvent.click(screen.getByLabelText('Shell Courses Photo Required'));
    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = JSON.parse(window.localStorage.getItem(API510_TOWER_COLUMN_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) as string);
    expect(saved.componentStates['Shell Courses'].condition).toBe('Requires Evaluation');
    expect(saved.componentStates['Shell Courses'].checklist['Repair Required']).toBe(true);
    expect(saved.checklistCounts['Repair Required']).toBe(1);
    expect(saved.checklistCounts['Photo Required']).toBe(1);
    expect(Object.keys(saved.componentStates)).toContain('Tray Manways');
  });

  it('View Draft Payload shows payload JSON', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByText('View Draft Payload')).toBeInTheDocument();
    const debugPayload = screen.getByLabelText('API 510 Tower/Column draft payload');
    expect(debugPayload).toHaveTextContent('reportHeader');
    expect(debugPayload).toHaveTextContent('designConditions');
    expect(debugPayload).toHaveTextContent('componentStates');
  });

  it('Start Wizard exposes the Tower/Column External Report action after starting any Tower/Column draft', async () => {
    renderRoute('/reports/new');
    expect(await screen.findByRole('heading', { name: 'Step 1: Select Report Type' })).toBeInTheDocument();
    fireEvent.click(screen.getByLabelText('Tray Column External'));
    expect(screen.queryByRole('button', { name: 'Open External Inspection Report' })).not.toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Start Draft Report' }));

    expect(await screen.findByRole('button', { name: 'Open External Inspection Report' })).toBeInTheDocument();
  });

  it('API 570 route still works', async () => {
    renderRoute('/reports/api-570-piping-external');

    expect(await screen.findByRole('heading', { name: /API 570 Piping External/i })).toBeInTheDocument();
  });

  it('Shell-and-Tube and Drum/Vessel routes still work', () => {
    storeDraft({ ...baseDraft, reportTypeId: 'api510.external.exchanger.shell-tube', reportTypeLabel: 'Shell and Tube Exchanger External' });
    const { unmount } = renderRoute('/reports/api-510-shell-tube-external');
    expect(screen.getByRole('heading', { name: 'API 510 Shell-and-Tube External Report' })).toBeInTheDocument();
    unmount();

    storeDraft({ ...baseDraft, reportTypeId: 'api510.external.drum-vessel.horizontal-drum', reportTypeLabel: 'Horizontal Drum External' });
    renderRoute('/reports/api-510-drum-vessel-external');
    expect(screen.getByRole('heading', { name: 'API 510 Drum/Vessel External Report' })).toBeInTheDocument();
  });

  it('API 510 Internal does not appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.queryByText(/API 510 Internal/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Internal Report/i)).not.toBeInTheDocument();
  });

  it('assist panel and warning counts update', () => {
    storeDraft();
    renderRoute();
    expect(screen.getAllByText('iE Assist (Rule-Based)').length).toBeGreaterThan(0);
    const assistCard = screen.getByLabelText('iE Assist');
    expect(within(assistCard).getByTestId('total-assist-warnings-count')).toHaveTextContent('0');
    fireEvent.click(screen.getByLabelText('Shell Courses Photo Required'));
    expect(within(assistCard).getByTestId('total-assist-warnings-count')).toHaveTextContent('1');
    expect(within(assistCard).getByTestId('missing-photo-tags-count')).toHaveTextContent('1');
  });
});
