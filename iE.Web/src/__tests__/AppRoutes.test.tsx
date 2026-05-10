import { cleanup, fireEvent, render, screen, waitFor, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { afterEach, describe, expect, it, vi } from 'vitest';
import App from '../App';
import { allNavigationRoutes } from '../navigation/roleNavigation';
import { reportingApi } from '../api';

vi.mock('../api', async () => {
  const actual = await vi.importActual<typeof import('../api')>('../api');
  return {
    ...actual,
    reportingApi: {
      ...actual.reportingApi,
      getInstances: vi.fn().mockResolvedValue([]),
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

  it('scopes NDE reports route to report-centric statuses', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByText('NDE-24-005')).toBeInTheDocument();
    expect(screen.getByText('NDE-24-006')).toBeInTheDocument();
    expect(screen.getByText('NDE-24-007')).toBeInTheDocument();
    expect(screen.queryByText('NDE-24-002')).not.toBeInTheDocument();
  });

  it('defaults schedule route status filter to Scheduled', () => {
    render(
      <MemoryRouter initialEntries={['/schedule']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByDisplayValue('Scheduled')).toBeInTheDocument();
    expect(screen.getByText('NDE-24-003')).toBeInTheDocument();
    expect(screen.getByText('NDE-24-010')).toBeInTheDocument();
    expect(screen.queryByText('NDE-24-004')).not.toBeInTheDocument();
  });


  it('shows enabled Download Report action for report-ready rows', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    const readyRow = screen.getByText('NDE-24-006').closest('tr');
    expect(readyRow).not.toBeNull();
    expect(within(readyRow as HTMLTableRowElement).getByRole('button', { name: 'Download Report' })).toBeEnabled();
  });

  it('shows enabled Download Report action for downloaded rows', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    const downloadedRow = screen.getByText('NDE-24-007').closest('tr');
    expect(downloadedRow).not.toBeNull();
    expect(within(downloadedRow as HTMLTableRowElement).getByRole('button', { name: 'Download Report' })).toBeEnabled();
  });

  it('shows disabled Download Report action for non-ready rows with connected-yet guidance', () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    const nonReadyRow = screen.getByText('NDE-24-001').closest('tr');
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

    fireEvent.click(screen.getByRole('checkbox', { name: 'Select NDE-24-007' }));
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

  it('enforces valid frontend-only NDE workflow transitions and no-action statuses', () => {
    render(
      <MemoryRouter initialEntries={['/nde-requests']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByText('Workflow actions are frontend-only demo behavior until backend persistence is connected.')).toBeInTheDocument();

    fireEvent.click(screen.getByText('NDE-24-001'));
    expect(screen.getByText('Selected: NDE-24-001 (Draft)')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Mark Requested' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Cancel' })).toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Mark Scheduled' })).not.toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Mark Requested' }));
    expect(screen.getByText('Selected: NDE-24-001 (Requested)')).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Mark Scheduled' }));
    expect(screen.getByText('Selected: NDE-24-001 (Scheduled)')).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Mark In Progress' }));
    expect(screen.getByText('Selected: NDE-24-001 (In Progress)')).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Mark Results Received' }));
    expect(screen.getByText('Selected: NDE-24-001 (Results Received)')).toBeInTheDocument();
    const todayIso = new Date().toISOString().slice(0, 10);
    const selectedRow = screen.getByText('NDE-24-001').closest('tr');
    expect(selectedRow).not.toBeNull();
    expect(within(selectedRow as HTMLTableRowElement).getByText(todayIso)).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Mark Reviewed' }));
    expect(screen.getByText('Selected: NDE-24-001 (Reviewed)')).toBeInTheDocument();

    fireEvent.click(screen.getByRole('button', { name: 'Mark Closed' }));
  });

  it('shows no forward workflow actions for Closed rows', () => {
    render(
      <MemoryRouter initialEntries={['/nde-reports']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.click(screen.getByText('NDE-24-007'));
    expect(screen.getByText('Selected: NDE-24-007 (Closed)')).toBeInTheDocument();
    expect(screen.getByText('No workflow actions available for this status.')).toBeInTheDocument();
  });

  it('shows no forward workflow actions for Cancelled rows', () => {
    render(
      <MemoryRouter initialEntries={['/cancelled']}>
        <App />
      </MemoryRouter>,
    );

    fireEvent.click(screen.getByText('NDE-24-008'));
    expect(screen.getByText('Selected: NDE-24-008 (Cancelled)')).toBeInTheDocument();
    expect(screen.getByText('No workflow actions available for this status.')).toBeInTheDocument();
  });
});
