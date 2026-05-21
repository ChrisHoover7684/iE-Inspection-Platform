import { Link } from 'react-router-dom';
import { ApiInspectionReportStartWizard } from './ApiInspectionReportStartWizard';

export function ApiInspectionReportCreatePage() {
  return (
    <div className="dashboard-shell">
      <section className="dashboard-content create-report-page">
        <Link to="/reports" className="back-link">Back to Reports Dashboard</Link>
        <header className="card create-report-header">
          <h2>Create Inspection Report</h2>
          <p className="wizard-note">Choose the inspection standard and report type, then launch the report page.</p>
        </header>
        <ApiInspectionReportStartWizard />
      </section>
    </div>
  );
}
