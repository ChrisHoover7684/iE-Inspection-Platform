import { cleanup, fireEvent, render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, beforeEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { API510_DRUM_VESSEL_EXTERNAL_REPORT_LOCAL_STORAGE_KEY } from '../reporting/Api510DrumVesselExternalReportPage';
import { readApi510DrumVesselExternalDraftPayload } from '../reporting/api510DrumVesselReportDraft';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from '../reporting/componentCalculationPrefill';

vi.mock('../reporting/Api510DrumVesselCalculationWorkspace', () => ({
  Api510DrumVesselCalculationWorkspace: ({
    onSaveSnapshot,
    onCreateFindingDraft,
    initialEquipmentSubtype,
    initialHeadType
  }: {
    onSaveSnapshot?: (snapshot: unknown) => void;
    onCreateFindingDraft?: (draft: unknown) => void;
    initialEquipmentSubtype?: string;
    initialHeadType?: string;
  }) => (
    <section aria-label="Mock Drum/Vessel Calculation Workspace">
      <h3>API 510 Drum/Vessel Calculation Workspace</h3>
      <p>Subtype: {initialEquipmentSubtype}</p>
      <p>Head type: {initialHeadType}</p>
      <button type="button" onClick={() => onSaveSnapshot?.({ id: 'calc-1', label: 'Mock calculation' })}>Mock Save Calculation Snapshot</button>
      <button type="button" onClick={() => onCreateFindingDraft?.({ id: 'finding-1', findingText: 'Mock finding' })}>Mock Create Finding Draft</button>
    </section>
  )
}));

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

const horizontalDraft = {
  reportTypeId: 'api510.external.drum-vessel.horizontal-drum',
  reportTypeLabel: 'Horizontal Drum External',
  header: {
    inspectionDate: '2026-05-18',
    inspector: 'A. Inspector',
    clientFacility: 'Demo Refinery',
    unitArea: 'Crude Unit',
    equipmentIdentifier: 'D-101',
    service: 'Wet gas',
    inspectionReason: 'Routine external',
    operatingStatus: 'Online',
    inspectionScope: 'Accessible external inspection',
    vesselDesignPressure: '150',
    vesselDesignTemperature: '450',
    shellMaterialSpec: 'SA-516',
    shellMaterialGrade: '70',
    shellProductForm: 'Plate',
    headMaterialSpec: 'SA-516',
    headMaterialGrade: '70N',
    headProductForm: 'Plate',
    headType: 'Ellipsoidal2To1'
  },
  components: ['Shell', 'Heads', 'Nozzles', 'Saddles / Supports', 'Manway', 'External Coating', 'Insulation']
};

const verticalDraft = {
  ...horizontalDraft,
  reportTypeId: 'api510.external.drum-vessel.vertical-drum',
  reportTypeLabel: 'Vertical Drum External',
  components: ['Shell', 'Heads', 'Nozzles', 'Skirt / Legs / Supports', 'Manway']
};

const storeDraft = (draft = horizontalDraft) => {
  window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify(draft));
};

const renderRoute = (route = '/reports/api-510-drum-vessel-external') => render(<MemoryRouter initialEntries={[route]}><App /></MemoryRouter>);

beforeEach(() => {
  window.localStorage.clear();
});

afterEach(() => {
  cleanup();
  vi.useRealTimers();
  window.localStorage.clear();
});

describe('Api510DrumVesselExternalReportPage', () => {
  it('route renders', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'API 510 Drum/Vessel External Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Report Header' })).toBeInTheDocument();
  });

  it('wrong draft type shows warning', () => {
    storeDraft({ ...horizontalDraft, reportTypeId: 'api570.piping-external', reportTypeLabel: 'Piping External' });
    renderRoute();

    expect(screen.getByRole('alert')).toHaveTextContent('The saved draft setup is not for an API 510 External Drum/Vessel report.');
    expect(screen.getByRole('link', { name: 'Return to Start Wizard' })).toHaveAttribute('href', '/reports');
  });

  it('draft header/design/material fields appear', () => {
    storeDraft();
    renderRoute();

    expect(within(screen.getByLabelText('Report Header')).getByText('A. Inspector')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Report Header')).getByText('D-101')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Design Conditions')).getByText('150')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Design Conditions')).getByText('450')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Materials')).getAllByText('SA-516')).toHaveLength(2);
    expect(within(screen.getByLabelText('Materials')).getByText('70N')).toBeInTheDocument();
    expect(within(screen.getByLabelText('Materials')).getByText('Ellipsoidal2To1')).toBeInTheDocument();
  });

  it('Horizontal Drum default components appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Shell' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Heads' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Nozzles' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Saddles / Supports' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Manway' })).toBeInTheDocument();
  });

  it('Vertical Drum default components appear', () => {
    storeDraft(verticalDraft);
    renderRoute();

    expect(screen.getByRole('heading', { name: 'Shell' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Heads' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Nozzles' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Skirt / Legs / Supports' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Manway' })).toBeInTheDocument();
  });

  it('optional selected components appear', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByRole('heading', { name: 'External Coating' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Insulation' })).toBeInTheDocument();
  });

  it('component checklist fields appear', () => {
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

  it('embedded Drum/Vessel calculation workspace appears', () => {
    storeDraft();
    renderRoute();

    const calculations = screen.getByLabelText('Calculations');
    expect(within(calculations).getByRole('heading', { name: 'API 510 Drum/Vessel Calculation Workspace' })).toBeInTheDocument();
    expect(within(calculations).getByText('Subtype: Horizontal Drum')).toBeInTheDocument();
    expect(within(calculations).getByText('Head type: Ellipsoidal2To1')).toBeInTheDocument();
  });

  it('local save stores structured payload', () => {
    storeDraft();
    vi.useFakeTimers();
    vi.setSystemTime(new Date('2026-05-18T12:34:56.000Z'));
    renderRoute();

    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = JSON.parse(window.localStorage.getItem(API510_DRUM_VESSEL_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) as string);
    expect(saved).toMatchObject({
      reportTypeId: 'api510.external.drum-vessel.horizontal-drum',
      reportTypeLabel: 'Horizontal Drum External',
      equipmentSubtype: 'Horizontal Drum',
      savedAt: '2026-05-18T12:34:56.000Z',
      reportHeader: { inspector: 'A. Inspector', equipmentIdentifier: 'D-101' },
      designConditions: { vesselDesignPressure: '150', vesselDesignTemperature: '450' },
      materials: { shellMaterialSpec: 'SA-516', headMaterialGrade: '70N', headType: 'Ellipsoidal2To1' }
    });
    expect(readApi510DrumVesselExternalDraftPayload()?.reportHeader.equipmentIdentifier).toBe('D-101');
  });

  it('payload includes component states and checklist counts', () => {
    storeDraft();
    renderRoute();

    fireEvent.change(screen.getByLabelText('Shell Condition'), { target: { value: 'Requires Evaluation' } });
    fireEvent.click(screen.getByLabelText('Shell Repair Required'));
    fireEvent.click(screen.getByLabelText('Shell Photo Required'));
    fireEvent.click(screen.getAllByRole('button', { name: 'Save Draft Locally' })[0]);

    const saved = JSON.parse(window.localStorage.getItem(API510_DRUM_VESSEL_EXTERNAL_REPORT_LOCAL_STORAGE_KEY) as string);
    expect(saved.componentStates.Shell.condition).toBe('Requires Evaluation');
    expect(saved.componentStates.Shell.checklist['Repair Required']).toBe(true);
    expect(saved.checklistCounts['Repair Required']).toBe(1);
    expect(saved.checklistCounts['Photo Required']).toBe(1);
    expect(Object.keys(saved.componentStates)).toContain('External Coating');
  });

  it('calculation/finding counts update through workspace callbacks', () => {
    storeDraft();
    renderRoute();

    fireEvent.click(screen.getByRole('button', { name: 'Mock Save Calculation Snapshot' }));
    fireEvent.click(screen.getByRole('button', { name: 'Mock Create Finding Draft' }));

    const summary = screen.getByLabelText('Report Summary Preview');
    expect(within(summary).getByText('Calculation Snapshots').nextSibling).toHaveTextContent('1');
    expect(within(summary).getByText('Finding Drafts').nextSibling).toHaveTextContent('1');
  });

  it('View Draft Payload shows payload JSON', () => {
    storeDraft();
    renderRoute();

    expect(screen.getByText('View Draft Payload')).toBeInTheDocument();
    const debugPayload = screen.getByLabelText('API 510 Drum/Vessel draft payload');
    expect(debugPayload).toHaveTextContent('reportHeader');
    expect(debugPayload).toHaveTextContent('designConditions');
    expect(debugPayload).toHaveTextContent('componentStates');
  });

  it('Start Wizard exposes the Drum/Vessel External Report action after starting any Drum/Vessel draft', async () => {
    renderRoute('/reports');
    expect(await screen.findByRole('heading', { name: 'Start New API Inspection Report' })).toBeInTheDocument();
    fireEvent.click(screen.getByLabelText('Vertical Drum External'));
    expect(screen.queryByRole('button', { name: 'Open Drum/Vessel External Report' })).not.toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Start Draft Report' }));

    expect(await screen.findByRole('button', { name: 'Open Drum/Vessel External Report' })).toBeInTheDocument();
  });

  it('API 570 route still works', async () => {
    renderRoute('/reports/api-570-piping-external');

    expect(await screen.findByRole('heading', { name: /API 570 Piping External/i })).toBeInTheDocument();
  });

  it('Shell-and-Tube report route still works', () => {
    storeDraft({ ...horizontalDraft, reportTypeId: 'api510.external.exchanger.shell-tube', reportTypeLabel: 'Shell and Tube Exchanger External' });
    renderRoute('/reports/api-510-shell-tube-external');

    expect(screen.getByRole('heading', { name: 'API 510 Shell-and-Tube External Report' })).toBeInTheDocument();
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
    fireEvent.click(screen.getByLabelText('Shell Photo Required'));
    expect(within(assistCard).getByTestId('total-assist-warnings-count')).toHaveTextContent('1');
    expect(within(assistCard).getByTestId('missing-photo-tags-count')).toHaveTextContent('1');
  });
});
