# Backend Operations Runbook

## Troubleshooting checks

### Report creation denied
- Confirm tenant context is resolved for authenticated requests.
- Confirm `reports.create` entitlement exists and is enabled for tenant plan when enforcement is on.
- Confirm facility/process unit/asset references are valid and tenant-scoped.

### max.activeReports denies
- Verify entitlement row `max.activeReports` exists and `LimitValue` is set as intended.
- Verify tenant active report count and report statuses.
- If emergency mitigation is required, keep enforcement disabled until data is corrected.

### max.users denies
- Verify entitlement row `max.users` exists and `LimitValue` is set correctly.
- Verify `ClientOrganizationUser` active member count and invite/member lifecycle state.

### Tenant member invite fails
- Validate `ClientOrganization` exists and is active.
- Validate target facility access rows are valid (`UserFacilityAccess`).
- Validate `max.users` entitlement capacity when enforcement is enabled.

### Suspicious account sharing detected
- Review account-sharing audit events and reason codes.
- Confirm whether device/IP diversity is expected (VPN, travel, shared kiosk).
- Current behavior is audit-only: no automatic blocking or revocation is applied.

## Health endpoint operations
- `/health/live` is public and should only represent process liveness.
- `/health/ready` is public and should represent database readiness checks.
- Auth/subscription/account-sharing/audit issues should not mark liveness down.

## PR #204 backend workflow foundation
Added backend-only ReportLogEntry and NdeRequest persistence/service foundation. No UI, vendor workflow, email sending, or automatic findings creation added.
- PR #204 expanded: backend-only report log/NDE foundation includes built-in + tenant-custom NDE type definition support (no UI/API/email/vendor flow).

## PR #205 workflow migration and hardening
- Added the missing migration for PR #204 backend workflow models (`ReportLogEntries`, `NdeRequests`, `NdeRequestTypeDefinitions`).
- Report Log and NDE Request Log remain backend-only.
- No UI was added.
- No email/vendor workflow was added.
- No automatic findings creation was added.
- Built-in NDE types include UTSW, PAUT, WFMT, WFPT, ETC, IRIS, Guided Wave UT, and UTT Grid.
- Tenant-custom NDE type foundation exists; public UI/API management remains future work.

- NDE requests support a user-facing "Cancelled" action (internal status code remains `canceled`).
- Cancelled is terminal and kills request workflow transitions; the request is preserved (not deleted) for traceability.
- No UI was added.
- No vendor workflow/email was added.
- No automatic findings creation was added.


## PR #206 report workflow timeline wiring
- Wired `ReportLogEntry` writes into existing report lifecycle backend actions (create, create-from-template, update, submit/start/approve/return review, and status changes).
- `ReportLogEntry` remains business workflow history and does not replace `AuditEvent`.
- No UI, email/vendor workflow, or automatic findings generation was added.
- NDE `Canceled` remains terminal and traceability is preserved.
- Future UI timeline/status history can query report log entries.

## Internal stress-test seed readiness (PR #207)
- Config defaults are safe and disabled: `StressTestSeed:Enabled=false`, `StressTestSeed:DurationDays=90`, optional `StartsAtUtc`, required `ClientOrganizationId` when enabled.
- Internal only: no public admin API, no UI, no billing provider or checkout integration.
- Entitlements seeded for `company_stress_test_90d`: `reports.create`, `reports.export`, `photos.markup`, `audit.query`, `max.activeReports=null`, `max.users=null`.
- If `max.facilities` exists for the plan, it is normalized to `null` for unlimited capacity.
