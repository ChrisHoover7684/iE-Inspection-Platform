import { useEffect, useMemo, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { ApiError, reportingApi } from './api';
import type { InspectionReport } from './types';

const ACTIVE_REPORT_ID_STORAGE_KEY = 'ie_api570_active_report_id';

const getErrorMessage = (error: unknown, fallback: string) =>
  error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback;

const formatDateTime = (value?: string | null) => {
  if (!value) return '—';
  return new Date(value).toLocaleString();
};

export function DashboardPage() {
  const navigate = useNavigate();
  const [reports, setReports] = useState<InspectionReport[]>([]);
  const [ieAssistEnabled, setIeAssistEnabled] = useState(true);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

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
    const toCount = (status: string) => reports.filter((report) => report.status.toLowerCase() === status).length;
    return {
      drafts: toCount('draft'),
      returned: toCount('returned'),
      inReview: toCount('in review'),
      approved: toCount('approved')
    };
  }, [reports]);

  const continueLastReport = () => {
    const activeId = localStorage.getItem(ACTIVE_REPORT_ID_STORAGE_KEY);
    if (activeId) {
      navigate('/reports/api-570-piping-external', { state: { reportId: activeId } });
      return;
    }

    const latest = [...reports].sort((a, b) => (b.updatedAt || b.createdAt).localeCompare(a.updatedAt || a.createdAt))[0];
    if (latest) {
      localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, latest.id);
      navigate('/reports/api-570-piping-external', { state: { reportId: latest.id } });
      return;
    }

    navigate('/reports/api-570-piping-external');
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
            <Link className="card action-card" to="/reports/api-570-piping-external">Start New Report</Link>
            <button className="card action-card" type="button" onClick={continueLastReport}>Continue Last Report</button>
            <button className="card action-card" type="button">Verify Drafts</button>
            <button className="card action-card" type="button">Open Tools</button>
          </section>

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
                          <button type="button" onClick={() => navigate('/reports/api-570-piping-external', { state: { reportId: report.id } })}>Open</button>
                          <Link to={`/reports-test/${report.id}`}>Preview</Link>
                          <button type="button">Verify</button>
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
                <Link to="/reports/api-570-piping-external" className="card feature-card">API 570 Piping External</Link>
                <Link to="/reports/api-570-piping-external" className="card feature-card">API 510 Vessel External</Link>
                <Link to="/reports/api-570-piping-external" className="card feature-card">STI SP001 Tank External</Link>
                <Link to="/reports/api-570-piping-external" className="card feature-card">Repair Recommendation</Link>
              </div>
            </div>
            <div className="card">
              <h2>Tools</h2>
              <div className="feature-grid">
                {['T-Min Calculator', 'Material Stress Lookup', 'API 571 Damage Mechanisms', 'HTHA Checker', 'PCC-1 / Flange Tools', 'STI SP001 Helper'].map((tool) => (
                  <button key={tool} className="card feature-card" type="button">{tool}</button>
                ))}
              </div>
            </div>
          </section>
        </main>

        <aside className="card assist-panel">
          <h2>iE Assist</h2>
          <label className="toggle-row">
            <input type="checkbox" checked={ieAssistEnabled} onChange={(e) => setIeAssistEnabled(e.target.checked)} />
            <span>{ieAssistEnabled ? 'ON' : 'OFF'}</span>
          </label>
          <p><strong>Trust but Verify</strong></p>
          <button type="button">Verify Drafts</button>
        </aside>
      </div>
    </div>
  );
}
