import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it, vi } from 'vitest';
import { DashboardPage } from '../DashboardPage';

vi.mock('../api', () => ({
  reportingApi: { getInstances: vi.fn().mockResolvedValue([]) },
  ApiError: class extends Error {},
}));

describe('DashboardPage', () => {
  it('does not render legacy duplicate sidebar sections', () => {
    render(
      <MemoryRouter>
        <DashboardPage />
      </MemoryRouter>,
    );

    expect(screen.queryByText('Engineering Tools')).not.toBeInTheDocument();
    expect(screen.queryByText('Report Actions')).not.toBeInTheDocument();
  });
});
