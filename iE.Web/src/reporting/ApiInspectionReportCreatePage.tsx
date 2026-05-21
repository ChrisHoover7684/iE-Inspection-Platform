import { Link } from 'react-router-dom';
import { ApiInspectionReportStartWizard } from './ApiInspectionReportStartWizard';

export function ApiInspectionReportCreatePage() {
  return (
    <div className="dashboard-shell">
      <section className="dashboard-content create-report-page">
        <Link to="/reports" className="back-link">Back to Reports Dashboard</Link>
        <header className="card create-report-header">
          <h2>Create Inspection Report</h2>
          <p className="wizard-note">Use this launcher to choose a standard, select a report type, and start the inspection report.</p>
        </header>
        <ApiInspectionReportStartWizard />
      </section>
    </div>
  );
}
