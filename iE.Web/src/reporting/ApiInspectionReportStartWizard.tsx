import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { allApiInspectionReportOptions, apiInspectionReportHierarchy, type ApiInspectionReportOption } from './apiInspectionReportHierarchy';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from './componentCalculationPrefill';

type StandardId = 'api570' | 'api510' | 'sti653';

type MinimalDraftSetup = {
  selectedStandard: string;
  selectedReportTypeId: string;
  selectedReportTypeLabel: string;
  equipmentFamily: string;
  equipmentSubtype: string;
  startedAt: string;
  reportTypeId: string;
  reportTypeLabel: string;
  header: Record<string, string>;
  components: string[];
};

const standardCards: { id: StandardId; label: string }[] = [
  { id: 'api570', label: 'API 570 Piping' },
  { id: 'api510', label: 'API 510 Pressure Equipment' },
  { id: 'sti653', label: 'STI / API 653 Tanks' }
];

const reportRouteByTypeId: Record<string, string> = {
  'api570.piping-external': '/reports/api-570-piping-external',
  'api570.piping-cui-external': '/reports/api-570-piping-external?type=cui',
  'api510.external.exchanger.shell-tube': '/reports/api-510-shell-tube-external',
  'api510.external.exchanger.plate-frame': '/reports/api-510-exchanger-external',
  'api510.external.exchanger.double-pipe': '/reports/api-510-exchanger-external',
  'api510.external.exchanger.air-cooler': '/reports/api-510-exchanger-external',
  'api510.external.drum-vessel.horizontal-drum': '/reports/api-510-drum-vessel-external',
  'api510.external.drum-vessel.vertical-drum': '/reports/api-510-drum-vessel-external',
  'api510.external.drum-vessel.separator-ko-drum': '/reports/api-510-drum-vessel-external',
  'api510.external.drum-vessel.accumulator-receiver': '/reports/api-510-drum-vessel-external',
  'api510.external.drum-vessel.generic-pressure-vessel': '/reports/api-510-drum-vessel-external',
  'api510.external.tower-column.distillation': '/reports/api-510-tower-column-external',
  'api510.external.tower-column.absorber-stripper': '/reports/api-510-tower-column-external',
  'api510.external.tower-column.packed-column': '/reports/api-510-tower-column-external',
  'api510.external.tower-column.tray-column': '/reports/api-510-tower-column-external',
  'sti653.external.tank': '/reports/sti-api-653-tank-external'
};

const equipmentFamilyByStandard: Record<StandardId, string> = {
  api570: 'Piping',
  api510: 'Pressure Equipment',
  sti653: 'Tanks'
};

const equipmentSubtypeByTypeId: Record<string, string> = {
  'api570.piping-external': 'Piping External',
  'api570.piping-cui-external': 'Piping CUI External',
  'api510.external.exchanger.shell-tube': 'Shell and Tube Exchanger',
  'api510.external.exchanger.plate-frame': 'Plate and Frame Exchanger',
  'api510.external.exchanger.double-pipe': 'Double Pipe Exchanger',
  'api510.external.exchanger.air-cooler': 'Air Cooler / Fin Fan',
  'api510.external.drum-vessel.horizontal-drum': 'Horizontal Drum',
  'api510.external.drum-vessel.vertical-drum': 'Vertical Drum',
  'api510.external.drum-vessel.separator-ko-drum': 'Separator / KO Drum',
  'api510.external.drum-vessel.accumulator-receiver': 'Accumulator / Receiver',
  'api510.external.drum-vessel.generic-pressure-vessel': 'Generic Pressure Vessel',
  'api510.external.tower-column.distillation': 'Distillation Tower',
  'api510.external.tower-column.absorber-stripper': 'Absorber / Stripper',
  'api510.external.tower-column.packed-column': 'Packed Column',
  'api510.external.tower-column.tray-column': 'Tray Column',
  'sti653.external.tank': 'Tank'
};

function getOptionsForStandard(standard: StandardId) {
  if (standard === 'api570') return [{ label: null, options: apiInspectionReportHierarchy.api570.options }];
  if (standard === 'sti653') return [{ label: null, options: apiInspectionReportHierarchy.sti653External.options }];
  return apiInspectionReportHierarchy.api510External.groups.map((group) => ({ label: group.label, options: group.options }));
}

export function ApiInspectionReportStartWizard() {
  const navigate = useNavigate();
  const [selectedStandard, setSelectedStandard] = useState<StandardId>('api570');
  const [selectedTypeId, setSelectedTypeId] = useState('api570.piping-external');

  const selectedReportType = useMemo(() => allApiInspectionReportOptions.find((option) => option.id === selectedTypeId), [selectedTypeId]);
  const groupedOptions = useMemo(() => getOptionsForStandard(selectedStandard), [selectedStandard]);

  const onSelectStandard = (standard: StandardId) => {
    setSelectedStandard(standard);
    const first = getOptionsForStandard(standard).flatMap((g) => g.options)[0];
    if (first) setSelectedTypeId(first.id);
  };

  const startInspectionReport = () => {
    if (!selectedReportType) return;
    const draft: MinimalDraftSetup = {
      selectedStandard: standardCards.find((s) => s.id === selectedStandard)?.label ?? selectedStandard,
      selectedReportTypeId: selectedReportType.id,
      selectedReportTypeLabel: selectedReportType.label,
      equipmentFamily: equipmentFamilyByStandard[selectedStandard],
      equipmentSubtype: equipmentSubtypeByTypeId[selectedReportType.id] ?? selectedReportType.label,
      startedAt: new Date().toISOString(),
      reportTypeId: selectedReportType.id,
      reportTypeLabel: selectedReportType.label,
      header: {},
      components: []
    };
    window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify(draft));
    navigate(reportRouteByTypeId[selectedReportType.id] ?? '/reports');
  };

  return <section className="card start-wizard" aria-label="Create Inspection Report Launcher">
    <h3>Step 1: Choose Inspection Standard</h3>
    <div className="wizard-hierarchy wizard-step-layout" aria-label="Inspection standards">
      {standardCards.map((standard) => <button key={standard.id} type="button" className="wizard-standard-card" aria-pressed={selectedStandard === standard.id} onClick={() => onSelectStandard(standard.id)}>{standard.label}</button>)}
    </div>

    <h3>Step 2: Choose Report Type</h3>
    <div className="wizard-step-layout" aria-label="Report types">
      {groupedOptions.map((group) => <section key={group.label ?? 'default'} className="wizard-hierarchy-group">
        {group.label && <h4>{group.label}</h4>}
        {group.options.map((option: ApiInspectionReportOption) => <button key={option.id} type="button" className="wizard-report-type-option" aria-pressed={selectedTypeId === option.id} onClick={() => setSelectedTypeId(option.id)}>{option.label}</button>)}
      </section>)}
    </div>

    <section className="card wizard-setup-card" aria-label="Selected report summary">
      <h3>Step 3: Start Inspection Report</h3>
      <p><strong>Selected Standard:</strong> {standardCards.find((s) => s.id === selectedStandard)?.label}</p>
      <p><strong>Selected Report Type:</strong> {selectedReportType?.label}</p>
      <button type="button" onClick={startInspectionReport}>Start Inspection Report</button>
    </section>
  </section>;
}
