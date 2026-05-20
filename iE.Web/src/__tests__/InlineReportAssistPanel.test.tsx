import { fireEvent, render, screen } from '@testing-library/react';
import { describe, expect, it, vi } from 'vitest';
import { InlineReportAssistPanel } from '../reporting/InlineReportAssistPanel';
import type { Api510ShellTubeExternalComponentState } from '../types';

const baseState = (): Api510ShellTubeExternalComponentState => ({
  condition: '',
  location: '',
  findingNotes: '',
  recommendationText: '',
  photoTag: '',
  checklist: {
    'Create Finding': false,
    'Recommendation Required': false,
    'Repair Required': false,
    'Photo Required': false,
    'NDE Required': false,
    'Add to Summary': false
  }
});

describe('InlineReportAssistPanel', () => {
  it('pitting note creates depth/extent suggestion and wording is shown but not auto-applied', () => {
    const state = baseState();
    state.findingNotes = 'Observed pitting on lower shell course';
    const onChange = vi.fn();
    render(<InlineReportAssistPanel component="Shell" state={state} onChange={onChange} />);
    expect(screen.getByText('Pitting detail needed')).toBeInTheDocument();
    expect(screen.getByText(/depth, extent, and localized\/general/i)).toBeInTheDocument();
    expect(screen.getByText('Suggested Finding Wording')).toBeInTheDocument();
    expect(onChange).not.toHaveBeenCalled();
  });

  it('photo required without photo tag creates warning', () => {
    const state = baseState();
    state.checklist['Photo Required'] = true;
    render(<InlineReportAssistPanel component="Shell" state={state} onChange={() => undefined} />);
    expect(screen.getByText('Photo tag missing')).toBeInTheDocument();
  });

  it('recommendation required without text creates warning', () => {
    const state = baseState();
    state.checklist['Recommendation Required'] = true;
    render(<InlineReportAssistPanel component="Shell" state={state} onChange={() => undefined} />);
    expect(screen.getByText('Recommendation text missing')).toBeInTheDocument();
  });

  it('create finding without notes creates warning', () => {
    const state = baseState();
    state.checklist['Create Finding'] = true;
    render(<InlineReportAssistPanel component="Shell" state={state} onChange={() => undefined} />);
    expect(screen.getByText('Finding notes missing')).toBeInTheDocument();
  });

  it('Apply Suggested Flags updates component checkbox flags', () => {
    const state = baseState();
    state.checklist['Recommendation Required'] = true;
    const onChange = vi.fn();
    render(<InlineReportAssistPanel component="Shell" state={state} onChange={onChange} />);
    fireEvent.click(screen.getByRole('button', { name: 'Apply Suggested Flags' }));
    expect(onChange).toHaveBeenCalled();
    expect(onChange.mock.calls[0][0].checklist['Add to Summary']).toBe(true);
  });

  it('Apply Suggested Flags updates checkboxes only and does not overwrite text fields', () => {
    const state = baseState();
    state.findingNotes = 'Observed pitting on lower shell course';
    state.recommendationText = 'Existing recommendation';
    state.photoTag = 'P-1';
    state.checklist['Recommendation Required'] = true;
    const onChange = vi.fn();
    render(<InlineReportAssistPanel component="Shell" state={state} onChange={onChange} />);
    fireEvent.click(screen.getByRole('button', { name: 'Apply Suggested Flags' }));
    const next = onChange.mock.calls[0][0];
    expect(next.checklist['Add to Summary']).toBe(true);
    expect(next.findingNotes).toBe('Observed pitting on lower shell course');
    expect(next.recommendationText).toBe('Existing recommendation');
    expect(next.photoTag).toBe('P-1');
  });
});
