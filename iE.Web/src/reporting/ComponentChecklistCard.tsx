import type { ReactNode } from 'react';

type ComponentState<TField extends string> = {
  condition: string;
  location: string;
  findingNotes: string;
  recommendationText: string;
  photoTag: string;
  checklist: Record<TField, boolean>;
};

export function ComponentChecklistCard<TField extends string>({ component, sectionId, variant, state, checklistFields, locationLabel = 'Location', onChange, notesLabel, recommendationLabel, extraFields }: { component: string; sectionId: string; variant?: 'default' | 'optional'; state: ComponentState<TField>; checklistFields: readonly TField[]; locationLabel?: string; onChange: (next: ComponentState<TField>) => void; notesLabel?: string; recommendationLabel?: string; extraFields?: ReactNode }) {
  const setField = (key: keyof Omit<ComponentState<TField>, 'checklist'>, value: string) => onChange({ ...state, [key]: value });
  const setChecklist = (field: TField, checked: boolean) => onChange({ ...state, checklist: { ...state.checklist, [field]: checked } });
  return <details id={sectionId} className={`card wizard-setup-card api510-component-card ${variant ? `api510-component-card-${variant}` : ''}`.trim()} aria-label={`${component} Component Section`} open><summary><h4>{component}</h4>{variant && <span className="badge">{variant === 'default' ? 'Default' : 'Optional'}</span>}</summary><div className="wizard-field-grid"><label className="wizard-field"><span>Condition</span><select aria-label={`${component} Condition`} value={state.condition} onChange={(e) => setField('condition', e.target.value)}><option value="">Select condition</option><option>Acceptable</option><option>Monitor</option><option>Requires Evaluation</option><option>Requires Repair</option><option>Repair Required</option><option>NDE Required</option></select></label><label className="wizard-field"><span>{locationLabel}</span><input aria-label={`${component} ${locationLabel}`} value={state.location} onChange={(e) => setField('location', e.target.value)} placeholder="Location / orientation" /></label><label className="wizard-field"><span>{notesLabel ?? `${component} Finding Notes`}</span><textarea aria-label={`${component} Finding Notes`} rows={3} value={state.findingNotes} onChange={(e) => setField('findingNotes', e.target.value)} /></label><label className="wizard-field"><span>{recommendationLabel ?? `${component} Recommendation Text`}</span><textarea aria-label={`${component} Recommendation Text`} rows={3} value={state.recommendationText} onChange={(e) => setField('recommendationText', e.target.value)} /></label><label className="wizard-field"><span>Photo Tag</span><input aria-label={`${component} Photo Tag`} value={state.photoTag} onChange={(e) => setField('photoTag', e.target.value)} placeholder="Photo reference" /></label>{extraFields}</div><div className="wizard-component-list" aria-label={`${component} Checklist`}>{checklistFields.map((field) => <label key={field}><input type="checkbox" aria-label={`${component} ${field}`} checked={state.checklist[field]} onChange={(e) => setChecklist(field, e.target.checked)} />{field}</label>)}</div></details>;
}
