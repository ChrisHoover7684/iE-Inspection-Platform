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

const mockRows: NdeLogItem[] = [];

export function NdeWorkspacePage({
  initialStatus = 'All',
  title = 'NDE Requests',
  description = 'Track NDE requests and reports without mixing API inspection report workflow data.',
}: NdeWorkspacePageProps) {
  const [statusFilter, setStatusFilter] = useState<NdeLogStatus | 'All'>(initialStatus);
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    setStatusFilter(initialStatus);
  }, [initialStatus]);

  const filteredItems = useMemo(() => {
    return mockRows.filter((row) => {
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
  }, [searchText, statusFilter]);

  const summary = {
    total: mockRows.length,
    open: mockRows.filter((item) => !['Closed', 'Cancelled'].includes(item.status)).length,
    resultsReceived: mockRows.filter((item) => item.status === 'Results Received').length,
    overdue: mockRows.filter((item) => item.status === 'Overdue').length,
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
