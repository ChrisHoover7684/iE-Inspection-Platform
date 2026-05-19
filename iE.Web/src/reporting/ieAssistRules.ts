import type { Api510ShellTubeExternalComponentState } from '../types';

export type IeAssistTarget = 'createFinding' | 'recommendationRequired' | 'photoRequired' | 'ndeRequired' | 'addToSummary' | 'repairRequired';
export type IeAssistSeverity = 'info' | 'warning' | 'critical';

export type IeAssistSuggestion = {
  severity: IeAssistSeverity;
  title: string;
  message: string;
  suggestedAction: string;
  target?: IeAssistTarget;
};

export type IeAssistResult = {
  suggestions: IeAssistSuggestion[];
  suggestedFindingWording?: string;
};

const hasText = (value: string) => value.trim().length > 0;

export function evaluateInlineAssistRules(state: Api510ShellTubeExternalComponentState): IeAssistResult {
  const suggestions: IeAssistSuggestion[] = [];
  const notes = state.findingNotes.toLowerCase();

  if (state.checklist['Create Finding'] && !hasText(state.findingNotes)) {
    suggestions.push({ severity: 'warning', title: 'Finding notes missing', message: 'Create Finding is selected, but Finding Notes is blank.', suggestedAction: 'Add finding description.', target: 'createFinding' });
  }

  if (state.checklist['Recommendation Required']) {
    if (!hasText(state.recommendationText)) {
      suggestions.push({ severity: 'warning', title: 'Recommendation text missing', message: 'Recommendation Required is selected, but Recommendation Text is blank.', suggestedAction: 'Add recommendation text.', target: 'recommendationRequired' });
    }
    suggestions.push({ severity: 'info', title: 'Recommendation summary reminder', message: 'Recommendation Required is selected for this component.', suggestedAction: 'Add this item to Summary.', target: 'addToSummary' });
  }

  if (state.checklist['Photo Required'] && !hasText(state.photoTag)) {
    suggestions.push({ severity: 'warning', title: 'Photo tag missing', message: 'Photo Required is selected, but Photo Tag is blank.', suggestedAction: 'Add photo tag.', target: 'photoRequired' });
  }

  if (state.checklist['NDE Required'] && !hasText(state.location)) {
    suggestions.push({ severity: 'critical', title: 'NDE details missing', message: 'NDE Required is selected, but no clear method/location detail is available.', suggestedAction: 'Add NDE method/location.', target: 'ndeRequired' });
  }

  if (/(pitting)/.test(notes)) {
    suggestions.push({ severity: 'warning', title: 'Pitting detail needed', message: 'Pitting is noted in Finding Notes.', suggestedAction: 'Document depth, extent, and localized/general classification.' });
  }
  if (/(corrosion)/.test(notes)) {
    suggestions.push({ severity: 'warning', title: 'Corrosion detail needed', message: 'Corrosion is noted in Finding Notes.', suggestedAction: 'Document metal loss extent, location, and thickness verification.' });
  }
  if (/(leak|leakage|weeping)/.test(notes)) {
    suggestions.push({ severity: 'critical', title: 'Leakage follow-up needed', message: 'Leakage/weeping language appears in Finding Notes.', suggestedAction: 'Confirm active leakage status, component/joint location, and repair recommendation.' });
  }
  if (/(crack|cracking)/.test(notes)) {
    suggestions.push({ severity: 'critical', title: 'Crack follow-up needed', message: 'Crack/cracking language appears in Finding Notes.', suggestedAction: 'Add NDE method and engineering review recommendation.', target: 'ndeRequired' });
  }
  if (/(bulge|distortion|deformation)/.test(notes)) {
    suggestions.push({ severity: 'critical', title: 'Distortion follow-up needed', message: 'Bulge/distortion/deformation language appears in Finding Notes.', suggestedAction: 'Add dimensional evaluation and engineering review recommendation.' });
  }

  if (state.checklist['Repair Required']) {
    suggestions.push({ severity: 'info', title: 'Return-to-service reminder', message: 'Repair Required is selected for this component.', suggestedAction: 'Include return-to-service review notes.', target: 'repairRequired' });
  }

  const suggestedFindingWording = /(pitting)/.test(notes)
    ? 'Localized pitting was noted at [location]. Additional thickness verification should be performed to confirm remaining wall and determine if repair or monitoring is required.'
    : undefined;

  return { suggestions, suggestedFindingWording };
}
