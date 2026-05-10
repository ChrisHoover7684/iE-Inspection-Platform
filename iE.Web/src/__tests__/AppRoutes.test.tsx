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
        { id: 'nde-005', requestNumber: 'NDE-26-005', circuitId: 'CIR-3C-118', method: 'PMI', status: 'Results Received', priority: 'High', requestedBy: 'G. Martin', assignedTo: 'V. Chen', dueDate: '2026-05-10', resultReceivedDate: '2026-05-09', reportStatus: 'Results Received', reportNumber: 'RPT-26-PMI-005' },
        { id: 'nde-006', requestNumber: 'NDE-26-006', equipmentTag: 'TK-804', method: 'PAUT', status: 'Reviewed', priority: 'Normal', requestedBy: 'P. Singh', assignedTo: 'N. Brooks', dueDate: '2026-05-09', resultReceivedDate: '2026-05-08', reportStatus: 'Report Ready', reportNumber: 'RPT-26-PAUT-006', reportFileName: 'RPT-26-PAUT-006.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-PAUT-006.pdf' },
        { id: 'nde-007', requestNumber: 'NDE-26-007', assetTag: 'L-5507', method: 'VT', status: 'Closed', priority: 'Low', requestedBy: 'R. Scott', assignedTo: 'H. Diaz', dueDate: '2026-05-06', resultReceivedDate: '2026-05-05', reportStatus: 'Downloaded', reportNumber: 'RPT-26-VT-007', reportFileName: 'RPT-26-VT-007.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-VT-007.pdf' },
        { id: 'nde-001', requestNumber: 'NDE-26-001', assetTag: 'P-102A', method: 'UT Thickness', status: 'Draft', priority: 'Normal', requestedBy: 'J. Rivera', assignedTo: 'L. Tran', dueDate: '2026-05-20', reportStatus: 'Not Started' },
        { id: 'nde-003', requestNumber: 'NDE-26-003', equipmentTag: 'E-4401', method: 'MT', status: 'Scheduled', priority: 'Normal', requestedBy: 'T. Nguyen', assignedTo: 'R. Hall', dueDate: '2026-05-13', reportStatus: 'Not Available' },
        { id: 'nde-008', requestNumber: 'NDE-26-008', equipmentTag: 'PSV-91', method: 'UT Thickness', status: 'Cancelled', priority: 'Low', requestedBy: 'C. White', assignedTo: 'B. Young', dueDate: '2026-05-04', reportStatus: 'Not Started' },
        { id: 'nde-009', requestNumber: 'NDE-26-009', circuitId: 'CIR-9D-032', method: 'RT', status: 'Overdue', priority: 'Critical', requestedBy: 'D. Reed', assignedTo: 'M. Gray', dueDate: '2026-05-01', reportStatus: 'Results Received', reportNumber: 'RPT-26-RT-009' },
        { id: 'nde-010', requestNumber: 'NDE-26-010', assetTag: 'P-300C', method: 'PAUT', status: 'Scheduled', priority: 'High', requestedBy: 'L. Ward', assignedTo: 'K. Adams', dueDate: '2026-05-14', reportStatus: 'Report Ready', reportNumber: 'RPT-26-PAUT-010', reportFileName: 'RPT-26-PAUT-010.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-PAUT-010.pdf' }
      ]),
      transitionLogItem: vi.fn().mockResolvedValue({ id: 'nde-001', requestNumber: 'NDE-26-001', assetTag: 'P-102A', method: 'UT Thickness', status: 'Requested', priority: 'Normal', reportStatus: 'Not Started' }),
      getLogItemEvents: vi.fn().mockResolvedValue([
        { id: 'evt-1', ndeRequestId: 'nde-001', fromStatus: 'Draft', toStatus: 'Requested', actor: 'demo.user', timestampUtc: '2026-05-10T12:00:00Z', comment: 'ready for scheduling' }
      ])
    },
  };
});

const nonPlaceholderRoutes = new Set(['/dashboard', '/calculators/corrosion-rate', '/calculators/b31-3-piping', '/reports', '/nde-requests', '/nde-reports', '/schedule', '/results-received', '/overdue', '/cancelled']);

afterEach(() => {
  cleanup();
  vi.clearAllMocks();
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
    expect(screen.getByText('NDE-26-010')).toBeInTheDocument();
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
