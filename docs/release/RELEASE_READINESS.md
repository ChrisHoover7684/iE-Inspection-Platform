# Release readiness notes

## PR #209 — Product/UI architecture: role hierarchy and workflow navigation

### What this PR includes
- Planning documentation for role-aware navigation hierarchy.
- Planning documentation for role dashboard queue definitions.
- Planning documentation for NDE Request Log and report library UI contracts.
- Optional non-rendering frontend navigation metadata only.

### What this PR does not include
- No new dashboard screen implementation.
- No NDE Request Log screen implementation.
- No report timeline screen implementation.
- No asset activity screen implementation.
- No login or subscription UI implementation.
- No backend business logic, status model, workflow service, or API contract changes.

### Readiness statement
This PR should be treated as **planning/navigation architecture only** and as a dependency/input for follow-on UI implementation PRs.

## PR #210 Readiness Note
- Includes backend read/query services and models only (no new UI).
- Supports future Client Dashboard, Owner Dashboard, NDE Request Log, Report Library, Report Timeline, and Asset Activity surfaces.

## PR #211 — Frontend foundation: role-aware app shell and left navigation

### What this PR includes
- Frontend app shell structure with top bar, left navigation, and main content region.
- Role-aware grouped navigation visibility for Client, Owner, Inspector, NDE Coordinator, Admin, Viewer, and Internal/System Owner roles.
- Minimal placeholder route targets for safe incremental navigation.

### What this PR does not include
- No full dashboard implementation.
- No full NDE Request Log UI implementation.
- No full Report Timeline UI implementation.
- No full Asset Activity UI implementation.
- No backend auth, tenancy, or facility authorization changes.

### Security statement
Frontend role-based visibility is UI convenience only and not enforcement.
Backend authentication and tenant/facility authorization remain authoritative.
