# Release Readiness (#203)

## Scope statement
PR #203 is **backend readiness/config/runbook hardening only**.
- No UI was added.
- No billing provider integration was added.
- No public admin/user-management APIs were added.
- No automatic session blocking/revocation was added.
- Calculator endpoints remain intentionally ungated.
- Health endpoints remain public.
- `Subscriptions:EnforcementEnabled` remains default `false`.
- `AccountSharingAudit` remains audit-only.

## Required environment variables
- `ConnectionStrings__InspectionReports` (required, non-placeholder value).
- `Authentication__Enabled` (default `false`).
- `Authentication__JwtBearer__Authority` (required only when auth enabled).
- `Authentication__JwtBearer__Audience` (required only when auth enabled).
- `Subscriptions__EnforcementEnabled` (default `false`).
- `Database__ApplyMigrationsOnStartup` (default `false`, explicit opt-in only).
- `AccountSharingAudit__Enabled` (default `false`, audit collection only).

## Safe local/dev defaults
- `Authentication:Enabled=false`.
- `Subscriptions:EnforcementEnabled=false`.
- `Database:ApplyMigrationsOnStartup=false`.
- `AccountSharingAudit:Enabled=false`.

## Staging enablement order
1. Configure database connection.
2. Apply migrations.
3. Seed tenant/facility/member/subscription data.
4. Validate `/health/live` and `/health/ready` behavior.
5. Enable authentication only when authority/audience are configured.
6. Enable subscription enforcement only after entitlements exist.
7. Enable account-sharing audit only for audit collection (non-blocking).

## Migration safety checklist
- Back up the database before applying migrations.
- Verify EF migrations are present and ordered before deploy.
- `Database:ApplyMigrationsOnStartup=false` means migrations are not auto-applied.
- `Database:ApplyMigrationsOnStartup=true` is explicit opt-in and should be temporary for controlled releases.
- Documented migration sequence includes tenant/subscription/session/device updates:
  - `20260506150000_AddSubscriptionEntitlementFoundation`
  - `20260508120000_AddClientOrganizationUsers`
  - `20260508170000_AddUserSessionDeviceAuditFoundation`

## Subscription readiness before enforcement
Before switching `Subscriptions:EnforcementEnabled=true`, verify seed data for each onboarded tenant:
- `ClientOrganization`
- `Facility`
- `ProcessUnit`
- `Asset` (where needed)
- `UserFacilityAccess`
- `ClientOrganizationUser`
- `SubscriptionPlan`
- `ClientSubscription`
- `SubscriptionEntitlement` rows:
  - `reports.create`
  - `reports.export`
  - `photos.markup`
  - `audit.query`
  - `max.activeReports`
  - `max.users`

