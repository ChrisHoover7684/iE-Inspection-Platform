import type {
  InlineSuggestionsResponse,
  InspectionReport,
  NarrativeResult,
  ReportTemplate,
  UiAlert,
  CorrosionRateInput,
  CorrosionRateResult
} from './types';

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

function buildApiUrl(path: string): string {
  const base = getApiBaseUrl().replace(/\/+$/, '');
  return `${base}${path}`;
}

async function apiFetch<T>(path: string, init?: RequestInit, operation?: string): Promise<T> {
  const response = await fetch(buildApiUrl(path), {
    ...init,
    headers: { 'Content-Type': 'application/json', ...(init?.headers || {}) }
  });
  if (!response.ok) {
    const op = operation ?? `${init?.method ?? 'GET'} ${path}`;
    throw new ApiError(`${op} failed with status ${response.status}`, response.status);
  }
  return (await response.json()) as T;
}

export const reportingApi = {
  getTemplates: () => apiFetch<ReportTemplate[]>('/api/reports/templates', undefined, 'GET /api/reports/templates'),
  getTemplateById: (templateId: string) => apiFetch<ReportTemplate>(`/api/reports/templates/${templateId}`, undefined, `GET /api/reports/templates/${templateId}`),
  getInstances: () => apiFetch<InspectionReport[]>('/api/reports/instances'),
  getInstanceById: (id: string) => apiFetch<InspectionReport>(`/api/reports/instances/${id}`),
  createInstanceFromTemplate: (templateId: string) =>
    apiFetch<InspectionReport>(`/api/reports/templates/${templateId}/instances`, {
      method: 'POST',
      body: JSON.stringify({ clientOrganizationId: 'client-demo-refining', facilityId: 'facility-demo-gulf-coast' })
    }, `POST /api/reports/templates/${templateId}/instances`),
  saveInstance: (id: string, report: InspectionReport) =>
    apiFetch<InspectionReport>(`/api/reports/instances/${id}`, { method: 'PUT', body: JSON.stringify(report) }),
  syncFindings: (id: string) =>
    apiFetch<InspectionReport>(`/api/reports/instances/${id}/sync-findings`, { method: 'POST', body: '{}' }),
  async exportDocx(id: string): Promise<{ blob: Blob; fileName: string }> {
    const response = await fetch(buildApiUrl(`/api/reports/instances/${id}/export/docx`));
    if (!response.ok) throw new ApiError(`DOCX export failed (${response.status})`, response.status);
    return { blob: await response.blob(), fileName: `${id}.docx` };
  },
  inlineSuggestions: (report: InspectionReport, currentFieldId: string, currentText: string, ieAssistEnabled: boolean, manualVerificationRequested: boolean, sectionId?: string) =>
    apiFetch<InlineSuggestionsResponse>('/api/reports/assistant/inline-suggestions', {
      method: 'POST',
      body: JSON.stringify({ report, currentFieldId, currentText, sectionId, ieAssistEnabled, manualVerificationRequested })
    }),
  getAlerts: (report: InspectionReport) => apiFetch<UiAlert[]>('/api/reports/alerts', { method: 'POST', body: JSON.stringify(report) }),
  generateNarrative: (report: InspectionReport) => apiFetch<NarrativeResult>('/api/reports/generate-narrative', { method: 'POST', body: JSON.stringify(report) })
};


export const corrosionRateApi = {
  calculate: (input: CorrosionRateInput) =>
    apiFetch<CorrosionRateResult>('/api/inspection/corrosion-rate/calculate', {
      method: 'POST',
      body: JSON.stringify(input)
    }, 'POST /api/inspection/corrosion-rate/calculate')
};
