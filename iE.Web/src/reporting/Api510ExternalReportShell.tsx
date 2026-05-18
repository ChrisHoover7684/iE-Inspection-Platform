import { useNavigate } from 'react-router-dom';

type Props = { reportTypeId?: string };

export function Api510ExternalReportShell({ reportTypeId }: Props) {
  const navigate = useNavigate();
  const isShellTube = reportTypeId === 'api510.external.exchanger.shell-tube';
  const isDrumVessel = reportTypeId?.includes('api510.external.drum-vessel.');

  if (!isShellTube && !isDrumVessel) return null;

  return (
    <section className="card" aria-label="Calculations">
      <h4>Calculations</h4>
      <p>Open the existing calculation workspace. Report field-value handoff will follow in a subsequent update.</p>
      {isShellTube && (
        <button type="button" onClick={() => navigate('/reports/api-510-shell-tube-workspace')}>
          Open Shell-and-Tube Calculation Workspace
        </button>
      )}
      {isDrumVessel && (
        <button type="button" onClick={() => navigate('/reports/api-510-drum-vessel-workspace')}>
          Open Drum/Vessel Calculation Workspace
        </button>
      )}
    </section>
  );
}
