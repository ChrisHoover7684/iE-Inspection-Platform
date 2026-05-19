import { cleanup, fireEvent, render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY } from '../reporting/Api510ShellTubeExternalReportPage';
import { readApi510ShellTubeExternalDraftPayload } from '../reporting/api510ShellTubeReportDraft';
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

const draft = {
  reportTypeId: 'api510.external.exchanger.shell-tube',
  reportTypeLabel: 'Shell and Tube Exchanger External',
  header: {
    inspectionDate: '2026-05-18',
    inspector: 'A. Inspector',
    clientFacility: 'Demo Refinery',
    unitArea: 'Crude Unit',
    equipmentIdentifier: 'E-101',
    service: 'Naphtha',
    inspectionReason: 'Routine external',
    operatingStatus: 'Online',
    inspectionScope: 'Accessible external inspection',
    shellSideDesignPressure: '150',
    shellSideDesignTemperature: '450',
    tubeSideDesignPressure: '200',
    tubeSideDesignTemperature: '350',
    shellMaterialSpec: 'SA-516',
    shellMaterialGrade: '70',
    shellProductForm: 'Plate',
    tubeChannelMaterialSpec: 'SA-106',
    tubeChannelMaterialGrade: 'B',
    tubeChannelProductForm: 'Pipe'
  },
  components: ['Shell', 'Channel / Channel Head', 'Nozzles', 'Shell Cover', 'Saddles / Supports']
};

const storeDraft = () => {
  window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify(draft));
};

const renderRoute = (route = '/reports/api-510-shell-tube-external') => render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);

beforeEach(() => {
  window.localStorage.clear();
});

afterEach(() => {
  cleanup();
  window.localStorage.clear();
});

describe('Api510ShellTubeExternalReportPage', () => {
  it('Shell-and-Tube report page route renders', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'API 510 Shell-and-Tube External Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Header' })).toBeInTheDocument();
  });

  it('renders sticky header layout elements, report actions, and summary sidebar', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByLabelText('Sticky report header')).toBeInTheDocument();
    expect(screen.getByLabelText('Report sidebar')).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Actions' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'iE Assist' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Summary Warnings' })).toBeInTheDocument();
    expect(screen.getAllByRole('button', { name: 'Save Draft Locally' })).toHaveLength(2);
    expect(screen.getAllByRole('button', { name: 'Export Word Report' })[0]).toBeDisabled();
    expect(screen.getByText('Word export will be connected after report persistence is finalized.')).toBeInTheDocument();
  });

  it('section navigation and component quick links appear', () => {
    storeDraft();
    renderRoute();

    const sectionNav = screen.getByLabelText('Report sections');
    expect(within(sectionNav).getByText('Section navigation')).toBeInTheDocument();
    expect(within(sectionNav).getByRole('link', { name: 'Report Header' })).toHaveAttribute('href', '#report-header');
    expect(within(sectionNav).getByRole('link', { name: 'Calculations' })).toHaveAttribute('href', '#calculations');

    const componentLinks = screen.getByLabelText('Component quick links');
    expect(within(componentLinks).getByRole('link', { name: 'Shell' })).toHaveAttribute('href', '#component-shell');
    expect(within(componentLinks).getByRole('link', { name: 'Shell Cover' })).toHaveAttribute('href', '#component-shell-cover');
  });

  it('local save button stores state and shows timestamp', () => {
    storeDraft();
    vi.setSystemTime(new Date('2026-05-18T12:34:56.000Z'));
    renderRoute();

    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = window.localStorage.getItem(API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY);
    expect(saved).not.toBeNull();
    expect(JSON.parse(saved as string)).toMatchObject({
      reportTypeId: 'api510.external.exchanger.shell-tube',
      reportTypeLabel: 'Shell and Tube Exchanger External',
      savedAt: '2026-05-18T12:34:56.000Z',
      reportHeader: {
        inspector: 'A. Inspector',
        equipmentIdentifier: 'E-101'
      },
      designConditions: {
        shellSideDesignPressure: '150',
        tubeSideDesignPressure: '200'
      },
      materials: {
        shellMaterialSpec: 'SA-516',
        tubeChannelMaterialSpec: 'SA-106'
      },
      calculationSnapshots: [],
      findingDrafts: []
    });
    expect(screen.getByText(/Last Saved:/)).toBeInTheDocument();
    vi.useRealTimers();
  });

  it('summary preview counts components, calculations, findings, and checklist fields', () => {
    storeDraft();
    renderRoute();

    const summary = screen.getByLabelText('Report Summary Preview');
    expect(within(summary).getByText('Selected Components')).toBeInTheDocument();
    expect(within(summary).getByText('Calculation Snapshots')).toBeInTheDocument();
    expect(within(summary).getByText('Finding Drafts')).toBeInTheDocument();
    expect(within(summary).getAllByText('0').length).toBeGreaterThanOrEqual(7);
    expect(within(summary).getAllByText('Shell').length).toBeGreaterThanOrEqual(1);
    expect(within(summary).getByText('Shell Cover')).toBeInTheDocument();

    fireEvent.click(screen.getByLabelText('Shell Repair Required'));
    fireEvent.click(screen.getByLabelText('Shell Photo Required'));

    expect(JSON.parse(window.localStorage.getItem(API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) ?? 'null')).toBeNull();
    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);
    const saved = JSON.parse(window.localStorage.getItem(API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) as string);
    expect(saved.checklistCounts['Repair Required']).toBe(1);
    expect(saved.checklistCounts['Photo Required']).toBe(1);
    expect(Object.keys(saved.componentStates)).toHaveLength(5);
    expect(saved.componentStates.Shell.checklist['Repair Required']).toBe(true);
    expect(saved.componentStates.Shell.checklist['Photo Required']).toBe(true);
  });

  it('local save stores structured API 510 Shell-and-Tube draft payload sections', () => {
    storeDraft();
    renderRoute();

    fireEvent.change(screen.getByLabelText('Shell Condition'), { target: { value: 'Requires Evaluation' } });
    fireEvent.change(screen.getByLabelText('Shell Location'), { target: { value: '6 o’clock shell band' } });
    fireEvent.change(screen.getByText('General External Condition').closest('label')!.querySelector('textarea')!, { target: { value: 'Coating weathered but intact.' } });
    fireEvent.change(screen.getByText('Photo Log').closest('label')!.querySelector('textarea')!, { target: { value: 'P-1 Shell overview' } });
    fireEvent.change(screen.getByLabelText('Return to Service Status'), { target: { value: 'Acceptable with recommendations' } });
    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = JSON.parse(window.localStorage.getItem(API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) as string);
    expect(saved.sourceStartWizardDraft.storageKey).toBe(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
    expect(saved.reportHeader).toMatchObject({
      inspectionDate: '2026-05-18',
      inspector: 'A. Inspector',
      clientFacility: 'Demo Refinery',
      equipmentIdentifier: 'E-101'
    });
    expect(saved.designConditions).toMatchObject({
      shellSideDesignPressure: '150',
      shellSideDesignTemperature: '450',
      tubeSideDesignPressure: '200',
      tubeSideDesignTemperature: '350'
    });
    expect(saved.materials).toMatchObject({
      shellMaterialSpec: 'SA-516',
      shellMaterialGrade: '70',
      shellProductForm: 'Plate',
      tubeChannelMaterialSpec: 'SA-106'
    });
    expect(saved.componentStates.Shell).toMatchObject({
      condition: 'Requires Evaluation',
      location: '6 o’clock shell band'
    });
    expect(saved.calculationSnapshots).toHaveLength(0);
    expect(saved.findingDrafts).toHaveLength(0);
    expect(saved.summaryFields.generalExternalCondition).toBe('Coating weathered but intact.');
    expect(saved.photos.photoLog).toBe('P-1 Shell overview');
    expect(saved.returnToService.status).toBe('Acceptable with recommendations');
  });

  it('draft payload can be read back from localStorage', () => {
    storeDraft();
    renderRoute();

    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = readApi510ShellTubeExternalDraftPayload();
    expect(saved).toBeDefined();
    expect(saved?.reportTypeId).toBe('api510.external.exchanger.shell-tube');
    expect(saved?.reportHeader.equipmentIdentifier).toBe('E-101');
    expect(saved?.componentStates['Shell Cover']).toBeDefined();
    expect(saved?.calculationSnapshots).toHaveLength(0);
    expect(saved?.findingDrafts).toHaveLength(0);
  });

  it('View Draft Payload panel exposes the draft payload shape', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByText('View Draft Payload')).toBeInTheDocument();
    const debugPayload = screen.getByLabelText('API 510 Shell-and-Tube draft payload');
    expect(debugPayload).toHaveTextContent('reportHeader');
    expect(debugPayload).toHaveTextContent('designConditions');
    expect(debugPayload).toHaveTextContent('componentStates');
  });

  it('Draft setup fields appear in header, design, and material sections', () => {
    storeDraft();
    renderRoute();

    const header = screen.getByLabelText('Report Header');
    expect(within(header).getByText('A. Inspector')).toBeInTheDocument();
    expect(within(header).getByText('Demo Refinery')).toBeInTheDocument();
    expect(within(header).getByText('E-101')).toBeInTheDocument();

    const design = screen.getByLabelText('Design Conditions');
    expect(within(design).getByText('150')).toBeInTheDocument();
    expect(within(design).getByText('450')).toBeInTheDocument();
    expect(within(design).getByText('200')).toBeInTheDocument();
    expect(within(design).getByText('350')).toBeInTheDocument();

    const materials = screen.getByLabelText('Materials');
    expect(within(materials).getByText('SA-516')).toBeInTheDocument();
    expect(within(materials).getByText('70')).toBeInTheDocument();
    expect(within(materials).getByText('Plate')).toBeInTheDocument();
    expect(within(materials).getByText('SA-106')).toBeInTheDocument();
  });

  it('Default components appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Shell' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Channel / Channel Head' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Nozzles' })).toBeInTheDocument();
  });

  it('Optional selected components appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Shell Cover' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Saddles / Supports' })).toBeInTheDocument();
  });

  it('Component checklist fields appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByLabelText('Shell Condition')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Location')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Finding Notes')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Recommendation Text')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Photo Tag')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Create Finding')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Recommendation Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Repair Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Photo Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell NDE Required')).toBeInTheDocument();
    expect(screen.getByLabelText('Shell Add to Summary')).toBeInTheDocument();
  });

  it('Calculations section appears', () => {
    storeDraft();
    renderRoute();

    const calculations = screen.getByLabelText('Calculations');
    expect(within(calculations).getByRole('heading', { name: 'Calculations' })).toBeInTheDocument();
    expect(within(calculations).getByRole('heading', { name: 'API 510 Shell-and-Tube Calculation Workspace' })).toBeInTheDocument();
    expect(within(calculations).getByText(/using draft design field values/i)).toBeInTheDocument();
  });

  it('Start Wizard exposes the Shell-and-Tube External Report action after starting a Shell-and-Tube draft', async () => {
    renderRoute('/reports');
    expect(await screen.findByRole('heading', { name: 'Start New API Inspection Report' })).toBeInTheDocument();
    fireEvent.click(screen.getByLabelText('Shell and Tube Exchanger External'));
    expect(screen.queryByRole('button', { name: 'Open Shell-and-Tube External Report' })).not.toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Start Draft Report' }));

    expect(await screen.findByRole('button', { name: 'Open Shell-and-Tube External Report' })).toBeInTheDocument();
  });

  it('API 570 route still works', async () => {
    renderRoute('/reports/api-570-piping-external');

    expect(await screen.findByRole('heading', { name: /API 570 Piping External/i })).toBeInTheDocument();
  });

  it('API 510 Internal does not appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.queryByText(/API 510 Internal/i)).not.toBeInTheDocument();
    expect(screen.queryByText(/Internal Report/i)).not.toBeInTheDocument();
  });
});
