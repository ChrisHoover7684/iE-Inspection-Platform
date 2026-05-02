import { useEffect, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ApiError, reportingApi } from './api';
import type { InspectionReport } from './types';

type SortColumn =
  | 'reportNumber'
  | 'templateId'
  | 'facilityId'
  | 'equipmentTag'
  | 'circuitId'
  | 'service'
  | 'status'
  | 'findings'
  | 'recommendations'
  | 'updatedAt';

type SortDirection = 'asc' | 'desc';

const getErrorMessage = (error: unknown, fallback: string) =>
  error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback;

const formatDateTime = (value?: string | null) => {
  if (!value) return '—';
  return new Date(value).toLocaleString();
};

const normalizeStatus = (status?: string | null) => (status || '').replace(/[^a-z]/gi, '').toLowerCase();

const getReportType = (report: InspectionReport) => {
  if (report.templateId?.toLowerCase().includes('570')) return 'API 570';
  if (report.templateId?.toLowerCase().includes('510')) return 'API 510';
  if (report.templateId?.toLowerCase().includes('sti')) return 'STI-SP001';
  return report.templateId || 'General';
};

const engineeringTools = ['B31.3 Pressure Calcs', 'Pressure Vessel Calcs', 'Damage Mechanisms'];
const reportMenuActions = ['Create', 'View', 'Edit'];

export function DashboardPage() {
  const navigate = useNavigate();
  const [reports, setReports] = useState<InspectionReport[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('all');
  const [typeFilter, setTypeFilter] = useState('all');
  const [sortColumn, setSortColumn] = useState<SortColumn>('updatedAt');
  const [sortDirection, setSortDirection] = useState<SortDirection>('desc');
  const [columnFilters, setColumnFilters] = useState({
    reportNumber: '',
    reportType: '',
    client: '',
    facility: '',
    unit: '',
    systemId: '',
    circuitId: '',
    service: '',
    status: '',
    findings: '',
    recommendations: '',
    updatedDate: ''
  });

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
    const totalReports = reports.length;
    const draftReports = reports.filter((r) => normalizeStatus(r.status) === 'draft').length;
    const inReview = reports.filter((r) => ['inreview', 'submittedforreview'].includes(normalizeStatus(r.status))).length;
    const completed = reports.filter((r) => ['approved', 'complete'].includes(normalizeStatus(r.status))).length;
    const openFindings = reports.reduce((sum, r) => sum + (r.findings?.length || 0), 0);
    const recommendations = reports.reduce((sum, r) => sum + (r.findings?.filter((finding) => Boolean(finding.repairRecommendation)).length || 0), 0);

    return { totalReports, draftReports, inReview, completed, openFindings, recommendations };
  }, [reports]);

  const statusOptions = useMemo(
    () => ['all', ...Array.from(new Set(reports.map((r) => r.status || 'Unknown')))],
    [reports]
  );

  const typeOptions = useMemo(() => ['all', ...Array.from(new Set(reports.map(getReportType)))], [reports]);

  const filteredReports = useMemo(() => {
    const loweredSearch = searchTerm.toLowerCase().trim();
    const matchesColumnFilter = (value: string | number | undefined | null, filter: string) => {
      if (!filter.trim()) return true;
      return String(value || '').toLowerCase().includes(filter.toLowerCase().trim());
    };

    return reports
      .filter((report) => (statusFilter === 'all' ? true : (report.status || 'Unknown') === statusFilter))
      .filter((report) => (typeFilter === 'all' ? true : getReportType(report) === typeFilter))
      .filter((report) => {
        if (!loweredSearch) return true;
        const rowText = [
          report.reportNumber,
          getReportType(report),
          report.clientOrganizationId,
          report.facilityId,
          report.unit || report.processUnitId,
          report.systemId,
          report.circuitId,
          report.service,
          report.status,
          report.equipmentTag
        ]
          .filter(Boolean)
          .join(' ')
          .toLowerCase();
        return rowText.includes(loweredSearch);
      })
      .filter((report) => (
        matchesColumnFilter(report.reportNumber || report.id, columnFilters.reportNumber)
        && matchesColumnFilter(getReportType(report), columnFilters.reportType)
        && matchesColumnFilter(report.clientOrganizationId || '—', columnFilters.client)
        && matchesColumnFilter(report.facilityId || '—', columnFilters.facility)
        && matchesColumnFilter(report.unit || report.processUnitId || '—', columnFilters.unit)
        && matchesColumnFilter(report.systemId || '—', columnFilters.systemId)
        && matchesColumnFilter(report.circuitId || '—', columnFilters.circuitId)
        && matchesColumnFilter(report.service || '—', columnFilters.service)
        && matchesColumnFilter(report.status || 'Unknown', columnFilters.status)
        && matchesColumnFilter(report.findings?.length || 0, columnFilters.findings)
        && matchesColumnFilter(report.findings?.filter((finding) => Boolean(finding.repairRecommendation)).length || 0, columnFilters.recommendations)
        && matchesColumnFilter(formatDateTime(report.updatedAt || report.createdAt), columnFilters.updatedDate)
      ))
      .sort((a, b) => {
        const valueFor = (report: InspectionReport) => {
          switch (sortColumn) {
            case 'reportNumber':
              return report.reportNumber || report.id;
            case 'templateId':
              return getReportType(report);
            case 'facilityId':
              return report.facilityId || '';
            case 'equipmentTag':
              return report.equipmentTag || '';
            case 'circuitId':
              return report.circuitId || '';
            case 'service':
              return report.service || '';
            case 'status':
              return report.status || '';
            case 'findings':
              return report.findings?.length || 0;
            case 'recommendations':
              return report.findings?.filter((finding) => Boolean(finding.repairRecommendation)).length || 0;
            case 'updatedAt':
            default:
              return report.updatedAt || report.createdAt || '';
          }
        };

        const left = valueFor(a);
        const right = valueFor(b);
        const comparison = typeof left === 'number' && typeof right === 'number'
          ? left - right
          : String(left).localeCompare(String(right));
        return sortDirection === 'asc' ? comparison : -comparison;
      });
  }, [reports, searchTerm, statusFilter, typeFilter, columnFilters, sortColumn, sortDirection]);

  const onSort = (column: SortColumn) => {
    if (sortColumn === column) {
      setSortDirection((prev) => (prev === 'asc' ? 'desc' : 'asc'));
      return;
    }
    setSortColumn(column);
    setSortDirection('asc');
  };

  const openReport = (report: InspectionReport) => {
    navigate('/reports/api-570-piping-external', { state: { reportId: report.id } });
  };

  return (
    <div className="dashboard-shell">
      <aside className="dashboard-sidebar">
        <h2>iE Inspection Platform</h2>
        <nav>
          {['Dashboard', 'Reports'].map((item) => (
            <button
              key={item}
              type="button"
              className={`sidebar-link ${item === 'Dashboard' ? 'active' : ''}`}
            >
              {item}
            </button>
          ))}
          <p className="sidebar-section-title">Engineering Tools</p>
          {engineeringTools.map((item) => <button key={item} type="button" className="sidebar-link">{item}</button>)}
          <p className="sidebar-section-title">Report Actions</p>
          {reportMenuActions.map((item) => <button key={item} type="button" className="sidebar-link">{item}</button>)}
          <button type="button" className="sidebar-link">Settings</button>
        </nav>
      </aside>

      <section className="dashboard-content">
        <header className="dashboard-topbar card">
          <h1>Dashboard</h1>
          <input
            type="search"
            placeholder="Search reports, client, facility..."
            value={searchTerm}
            onChange={(event) => setSearchTerm(event.target.value)}
          />
          <button type="button" className="new-report-btn" onClick={() => navigate('/reports/api-570-piping-external')}>
            New Report
          </button>
          <div className="profile-pill">Inspector User</div>
        </header>

        <main className="dashboard-main">
          <section className="dashboard-card-grid">
            <article className="card"><h3>Total Reports</h3><p>{statusCounts.totalReports}</p></article>
            <article className="card"><h3>Draft Reports</h3><p>{statusCounts.draftReports}</p></article>
            <article className="card"><h3>In Review</h3><p>{statusCounts.inReview}</p></article>
            <article className="card"><h3>Completed</h3><p>{statusCounts.completed}</p></article>
            <article className="card"><h3>Open Findings</h3><p>{statusCounts.openFindings}</p></article>
            <article className="card"><h3>Recommendations</h3><p>{statusCounts.recommendations}</p></article>
          </section>

          <section className="card reports-panel">
            <div className="reports-filters">
              <input
                type="search"
                placeholder="Filter table..."
                value={searchTerm}
                onChange={(event) => setSearchTerm(event.target.value)}
              />
              <select value={statusFilter} onChange={(event) => setStatusFilter(event.target.value)}>
                {statusOptions.map((status) => <option key={status} value={status}>{status === 'all' ? 'All Statuses' : status}</option>)}
              </select>
              <select value={typeFilter} onChange={(event) => setTypeFilter(event.target.value)}>
                {typeOptions.map((type) => <option key={type} value={type}>{type === 'all' ? 'All Report Types' : type}</option>)}
              </select>
            </div>

            {error && <p className="error">{error}</p>}
            {isLoading ? <p>Loading reports...</p> : (
              <div className="reports-table-wrap">
                <table>
                  <thead>
                    <tr className="column-filter-row">
                      <th />
                      <th><input type="search" placeholder="Filter..." value={columnFilters.reportNumber} onChange={(event) => setColumnFilters((prev) => ({ ...prev, reportNumber: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.reportType} onChange={(event) => setColumnFilters((prev) => ({ ...prev, reportType: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.client} onChange={(event) => setColumnFilters((prev) => ({ ...prev, client: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.facility} onChange={(event) => setColumnFilters((prev) => ({ ...prev, facility: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.unit} onChange={(event) => setColumnFilters((prev) => ({ ...prev, unit: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.systemId} onChange={(event) => setColumnFilters((prev) => ({ ...prev, systemId: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.circuitId} onChange={(event) => setColumnFilters((prev) => ({ ...prev, circuitId: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.service} onChange={(event) => setColumnFilters((prev) => ({ ...prev, service: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.status} onChange={(event) => setColumnFilters((prev) => ({ ...prev, status: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.findings} onChange={(event) => setColumnFilters((prev) => ({ ...prev, findings: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.recommendations} onChange={(event) => setColumnFilters((prev) => ({ ...prev, recommendations: event.target.value }))} /></th>
                      <th><input type="search" placeholder="Filter..." value={columnFilters.updatedDate} onChange={(event) => setColumnFilters((prev) => ({ ...prev, updatedDate: event.target.value }))} /></th>
                      <th />
                    </tr>
                    <tr>
                      <th className="select-column">Select</th>
                      <th><button type="button" onClick={() => onSort('reportNumber')}>Report Number</button></th>
                      <th><button type="button" onClick={() => onSort('templateId')}>Report Type</button></th>
                      <th>Client</th>
                      <th><button type="button" onClick={() => onSort('facilityId')}>Facility</button></th>
                      <th>Unit</th>
                      <th>System ID</th>
                      <th><button type="button" onClick={() => onSort('circuitId')}>Circuit ID</button></th>
                      <th><button type="button" onClick={() => onSort('service')}>Service</button></th>
                      <th><button type="button" onClick={() => onSort('status')}>Status</button></th>
                      <th><button type="button" onClick={() => onSort('findings')}>Findings</button></th>
                      <th><button type="button" onClick={() => onSort('recommendations')}>Recommendations</button></th>
                      <th><button type="button" onClick={() => onSort('updatedAt')}>Updated Date</button></th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {filteredReports.map((report) => (
                      <tr key={report.id} onClick={() => openReport(report)} role="button" tabIndex={0}>
                        <td className="select-column">
                          <input type="checkbox" aria-label={`Select report ${report.reportNumber || report.id}`} onClick={(event) => event.stopPropagation()} />
                        </td>
                        <td>{report.reportNumber || report.id}</td>
                        <td>{getReportType(report)}</td>
                        <td>{report.clientOrganizationId || '—'}</td>
                        <td>{report.facilityId || '—'}</td>
                        <td>{report.unit || report.processUnitId || '—'}</td>
                        <td>{report.systemId || '—'}</td>
                        <td>{report.circuitId || '—'}</td>
                        <td>{report.service || '—'}</td>
                        <td>{report.status || 'Unknown'}</td>
                        <td>{report.findings?.length || 0}</td>
                        <td>{report.findings?.filter((finding) => Boolean(finding.repairRecommendation)).length || 0}</td>
                        <td>{formatDateTime(report.updatedAt || report.createdAt)}</td>
                        <td>
                          <div className="row-action-toolbar">
                            <button type="button" onClick={(event) => { event.stopPropagation(); openReport(report); }}>Preview</button>
                            <button type="button" onClick={(event) => event.stopPropagation()}>Edit</button>
                            <button type="button" onClick={(event) => event.stopPropagation()}>Download</button>
                            <button type="button" onClick={(event) => event.stopPropagation()}>Export</button>
                            <button type="button" onClick={(event) => event.stopPropagation()}>Change Status</button>
                          </div>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </section>
        </main>
      </section>
    </div>
  );
}
