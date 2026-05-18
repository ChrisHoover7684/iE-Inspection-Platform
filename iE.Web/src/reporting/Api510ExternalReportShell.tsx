import { useNavigate } from 'react-router-dom';

type Props = { reportTypeId?: string };

export function Api510ExternalReportShell({ reportTypeId }: Props) {
  const navigate = useNavigate();
  const isApi510External = reportTypeId?.startsWith('api510.external.');
  const isShellTube = reportTypeId === 'api510.external.exchanger.shell-tube';
  const isDrumVessel = reportTypeId?.includes('api510.external.drum-vessel.');
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
      <p>Open the existing calculation workspace. Report field-value handoff will follow in a subsequent update.</p>
      <button
        type="button"
        disabled={!workspaceRoute}
        onClick={() => workspaceRoute && navigate(workspaceRoute)}
      >
        {workspaceRoute ? 'Open Calculation Workspace' : 'Calculation workspace coming soon'}
      </button>
    </div>
  );
}
