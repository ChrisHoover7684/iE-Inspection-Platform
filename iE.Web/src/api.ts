import type {
  InlineSuggestionsResponse,
  InspectionReport,
  NarrativeResult,
  ReportTemplate,
  UiAlert,
  CorrosionRateInput,
  CorrosionRateResult,
  PipeLookupInput,
  PipeLookupResult,
  LwnLookupInput,
  LwnLookupResult,
  CylindricalShellInput,
  CylindricalShellResult,
  CylindricalShellCalculationRequest,
  SphericalShellCalculationRequest,
  ConicalShellCalculationRequest,
  HeadCalculationRequest,
  NozzleCalculationRequest,
  CalculationEnvelope,
  SphericalShellInput,
  SphericalShellResult,
  ConicalShellInput,
  ConicalShellResult,
  HeadThicknessInput,
  HeadThicknessResult,
  NozzleThicknessInput,
  NozzleThicknessResult,
  HeadType,
  NozzleType,
  Ug45TableEntry
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


export const pipeLookupApi = {
  lookup: (input: PipeLookupInput) =>
    apiFetch<PipeLookupResult>('/api/mechanical/pipe-data/lookup', {
      method: 'POST',
      body: JSON.stringify({ nps: input.nps, schedule: input.schedule })
    }, 'POST /api/mechanical/pipe-data/lookup'),
  getNps: () => apiFetch<string[]>('/api/mechanical/pipe-data/nps'),
  getSchedules: (nps: string) => apiFetch<string[]>(`/api/mechanical/pipe-data/schedules/${encodeURIComponent(nps)}`)
};

export const lwnLookupApi = {
  lookup: (input: LwnLookupInput) =>
    apiFetch<LwnLookupResult>('/api/mechanical/lwn/calculate', {
      method: 'POST',
      body: JSON.stringify({ size: input.size, schedule: input.schedule })
    }, 'POST /api/mechanical/lwn/calculate'),
  getSizes: () => apiFetch<string[]>('/api/mechanical/lwn/sizes'),
  getSchedules: (size: string) => apiFetch<string[]>(`/api/mechanical/lwn/schedules/${encodeURIComponent(size)}`)
};


export const pressureVesselApi = {
  calculateCylindrical: (input: CylindricalShellCalculationRequest) => apiFetch<CalculationEnvelope<CylindricalShellResult>>('/api/mechanical/pressure-vessels/shells/cylindrical/calculate', { method: 'POST', body: JSON.stringify(input) }),
  calculateSpherical: (input: SphericalShellCalculationRequest) => apiFetch<CalculationEnvelope<SphericalShellResult>>('/api/mechanical/pressure-vessels/shells/spherical/calculate', { method: 'POST', body: JSON.stringify(input) }),
  calculateConical: (input: ConicalShellCalculationRequest) => apiFetch<CalculationEnvelope<ConicalShellResult>>('/api/mechanical/pressure-vessels/shells/conical/calculate', { method: 'POST', body: JSON.stringify(input) }),
  calculateHead: (input: HeadCalculationRequest) => apiFetch<CalculationEnvelope<HeadThicknessResult>>('/api/mechanical/pressure-vessels/heads/calculate', { method: 'POST', body: JSON.stringify(input) }),
  calculateNozzle: (input: NozzleCalculationRequest) => apiFetch<CalculationEnvelope<NozzleThicknessResult>>('/api/mechanical/pressure-vessels/nozzles/ug45/calculate', { method: 'POST', body: JSON.stringify(input) }),
  getHeadTypes: () => apiFetch<HeadType[]>('/api/mechanical/pressure-vessels/heads/types'),
  getNozzleTypes: () => apiFetch<NozzleType[]>('/api/mechanical/pressure-vessels/nozzles/types'),
  getUg45Table: () => apiFetch<Ug45TableEntry[]>('/api/mechanical/pressure-vessels/ug45-table')
};
