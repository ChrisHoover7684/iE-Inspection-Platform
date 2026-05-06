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
