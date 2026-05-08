# Entitlements Foundation (PR #194)

This PR introduces backend-only subscription and entitlement foundation:
- SubscriptionPlan
- ClientSubscription
- SubscriptionEntitlement
- IEntitlementService/EntitlementService

Out of scope in this phase:
- Billing provider integration
- Subscription/admin public APIs
- UI changes
- Endpoint-level entitlement enforcement

Future PRs will map entitlement keys to report/export/photo/audit operations.
Health endpoints remain public.
Calculator endpoints remain intentionally unprotected for this phase.

## PR #195 Enforcement Guardrails
- New flag: `Subscriptions:EnforcementEnabled` (default `false`).
- When disabled, entitlement checks are allow/no-op.
- When enabled, `IEntitlementService` is enforced for report creation/export, photo markup creation, and internal audit query.
- No billing provider integration or UI/public subscription APIs in this phase.
- Calculator endpoints are intentionally not entitlement-gated.
- Health endpoints remain public.

## PR #196 active report limit enforcement
- `max.activeReports` is enforced only when `Subscriptions:EnforcementEnabled=true`.
- MVP active report definition: all tenant reports with status other than `Final`.
- Enforcement is applied only to persisted report creation paths (`CreateInstance`, `CreateInstanceFromTemplate`, and create branch of `BuildDraftFromChecklist`).
- No billing provider integration, UI, or public subscription/admin APIs in this PR.

## PR #198 safety/regression hardening
- Subscription enforcement remains feature-flagged via `Subscriptions:EnforcementEnabled` (default `false`).
- Added backend safety/regression coverage for startup configuration behavior, entitlement denial safety mapping, guard ordering, and audit metadata allow-listing.
- Billing provider integration is still future work; no Stripe/Marketplace/AppSource/payment integration in this PR.
- No UI and no public subscription/admin APIs were added.
- Calculator endpoints remain intentionally ungated.

## PR #199 seat entitlement foundation
- Added backend foundation for tenant seat limits using entitlement key `max.users`.
- Plan tiers remain entitlement/value driven (for example, Starter may have `max.users=10`), without hard-coding tier logic in controllers.
- MVP seat counting rule: `active + invited` users consume seats; `disabled + removed` do not.
- No billing provider integration, no subscription UI, and no public admin/user-management API were added.
- Calculator endpoints remain intentionally ungated; health endpoints remain public.

## PR #200 Tenant Member Workflow Service
- Backend-only tenant member workflow service adds invite/activate/disable/remove flows.
- `max.users` is enforced only when `Subscriptions:EnforcementEnabled=true`.
- Seat counting: `active` + `invited` consume seats; `disabled` + `removed` do not.
- No public admin/user-management API, no UI, no email sending, and no billing provider integration in this PR.
- Calculator endpoints remain intentionally ungated; health endpoints remain public.

- `max.users` is a named-user seat limit: one active/invited tenant member record represents one named human user seat.
- Shared logins/accounts are not supported as a bypass for seat limits.
- Session/device sharing enforcement is not part of PR #200 (future hardening).

## PR #201 session/device audit-only foundation
- Backend-only session/device audit persistence and conservative suspicious-sharing detection hooks were added.
- Named-user seats remain one active/invited member per human; shared logins are unsupported.
- Multi-device use for a legitimate single user remains allowed.
- No enforcement/deny behavior yet; audit-only reason codes.
- No UI/public admin API and no billing provider integration in this PR.
- Calculator endpoints remain intentionally ungated and health endpoints remain public.

## PR #202 session/device audit safety hardening
- Added backend-only safety/regression tests that harden the PR #201 session/device audit foundation.
- Named-user seats remain the licensing model (`max.users`): one active/invited tenant member record equals one named human seat.
- Shared logins/accounts remain unsupported for seat-limit bypass.
- Legitimate single-user multi-device use remains allowed.
- Suspicious-sharing detection remains audit-only (no automatic block/revoke/lockout behavior).
- No public user/admin API, no UI, and no billing/provider integration were added.
- Calculator endpoints remain intentionally ungated; health endpoints remain public.

## #203 operational readiness note
This PR only hardens backend release/configuration readiness.
- No UI additions.
- No Stripe/Marketplace/AppSource/payment integration.
- No new public admin/user APIs.
- No automatic session blocking/revocation.

Before enabling `Subscriptions:EnforcementEnabled=true`, ensure tenant seed readiness:
- `ClientOrganization`, `Facility`, `ProcessUnit`, `Asset` (when needed)
- `UserFacilityAccess`, `ClientOrganizationUser`
- `SubscriptionPlan`, `ClientSubscription`
- Entitlements: `reports.create`, `reports.export`, `photos.markup`, `audit.query`, `max.activeReports`, `max.users`
