import { useEffect, useMemo, useState } from 'react';
import { ndeApi } from '../api';
import type { NdeLogItem, NdeLogStatus, NdeLogTransitionEvent } from '../types';
import { accessMethodOptions, ndeMethodOptions, owningGroupOptions, projectOptions } from '../ndeRequestReferenceData';

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
  { id: 'nde-001', requestNumber: 'NDE-26-001', assetTag: 'P-102A', method: 'UT Thickness', status: 'Draft', priority: 'Normal', requestedBy: 'J. Rivera', assignedTo: 'L. Tran', dueDate: '2026-05-20', reportStatus: 'Not Started', accessType: 'Ground' },
  { id: 'nde-002', requestNumber: 'NDE-26-002', circuitId: 'CIR-4A-220', method: 'RT', status: 'Requested', priority: 'High', requestedBy: 'M. Patel', assignedTo: 'S. Owens', dueDate: '2026-05-17', reportStatus: 'In Progress', reportNumber: 'RPT-26-RT-002' },
  { id: 'nde-003', requestNumber: 'NDE-26-003', equipmentTag: 'E-4401', method: 'MT', status: 'Scheduled', priority: 'Normal', requestedBy: 'T. Nguyen', assignedTo: 'R. Hall', dueDate: '2026-05-13', reportStatus: 'Not Available' },
  {
    id: 'nde-004',
    requestNumber: 'NDE-26-004',
    assetTag: 'HX-22B',
    method: 'PT',
    status: 'In Progress',
    priority: 'Critical',
    requestedBy: 'A. Lopez',
    assignedTo: 'D. Kim',
    dueDate: '2026-05-12',
    reportStatus: 'In Progress',
    reportNumber: 'RPT-26-PT-004',
    accessType: 'Rope Access',
    inspectionDetails: 'Nozzle N2 root and cap PT verification before hydrotest.',
    scopeItems: [
      { id: 'scope-004-pt-prep', method: 'PT', stage: 'Prep', displayName: 'PT Prep', weldId: 'W-22B-01', location: 'Nozzle N2 Root', notes: 'Surface prep and cleaning' },
      { id: 'scope-004-pt-root', method: 'PT', stage: 'Root', displayName: 'PT Root', weldId: 'W-22B-01', location: 'Nozzle N2 Root', notes: 'Root pass examination' },
      { id: 'scope-004-pt-final', method: 'PT', stage: 'Final', displayName: 'PT Final', weldId: 'W-22B-01', location: 'Nozzle N2 Cap', notes: 'Final cap examination' },
    ],
  },
  { id: 'nde-005', requestNumber: 'NDE-26-005', circuitId: 'CIR-3C-118', method: 'PMI', status: 'Results Received', priority: 'High', requestedBy: 'G. Martin', assignedTo: 'V. Chen', dueDate: '2026-05-10', resultReceivedDate: '2026-05-09', reportStatus: 'In Progress', reportNumber: 'RPT-26-PMI-005' },
  { id: 'nde-006', requestNumber: 'NDE-26-006', equipmentTag: 'TK-804', method: 'PAUT', status: 'Reviewed', priority: 'Normal', requestedBy: 'P. Singh', assignedTo: 'N. Brooks', dueDate: '2026-05-09', resultReceivedDate: '2026-05-08', reportStatus: 'Complete', reportNumber: 'RPT-26-PAUT-006', reportFileName: 'RPT-26-PAUT-006.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-PAUT-006.pdf', accessType: 'Ladder' },
  { id: 'nde-007', requestNumber: 'NDE-26-007', assetTag: 'L-5507', method: 'VT', status: 'Closed', priority: 'Low', requestedBy: 'R. Scott', assignedTo: 'H. Diaz', dueDate: '2026-05-06', resultReceivedDate: '2026-05-05', reportStatus: 'Complete', reportNumber: 'RPT-26-VT-007', reportFileName: 'RPT-26-VT-007.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-VT-007.pdf' },
  { id: 'nde-008', requestNumber: 'NDE-26-008', equipmentTag: 'PSV-91', method: 'UT Thickness', status: 'Cancelled', priority: 'Low', requestedBy: 'C. White', assignedTo: 'B. Young', dueDate: '2026-05-04', reportStatus: 'Not Started' },
  { id: 'nde-009', requestNumber: 'NDE-26-009', circuitId: 'CIR-9D-032', method: 'RT', status: 'Overdue', priority: 'Critical', requestedBy: 'D. Reed', assignedTo: 'M. Gray', dueDate: '2026-05-01', reportStatus: 'In Progress', reportNumber: 'RPT-26-RT-009' },
  { id: 'nde-010', requestNumber: 'NDE-26-010', assetTag: 'P-300C', method: 'PAUT', status: 'Reviewed', priority: 'High', requestedBy: 'L. Ward', assignedTo: 'K. Adams', dueDate: '2026-05-14', reportStatus: 'Complete', reportNumber: 'RPT-26-PAUT-010', reportFileName: 'RPT-26-PAUT-010.pdf', reportDownloadUrl: '/demo-downloads/RPT-26-PAUT-010.pdf' },
];

type NdeTransition = { label: string; status: NdeLogStatus };
function getAllowedNdeTransitions(status: NdeLogStatus): NdeTransition[] {
  switch (status) {
    case 'Draft':
      return [{ label: 'Mark Requested', status: 'Requested' }, { label: 'Cancel', status: 'Cancelled' }];
    case 'Requested':
      return [{ label: 'Mark Scheduled', status: 'Scheduled' }, { label: 'Cancel', status: 'Cancelled' }];
    case 'Scheduled':
      return [{ label: 'Mark In Progress', status: 'In Progress' }, { label: 'Cancel', status: 'Cancelled' }];
    case 'In Progress':
      return [{ label: 'Mark Results Received', status: 'Results Received' }, { label: 'Cancel', status: 'Cancelled' }];
    case 'Results Received':
      return [{ label: 'Mark Reviewed', status: 'Reviewed' }, { label: 'Cancel', status: 'Cancelled' }];
    case 'Reviewed':
      return [{ label: 'Mark Closed', status: 'Closed' }];
    case 'Overdue':
      return [{ label: 'Mark Scheduled', status: 'Scheduled' }, { label: 'Cancel', status: 'Cancelled' }];
    case 'Closed':
    case 'Cancelled':
      return [];
    default:
      return [];
  }
}

export function NdeWorkspacePage({
  initialStatus = 'All',
  initialStatuses,
  title = 'NDE Requests',
  description = 'Track NDE requests and reports without mixing API inspection report workflow data.',
}: NdeWorkspacePageProps) {
  const [items, setItems] = useState<NdeLogItem[]>(mockRows);
  const [isLoading, setIsLoading] = useState(true);
  const [loadError, setLoadError] = useState<string | null>(null);
  const [statusFilter, setStatusFilter] = useState<NdeLogStatus | 'All'>(initialStatus);
  const [searchText, setSearchText] = useState('');
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [selectedForBulkDownload, setSelectedForBulkDownload] = useState<string[]>([]);
  const [bulkMessage, setBulkMessage] = useState<string>('');
  const [isTransitioning, setIsTransitioning] = useState(false);
  const [transitionError, setTransitionError] = useState<string | null>(null);
  const [transitionComment, setTransitionComment] = useState('');
  const [eventHistory, setEventHistory] = useState<NdeLogTransitionEvent[]>([]);
  const [isLoadingEvents, setIsLoadingEvents] = useState(false);
  const [eventHistoryError, setEventHistoryError] = useState<string | null>(null);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [demoCreateMessage, setDemoCreateMessage] = useState<string>('');
  const currentUserDisplayName = 'Operator (placeholder)';
  const [createForm, setCreateForm] = useState({
    project: '', owningGroup: '', requester: currentUserDisplayName, priority: 'Normal' as NdeLogItem['priority'], dueDate: '',
    taskType: '', code: '', unit: '', asset: '', inspectionDetails: '', requestStatus: 'Draft' as NdeLogStatus,
    accessType: '', reference: '',
  });

  useEffect(() => {
    setStatusFilter(initialStatus);
  }, [initialStatus]);

  useEffect(() => {
    let active = true;
    setIsLoading(true);
    ndeApi.getLogItems()
      .then((rows) => {
        if (!active) return;
        setItems(rows);
        setLoadError(null);
      })
      .catch(() => {
        if (!active) return;
        setLoadError('Unable to load NDE log rows from API. Showing demo rows.');
      })
      .finally(() => {
        if (active) setIsLoading(false);
      });

    return () => {
      active = false;
    };
  }, []);


  useEffect(() => {
    if (!selectedId) {
      setEventHistory([]);
      setEventHistoryError(null);
      return;
    }

    let active = true;
    setIsLoadingEvents(true);
    setEventHistoryError(null);

    ndeApi.getLogItemEvents(selectedId)
      .then((events) => {
        if (!active) return;
        setEventHistory(events);
      })
      .catch(() => {
        if (!active) return;
        setEventHistory([]);
        setEventHistoryError('Unable to load workflow history from API.');
      })
      .finally(() => {
        if (active) setIsLoadingEvents(false);
      });

    return () => {
      active = false;
    };
  }, [selectedId]);

  const baseItems = useMemo(() => {
    if (!initialStatuses || initialStatuses.length === 0) {
      return items;
    }

    const statuses = new Set(initialStatuses);
    return items.filter((row) => statuses.has(row.status));
  }, [initialStatuses, items]);

  const applyStatusToSelection = async (nextStatus: NdeLogStatus) => {
    if (!selectedId) {
      return;
    }

    const todayIso = new Date().toISOString().slice(0, 10);
    const applyLocal = () => setItems((current) => current.map((item) => {
      if (item.id !== selectedId) {
        return item;
      }

      return {
        ...item,
        status: nextStatus,
        resultReceivedDate: nextStatus === 'Results Received' ? (item.resultReceivedDate ?? todayIso) : item.resultReceivedDate,
      };
    }));

    setIsTransitioning(true);
    setTransitionError(null);
    try {
      await ndeApi.transitionLogItem(selectedId, nextStatus, transitionComment, 'demo.user');
      const rows = await ndeApi.getLogItems();
      setItems(rows);
      const events = await ndeApi.getLogItemEvents(selectedId);
      setEventHistory(events);
      setTransitionComment('');
    } catch {
      setTransitionError('Unable to persist transition to API. Applied demo fallback update.');
      applyLocal();
    } finally {
      setIsTransitioning(false);
    }
  };

  const formatTimestamp = (timestampUtc: string) => new Date(timestampUtc).toLocaleString();
  const formatScopeSummary = (item: NdeLogItem) =>
    item.scopeItems?.length
      ? item.scopeItems.map((scopeItem) => scopeItem.displayName).join(', ')
      : '';

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
          row.accessType,
          row.reference,
          row.scopeItems?.map((scopeItem) => `${scopeItem.displayName} ${scopeItem.stage} ${scopeItem.weldId ?? ''}`).join(' '),
          row.inspectionDetails,
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

  const createRequestFromForm = () => {
    const nextSequence = items.length + 1;
    const createdItem: NdeLogItem = {
      id: `nde-${String(nextSequence).padStart(3, '0')}`,
      requestNumber: `NDE-26-${String(nextSequence).padStart(3, '0')}`,
      assetTag: createForm.asset,
      method: createForm.taskType,
      status: createForm.requestStatus,
      priority: createForm.priority,
      requestedBy: createForm.requester,
      dueDate: createForm.dueDate,
      reportStatus: 'Not Started',
      accessType: createForm.accessType || undefined,
      reference: createForm.reference || undefined,
      inspectionDetails: createForm.inspectionDetails || undefined,
    };

    setItems((current) => [createdItem, ...current]);
    setSelectedId(createdItem.id);
    setIsCreateModalOpen(false);
    setDemoCreateMessage('Request creation is demo-only (in-memory) until backend persistence is connected.');
  };

  const visibleIds = useMemo(() => filteredItems.map((item) => item.id), [filteredItems]);
  const visibleSelectedCount = useMemo(() => selectedForBulkDownload.filter((id) => visibleIds.includes(id)).length, [selectedForBulkDownload, visibleIds]);
  const isReportWorkflowCompleteEnough = (item: NdeLogItem) =>
    item.status === 'Reviewed' || item.status === 'Closed';

  const isDownloadableReport = (item: NdeLogItem) =>
    isReportWorkflowCompleteEnough(item)
    && item.reportStatus === 'Complete'
    && Boolean(item.reportDownloadUrl)
    && Boolean(item.reportFileName);

  const downloadableSelectedItems = useMemo(() => filteredItems.filter((item) => selectedForBulkDownload.includes(item.id) && isDownloadableReport(item)), [filteredItems, selectedForBulkDownload]);

  const triggerDownload = (url: string, filename: string) => {
    const link = document.createElement('a');
    link.href = url;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  };

  const exportVisibleTable = () => {
    const headers = ['Request #', 'Asset / Circuit / Equipment', 'Method', 'Access Method', 'Reference', 'Inspection Details', 'Status', 'Priority', 'Due', 'Results Received', 'Report Status', 'Report #'];
    const rows = filteredItems.map((item) => [
      item.requestNumber,
      item.assetTag ?? item.circuitId ?? item.equipmentTag ?? '—',
      item.method,
      item.accessType ?? '—',
      item.reference ?? '—',
      item.inspectionDetails ?? '—',
      item.status,
      item.priority,
      item.dueDate ?? '—',
      item.resultReceivedDate ?? '—',
      item.reportStatus,
      item.reportNumber ?? '—',
    ]);
    const csv = [headers, ...rows].map((line) => line.map((cell) => `"${String(cell).replace(/"/g, '""')}"`).join(',')).join('\n');
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    triggerDownload(url, 'nde-log-export.csv');
    URL.revokeObjectURL(url);
  };


  const selectedItem = useMemo(
    () => filteredItems.find((item) => item.id === selectedId) ?? null,
    [filteredItems, selectedId],
  );

  const allowedTransitions = useMemo(
    () => (selectedItem ? getAllowedNdeTransitions(selectedItem.status) : []),
    [selectedItem],
  );

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
        <button type="button" onClick={() => setIsCreateModalOpen(true)}>+ New NDE Request</button>
        {isLoading && <p className="muted">Loading NDE log rows…</p>}
        {loadError && <p className="muted">{loadError}</p>}
        {demoCreateMessage && <p className="muted">{demoCreateMessage}</p>}
      </div>

      {isCreateModalOpen && (
        <div className="nde-modal-backdrop" role="dialog" aria-modal="true" aria-label="New NDE Request">
          <div className="card nde-modal-card">
            <h3>New NDE Request</h3>
            <div className="nde-modal-grid">
              <label>Project<select value={createForm.project} onChange={(event) => setCreateForm((current) => ({ ...current, project: event.target.value }))}><option value="">Select a Project...</option>{projectOptions.map((option) => <option key={option} value={option}>{option}</option>)}</select></label>
              <label>Owning Group<select value={createForm.owningGroup} onChange={(event) => setCreateForm((current) => ({ ...current, owningGroup: event.target.value }))}><option value="">Select an Owning Group...</option>{owningGroupOptions.map((option) => <option key={option} value={option}>{option}</option>)}</select></label>
              <label>Requester<input value={createForm.requester} readOnly /></label>
              <label>Priority<select value={createForm.priority} onChange={(event) => setCreateForm((current) => ({ ...current, priority: event.target.value as NdeLogItem['priority'] }))}><option>Low</option><option>Normal</option><option>High</option><option>Critical</option></select></label>
              <label>Due Date<input type="date" value={createForm.dueDate} onChange={(event) => setCreateForm((current) => ({ ...current, dueDate: event.target.value }))} /></label>
              <label>NDE Method<select value={createForm.taskType} onChange={(event) => setCreateForm((current) => ({ ...current, taskType: event.target.value }))}><option value="">Select an NDE Method...</option>{ndeMethodOptions.map((option) => <option key={option} value={option}>{option}</option>)}</select></label>
              <label>Code<input value={createForm.code} onChange={(event) => setCreateForm((current) => ({ ...current, code: event.target.value }))} /></label>
              <label>Unit<input value={createForm.unit} onChange={(event) => setCreateForm((current) => ({ ...current, unit: event.target.value }))} /></label>
              <label>Asset<input value={createForm.asset} onChange={(event) => setCreateForm((current) => ({ ...current, asset: event.target.value }))} /></label>
              <label>Request Status<select value={createForm.requestStatus} onChange={(event) => setCreateForm((current) => ({ ...current, requestStatus: event.target.value as NdeLogStatus }))}><option>Draft</option><option>Requested</option></select></label>
              <label>Access Method<select value={createForm.accessType} onChange={(event) => setCreateForm((current) => ({ ...current, accessType: event.target.value }))}><option value="">Select an Access Method...</option>{accessMethodOptions.map((option) => <option key={option} value={option}>{option}</option>)}</select></label>
              
              
              
              
              <label>Reference<input value={createForm.reference} onChange={(event) => setCreateForm((current) => ({ ...current, reference: event.target.value }))} /></label>
              <label>Attachments placeholder<input disabled value="File upload coming soon" /></label>
            </div>
            <label>Inspection Details<textarea value={createForm.inspectionDetails} onChange={(event) => setCreateForm((current) => ({ ...current, inspectionDetails: event.target.value }))} /></label>
            <p className="muted nde-reference-note">Project options are demo reference data. Admin-managed project setup will be connected later.</p>
            <div className="row">
              <button type="button" onClick={createRequestFromForm}>Create Request</button>
              <button type="button" onClick={() => setIsCreateModalOpen(false)}>Cancel</button>
            </div>
          </div>
        </div>
      )}

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
            placeholder="Search request #, asset, method, access method, reference, inspection details, or assignee"
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
        <div className="nde-download-actions" role="group" aria-label="NDE report download actions">
          <button type="button" onClick={exportVisibleTable}>Export Table</button>
          <button
            type="button"
            onClick={() => {
              const selectedVisible = filteredItems.filter((item) => selectedForBulkDownload.includes(item.id));
              const readyRows = selectedVisible.filter((item) => isDownloadableReport(item));
              readyRows.forEach((item) => triggerDownload(item.reportDownloadUrl as string, item.reportFileName as string));
              if (selectedVisible.length > readyRows.length) {
                setBulkMessage('Only report-ready rows will be included.');
              } else {
                setBulkMessage('');
              }
            }}
            disabled={downloadableSelectedItems.length === 0}
          >
            Bulk Download Reports
          </button>
          {bulkMessage && <span className="muted">{bulkMessage}</span>}
        </div>
        <p className="muted nde-frontend-note">Workflow actions persist via NDE API when available, with demo fallback behavior on failures.</p>
        <div className="nde-workspace-layout">
          <div className="nde-table-panel">
        <table>
          <thead>
            <tr>
              <th>Select</th><th>Request #</th><th>Asset / Circuit / Equipment</th><th>Method</th><th>Access Method</th><th>Status</th><th>Priority</th><th>Due</th>
              <th>Report Status</th><th>Report #</th><th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredItems.map((item) => (
              <tr
                key={item.id}
                className={selectedId === item.id ? 'nde-row-selected' : undefined}
                onClick={() => setSelectedId(item.id)}
                aria-selected={selectedId === item.id}
              >
                <td><input aria-label={`Select ${item.requestNumber}`} type="checkbox" checked={selectedForBulkDownload.includes(item.id)} onChange={(event) => {
                  setSelectedForBulkDownload((current) => event.target.checked ? [...current, item.id] : current.filter((id) => id !== item.id));
                }} onClick={(event) => event.stopPropagation()} /></td>
                <td>{item.requestNumber}</td>
                <td>{item.assetTag ?? item.circuitId ?? item.equipmentTag ?? '—'}</td>
                <td>
                  <div>{item.method}</div>
                  {formatScopeSummary(item) && <div className="muted nde-scope-summary">{formatScopeSummary(item)}</div>}
                </td>
                <td>{item.accessType ?? '—'}</td>
                <td>{item.status}</td>
                <td>{item.priority}</td>
                <td>{item.dueDate ?? '—'}</td>
                <td>{formatReportStatus(item.reportStatus)}</td>
                <td>{item.reportNumber ?? '—'}</td>
                <td>
                  <button
                    type="button"
                    title={isDownloadableReport(item) ? 'Download ready report' : 'Report template/download not connected yet.'}
                    disabled={!isDownloadableReport(item)}
                    onClick={(event) => {
                      event.stopPropagation();
                      if (item.reportDownloadUrl && item.reportFileName) {
                        triggerDownload(item.reportDownloadUrl, item.reportFileName);
                      }
                    }}
                  >Download Report</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <div className="nde-table-select-all">
          <label>
            <input
              type="checkbox"
              aria-label="Select all visible"
              checked={visibleIds.length > 0 && visibleSelectedCount === visibleIds.length}
              onChange={(event) => {
                if (event.target.checked) {
                  setSelectedForBulkDownload((current) => [...new Set([...current, ...visibleIds])]);
                } else {
                  setSelectedForBulkDownload((current) => current.filter((id) => !visibleIds.includes(id)));
                }
              }}
            />
            Select all visible
          </label>
        </div>
        {filteredItems.length === 0 && (
          <div className="nde-empty-state">
            <h3>No {title} NDE items yet</h3>
            <p className="muted">NDE workflow scaffolding is in place. Connect read-model/API data to populate this queue.</p>
          </div>
        )}
          </div>
          {selectedItem && (
            <aside className="nde-detail-panel" aria-label="NDE request details">
              <div className="nde-detail-header">
                <h3>Request Details</h3>
                <button type="button" onClick={() => setSelectedId(null)}>Close details</button>
              </div>
              <p className="muted nde-selection-summary">Selected: {selectedItem.requestNumber} ({selectedItem.status})</p>
              <button type="button" onClick={() => setDemoCreateMessage('Request edit/open is demo-only until request persistence is connected.')}>Open request for edit</button>
              <dl className="nde-detail-grid">
                <dt>Request #</dt><dd>{selectedItem.requestNumber}</dd>
                <dt>Report #</dt><dd>{selectedItem.reportNumber ?? '—'}</dd>
                <dt>Asset / Circuit / Equipment</dt><dd>{selectedItem.assetTag ?? selectedItem.circuitId ?? selectedItem.equipmentTag ?? '—'}</dd>
                <dt>Method</dt><dd>{selectedItem.method}</dd>
                <dt>Access Method</dt><dd>{selectedItem.accessType ?? '—'}</dd>
                <dt>Reference</dt><dd>{selectedItem.reference ?? '—'}</dd>
                <dt>Status</dt><dd>{selectedItem.status}</dd>
                <dt>Inspection Details</dt><dd>{selectedItem.inspectionDetails ?? '—'}</dd>
                <dt>Report Status</dt><dd>{formatReportStatus(selectedItem.reportStatus)}</dd>
                <dt>Priority</dt><dd>{selectedItem.priority}</dd>
                <dt>Requested By</dt><dd>{selectedItem.requestedBy ?? '—'}</dd>
                <dt>Assigned To</dt><dd>{selectedItem.assignedTo ?? '—'}</dd>
                <dt>Due Date</dt><dd>{selectedItem.dueDate ?? '—'}</dd>
                <dt>Results Received Date</dt><dd>{selectedItem.resultReceivedDate ?? '—'}</dd>
              </dl>
              {!!selectedItem.scopeItems?.length && (
                <div className="nde-scope-items-panel">
                  <h3>Request Scope Items</h3>
                  <ul>
                    {selectedItem.scopeItems.map((scopeItem) => (
                      <li key={scopeItem.id}>
                        <strong>{scopeItem.displayName}</strong>
                        <span className="muted"> ({scopeItem.method} / {scopeItem.stage})</span>
                        {scopeItem.weldId && <span> • Weld {scopeItem.weldId}</span>}
                        {scopeItem.location && <span> • {scopeItem.location}</span>}
                      </li>
                    ))}
                  </ul>
                </div>
              )}
              {isDownloadableReport(selectedItem) && selectedItem.reportDownloadUrl && selectedItem.reportFileName && (
                <button type="button" onClick={() => triggerDownload(selectedItem.reportDownloadUrl as string, selectedItem.reportFileName as string)}>Download Report</button>
              )}
              <div className="nde-history-panel">
                <h3>Workflow Actions</h3>
                <div className="nde-actions" role="group" aria-label="NDE workflow actions">
                  {allowedTransitions.map((transition) => (
                    <button key={transition.label} type="button" disabled={isTransitioning} onClick={() => void applyStatusToSelection(transition.status)}>{transition.label}</button>
                  ))}
                </div>
                <label className="nde-transition-comment">
                  Comment / Reason
                  <input value={transitionComment} onChange={(event) => setTransitionComment(event.target.value)} placeholder="Enter transition reason" />
                </label>
                {isTransitioning && <p className="muted">Applying transition…</p>}
                {transitionError && <p className="muted">{transitionError}</p>}
                {allowedTransitions.length === 0 && <p className="muted">No workflow actions available for this status.</p>}
              </div>
              <div className="nde-history-panel">
                <h3>Workflow History</h3>
                {isLoadingEvents && <p className="muted">Loading workflow history…</p>}
                {eventHistoryError && <p className="muted">{eventHistoryError}</p>}
                {!isLoadingEvents && eventHistory.length === 0 && <p className="muted">No workflow history yet.</p>}
                {eventHistory.map((event) => (
                  <article key={event.id} className="nde-history-event">
                    <p><strong>{event.fromStatus} → {event.toStatus}</strong></p>
                    <p className="muted">{event.actor} • {formatTimestamp(event.timestampUtc)}</p>
                    {event.comment && <p>{event.comment}</p>}
                  </article>
                ))}
              </div>
            </aside>
          )}
        </div>
      </div>
    </section>
  );
}
  const formatReportStatus = (reportStatus: NdeLogItem['reportStatus']) =>
    reportStatus === 'Complete' ? 'Reviewed / Complete' : reportStatus;
