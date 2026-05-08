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
import { appPlaceholderRoutes } from './navigation/appRoutes';

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

      {appPlaceholderRoutes.map((route) => (
        <Route
          key={route.path}
          path={route.path}
          element={<ShellRoute><PlaceholderPage title={route.title} description={route.description} /></ShellRoute>}
        />
      ))}

      <Route path="*" element={<Navigate to="/dashboard" replace />} />
    </Routes>
  );
}
