import { cleanup, render, screen, waitFor, within } from '@testing-library/react';
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

  it('loads API Inspection Reports page from /reports', async () => {
    render(
      <MemoryRouter initialEntries={['/reports']}>
        <App />
      </MemoryRouter>,
    );

    expect(screen.getByRole('heading', { level: 1, name: 'API Inspection Reports' })).toBeInTheDocument();
    await waitFor(() => expect(reportingApi.getInstances).toHaveBeenCalledTimes(1));
  });
});
