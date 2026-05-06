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
