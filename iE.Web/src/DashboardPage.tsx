import { useEffect, useMemo, useRef, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { ApiError, reportingApi } from './api';
import type { InspectionReport } from './types';

const ACTIVE_REPORT_ID_STORAGE_KEY = 'ie_api570_active_report_id';
const IE_ASSIST_STORAGE_KEY = 'ie_dashboard_ie_assist_enabled';

const getErrorMessage = (error: unknown, fallback: string) =>
  error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback;

const formatDateTime = (value?: string | null) => {
  if (!value) return '—';
  return new Date(value).toLocaleString();
};

const normalizeStatus = (status: string) => status.replace(/[^a-z]/gi, '').toLowerCase();

export function DashboardPage() {
  const navigate = useNavigate();
  const toolsSectionRef = useRef<HTMLElement | null>(null);
  const [reports, setReports] = useState<InspectionReport[]>([]);
  const [ieAssistEnabled, setIeAssistEnabled] = useState(() => localStorage.getItem(IE_ASSIST_STORAGE_KEY) !== 'false');
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');

  useEffect(() => {
    localStorage.setItem(IE_ASSIST_STORAGE_KEY, String(ieAssistEnabled));
  }, [ieAssistEnabled]);

  useEffect(() => {
    (async () => {
      setIsLoading(true);
      setError('');
      try {
        const instances = await reportingApi.getInstances();
        setReports(instances);
      } catch (loadError) {
        setError(getErrorMessage(loadError, 'Unable to load reports.'));
      } finally {
        setIsLoading(false);
      }
    })();
  }, []);

  const statusCounts = useMemo(() => {
    const grouped = reports.reduce(
      (counts, report) => {
        const status = normalizeStatus(report.status || '');
        if (status === 'draft') counts.drafts += 1;
        if (status === 'returned' || status === 'returnedforrevision') counts.returned += 1;
        if (status === 'inreview' || status === 'submittedforreview') counts.inReview += 1;
        if (status === 'approved' || status === 'complete') counts.approved += 1;
        return counts;
      },
      { drafts: 0, returned: 0, inReview: 0, approved: 0 }
    );

    return grouped;
  }, [reports]);

  const goToApi570Entry = (reportId?: string) => {
    navigate('/reports/api-570-piping-external', {
      state: { reportId, ieAssistEnabled }
    });
  };

  const continueLastReport = () => {
    const activeId = localStorage.getItem(ACTIVE_REPORT_ID_STORAGE_KEY);
    if (activeId) {
      goToApi570Entry(activeId);
      return;
    }

    const latest = [...reports].sort((a, b) => (b.updatedAt || b.createdAt).localeCompare(a.updatedAt || a.createdAt))[0];
    if (latest) {
      localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, latest.id);
      goToApi570Entry(latest.id);
      return;
    }

    goToApi570Entry();
  };

  const verifyDrafts = async () => {
    const drafts = reports.filter((report) => normalizeStatus(report.status || '') === 'draft');
    if (drafts.length === 0) {
      setMessage('No draft reports available to verify.');
      return;
    }

    try {
      await reportingApi.getAlerts(drafts[0]);
      setMessage('Bulk draft verification is coming soon. Open a draft report to verify alerts now.');
    } catch {
      setMessage('Verify Drafts is coming soon.');
    }
  };

  const openTools = () => {
    toolsSectionRef.current?.scrollIntoView({ behavior: 'smooth', block: 'start' });
    toolsSectionRef.current?.focus({ preventScroll: true });
  };

  return (
    <div className="page dashboard-page">
      <div className="dashboard-layout">
        <main>
          <section className="card">
            <p className="muted">Site / Facility: Gulf Coast Refinery (placeholder)</p>
            <h1>Good afternoon, Chris</h1>
            <p>What are you working on today?</p>
          </section>

          <section className="quick-actions-grid">
            <Link className="card action-card" to="/reports/api-570-piping-external" state={{ ieAssistEnabled }}>Start New Report</Link>
            <button className="card action-card" type="button" onClick={continueLastReport}>Continue Last Report</button>
            <button className="card action-card" type="button" onClick={() => void verifyDrafts()}>Verify Drafts</button>
            <button className="card action-card" type="button" onClick={openTools}>Open Tools</button>
          </section>

          {message && <p className="muted">{message}</p>}

          <section className="status-grid">
            <div className="card"><h3>Drafts</h3><p className="status-count">{statusCounts.drafts}</p></div>
            <div className="card"><h3>Returned</h3><p className="status-count">{statusCounts.returned}</p></div>
            <div className="card"><h3>In Review</h3><p className="status-count">{statusCounts.inReview}</p></div>
            <div className="card"><h3>Approved</h3><p className="status-count">{statusCounts.approved}</p></div>
          </section>

          <section className="card">
            <h2>My Reports</h2>
            {error && <p className="error">{error}</p>}
            {isLoading ? <p>Loading reports...</p> : (
              <table>
                <thead>
                  <tr><th>Report #</th><th>Equipment Tag / Line</th><th>Template</th><th>Status</th><th>Last Updated</th><th>Actions</th></tr>
                </thead>
                <tbody>
                  {reports.map((report) => (
                    <tr key={report.id}>
                      <td>{report.reportNumber || report.id}</td>
                      <td>{report.equipmentTag || report.circuitId || '—'}</td>
                      <td>{report.templateId}</td>
                      <td>{report.status}</td>
                      <td>{formatDateTime(report.updatedAt || report.createdAt)}</td>
                      <td>
                        <div className="row">
                          <button type="button" onClick={() => goToApi570Entry(report.id)}>Open</button>
                          <Link to={`/reports-test/${report.id}`}>Preview</Link>
                          <button type="button" onClick={() => void reportingApi.getAlerts(report).then(() => setMessage(`Verified ${report.reportNumber || report.id}.`)).catch(() => setMessage('Verification unavailable.'))}>Verify</button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}
          </section>

          <section className="dashboard-two-col">
            <div className="card">
              <h2>Start New Report</h2>
              <div className="feature-grid">
                <Link to="/reports/api-570-piping-external" state={{ ieAssistEnabled }} className="card feature-card">API 570 Piping External</Link>
                <span className="card feature-card feature-card-disabled">API 510 Vessel External <small>Coming Soon</small></span>
                <span className="card feature-card feature-card-disabled">STI SP001 Tank External <small>Coming Soon</small></span>
                <span className="card feature-card feature-card-disabled">Repair Recommendation <small>Coming Soon</small></span>
              </div>
            </div>
            <section className="card" ref={toolsSectionRef} tabIndex={-1} aria-label="Tools section">
              <h2>Tools</h2>
              <div className="feature-grid">
                {['T-Min Calculator', 'Material Stress Lookup', 'API 571 Damage Mechanisms', 'HTHA Checker', 'PCC-1 / Flange Tools', 'STI SP001 Helper'].map((tool) => (
                  <button key={tool} className="card feature-card" type="button">{tool}</button>
                ))}
              </div>
            </section>
          </section>
        </main>

        <aside className="card assist-panel">
          <h2>iE Assist</h2>
          <label className="toggle-row">
            <input type="checkbox" checked={ieAssistEnabled} onChange={(e) => setIeAssistEnabled(e.target.checked)} />
            <span>{ieAssistEnabled ? 'ON' : 'OFF'}</span>
          </label>
          <p><strong>Trust but Verify</strong></p>
          <button type="button" onClick={() => void verifyDrafts()}>Verify Drafts</button>
        </aside>
      </div>
    </div>
  );
}
