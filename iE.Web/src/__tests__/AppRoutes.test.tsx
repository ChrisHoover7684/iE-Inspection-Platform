import { cleanup, fireEvent, render, screen, waitFor, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { allNavigationRoutes } from '../navigation/roleNavigation';
import { ndeApi, reportingApi } from '../api';

vi.mock('../api', async () => {
  const actual = await vi.importActual<typeof import('../api')>('../api');
  return {
    ...actual,
    reportingApi: {
      ...actual.reportingApi,
      getInstances: vi.fn().mockResolvedValue([]),
    },
    ndeApi: {
      ...actual.ndeApi,
      getLogItems: vi.fn().mockResolvedValue([
        { id: 'nde-005', requestNumber: 'NDE-26-005', circuitId: 'CIR-3C-118', method: 'PMI', status: 'Results Received', priority: 'High', requestedBy: 'G. Martin', assignedTo: 'V. Chen', dueDate: '2026-05-10', resultReceivedDate: '2026-05-09', reportStatus: 'In Progress', reportNumber: 'RPT-26-PMI-005' },
        { id: 'nde-006', requestNumber: 'NDE-26-006', equipmentTag: 'TK-804', method: 'PAUT', status: 'Reviewed', priority: 'Normal', requestedBy: 'P. Singh', assignedTo: 'N. Brooks', dueDate: '2026-05-09', resultReceivedDate: '2026-05-08', reportStatus: 'Complete', reportNumber: 'RPT-26-PAUT-006', reportFileName: 'RPT-26-PAUT-006.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-PAUT-006.pdf', accessType: 'Ladder' },
        { id: 'nde-007', requestNumber: 'NDE-26-007', assetTag: 'L-5507', method: 'VT', status: 'Closed', priority: 'Low', requestedBy: 'R. Scott', assignedTo: 'H. Diaz', dueDate: '2026-05-06', resultReceivedDate: '2026-05-05', reportStatus: 'Complete', reportNumber: 'RPT-26-VT-007', reportFileName: 'RPT-26-VT-007.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-VT-007.pdf' },
        { id: 'nde-001', requestNumber: 'NDE-26-001', assetTag: 'P-102A', method: 'UT Thickness', status: 'Draft', priority: 'Normal', requestedBy: 'J. Rivera', assignedTo: 'L. Tran', dueDate: '2026-05-20', reportStatus: 'Not Started', project: 'Demo Turnaround 2026', owningGroup: 'Inspection', code: 'API 570', unit: '01-CRUDE' },
        { id: 'nde-002', requestNumber: 'NDE-26-002', circuitId: 'CIR-4A-220', method: 'RT', status: 'Requested', priority: 'High', requestedBy: 'M. Patel', assignedTo: 'S. Owens', dueDate: '2026-05-17', reportStatus: 'In Progress', reportNumber: 'RPT-26-RT-002' },
        { id: 'nde-004', requestNumber: 'NDE-26-004', assetTag: 'HX-22B', method: 'PT', status: 'In Progress', priority: 'Critical', requestedBy: 'A. Lopez', assignedTo: 'D. Kim', dueDate: '2026-05-12', reportStatus: 'In Progress', reportNumber: 'RPT-26-PT-004', accessType: 'Rope Access', inspectionDetails: 'Nozzle N11 Weld 213 root and final cap PT verification before hydrotest.', scopeItems: [{ id: 'scope-004-pt-prep', method: 'PT', stage: 'Prep', displayName: 'PT Prep', weldId: 'W-22B-01', location: 'Nozzle N2 Root' }, { id: 'scope-004-pt-root', method: 'PT', stage: 'Root', displayName: 'PT Root', weldId: 'W-22B-01', location: 'Nozzle N2 Root' }, { id: 'scope-004-pt-final', method: 'PT', stage: 'Final', displayName: 'PT Final', weldId: 'W-22B-01', location: 'Nozzle N2 Cap' }] },
        { id: 'nde-003', requestNumber: 'NDE-26-003', equipmentTag: 'E-4401', method: 'MT', status: 'Scheduled', priority: 'Normal', requestedBy: 'T. Nguyen', assignedTo: 'R. Hall', dueDate: '2026-05-13', reportStatus: 'Not Available' },
        { id: 'nde-008', requestNumber: 'NDE-26-008', equipmentTag: 'PSV-91', method: 'UT Thickness', status: 'Cancelled', priority: 'Low', requestedBy: 'C. White', assignedTo: 'B. Young', dueDate: '2026-05-04', reportStatus: 'Not Started' },
        { id: 'nde-009', requestNumber: 'NDE-26-009', circuitId: 'CIR-9D-032', method: 'RT', status: 'Overdue', priority: 'Critical', requestedBy: 'D. Reed', assignedTo: 'M. Gray', dueDate: '2026-05-01', reportStatus: 'In Progress', reportNumber: 'RPT-26-RT-009' },
        { id: 'nde-010', requestNumber: 'NDE-26-010', assetTag: 'P-300C', method: 'PAUT', status: 'Reviewed', priority: 'High', requestedBy: 'L. Ward', assignedTo: 'K. Adams', dueDate: '2026-05-14', reportStatus: 'Complete', reportNumber: 'RPT-26-PAUT-010', reportFileName: 'RPT-26-PAUT-010.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-PAUT-010.pdf' }
      ]),
      transitionLogItem: vi.fn().mockResolvedValue({ id: 'nde-001', requestNumber: 'NDE-26-001', assetTag: 'P-102A', method: 'UT Thickness', status: 'Requested', priority: 'Normal', reportStatus: 'Not Started' }),
      getLogItemEvents: vi.fn().mockResolvedValue([
        { id: 'evt-1', ndeRequestId: 'nde-001', fromStatus: 'Draft', toStatus: 'Requested', actor: 'demo.user', timestampUtc: '2026-05-10T12:00:00Z', comment: 'ready for scheduling' }
      ])
    },
  };
});

const nonPlaceholderRoutes = new Set(['/dashboard', '/calculators/corrosion-rate', '/calculators/b31-3-piping', '/reports', '/nde-requests', '/nde-reports', '/schedule', '/results-received', '/overdue', '/cancelled', '/reference-data-projects', '/reference-data-units-assets']);

afterEach(() => {
  cleanup();
  vi.clearAllMocks();
  window.localStorage.clear();
});

describe('App routes', () => {
  it.each(allNavigationRoutes)('route %s resolves without dashboard fallback', (route: string) => {
    render(
      <MemoryRouter initialEntries={[route]}>
        <App />
      </MemoryRouter>,
    );

    if (nonPlaceholderRoutes.has(route)) {
      expect(screen.queryByText(/Coming soon:/)).not.toBeInTheDocument();
    } else {
      expect(screen.getByText(/Coming soon:/)).toBeInTheDocument();
    }
  });

  it('renders dashboard home sections and does not load report API data', () => {
    render(
      <MemoryRouter initialEntries={['/dashboard']}>
        <App />
      </MemoryRouter>,
    );

    const main = within(screen.getByRole('main'));

    expect(main.getByRole('heading', { name: 'Engineering Tools' })).toBeInTheDocument();
    expect(main.getByRole('heading', { name: 'API Inspection Reports' })).toBeInTheDocument();
    expect(main.getByRole('heading', { name: 'NDE Log / Reports' })).toBeInTheDocument();
    expect(reportingApi.getInstances).not.toHaveBeenCalled();
  });




  it.each([
    ['/results-received', 'Results Received'],
    ['/schedule', 'Schedule'],
    ['/overdue', 'Overdue'],
    ['/cancelled', 'Cancelled'],
    ['/nde-reports', 'NDE Reports'],
  ])('renders route header %s as %s', (route: string, expectedHeader: string) => {
    render(
      <MemoryRouter initialEntries={[route]}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByRole('heading', { level: 1, name: expectedHeader })).toBeInTheDocument();
    expect(screen.queryByRole('heading', { level: 1, name: 'Coming soon' })).not.toBeInTheDocument();
  });


  it('loads NDE rows from ndeApi without real backend calls', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();
    expect(ndeApi.getLogItems).toHaveBeenCalledTimes(1);
  });

  it('scopes NDE reports route to report-centric statuses', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-005')).toBeInTheDocument();
    expect(screen.getByText('NDE-26-006')).toBeInTheDocument();
    expect(screen.getByText('NDE-26-007')).toBeInTheDocument();
    expect(screen.queryByText('NDE-26-002')).not.toBeInTheDocument();
  });

  it('defaults schedule route status filter to Scheduled', () => {
    render(
      <MemoryRouter initialEntries={['/schedule']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByDisplayValue('Scheduled')).toBeInTheDocument();
    expect(screen.getByText('NDE-26-003')).toBeInTheDocument();
    expect(screen.queryByText('NDE-26-010')).not.toBeInTheDocument();
    expect(screen.queryByText('NDE-26-004')).not.toBeInTheDocument();
  });


  it('shows enabled Download Report action for report-ready rows', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    const readyRow = screen.getByText('NDE-26-006').closest('tr');
    expect(readyRow).not.toBeNull();
    expect(within(readyRow as HTMLTableRowElement).getByRole('button', { name: 'Download Report' })).toBeEnabled();
  });

  it('shows enabled Download Report action for downloaded rows', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    const downloadedRow = screen.getByText('NDE-26-007').closest('tr');
    expect(downloadedRow).not.toBeNull();
    expect(within(downloadedRow as HTMLTableRowElement).getByRole('button', { name: 'Download Report' })).toBeEnabled();
  });

  it('shows disabled Download Report action for non-ready rows with connected-yet guidance', () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    const nonReadyRow = screen.getByText('NDE-26-001').closest('tr');
    expect(nonReadyRow).not.toBeNull();
    const downloadButton = within(nonReadyRow as HTMLTableRowElement).getByRole('button', { name: 'Download Report' });
    expect(downloadButton).toBeDisabled();
    expect(downloadButton).toHaveAttribute('title', 'Report template/download not connected yet.');
  });

  it('enables bulk download when a downloadable row is selected', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    const bulkDownloadButton = screen.getByRole('button', { name: 'Bulk Download Reports' });
    expect(bulkDownloadButton).toBeDisabled();

    fireEvent.click(screen.getByRole('checkbox', { name: 'Select NDE-26-007' }));
    expect(bulkDownloadButton).toBeEnabled();
  });

  it('shows export table action in NDE workspace routes', () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByRole('button', { name: 'Export Table' })).toBeInTheDocument();
  });

  it('opens in-page Edit Request modal and saves updated request fields into details, table, and search', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();
    fireEvent.click(screen.getByText('NDE-26-001'));
    fireEvent.click(screen.getByRole('button', { name: 'Edit Request' }));

    const modal = screen.getByRole('dialog', { name: 'Edit NDE Request' });
    expect(within(modal).getByDisplayValue('NDE-26-001')).toHaveAttribute('readonly');
    expect(within(modal).getByLabelText('Project *')).toBeInTheDocument();
    expect(within(modal).getByLabelText('Owning Group *')).toBeInTheDocument();
    expect(within(modal).getByLabelText('NDE Method *')).toBeInTheDocument();
    expect(within(modal).getByLabelText('Access Method *')).toBeInTheDocument();
    expect(within(modal).getByLabelText('Reference')).toBeInTheDocument();
    expect(within(modal).getByLabelText('Inspection Details')).toBeInTheDocument();

    expect(within(modal).getByLabelText('Project *')).toHaveValue('Demo Turnaround 2026');
    expect(within(modal).getByLabelText('Owning Group *')).toHaveValue('Inspection');
    expect(within(modal).getByLabelText('Code Criteria')).toHaveValue('API 570');
    expect(within(modal).getByLabelText('Unit *')).toHaveValue('01-CRUDE');

    fireEvent.change(within(modal).getByLabelText('Project *'), { target: { value: 'Unit 73 Maintenance' } });
    fireEvent.change(within(modal).getByLabelText('Owning Group *'), { target: { value: 'Operations' } });
    fireEvent.change(within(modal).getByLabelText('Code Criteria'), { target: { value: 'NBIC' } });
    fireEvent.change(within(modal).getByLabelText('Unit *'), { target: { value: '02-VAC' } });
    fireEvent.change(within(modal).getByLabelText('Reference'), { target: { value: 'REF-EDIT-900' } });
    fireEvent.change(within(modal).getByLabelText('NDE Method *'), { target: { value: 'RT' } });
    fireEvent.change(within(modal).getByLabelText('Due Date *'), { target: { value: '2026-05-21' } });
    fireEvent.change(within(modal).getByLabelText('Asset *'), { target: { value: 'P-102A' } });
    fireEvent.change(within(modal).getByLabelText('Access Method *'), { target: { value: 'Ladder' } });
    fireEvent.change(within(modal).getByLabelText('Inspection Details'), { target: { value: 'Edited in-page demo details' } });
    fireEvent.click(within(modal).getByRole('button', { name: 'Save' }));

    expect(screen.getByText('Request edits are demo-only until backend persistence is connected.')).toBeInTheDocument();
    expect(screen.getByText('REF-EDIT-900')).toBeInTheDocument();
    expect(screen.getByText('Edited in-page demo details')).toBeInTheDocument();
    expect(screen.getByText('Unit 73 Maintenance')).toBeInTheDocument();
    expect(screen.getByText('Operations')).toBeInTheDocument();
    expect(screen.getByText('NBIC')).toBeInTheDocument();
    expect(screen.getByText('Unit 77')).toBeInTheDocument();

    const updatedRow = screen.getAllByText('NDE-26-001')[0].closest('tr') as HTMLTableRowElement;
    expect(within(updatedRow).getByText('RT')).toBeInTheDocument();

    fireEvent.change(screen.getByPlaceholderText('Search request #, asset, method, project, owning group, code criteria, unit, access method, reference, inspection details, or assignee'), { target: { value: 'REF-EDIT-900' } });
    expect(screen.getAllByText('NDE-26-001').length).toBeGreaterThan(0);

    fireEvent.change(screen.getByPlaceholderText('Search request #, asset, method, project, owning group, code criteria, unit, access method, reference, inspection details, or assignee'), { target: { value: 'Unit 73 Maintenance' } });
    expect(screen.getAllByText('NDE-26-001').length).toBeGreaterThan(0);

    fireEvent.change(screen.getByPlaceholderText('Search request #, asset, method, project, owning group, code criteria, unit, access method, reference, inspection details, or assignee'), { target: { value: 'Operations' } });
    expect(screen.getAllByText('NDE-26-001').length).toBeGreaterThan(0);

    fireEvent.change(screen.getByPlaceholderText('Search request #, asset, method, project, owning group, code criteria, unit, access method, reference, inspection details, or assignee'), { target: { value: 'NBIC' } });
    expect(screen.getAllByText('NDE-26-001').length).toBeGreaterThan(0);
  });

  it('supports access method/reference in create form, details, and search without filter dropdown', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();
    expect(screen.queryByDisplayValue('All')).toBeInTheDocument();
    expect(screen.queryByLabelText('Access Method')).not.toBeInTheDocument();

    fireEvent.change(screen.getByPlaceholderText('Search request #, asset, method, project, owning group, code criteria, unit, access method, reference, inspection details, or assignee'), { target: { value: 'Rope Access' } });
    expect(screen.getByText('NDE-26-004')).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: '+ New NDE Request' }));
    const modal = screen.getByRole('dialog', { name: 'New NDE Request' });
    expect(within(modal).getByLabelText('Requester')).toHaveValue('Operator (placeholder)');
    expect(within(modal).getByLabelText('Access Method *')).toBeInTheDocument();
    expect(within(modal).getByText('Project options are demo reference data. Admin-managed project setup will be connected later.')).toBeInTheDocument();

    fireEvent.change(within(modal).getByLabelText('Project *'), { target: { value: 'Demo Turnaround 2026' } });
    fireEvent.change(within(modal).getByLabelText('Owning Group *'), { target: { value: 'Inspection' } });
    fireEvent.change(within(modal).getByLabelText('Due Date *'), { target: { value: '2026-05-22' } });
    fireEvent.change(within(modal).getByLabelText('NDE Method *'), { target: { value: 'UT Thickness' } });
    fireEvent.change(within(modal).getByLabelText('Unit *'), { target: { value: '01-CRUDE' } });
    expect(within(modal).queryByText('Robinson TA 2026')).not.toBeInTheDocument();
    fireEvent.change(within(modal).getByLabelText('Asset *'), { target: { value: 'P-102A' } });
    fireEvent.change(within(modal).getByLabelText('Access Method *'), { target: { value: 'Rope Access' } });
    fireEvent.change(within(modal).getByLabelText('Reference'), { target: { value: 'REF-ROPE-77' } });
    fireEvent.click(screen.getByRole('button', { name: 'Create Request' }));

    fireEvent.click(screen.getAllByText('NDE-26-011')[0]);
    expect(screen.getAllByText('Rope Access').length).toBeGreaterThan(0);
    expect(screen.getByText('REF-ROPE-77')).toBeInTheDocument();
  });

  it('opens new NDE request modal with dropdown fields and searchable inspection details', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: '+ New NDE Request' }));
    expect(screen.getByRole('dialog', { name: 'New NDE Request' })).toBeInTheDocument();
    expect(screen.getByLabelText('Project *')).toBeInTheDocument();
    expect(screen.getByLabelText('Owning Group *')).toBeInTheDocument();
    expect(screen.getByLabelText('Requester')).toBeInTheDocument();
    expect(screen.getByLabelText('Priority')).toBeInTheDocument();
    expect(screen.getByLabelText('Due Date *')).toBeInTheDocument();
    expect(screen.getByLabelText('NDE Method *')).toBeInTheDocument();
    expect(screen.getByLabelText('Code Criteria')).toBeInTheDocument();
    expect(screen.getByLabelText('Unit *')).toBeInTheDocument();
    expect(screen.getByLabelText('Asset *')).toBeInTheDocument();
    expect(screen.getByLabelText('Inspection Details')).toBeInTheDocument();

    expect(screen.queryByLabelText('Billing #1')).not.toBeInTheDocument();
    expect(screen.queryByLabelText('PT Root')).not.toBeInTheDocument();
    fireEvent.change(screen.getByLabelText('Project *'), { target: { value: 'Demo Turnaround 2026' } });
    fireEvent.change(screen.getByLabelText('Owning Group *'), { target: { value: 'Inspection' } });
    fireEvent.change(screen.getByLabelText('Due Date *'), { target: { value: '2026-05-22' } });
    fireEvent.change(screen.getByLabelText('NDE Method *'), { target: { value: 'UT Thickness' } });
    fireEvent.change(screen.getByLabelText('Unit *'), { target: { value: '01-CRUDE' } });
    fireEvent.change(screen.getByLabelText('Asset *'), { target: { value: 'P-102A' } });
    fireEvent.change(screen.getByLabelText('Access Method *'), { target: { value: 'Ladder' } });
    fireEvent.change(screen.getByLabelText('Request Status'), { target: { value: 'Requested' } });
    fireEvent.change(screen.getByLabelText('Inspection Details'), { target: { value: 'Created request detail unique token ZX-441' } });

    fireEvent.click(screen.getByRole('button', { name: 'Create Request' }));
    expect(await screen.findByText('Request creation is demo-only (in-memory) until backend persistence is connected.')).toBeInTheDocument();
    expect(screen.getByText('Created request detail unique token ZX-441')).toBeInTheDocument();
    fireEvent.change(screen.getByPlaceholderText('Search request #, asset, method, project, owning group, code criteria, unit, access method, reference, inspection details, or assignee'), { target: { value: 'zx-441' } });
    expect(screen.queryByText('NDE-26-004')).not.toBeInTheDocument();
    expect(screen.queryByText('No NDE Requests NDE items yet')).not.toBeInTheDocument();
  });

  it('blocks create and save when required fields are missing and shows validation message', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();
    fireEvent.click(screen.getByRole('button', { name: '+ New NDE Request' }));
    fireEvent.click(screen.getByRole('button', { name: 'Create Request' }));
    expect(screen.getByText('Project, Owning Group, Due Date, NDE Method, Unit, Asset, and Access Method are required.')).toBeInTheDocument();
    expect(screen.queryByText('NDE-26-011')).not.toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Cancel' }));
    fireEvent.click(screen.getByText('NDE-26-001'));
    fireEvent.click(screen.getByRole('button', { name: 'Edit Request' }));
    const editModal = screen.getByRole('dialog', { name: 'Edit NDE Request' });
    fireEvent.change(within(editModal).getByLabelText('Asset *'), { target: { value: '' } });
    fireEvent.click(within(editModal).getByRole('button', { name: 'Save' }));
    expect(within(editModal).getByText('Project, Owning Group, Due Date, NDE Method, Unit, Asset, and Access Method are required.')).toBeInTheDocument();
  });


  it('renders reference-data-projects page with default demo options and no Robinson-specific names', () => {
    render(
      <MemoryRouter initialEntries={['/reference-data-projects', '/reference-data-units-assets']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByRole('heading', { level: 2, name: 'Reference Data / Projects' })).toBeInTheDocument();
    expect(screen.getByDisplayValue('Demo Turnaround 2026')).toBeInTheDocument();
    expect(screen.getByDisplayValue('Unit 73 Maintenance')).toBeInTheDocument();
    expect(screen.queryByText('Robinson TA 2026')).not.toBeInTheDocument();
    expect(screen.queryByText('MPC Robinson 2026 TA')).not.toBeInTheDocument();
  });

  it('allows admin to add/deactivate/reset project options and reflects active options in New NDE Request project dropdown', async () => {
    render(
      <MemoryRouter initialEntries={['/reference-data-projects', '/reference-data-units-assets']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.change(screen.getByPlaceholderText('Add a project option'), { target: { value: 'Demo Reliability Sprint' } });
    fireEvent.click(screen.getByRole('button', { name: 'Add Project' }));
    expect(screen.getByDisplayValue('Demo Reliability Sprint')).toBeInTheDocument();

    const addedRow = screen.getByDisplayValue('Demo Reliability Sprint').closest('tr') as HTMLTableRowElement;
    fireEvent.click(within(addedRow).getByRole('button', { name: 'Deactivate' }));
    expect(within(addedRow).getByText('Inactive')).toBeInTheDocument();

    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );
    fireEvent.click(screen.getByRole('button', { name: '+ New NDE Request' }));
    const projectSelect = screen.getByLabelText('Project *');
    expect(within(projectSelect).queryByRole('option', { name: 'Demo Reliability Sprint' })).not.toBeInTheDocument();

    cleanup();
    render(
      <MemoryRouter initialEntries={['/reference-data-projects', '/reference-data-units-assets']}>
        <App />
      </MemoryRouter>,
    );
    fireEvent.click(screen.getByRole('button', { name: 'Reset to Demo Defaults' }));
    expect(screen.queryByDisplayValue('Demo Reliability Sprint')).not.toBeInTheDocument();
    expect(screen.getByDisplayValue('Demo Turnaround 2026')).toBeInTheDocument();
  });

  it('loads API Inspection Reports page from /reports', async () => {
    render(
      <MemoryRouter initialEntries={['/reports']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByRole('heading', { level: 1, name: 'API Inspection Reports' })).toBeInTheDocument();
    await waitFor(() => expect(reportingApi.getInstances).toHaveBeenCalledTimes(1));
  });

  it('selecting a row shows valid actions and transition calls ndeApi then refetches', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();

    fireEvent.click(screen.getByText('NDE-26-001'));
    expect(screen.getByText('Selected: NDE-26-001 (Draft)')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Close details' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Mark Requested' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Cancel' })).toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Mark Scheduled' })).not.toBeInTheDocument();

    fireEvent.change(screen.getByPlaceholderText('Enter transition reason'), { target: { value: 'Progressing to request' } });
    fireEvent.click(screen.getByRole('button', { name: 'Mark Requested' }));
    await waitFor(() => expect(ndeApi.transitionLogItem).toHaveBeenCalledWith('nde-001', 'Requested', 'Progressing to request', 'demo.user'));
    await waitFor(() => expect(ndeApi.getLogItems).toHaveBeenCalledTimes(2));
  });

  it('invalid/error transition shows message', async () => {
    vi.mocked(ndeApi.transitionLogItem).mockRejectedValueOnce(new Error('boom'));

    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();
    fireEvent.click(screen.getByText('NDE-26-001'));
    fireEvent.click(screen.getByRole('button', { name: 'Mark Requested' }));

    expect(await screen.findByText('Unable to persist transition to API. Applied demo fallback update.')).toBeInTheDocument();
  });

  it('shows no forward workflow actions for Closed rows', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.click(screen.getByText('NDE-26-007'));
    expect(screen.getByText('Selected: NDE-26-007 (Closed)')).toBeInTheDocument();
    expect(screen.getByText('No workflow actions available for this status.')).toBeInTheDocument();
  });

  it('shows selected row report number in detail panel when available', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.click(screen.getByText('NDE-26-006'));
    const detailsPanel = screen.getByLabelText('NDE request details');
    expect(within(detailsPanel).getByText('RPT-26-PAUT-006')).toBeInTheDocument();
  });

  it('shows weld examination stages as request scope items in details', async () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(await screen.findByText('NDE-26-004')).toBeInTheDocument();
    fireEvent.click(screen.getByText('NDE-26-004'));
    expect(screen.getByRole('heading', { name: 'Request Scope Items' })).toBeInTheDocument();
    expect(screen.getByText('PT Prep')).toBeInTheDocument();
    expect(screen.getByText('PT Root')).toBeInTheDocument();
    expect(screen.getByText('PT Final')).toBeInTheDocument();
  });

  it('close details clears selection', () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.click(screen.getByText('NDE-26-001'));
    expect(screen.getByText('Selected: NDE-26-001 (Draft)')).toBeInTheDocument();
    fireEvent.click(screen.getByRole('button', { name: 'Close details' }));
    expect(screen.queryByText('Selected: NDE-26-001 (Draft)')).not.toBeInTheDocument();
  });

  it('shows no forward workflow actions for Cancelled rows', () => {
    render(
      <MemoryRouter initialEntries={['/cancelled']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.click(screen.getByText('NDE-26-008'));
    expect(screen.getByText('Selected: NDE-26-008 (Cancelled)')).toBeInTheDocument();
    expect(screen.getByText('No workflow actions available for this status.')).toBeInTheDocument();
  });
});


it('selecting a row loads workflow history and renders transition details', async () => {
  render(
    <MemoryRouter initialEntries={['/nde-requests']}>
      <App />
    </MemoryRouter>,
  );

  expect(await screen.findByText('NDE-26-001')).toBeInTheDocument();
  fireEvent.click(screen.getByText('NDE-26-001'));

  await waitFor(() => expect(ndeApi.getLogItemEvents).toHaveBeenCalledWith('nde-001'));
  expect(await screen.findByText('Draft → Requested')).toBeInTheDocument();
  expect(screen.getByText('ready for scheduling')).toBeInTheDocument();
});
