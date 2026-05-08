import { Navigate, Route, Routes } from 'react-router-dom';
import { Api570PipingExternalEntryPage } from './Api570PipingExternalEntryPage';
import { ReportEditPage } from './ReportEditPage';
import { ReportsTestDashboardPage } from './ReportsTestDashboardPage';
import { DashboardPage } from './DashboardPage';
import { CorrosionRateCalculatorPage } from './CorrosionRateCalculatorPage';
import { PipeLookupCalculatorPage } from './PipeLookupCalculatorPage';
import { PressureVesselCalculatorPage } from './PressureVesselCalculatorPage';
import { AppShell } from './components/AppShell';
import { PlaceholderPage } from './pages/PlaceholderPage';

function ShellRoute({ children }: { children: React.ReactNode }) {
  return <AppShell>{children}</AppShell>;
}

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

      <Route path="/nde-requests" element={<ShellRoute><PlaceholderPage title="NDE Requests" description="Coming soon: NDE request queue and workflows." /></ShellRoute>} />
      <Route path="/reports" element={<ShellRoute><PlaceholderPage title="Reports" description="Coming soon: role-filtered report views and actions." /></ShellRoute>} />
      <Route path="/users-access" element={<ShellRoute><PlaceholderPage title="Admin" description="Coming soon: user and access administration." /></ShellRoute>} />
      <Route path="*" element={<Navigate to="/dashboard" replace />} />
    </Routes>
  );
}
