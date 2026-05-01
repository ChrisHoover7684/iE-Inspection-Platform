import { Navigate, Route, Routes } from 'react-router-dom';
import { Api570PipingExternalEntryPage } from './Api570PipingExternalEntryPage';
import { ReportEditPage } from './ReportEditPage';
import { ReportsTestDashboardPage } from './ReportsTestDashboardPage';

export default function App() {
  return (
    <Routes>
      <Route path="/reports-test" element={<ReportsTestDashboardPage />} />
      <Route path="/reports-test/:id" element={<ReportEditPage />} />
      <Route path="/reports/api-570-piping-external" element={<Api570PipingExternalEntryPage />} />
      <Route path="*" element={<Navigate to="/reports/api-570-piping-external" replace />} />
    </Routes>
  );
}
