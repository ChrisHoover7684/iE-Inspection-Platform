import { useMemo } from 'react';
import type { Api510ShellTubeExternalComponentState } from '../types';
import { evaluateInlineAssistRules, type IeAssistTarget } from './ieAssistRules';

type Props = {
  component: string;
  state: Api510ShellTubeExternalComponentState;
  onChange: (next: Api510ShellTubeExternalComponentState) => void;
};

export function InlineReportAssistPanel({ component, state, onChange }: Props) {
  const assist = useMemo(() => evaluateInlineAssistRules(state), [state]);

  const applySuggestedFlags = () => {
    const next = { ...state, checklist: { ...state.checklist } };
    const targets = new Set<IeAssistTarget>(assist.suggestions.map((x) => x.target).filter(Boolean) as IeAssistTarget[]);
    if (targets.has('createFinding')) next.checklist['Create Finding'] = true;
    if (targets.has('recommendationRequired')) next.checklist['Recommendation Required'] = true;
    if (targets.has('photoRequired')) next.checklist['Photo Required'] = true;
    if (targets.has('ndeRequired')) next.checklist['NDE Required'] = true;
    if (targets.has('addToSummary')) next.checklist['Add to Summary'] = true;
    if (targets.has('repairRequired')) next.checklist['Repair Required'] = true;
    onChange(next);
  };

  return (
    <section className="ie-assist-panel" aria-label={`${component} iE Assist`}>
      <div className="ie-assist-header-row">
        <h5>iE Assist (Rule-Based)</h5>
        <button type="button" onClick={applySuggestedFlags}>Apply Suggested Flags</button>
      </div>
      {assist.suggestions.length === 0 ? <p className="muted">No assist suggestions for current values.</p> : (
        <ul className="ie-assist-suggestion-list">
          {assist.suggestions.map((suggestion, index) => (
            <li key={`${suggestion.title}-${index}`} className={`ie-assist-severity-${suggestion.severity}`}>
              <strong>{suggestion.title}</strong>
              <p>{suggestion.message}</p>
              <p><em>Suggested action:</em> {suggestion.suggestedAction}</p>
            </li>
          ))}
        </ul>
      )}

      {assist.suggestedFindingWording && (
        <div className="ie-assist-wording-box">
          <strong>Suggested Finding Wording</strong>
          <p>{assist.suggestedFindingWording}</p>
          {!state.findingNotes.trim() ? (
            <button type="button" onClick={() => onChange({ ...state, findingNotes: assist.suggestedFindingWording ?? '' })}>Insert into Finding Notes</button>
          ) : (
            <button type="button" onClick={() => window.navigator.clipboard?.writeText(assist.suggestedFindingWording ?? '')}>Copy</button>
          )}
        </div>
      )}
    </section>
  );
}
