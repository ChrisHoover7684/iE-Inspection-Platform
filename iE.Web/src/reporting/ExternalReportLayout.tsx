import type { ReactNode } from 'react';

export function ExternalReportLayout({ header, emptyDraftNotice, children, sidebar }: { header: ReactNode; emptyDraftNotice?: ReactNode; children: ReactNode; sidebar: ReactNode }) {
  return (
    <>
      {header}
      {emptyDraftNotice}
      <div className="api510-report-layout">
        <main className="api510-report-main">{children}</main>
        <aside className="api510-report-sidebar" aria-label="Report sidebar">{sidebar}</aside>
      </div>
    </>
  );
}
