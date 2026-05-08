import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it, vi } from 'vitest';
import { AppShell } from '../AppShell';

vi.mock('../../navigation/currentUserRole', () => ({
  getCurrentUserRole: () => 'admin',
}));

describe('AppShell', () => {
  it('renders top bar, left navigation, children, and security reminder', () => {
    render(
      <MemoryRouter initialEntries={['/dashboard']}>
        <AppShell>
          <div>Child Content</div>
        </AppShell>
      </MemoryRouter>,
    );

    expect(screen.getByText('iE Inspection Platform')).toBeInTheDocument();
    expect(screen.getByRole('navigation', { name: 'Primary' })).toBeInTheDocument();
    expect(screen.getByText('Child Content')).toBeInTheDocument();
    expect(screen.getByText('Role-based visibility in navigation is a UI convenience only.')).toBeInTheDocument();
  });
});
