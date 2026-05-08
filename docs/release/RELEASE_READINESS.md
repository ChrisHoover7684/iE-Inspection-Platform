# Backend Release Readiness Checklist

Use this checklist before backend-focused releases.

## 1) CI / Build / Test
- [ ] Blocking CI is green on target branch.
- [ ] `dotnet restore`, `dotnet build`, and `dotnet test` pass on release commit.
- [ ] Any skipped checks are documented with owner and follow-up date.

## 2) Engineering calculation provenance
- [ ] Calculation changes are tied to provenance notes.
- [ ] Standard/code edition (if known) is recorded.
- [ ] Any baseline-only numbers are marked pending review.

## 3) Golden tests
- [ ] Affected calculators have golden-case coverage or an approved gap note.
- [ ] Golden cases include units, tolerance, and approval status.
- [ ] Expected-output changes received engineering review.

## 4) Secrets and configuration
- [ ] No production credentials committed.
- [ ] Required environment variables documented (including `ConnectionStrings__InspectionReports`).
- [ ] Connection strings and secrets resolved via secure config paths.

## 5) Database migrations
- [ ] `Database:ApplyMigrationsOnStartup` flag is reviewed for the target environment.
- [ ] Production migration strategy is deliberate (startup migration disabled unless explicitly approved).
- [ ] New migrations are reviewed and applied in staging.
- [ ] Backward-compatibility/rollback notes are documented.
- [ ] Data-seeding impacts are understood and tested.

## 6) Backups / restore
- [ ] Backup procedure validated for current schema.
- [ ] Restore drill completed recently or scheduled with owner/date.
- [ ] RPO/RTO expectations are acknowledged.

## 7) Logging / monitoring
- [ ] Critical backend paths emit actionable logs.
- [ ] Alerting thresholds are defined for key failures.
- [ ] Sensitive fields are redacted where required.

## 8) Authentication / tenant isolation
- [ ] Auth provider selected and token/claims contract documented (see `docs/security/AUTH_TENANCY_DESIGN.md` MVP decision record dated 2026-05-06).
- [ ] PR A auth foundation validated (`Authentication:Enabled`, JWT config validation, tenant context wiring) before enabling external access.
- [ ] Tenant isolation implemented across report/photo/markup/review data paths (including facility scope enforcement for MVP).
- [ ] Tenant isolation tests passing (including cross-tenant deny cases).
- [ ] Audit logging implemented for sensitive report and authorization events.
- [ ] Cross-tenant access behavior reviewed for consistent 404/403 policy.
- [ ] Access checks verified for modified endpoints/services.
- [ ] Update-body ownership overwrite protection verified (update payload cannot change `ClientOrganizationId`, `FacilityId`, `CreatedAt`, or `CreatedByUserId` on persisted reports).
- [ ] Tenant boundary assumptions validated in tests/review.
- [ ] Privilege escalation paths reviewed.

## 9) API error handling
- [ ] Failure responses are deterministic and documented.
- [ ] Validation and domain errors are distinguishable.
- [ ] Internal exceptions do not leak sensitive implementation details.

## 10) Legal / disclaimer
- [ ] Engineering-calculation disclaimer language is current.
- [ ] User-facing and report-facing risk statements are verified.
- [ ] Any new compliance obligations are documented.

## 11) Marketplace readiness (later)
- [ ] Deferred unless release explicitly targets marketplace exposure.
- [ ] If in scope later: packaging, terms, and support workflows are tracked.

## 12) Health endpoints
- [ ] Liveness endpoint responds at `GET /health/live`.
- [ ] Readiness endpoint responds at `GET /health/ready` and verifies database connectivity.
- [ ] Health responses do not expose sensitive configuration data.

- [ ] API error-handling consistency verified (standardized error envelope with code/message/traceId).
- [ ] Structured logging verified for startup migration decisions and unhandled exceptions.
- [ ] Logging review confirms no sensitive data (secrets, connection strings, raw report payloads) is emitted.

## Authorization PR B Evidence

- [ ] PR #189 persisted report-instance authorization sweep verified complete for `ReportingController` (create-from-template, checklist persisted create/update, sync-findings, submit/start/approve/return workflow transitions, and review-history).
- [ ] PR #190 dedicated photo/markup endpoint ownership enforcement verified complete through owning report scope.
- [ ] Endpoint classification documented: metadata/utility/non-persisted helpers intentionally out of scope for PR #189.
- [ ] Calculator endpoints remain intentionally unprotected in this phase.
- [ ] Health endpoints remain public.
- [ ] Audit logging follow-up remains tracked as a separate PR (not included in PR #190 scope).
- [ ] Facility move attempts remain blocked pending dedicated workflow.
- Report read/write/export/review paths now include tenant/capability checks and 404-vs-403 behavior.
- Dedicated photo/markup endpoint-level ownership enforcement is now complete in PR #190.

## PR #191
- Implemented append-only backend accountability auditing for sensitive report/photo/export/workflow and authorization-denial events.
- Sensitive metadata filtering and bounded metadata storage added.
- Health remains public; calculators remain intentionally unprotected and not audited in this phase.
- Audit administration/user interface is deferred.

- PR #192: Backend audit hardening complete (metadata sanitization bounds, append-only guardrails, tenant-scoped internal query service).
- No public audit API or UI shipped in this release; internal-only groundwork.
- Health endpoints remain public; calculator endpoints intentionally unprotected and unaudited for this phase.

## PR #193 tenancy hardening
- Added backend guardrails for persisted report ownership-reference consistency (tenant/facility/process-unit/asset).
- Facility moves remain blocked pending dedicated workflow.
- Health endpoints remain public.
- Calculator endpoints remain intentionally unprotected and not audited in this phase.
- No audit/admin/subscription UI surface added.

## PR #194 Subscription/Entitlement Foundation
- Backend data model + service foundation added for tenant-scoped entitlements.
- Billing provider integrations (Stripe/Marketplace/AppSource) are deferred.
- No UI or public admin subscription APIs added.
- Existing report/calculator endpoint behavior remains unchanged in this phase.

## PR #195
- Added configuration-gated entitlement guardrails (`Subscriptions:EnforcementEnabled`, default false).
- Enforced operations in this phase: report create/export, photo markup create, internal audit query.
- No Stripe/Marketplace/AppSource/payment integrations yet.
- No UI/public subscription APIs yet.
- Calculator endpoints remain not entitlement-gated; health endpoints remain public.

## PR #196 release note
- Added backend-only enforcement for subscription `max.activeReports` on persisted report creation paths, feature-flagged by `Subscriptions:EnforcementEnabled` (default `false`).
- Active report count for this MVP is non-`Final` tenant-scoped reports.
- No web UI, billing integration, or public subscription administration APIs were introduced.

## PR #198 release note
- Backend-only safety/regression coverage added for subscription enforcement startup/config behavior, safe reason-code handling, and enforcement check ordering.
- Entitlement-denial and limit-denial audit metadata coverage confirms allow-listed fields only.
- Subscription enforcement remains feature-flagged (`Subscriptions:EnforcementEnabled` default `false`).
- Billing/provider integration is still future work.
- No UI/public admin API work added.
- Calculator endpoints remain intentionally ungated.

## PR #199 release note
- Added backend-only tenant-user seat entitlement foundation (`max.users`) and seat-usage service/guard for future invite/member enforcement.
- Plan tiers continue to be represented by entitlement rows and optional limit values (example: Starter `max.users = 10`).
- No billing provider integration (Stripe/Marketplace/AppSource/payment), no subscription UI, and no public admin/user-management APIs were introduced.
- Health endpoints remain public and calculator endpoints remain intentionally ungated.
