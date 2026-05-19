import { useNavigate } from 'react-router-dom';

type Props = { reportTypeId?: string; draftPrepared?: boolean };

export function Api510ExternalReportShell({ reportTypeId, draftPrepared = false }: Props) {
  const navigate = useNavigate();
  const isApi510External = reportTypeId?.startsWith('api510.external.');
  const isShellTube = reportTypeId === 'api510.external.exchanger.shell-tube';
  const isDrumVessel = reportTypeId?.includes('api510.external.drum-vessel.');
  const isTowerColumn = reportTypeId?.includes('api510.external.tower-column.');
  const workspaceRoute = isShellTube
    ? '/reports/api-510-shell-tube-workspace'
    : isDrumVessel
      ? '/reports/api-510-drum-vessel-workspace'
      : undefined;

  if (!isApi510External) {
    return <p className="wizard-note">Calculation workspace is available for supported API 510 External report types.</p>;
  }

  return (
    <div className="wizard-calculation-action" aria-label="Calculations">
      <p>Open the calculation workspace with any saved Start Wizard draft setup applied.</p>
      {isShellTube && draftPrepared && <button type="button" onClick={() => navigate('/reports/api-510-shell-tube-external')}>Open Shell-and-Tube External Report</button>}
      {isDrumVessel && draftPrepared && <button type="button" onClick={() => navigate('/reports/api-510-drum-vessel-external')}>Open Drum/Vessel External Report</button>}
      {isTowerColumn && draftPrepared && <button type="button" onClick={() => navigate('/reports/api-510-tower-column-external')}>Open Tower/Column External Report</button>}
      <button
        type="button"
        disabled={!workspaceRoute}
        onClick={() => workspaceRoute && navigate(workspaceRoute)}
      >
        Open Calculation Workspace
      </button>
      {!workspaceRoute && <p className="wizard-note">Calculation workspace coming soon</p>}
    </div>
  );
}
