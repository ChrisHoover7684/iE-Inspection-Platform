import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';

type InspectionStandard = 'api570' | 'api510' | 'sti653';

type ReportTypeOption = {
  id: string;
  label: string;
  route: string;
  standard: InspectionStandard;
};

const standards: { id: InspectionStandard; label: string }[] = [
  { id: 'api570', label: 'API 570 Piping' },
  { id: 'api510', label: 'API 510 Pressure Equipment' },
  { id: 'sti653', label: 'STI / API 653 Tanks' }
];

const reportTypes: ReportTypeOption[] = [
  { id: 'api570-piping-external', label: 'API 570 Piping External', route: '/reports/api-570-piping-external', standard: 'api570' },
  { id: 'api570-piping-cui-external', label: 'API 570 Piping CUI External', route: '/reports/api-570-piping-external?type=cui', standard: 'api570' },
  { id: 'api510-shell-tube-external', label: 'API 510 Shell and Tube External', route: '/reports/api-510-shell-tube-external', standard: 'api510' },
  { id: 'api510-drum-vessel-external', label: 'API 510 Drum / Vessel External', route: '/reports/api-510-drum-vessel-external', standard: 'api510' },
  { id: 'api510-tower-column-external', label: 'API 510 Tower / Column External', route: '/reports/api-510-tower-column-external', standard: 'api510' },
  { id: 'api510-plate-frame-external', label: 'API 510 Plate and Frame External', route: '/reports/api-510-exchanger-external', standard: 'api510' },
  { id: 'sti653-tank-external', label: 'STI / API 653 Tank External', route: '/reports/sti-api-653-tank-external', standard: 'sti653' }
];

export function ApiInspectionReportStartWizard() {
  const navigate = useNavigate();
  const [selectedStandard, setSelectedStandard] = useState<InspectionStandard | ''>('');
  const [selectedReportTypeId, setSelectedReportTypeId] = useState('');

  const availableReportTypes = useMemo(
    () => reportTypes.filter((reportType) => reportType.standard === selectedStandard),
    [selectedStandard]
  );

  const selectedReportType = useMemo(
    () => reportTypes.find((reportType) => reportType.id === selectedReportTypeId),
    [selectedReportTypeId]
  );

  const startInspectionReport = () => {
    if (!selectedStandard || !selectedReportType) return;
    navigate(selectedReportType.route);
  };

  return <section className="card start-wizard" aria-label="Inspection Report Launcher">
    <h3>Step 1: Choose Inspection Standard</h3>
    <div className="wizard-hierarchy-group" aria-label="Inspection standards">
      {standards.map((standard) => (
        <label key={standard.id}>
          <input
            type="radio"
            name="inspection-standard"
            checked={selectedStandard === standard.id}
            onChange={() => {
              setSelectedStandard(standard.id);
              setSelectedReportTypeId('');
            }}
          />
          {standard.label}
        </label>
      ))}
    </div>

    <h3>Step 2: Choose Report Type</h3>
    <div className="wizard-hierarchy-group" aria-label="Report types">
      {availableReportTypes.length === 0
        ? <p className="wizard-note">Select an inspection standard to view report types.</p>
        : availableReportTypes.map((option) => (
          <label key={option.id}>
            <input
              type="radio"
              name="report-type"
              checked={selectedReportTypeId === option.id}
              onChange={() => setSelectedReportTypeId(option.id)}
            />
            {option.label}
          </label>
        ))}
    </div>

    <h3>Step 3: Start Inspection Report</h3>
    <button type="button" onClick={startInspectionReport} disabled={!selectedStandard || !selectedReportTypeId}>
      Start Inspection Report
    </button>
  </section>;
}
