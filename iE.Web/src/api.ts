import type { InspectionReport, ReportTemplate } from './types';

const DEFAULT_API_BASE_URL = 'http://localhost:5229';
const API_BASE_KEY = 'ie_reports_test_api_base_url';

export const getApiBaseUrl = () =>
  localStorage.getItem(API_BASE_KEY) ||
  import.meta.env.VITE_API_BASE_URL ||
  DEFAULT_API_BASE_URL;

export const setApiBaseUrl = (url: string) => {
  localStorage.setItem(API_BASE_KEY, url);
};

export class ApiError extends Error {
  constructor(message: string, public readonly status?: number) {
    super(message);
  }
}

async function apiFetch<T>(path: string, init?: RequestInit): Promise<T> {
  const base = getApiBaseUrl().replace(/\/+$/, '');
  const response = await fetch(`${base}${path}`, {
    ...init,
    headers: {
      'Content-Type': 'application/json',
      ...(init?.headers || {})
    }
  });

  if (!response.ok) {
    let errorMessage = `Request failed (${response.status})`;
    try {
      const body = await response.json();
      if (body?.error) {
        errorMessage = body.error;
      }
    } catch {
      // no-op
    }

    throw new ApiError(errorMessage, response.status);
  }

  return (await response.json()) as T;
}

export const reportingApi = {
  getTemplates: () => apiFetch<ReportTemplate[]>('/api/reports/templates'),
  getInstances: () => apiFetch<InspectionReport[]>('/api/reports/instances'),
  getInstanceById: (id: string) => apiFetch<InspectionReport>(`/api/reports/instances/${id}`),
  createInstanceFromTemplate: (templateId: string) =>
    apiFetch<InspectionReport>(`/api/reports/templates/${templateId}/instances`, {
      method: 'POST',
      body: JSON.stringify({
        clientOrganizationId: 'client-demo-refining',
        facilityId: 'facility-demo-gulf-coast',
        processUnitId: 'unit-demo-crude',
        assetId: 'asset-demo-piping-system'
      })
    }),
  saveInstance: (id: string, report: InspectionReport) =>
    apiFetch<InspectionReport>(`/api/reports/instances/${id}`, {
      method: 'PUT',
      body: JSON.stringify(report)
    }),
  syncFindings: (id: string) =>
    apiFetch<InspectionReport>(`/api/reports/instances/${id}/sync-findings`, {
      method: 'POST',
      body: JSON.stringify({})
    }),
  async exportDocx(id: string): Promise<{ blob: Blob; fileName: string }> {
    const base = getApiBaseUrl().replace(/\/+$/, '');
    const response = await fetch(`${base}/api/reports/instances/${id}/export/docx`);
    if (!response.ok) {
      throw new ApiError(`DOCX export failed (${response.status})`, response.status);
    }

    const disposition = response.headers.get('content-disposition') || '';
    const match = disposition.match(/filename="?([^";]+)"?/i);
    return {
      blob: await response.blob(),
      fileName: match?.[1] || `${id}.docx`
    };
  }
};
