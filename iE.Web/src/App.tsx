import { Navigate, Route, Routes } from 'react-router-dom';
import { Api570PipingExternalEntryPage } from './Api570PipingExternalEntryPage';
import { ReportEditPage } from './ReportEditPage';
import { ReportsTestDashboardPage } from './ReportsTestDashboardPage';
import { DashboardPage } from './DashboardPage';
import { ApiInspectionReportsPage } from './ApiInspectionReportsPage';
import { CorrosionRateCalculatorPage } from './CorrosionRateCalculatorPage';
import { PipeLookupCalculatorPage } from './PipeLookupCalculatorPage';
import { PressureVesselCalculatorPage } from './PressureVesselCalculatorPage';
import { AppShell } from './components/AppShell';
import { B31_3PipingCalculatorPage } from './B31_3PipingCalculatorPage';
import { PlaceholderPage } from './pages/PlaceholderPage';
import { NdeWorkspacePage } from './pages/NdeWorkspacePage';
import { ReferenceDataProjectsPage } from './pages/ReferenceDataProjectsPage';
import { ReferenceDataUnitsAssetsPage } from './pages/ReferenceDataUnitsAssetsPage';
import { UsersAccessFoundationPage } from './pages/UsersAccessFoundationPage';
import { FacilitiesPage } from './pages/FacilitiesPage';
import { Api510ShellTubeCalculationWorkspace } from './reporting/Api510ShellTubeCalculationWorkspace';

function ShellRoute({ children }: { children: React.ReactNode }) {
  return <AppShell>{children}</AppShell>;
}

const placeholder = (title: string, description: string) => <ShellRoute><PlaceholderPage title={title} description={description} /></ShellRoute>;

export default function App() {
  return (
    <Routes>
      <Route path="/reports-test" element={<ShellRoute><ReportsTestDashboardPage /></ShellRoute>} />
      <Route path="/reports-test/:id" element={<ShellRoute><ReportEditPage /></ShellRoute>} />
      <Route path="/reports/api-570-piping-external" element={<ShellRoute><Api570PipingExternalEntryPage /></ShellRoute>} />
      <Route path="/dashboard" element={<ShellRoute><DashboardPage /></ShellRoute>} />
      <Route path="/calculators/corrosion-rate" element={<ShellRoute><CorrosionRateCalculatorPage /></ShellRoute>} />
      <Route path="/calculators/pipe-lookup" element={<ShellRoute><PipeLookupCalculatorPage /></ShellRoute>} />
      <Route path="/calculators/pressure-vessels" element={<ShellRoute><PressureVesselCalculatorPage /></ShellRoute>} />
      <Route path="/calculators/b31-3-piping" element={<ShellRoute><B31_3PipingCalculatorPage /></ShellRoute>} />

      <Route path="/nde-requests" element={<ShellRoute><NdeWorkspacePage title="NDE Requests" initialStatuses={['Draft', 'Requested', 'Scheduled', 'In Progress', 'Results Received', 'Reviewed', 'Overdue']} /></ShellRoute>} />
      <Route path="/reports" element={<ShellRoute><ApiInspectionReportsPage /></ShellRoute>} />
      <Route path="/reports/api-510-shell-tube-workspace" element={<ShellRoute><Api510ShellTubeCalculationWorkspace /></ShellRoute>} />
      <Route path="/downloads" element={placeholder('Downloads', 'Coming soon: downloadable report and file library.')} />
      <Route path="/assets" element={placeholder('Assets', 'Coming soon: asset register and activity screens.')} />
      <Route path="/help" element={placeholder('Help', 'Coming soon: support resources and guidance.')} />
      <Route path="/approval-queue" element={placeholder('Approval Queue', 'Coming soon: owner approval queue and decision workflow.')} />
      <Route
        path="/nde-reports"
        element={<ShellRoute><NdeWorkspacePage title="NDE Reports" description="Fill out, review, generate, print, and download NDE reports." showCreateRequestButton={false} summaryMode="reports" /></ShellRoute>}
      />
      <Route path="/exports" element={placeholder('Exports', 'Coming soon: report export jobs and history.')} />
      <Route path="/api-inspection-log" element={placeholder('API Inspection Log', 'Coming soon: API inspection report log and history.')} />
      <Route path="/my-reports" element={placeholder('My Reports', 'Coming soon: personalized report queue and actions.')} />
      <Route path="/returned-reports" element={placeholder('Returned Reports', 'Coming soon: returned report corrections and resubmissions.')} />
      <Route path="/schedule" element={<ShellRoute><NdeWorkspacePage title="Schedule" initialStatus="Scheduled" description="View NDE items planned for execution and scheduling coordination." /></ShellRoute>} />
      <Route path="/results-received" element={<ShellRoute><NdeWorkspacePage title="Results Received" initialStatus="Results Received" description="Triage received NDE results before review and closure." /></ShellRoute>} />
      <Route path="/overdue" element={<ShellRoute><NdeWorkspacePage title="Overdue" initialStatus="Overdue" description="Focus on overdue NDE requests and reports requiring immediate attention." /></ShellRoute>} />
      <Route path="/cancelled" element={<ShellRoute><NdeWorkspacePage title="Cancelled" initialStatus="Cancelled" description="View cancelled NDE requests and reports for audit visibility." /></ShellRoute>} />
      <Route path="/users-access" element={<ShellRoute><UsersAccessFoundationPage /></ShellRoute>} />
      <Route path="/facilities" element={<ShellRoute><FacilitiesPage /></ShellRoute>} />
      <Route path="/custom-nde-types" element={placeholder('Custom NDE Types', 'Coming soon: tenant-specific NDE type configuration.')} />
      <Route path="/reference-data-projects" element={<ShellRoute><ReferenceDataProjectsPage /></ShellRoute>} />
      <Route path="/reference-data-units-assets" element={<ShellRoute><ReferenceDataUnitsAssetsPage /></ShellRoute>} />
      <Route path="/audit-log" element={placeholder('Audit Log', 'Coming soon: system audit events and history.')} />
      <Route path="/settings" element={placeholder('Settings', 'Coming soon: tenant and application settings.')} />
      <Route path="/system-readiness" element={placeholder('System Readiness', 'Coming soon: internal system readiness dashboard.')} />
      <Route path="/stress-test-setup" element={placeholder('Stress Test Setup', 'Coming soon: internal stress and load-test setup.')} />
      <Route path="/tenants" element={placeholder('Tenants', 'Coming soon: multi-tenant setup and governance.')} />
      <Route path="/engineering-tools/criteria-references" element={placeholder('Criteria / References', 'Coming soon: criteria and standards references for report workflows.')} />
      <Route path="/engineering-tools/damage-mechanisms" element={placeholder('Damage Mechanisms', 'Coming soon: mechanism lookup and contextual guidance.')} />
      <Route path="*" element={<Navigate to="/dashboard" replace />} />
    </Routes>
  );
}
