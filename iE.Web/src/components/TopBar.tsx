export function TopBar() {
  return (
    <header className="app-topbar">
      <div className="app-topbar-brand">
        <div className="app-title">iE Inspection Platform</div>
        <div className="app-subtitle">Organization / Facility (placeholder)</div>
      </div>
      <div className="app-topbar-actions" aria-label="Account controls placeholder">
        <span className="topbar-user-chip">Operator (placeholder)</span>
        <button type="button" className="topbar-user-menu" aria-label="User menu placeholder">
          User Menu (placeholder)
        </button>
      </div>
    </header>
  );
}
