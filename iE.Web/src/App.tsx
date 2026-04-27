import { Navigate, Route, Routes } from 'react-router-dom';
import { ReportsTestDashboardPage } from './ReportsTestDashboardPage';
import { ReportEditPage } from './ReportEditPage';

export default function App() {
  return (
    <Routes>
      <Route path="/reports-test" element={<ReportsTestDashboardPage />} />
      <Route path="/reports-test/:id" element={<ReportEditPage />} />
      <Route path="*" element={<Navigate to="/reports-test" replace />} />
    </Routes>
  );
}
