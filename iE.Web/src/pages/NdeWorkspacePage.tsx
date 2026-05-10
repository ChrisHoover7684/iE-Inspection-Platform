import { useEffect, useMemo, useState } from 'react';

export type NdeLogStatus =
  | 'Draft'
  | 'Requested'
  | 'Scheduled'
  | 'In Progress'
  | 'Results Received'
  | 'Reviewed'
  | 'Closed'
  | 'Cancelled'
  | 'Overdue';

export type NdeLogItem = {
  id: string;
  requestNumber: string;
  assetTag?: string;
  circuitId?: string;
  equipmentTag?: string;
  method: string;
  status: NdeLogStatus;
  priority: 'Low' | 'Normal' | 'High' | 'Critical';
  requestedBy?: string;
  assignedTo?: string;
  dueDate?: string;
  resultReceivedDate?: string;
};

type NdeWorkspacePageProps = {
  initialStatus?: NdeLogStatus | 'All';
  initialStatuses?: NdeLogStatus[];
  title?: string;
  description?: string;
};

const statusOptions: Array<NdeLogStatus | 'All'> = [
  'All',
  'Draft',
  'Requested',
  'Scheduled',
  'In Progress',
  'Results Received',
  'Reviewed',
  'Closed',
  'Cancelled',
  'Overdue',
];

// Frontend-only demo/read-model data for NDE workspace usability.
// Replace with backend read-model data when the NDE workflow API is connected.
const mockRows: NdeLogItem[] = [
  { id: 'nde-001', requestNumber: 'NDE-24-001', assetTag: 'P-102A', method: 'UT Thickness', status: 'Draft', priority: 'Normal', requestedBy: 'J. Rivera', assignedTo: 'L. Tran', dueDate: '2026-05-20' },
  { id: 'nde-002', requestNumber: 'NDE-24-002', circuitId: 'CIR-4A-220', method: 'RT', status: 'Requested', priority: 'High', requestedBy: 'M. Patel', assignedTo: 'S. Owens', dueDate: '2026-05-17' },
  { id: 'nde-003', requestNumber: 'NDE-24-003', equipmentTag: 'E-4401', method: 'MT', status: 'Scheduled', priority: 'Normal', requestedBy: 'T. Nguyen', assignedTo: 'R. Hall', dueDate: '2026-05-13' },
  { id: 'nde-004', requestNumber: 'NDE-24-004', assetTag: 'HX-22B', method: 'PT', status: 'In Progress', priority: 'Critical', requestedBy: 'A. Lopez', assignedTo: 'D. Kim', dueDate: '2026-05-12' },
  { id: 'nde-005', requestNumber: 'NDE-24-005', circuitId: 'CIR-3C-118', method: 'PMI', status: 'Results Received', priority: 'High', requestedBy: 'G. Martin', assignedTo: 'V. Chen', dueDate: '2026-05-10', resultReceivedDate: '2026-05-09' },
  { id: 'nde-006', requestNumber: 'NDE-24-006', equipmentTag: 'TK-804', method: 'PAUT', status: 'Reviewed', priority: 'Normal', requestedBy: 'P. Singh', assignedTo: 'N. Brooks', dueDate: '2026-05-09', resultReceivedDate: '2026-05-08' },
  { id: 'nde-007', requestNumber: 'NDE-24-007', assetTag: 'L-5507', method: 'VT', status: 'Closed', priority: 'Low', requestedBy: 'R. Scott', assignedTo: 'H. Diaz', dueDate: '2026-05-06', resultReceivedDate: '2026-05-05' },
  { id: 'nde-008', requestNumber: 'NDE-24-008', equipmentTag: 'PSV-91', method: 'UT Thickness', status: 'Cancelled', priority: 'Low', requestedBy: 'C. White', assignedTo: 'B. Young', dueDate: '2026-05-04' },
  { id: 'nde-009', requestNumber: 'NDE-24-009', circuitId: 'CIR-9D-032', method: 'RT', status: 'Overdue', priority: 'Critical', requestedBy: 'D. Reed', assignedTo: 'M. Gray', dueDate: '2026-05-01' },
  { id: 'nde-010', requestNumber: 'NDE-24-010', assetTag: 'P-300C', method: 'PAUT', status: 'Scheduled', priority: 'High', requestedBy: 'L. Ward', assignedTo: 'K. Adams', dueDate: '2026-05-14' },
];

export function NdeWorkspacePage({
  initialStatus = 'All',
  initialStatuses,
  title = 'NDE Requests',
  description = 'Track NDE requests and reports without mixing API inspection report workflow data.',
}: NdeWorkspacePageProps) {
  const [statusFilter, setStatusFilter] = useState<NdeLogStatus | 'All'>(initialStatus);
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    setStatusFilter(initialStatus);
  }, [initialStatus]);

  const baseItems = useMemo(() => {
    if (!initialStatuses || initialStatuses.length === 0) {
      return mockRows;
    }

    const statuses = new Set(initialStatuses);
    return mockRows.filter((row) => statuses.has(row.status));
  }, [initialStatuses]);

  const filteredItems = useMemo(() => {
    return baseItems.filter((row) => {
      const byStatus = statusFilter === 'All' || row.status === statusFilter;
      const bySearch =
        searchText.trim().length === 0
        || [
          row.requestNumber,
          row.assetTag,
          row.circuitId,
          row.equipmentTag,
          row.method,
          row.requestedBy,
          row.assignedTo,
        ]
          .filter(Boolean)
          .join(' ')
          .toLowerCase()
          .includes(searchText.toLowerCase());

      return byStatus && bySearch;
    });
  }, [baseItems, searchText, statusFilter]);

  const summary = {
    total: baseItems.length,
    open: baseItems.filter((item) => !['Closed', 'Cancelled'].includes(item.status)).length,
    resultsReceived: baseItems.filter((item) => item.status === 'Results Received').length,
    overdue: baseItems.filter((item) => item.status === 'Overdue').length,
  };

  return (
    <section className="nde-workspace">
      <div className="card">
        <h2>{title}</h2>
        <p className="muted">{description}</p>
      </div>

      <div className="nde-summary-grid" aria-label="NDE queue summary">
        <article className="card"><h3>Total</h3><p>{summary.total}</p></article>
        <article className="card"><h3>Open</h3><p>{summary.open}</p></article>
        <article className="card"><h3>Results Received</h3><p>{summary.resultsReceived}</p></article>
        <article className="card"><h3>Overdue</h3><p>{summary.overdue}</p></article>
      </div>

      <div className="card nde-filters">
        <label>
          Search
          <input
            value={searchText}
            onChange={(event) => setSearchText(event.target.value)}
            placeholder="Search request #, asset, method, or assignee"
          />
        </label>
        <label>
          Status
          <select value={statusFilter} onChange={(event) => setStatusFilter(event.target.value as NdeLogStatus | 'All')}>
            {statusOptions.map((status) => <option key={status} value={status}>{status}</option>)}
          </select>
        </label>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Request #</th><th>Asset / Circuit / Equipment</th><th>Method</th><th>Status</th><th>Priority</th><th>Due</th><th>Results Received</th>
            </tr>
          </thead>
          <tbody>
            {filteredItems.map((item) => (
              <tr key={item.id}>
                <td>{item.requestNumber}</td>
                <td>{item.assetTag ?? item.circuitId ?? item.equipmentTag ?? '—'}</td>
                <td>{item.method}</td>
                <td>{item.status}</td>
                <td>{item.priority}</td>
                <td>{item.dueDate ?? '—'}</td>
                <td>{item.resultReceivedDate ?? '—'}</td>
              </tr>
            ))}
          </tbody>
        </table>
        {filteredItems.length === 0 && (
          <div className="nde-empty-state">
            <h3>No {title} NDE items yet</h3>
            <p className="muted">NDE workflow scaffolding is in place. Connect read-model/API data to populate this queue.</p>
          </div>
        )}
      </div>
    </section>
  );
}
