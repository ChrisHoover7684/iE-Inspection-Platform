import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { ApiError, getApiBaseUrl, reportingApi, setApiBaseUrl } from './api';
import type { InspectionReport, ReportTemplate } from './types';

function formatDate(value?: string | null) {
  if (!value) return '-';
  return new Date(value).toLocaleString();
}

async function handleDocxExport(id: string, setError: (value: string) => void) {
  try {
    const { blob, fileName } = await reportingApi.exportDocx(id);
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName;
    anchor.click();
    URL.revokeObjectURL(url);
  } catch (error) {
    const message = error instanceof ApiError ? error.message : 'DOCX export failed.';
    setError(message);
  }
}

export function ReportsTestDashboardPage() {
  const navigate = useNavigate();
  const [templates, setTemplates] = useState<ReportTemplate[]>([]);
  const [instances, setInstances] = useState<InspectionReport[]>([]);
  const [selectedTemplateId, setSelectedTemplateId] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');
  const [apiBaseUrl, setApiBaseUrlInput] = useState(getApiBaseUrl());

  const loadData = async () => {
    setIsLoading(true);
    setError('');
    try {
      const [templateData, reportData] = await Promise.all([
        reportingApi.getTemplates(),
        reportingApi.getInstances()
      ]);
      setTemplates(templateData);
      setInstances(reportData);
      if (!selectedTemplateId && templateData.length > 0) {
        setSelectedTemplateId(templateData[0].id);
      }
    } catch (error) {
      setError(error instanceof Error ? error.message : 'API not reachable.');
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const onApplyApiBaseUrl = () => {
    setApiBaseUrl(apiBaseUrl.trim());
    loadData();
  };

  const onCreateReport = async () => {
    if (!selectedTemplateId) return;
    setError('');
    try {
      const created = await reportingApi.createInstanceFromTemplate(selectedTemplateId);
      navigate(`/reports-test/${created.id}`);
    } catch (error) {
      setError(error instanceof Error ? `Create report failed: ${error.message}` : 'Create report failed.');
    }
  };

  return (
    <div className="page">
      <h1>Reports Test Dashboard</h1>
      <div className="card">
        <label>API Base URL</label>
        <div className="row">
          <input value={apiBaseUrl} onChange={(event) => setApiBaseUrlInput(event.target.value)} />
          <button onClick={onApplyApiBaseUrl}>Apply</button>
        </div>
      </div>

      {error && <p className="error">{error}</p>}

      <div className="card">
        <h2>Templates</h2>
        <select
          value={selectedTemplateId}
          onChange={(event) => setSelectedTemplateId(event.target.value)}
          disabled={isLoading}
        >
          {templates.map((template) => (
            <option key={template.id} value={template.id}>
              {template.name} | {template.standard} | {template.equipmentType} | {template.id}
            </option>
          ))}
        </select>
        <button onClick={onCreateReport} disabled={!selectedTemplateId || isLoading}>
          Create Report
        </button>
      </div>

      <div className="card">
        <h2>Report Instances</h2>
        <button onClick={loadData} disabled={isLoading}>Reload</button>
        <table>
          <thead>
            <tr>
              <th>Report ID</th>
              <th>Template ID</th>
              <th>Status</th>
              <th>Created At</th>
              <th>Facility ID</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {instances.map((instance) => (
              <tr key={instance.id}>
                <td>{instance.id}</td>
                <td>{instance.templateId}</td>
                <td>{instance.status}</td>
                <td>{formatDate(instance.createdAt)}</td>
                <td>{instance.facilityId}</td>
                <td>
                  <button onClick={() => navigate(`/reports-test/${instance.id}`)}>Open</button>
                  <button onClick={() => handleDocxExport(instance.id, setError)}>Export DOCX</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
