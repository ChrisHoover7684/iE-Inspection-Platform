import { fireEvent, render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it, vi } from 'vitest';
import App from '../App';

vi.mock('../api', async () => {
  const actual = await vi.importActual<typeof import('../api')>('../api');
  return {
    ...actual,
    reportingApi: {
      ...actual.reportingApi,
      getInstances: vi.fn().mockResolvedValue([])
    }
  };
});

describe('ApiInspectionReportStartWizard launcher', () => {
  it('/reports renders dashboard content', async () => {
    render(<MemoryRouter initialEntries={['/reports']}><App /></MemoryRouter>);

    expect(await screen.findByRole('button', { name: 'Create Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Total Reports' })).toBeInTheDocument();
  });

  it('/reports/new renders launcher content and excludes old wizard text', async () => {
    render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);

    expect(await screen.findByRole('heading', { name: 'Create Inspection Report' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Step 1: Choose Inspection Standard' })).toBeInTheDocument();
    expect(screen.queryByText('Report Header')).not.toBeInTheDocument();
    expect(screen.queryByText('Design Conditions')).not.toBeInTheDocument();
    expect(screen.queryByText('Materials')).not.toBeInTheDocument();
    expect(screen.queryByText('Components')).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Start Draft Report' })).not.toBeInTheDocument();
    expect(screen.queryByRole('button', { name: 'Open Engineering Tools' })).not.toBeInTheDocument();
  });

  it('Start Inspection Report is disabled initially and enables with valid selections', async () => {
    render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);

    const startButton = await screen.findByRole('button', { name: 'Start Inspection Report' });
    expect(startButton).toBeDisabled();

    fireEvent.click(screen.getByLabelText('API 510 Pressure Equipment'));
    expect(startButton).toBeDisabled();

    fireEvent.click(screen.getByLabelText('API 510 Shell and Tube External'));
    expect(startButton).toBeEnabled();
  });

  it('selecting a standard reveals only valid report types', async () => {
    render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);

    await screen.findByRole('heading', { name: 'Create Inspection Report' });
    fireEvent.click(screen.getByLabelText('STI / API 653 Tanks'));

    expect(screen.getByLabelText('STI / API 653 Tank External')).toBeInTheDocument();
    expect(screen.queryByLabelText('API 510 Drum / Vessel External')).not.toBeInTheDocument();
    expect(screen.queryByLabelText('API 570 Piping External')).not.toBeInTheDocument();
  });

  it('clicking Start Inspection Report navigates to expected routes', async () => {
    const { rerender } = render(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);

    await screen.findByRole('heading', { name: 'Create Inspection Report' });
    fireEvent.click(screen.getByLabelText('API 570 Piping'));
    fireEvent.click(screen.getByLabelText('API 570 Piping CUI External'));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByRole('heading', { name: 'API 570 Piping External Visual Inspection Report' })).toBeInTheDocument();

    rerender(<MemoryRouter initialEntries={['/reports/new']}><App /></MemoryRouter>);
    fireEvent.click(await screen.findByLabelText('API 510 Pressure Equipment'));
    fireEvent.click(screen.getByLabelText('API 510 Drum / Vessel External'));
    fireEvent.click(screen.getByRole('button', { name: 'Start Inspection Report' }));
    expect(await screen.findByRole('heading', { name: 'API 510 Drum / Vessel External Inspection Report' })).toBeInTheDocument();
  });
});
