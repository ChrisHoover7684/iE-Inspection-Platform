import { Fragment, type ReactNode } from 'react';

export function ExternalReportSidebar({ onSave, lastSavedAt, summaryTitle, summaryPairs, selectedItems, payloadAriaLabel, payload, warnings }: {
  onSave: () => void;
  lastSavedAt: string;
  summaryTitle: string;
  summaryPairs: ReadonlyArray<{ label: string; value: ReactNode }>;
  selectedItems: readonly string[];
  payloadAriaLabel: string;
  payload: unknown;
  warnings?: ReactNode;
}) {
  return (
    <>
      <section className="card" aria-label="Report Actions"><h3>Report Actions</h3><button type="button" onClick={onSave}>Save Draft Locally</button><button type="button" disabled>Export Word Report</button><p className="wizard-note">Word export will be connected after report persistence is finalized.</p><p role="status">Last Saved: {lastSavedAt ? new Date(lastSavedAt).toLocaleString() : 'Not saved locally'}</p></section>
      <section className="card" aria-label="iE Assist"><h3>iE Assist</h3><p className="wizard-note">Assistant review hooks will use the local report draft until backend report persistence is finalized.</p></section>
      <section className="card" aria-label="Report Summary Preview"><h3>Report Summary Preview</h3><dl className="api510-summary-list">{summaryPairs.map((pair) => <Fragment key={pair.label}><dt>{pair.label}</dt><dd>{pair.value}</dd></Fragment>)}</dl><ul aria-label="Selected component preview">{selectedItems.map((item) => <li key={item}>{item}</li>)}</ul></section>
      <section className="card" aria-label="Draft Payload Debug"><details><summary>View Draft Payload</summary><pre aria-label={payloadAriaLabel}>{JSON.stringify(payload, null, 2)}</pre></details></section>
      <section className="card" aria-label="Summary warnings"><h3>Summary Warnings</h3>{warnings}</section>
    </>
  );
}
