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
