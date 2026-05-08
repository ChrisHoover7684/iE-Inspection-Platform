# Authentication, Authorization, Tenant Isolation, and Audit Design (Backend)

## Scope and non-goals

This design defines the backend production approach for:
- Authentication
- Authorization (role and policy based)
- Tenant isolation
- Audit logging

Non-goals for this PR:
- No login implementation
- No runtime behavior changes
- No EF migration changes
- No UI changes

## Current backend model observations

### Existing multi-organization data anchors
- `InspectionReport` already carries `ClientOrganizationId`, `FacilityId`, and optional `ProcessUnitId`/`AssetId`. This is a strong base for tenant partitioning at report scope.
- `InspectionReport` also tracks `CreatedByUserId` and `UpdatedByUserId`, which can be aligned with future identity provider subject/user IDs.
- `InspectionReportsDbContext` has first-class entities for `ClientOrganization`, `Facility`, `ProcessUnit`, `Asset`, and `UserFacilityAccess`.

### Existing report/photo/markup/review persistence shape
- Reports are stored in `InspectionReports` with owned collections for findings, observations, photos, and review history.
- `InspectionPhoto` is currently an owned entity under a report and therefore inherits report boundary implicitly.
- `PhotoMarkup` is currently a top-level table keyed by `PhotoId` and does not directly store tenant/report foreign keys.
- Review workflow history (`ReportReviewHistory`) captures actor (`PerformedByUserId`) and timestamp already.

### Current isolation gap
- Repository queries are optionally filterable by org/facility/status, but not enforced by authenticated principal context.
- `PhotoMarkups` query by `PhotoId` alone, which requires explicit tenant/report ownership enforcement in future implementation.

## Authentication approach options

### Option A — External OpenID Connect (OIDC) provider + JWT bearer (recommended)
Examples: Microsoft Entra ID / Auth0 / Okta / AWS Cognito.

Pros:
- Mature, standards-based auth (OIDC/OAuth2)
- Shortest path to production-grade security controls (MFA, lifecycle, SSO, passwordless)
- Reduced liability for credential handling

Cons:
- External dependency and provider configuration complexity

### Option B — ASP.NET Core Identity self-hosted
Pros:
- Full in-house control

Cons:
- Higher security/operations burden (credential storage, resets, MFA hardening, abuse controls)
- Slower to production readiness

### Option C — API key only (service integrations)
Pros:
- Simple for machine-to-machine

Cons:
- Not sufficient for human inspector/reviewer workflows
- Weak fit for role-granular UI/API access

## Recommended first implementation

Use **Option A** with JWT bearer auth:
1. Validate JWT signature/issuer/audience in API.
2. Map claims to internal principal model:
   - `sub` -> User external ID
   - tenant claim (e.g., `tenant_id`/`org_id`) -> `ClientOrganizationId`
   - role claims -> platform roles/policies
3. Resolve effective access from token + internal access table (`UserFacilityAccess`) for facility-level constraints.


## MVP decision record (approved for first implementation PR series)

Decision date: **2026-05-06**

### 1) Selected auth provider recommendation
- **Recommendation: Microsoft Entra ID (OIDC + OAuth2 JWT bearer) for MVP**.
- Rationale: enterprise customer fit, strong MFA/Conditional Access support, and direct alignment with Azure-hosted backend patterns.
- Contract note: keep JWT validation provider-agnostic at the middleware/policy layer so Auth0/Okta remain viable later without domain model churn.

### 2) Required JWT claims (MVP contract)
Required on every access token accepted by backend APIs:
- `iss` (issuer)
- `aud` (audience)
- `exp` (expiration)
- `iat` (issued-at)
- `sub` (external user id)
- `tid` (tenant directory id from IdP, for provenance/logging)
- `org_id` (**application tenant claim** mapping to `ClientOrganizationId`)
- `roles` (application roles)

Optional but recommended:
- `email`
- `name`

### 3) Claim-to-internal-user mapping
- `sub` -> `User.ExternalSubject` (canonical unique key).
- `email` -> `User.Email` (snapshot, non-authoritative).
- `name` -> `User.DisplayName` (snapshot for audit readability).
- `tid` -> `User.LastSeenIdentityTenantId` (diagnostics/audit provenance).

Rules:
- Internal user lookup is by (`ExternalSubject`, `IdentityProvider`) tuple.
- User record is auto-provisioned on first successful auth if no record exists (minimal profile only).
- Authorization decisions never rely on `email` or `name`; those are informational only.

### 4) Tenant claim mapping
- Token `org_id` -> internal `ClientOrganizationId` (GUID/UUID string).
- Backend rejects token when `org_id` is missing, malformed, or refers to unknown/inactive client org.
- Route/query tenant identifiers (when present) must match claim-derived tenant context or request is denied per policy below.

### 5) Role/capability mapping
MVP roles in token `roles` claim map to backend capabilities:
- `ie_owner` / `ie_admin` -> `reports.read`, `reports.write`, `reports.submit`, `reports.review`, `photos.read`, `photos.write`, `exports.read`, `admin.users.manage`
- `ie_inspector` -> `reports.read`, `reports.write`, `reports.submit`, `photos.read`, `photos.write`, `exports.read`
- `ie_reviewer` -> `reports.read`, `reports.review`, `photos.read`, `exports.read`
- `ie_readonly` -> `reports.read`, `photos.read`, `exports.read`

Implementation rule:
- Endpoints authorize policies/capabilities, not raw role strings.

### 6) 404 vs 403 policy (finalized for MVP)
- **404 Not Found**: resource id exists but is outside caller tenant boundary, or caller has no facility scope to know resource exists.
- **403 Forbidden**: caller is in correct tenant/resource scope but lacks required capability (policy failure).
- **401 Unauthorized**: token invalid/missing/expired.

### 7) Facility-scoped RBAC in MVP vs Phase 2
- **Decision: facility-scoped RBAC is included in MVP for enforcement (not deferred)**.
- Source of truth: internal `UserFacilityAccess` table.
- Token roles provide coarse capability; facility allow-list provides final data-scope filter.

### 8) First implementation PR breakdown (backend only)
1. **PR A — Auth foundation and contracts**
   - Add JWT bearer authentication configuration and issuer/audience validation.
   - Introduce claim parsing + tenant context abstraction (`ITenantContext`).
   - Add policy registration for capability-based authorization.
2. **PR B — Authorization enforcement on report/review/photo/markup endpoints**
   - Apply policy attributes/handlers across backend endpoints/services.
   - Enforce tenant + facility scope checks in repository/service query paths.
   - Standardize 401/403/404 behavior contract for authz failures.
3. **PR C — Audit + security verification**
   - Add audit events for sensitive actions and denied authorization attempts.
   - Add integration tests for cross-tenant denial and facility-scope denial.
   - Add release-readiness evidence updates in `docs/release/RELEASE_READINESS.md`.

Out of scope for this decision record:
- No login UI, no IdP tenant bootstrap scripts, no EF migration in this doc-only follow-up PR.

## Tenant and organization model

### Core model
- Tenant = `ClientOrganization` (one tenant per client org).
- `Facility`, `ProcessUnit`, `Asset` stay tenant-owned via existing relationships.
- Each request has one effective tenant context.

### Rules
1. Every report belongs to exactly one tenant (`ClientOrganizationId` required).
2. Every report query must be tenant-scoped server-side; client filters are never trusted.
3. Every photo belongs to same tenant as its parent report.
4. Every photo markup belongs to same tenant as the referenced photo/report.
5. Export/document generation must enforce report tenant match.
6. Cross-tenant access attempts must be handled consistently (recommended default: **404 Not Found** to avoid resource enumeration; use 403 only when tenant is valid but role insufficient).

## User model

## Identity fields
- External identity key (OIDC `sub`) as canonical user identifier.
- Optional profile fields stored internally for audit readability (display name, email snapshot).

### Tenant-user persistence foundation (PR #199)
- Added backend `ClientOrganizationUser` persistence for tenant-scoped membership/seat accounting.
- Fields: `Id`, `ClientOrganizationId`, `ExternalSubject`, optional `Email`, optional `DisplayName`, `Status`, `CreatedAtUtc`, optional `UpdatedAtUtc`, optional `LastSeenAtUtc`.
- Allowed statuses for current seat accounting MVP: `active`, `invited`, `disabled`, `removed`.
- Seat counting rule for entitlement enforcement foundation: `active + invited` consume seats; `disabled + removed` do not.
- No public admin/user-management API is exposed in this phase; this is backend-only groundwork.

### Access mapping
- Tenant membership and facility scope enforced by internal access mapping (`UserFacilityAccess`) and/or claims.
- User may have different roles per tenant/facility.

## MVP roles and permissions

Minimum roles:
- **Owner/Admin**
  - Full tenant administration, user/role assignment, report CRUD, review override, export
- **Inspector**
  - Create/edit reports, findings, photos, submit for review
- **Reviewer**
  - Review queue, approve/return reports, annotate with review comments
- **ReadOnly/Client**
  - Read-only report and export access within tenant scope

Permission model recommendation:
- Define capabilities as policies (e.g., `reports.read`, `reports.write`, `reports.review`, `photos.write`, `exports.read`, `admin.users.manage`).
- Roles map to policies; endpoints authorize by policy, not raw role strings.

## Report and file ownership model

### Report ownership
- Required: `Report.ClientOrganizationId` and `Report.FacilityId`.
- Optional but recommended future additions in implementation phase:
  - `SubmittedByUserId`, `ReviewedByUserId`, `FinalizedAt` for richer accountability.

### Photo/file ownership
- `InspectionPhoto` remains report-owned (inherits tenant via report).
- For `PhotoMarkup`, future implementation should add direct ownership anchors (`ReportId` and/or `ClientOrganizationId`) or enforce verified join from `PhotoId -> Report -> Tenant` in all reads/writes.
- Blob/object storage paths should include tenant prefix (for implementation), e.g., `tenant/{tenantId}/reports/{reportId}/photos/{photoId}`.

## API authorization policy plan

### Reports APIs
- List/Get: `reports.read` + tenant scope + facility scope.
- Create/Update/Delete: `reports.write` + tenant/facility scope.
- Submit/transition status: `reports.submit`.

### Report drafts / drafting helpers
- Read/write drafts: `reports.write`.
- Draft visibility tenant-scoped.

### Photos / markups
- Read photos/markups: `photos.read` within report tenant scope.
- Upload/update markups: `photos.write`.
- Markup endpoints must verify referenced photo belongs to tenant-visible report.

### Review workflow
- Start review/approve/return: `reports.review`.
- Reviewer actions require tenant scope and, optionally, dual-control checks (future).

### Calculators
- If calculator endpoints are tenant-agnostic utility: require authenticated user but no report ownership.
- If calculator results are persisted to report context: require `reports.write` and tenant scope.

### Health checks
- `/health/live`: unauthenticated for infrastructure probes.
- `/health/ready`: either unauthenticated internal network only, or protected by infrastructure auth boundary.

### Swagger/dev tools
- Production: disable open anonymous swagger UI.
- Allow authenticated access to API docs for privileged roles only, or restrict to non-production.

## Audit log model

### Events to capture
- Report created
- Report updated
- Finding changed
- Photo uploaded
- Review submitted
- Report exported
- Calculation run (if/when calculator persistence or regulatory traceability demands it)
- Failed authorization attempt

### Proposed audit record shape
- `AuditEventId`
- `OccurredAtUtc`
- `TenantId`
- `ActorUserId` (external subject)
- `ActorDisplay` (optional snapshot)
- `Action` (enum/string)
- `ResourceType` (Report/Finding/Photo/Export/Auth)
- `ResourceId`
- `Result` (Success/Denied/Failed)
- `CorrelationId` / `TraceId`
- `ClientIp` (if policy allows)
- `UserAgent` (optional)
- `MetadataJson` (non-sensitive diff context)

Audit principles:
- Immutable append-only storage.
- Never store secrets/tokens in metadata.
- Retention and access policy documented per compliance requirements.

## Tenant isolation enforcement strategy

1. **Request context resolution**
   - Build an `ITenantContext` from validated token claims + optional route constraints.
2. **Repository guardrails**
   - Enforce tenant predicate in every report/photo/markup query path.
   - Prefer centralized query helpers/specifications to prevent missed filters.
3. **Write-time validation**
   - On create/update, verify target facility/process unit/asset belongs to tenant.
4. **Cross-tenant behavior contract**
   - Standardize response semantics (default 404 for foreign resource IDs).
5. **Export and background jobs**
   - Require explicit tenant context and include it in job payload.

## Migration and implementation strategy (future work)

No migration is added in this PR. Planned migration sequence:
1. Add identity/tenant linkage tables and constraints.
2. Backfill tenant/user ownership fields where nullable today.
3. Add indexes supporting tenant-first query patterns.
4. Add foreign-key/consistency constraints for ownership chain.
5. Roll out policy enforcement behind feature flag if needed.

## Test strategy

### Unit tests
- Policy matrix tests for each role/capability.
- Workflow authorization tests (submit/approve/return).

### Integration tests
- Tenant A cannot read/write Tenant B reports/photos/markups.
- List endpoints return only tenant-scoped records.
- Export endpoints deny/obscure cross-tenant IDs consistently.
- Failed authorization attempts produce audit entries.

### Regression/security tests
- IDOR tests for report/photo/markup IDs.
- Claim tampering/tenant mismatch scenarios.
- Consistency tests for 404 vs 403 contract.

## Rollout phases

### Phase 1
- Add tenant/user models and identity mapping
- Add auth provider integration (JWT bearer)
- Add tenant-scoped repository filters/enforcement
- Add auth + isolation tests

### Phase 2
- Add audit log persistence and querying
- Add role/policy enforcement breadth across API areas
- Add admin/user management APIs

### Phase 3
- Add marketplace/subscription linkage
- Add tenant plan/entitlement enforcement
- Add billing-related audit/compliance hooks

## Open decisions to finalize before implementation

1. Identity provider selection (Entra/Auth0/Okta/Cognito).
2. Canonical tenant claim name and token contract.
3. Exact 404 vs 403 policy for each endpoint class.
4. Audit retention window and export requirements.
5. Whether facility-scoped RBAC is mandatory in MVP or follows shortly after tenant-wide RBAC.

## PR A implementation notes (backend auth foundation)

Implemented in PR A:
- Added `Authentication:Enabled` feature flag (default `false`) and JWT bearer config contract under `Authentication:JwtBearer`.
- Added fail-fast validation for missing `Authentication:JwtBearer:Authority` and `Authentication:JwtBearer:Audience` when `Authentication:Enabled=true`.
- Added backend tenant-context contracts (`ITenantContext`, `TenantContext`, `ITenantContextAccessor`) and claims mapping service.
- Added role-to-capability mapping for MVP roles and capability policy registration constants.
- Added tenant context middleware that runs after authentication (when enabled) and captures unauthenticated defaults when disabled.

Config keys added:
- `Authentication:Enabled`
- `Authentication:JwtBearer:Authority`
- `Authentication:JwtBearer:Audience`
- `Authentication:JwtBearer:RequireHttpsMetadata`
- `Authentication:JwtBearer:RequiredTenantClaimName` (default `org_id`)
- `Authentication:JwtBearer:RequiredRolesClaimName` (default `roles`)

Not enforced in PR A:
- Broad endpoint policy enforcement.
- Repository-level tenant/facility filtering enforcement.
- Full 401/403/404 endpoint behavior rollout.

PR B remains responsible for endpoint enforcement and repository tenant/facility filtering.

## PR B Phase 1 (Report Paths)
- Implemented report-path tenant and capability checks in `ReportingController` using `ReportAccessGuard`.
- `Authentication:Enabled=false` keeps existing permissive local/dev behavior.
- Update hardening: report ownership fields (`Id`, `ClientOrganizationId`, `FacilityId`, `CreatedAt`, `CreatedByUserId`) are preserved from the persisted record during updates to prevent request-body ownership overwrite.
- Facility moves are intentionally blocked in PR B follow-up; moving a report between facilities requires a future dedicated endpoint with explicit source + target facility authorization.
- Deferred: deeper photo/markup ownership scoping beyond report-level checks.

## PR #190 photo/markup ownership enforcement

PR #190 completes dedicated photo/markup endpoint ownership enforcement through the owning report scope.

Covered in scope:
- `PhotoMarkupsController` (`GET /api/photos/{photoId}/markups`, `POST /api/photos/{photoId}/markups`) now resolves `photoId -> owning report` and evaluates access via `ReportAccessGuard`.
- Read behavior requires `photos.read`.
- Write behavior requires `photos.write`.
- Cross-tenant and cross-facility report ownership mismatches are hidden as `404`.
- In-scope requests without required capability return `403`.
- Unknown or orphan photo identifiers return `404`.
- `Authentication:Enabled=false` preserves permissive local/dev behavior.

Still intentionally unchanged in this phase:
- Health endpoints remain public.
- Calculator endpoints remain intentionally unprotected.
- Audit logging expansion remains follow-up work.


## PR #189 persisted report-instance authorization sweep

PR #189 completes authorization coverage for persisted `ReportingController` report-instance routes.

Covered in scope (persisted report resources):
- Create from template (`POST /api/reports/templates/{templateId}/instances`) -> `reports.write` with create ownership stamp and facility-scope hiding.
- Checklist build-draft when persistence occurs (`POST /api/reports/checklist/build-draft`) -> `reports.write` on create/update paths; persisted ownership fields preserved on update.
- Checklist finding sync (`POST /api/reports/instances/{id}/sync-findings`) -> `reports.write` with tenant/facility 404 hiding and ownership-field preservation.
- Review workflow transitions (`submit-for-review`, `start-review`, `approve`, `return-for-revision`) -> `reports.submit` / `reports.review` with tenant/facility 404 hiding.
- Review history (`GET /api/reports/{id}/review-history`) -> `reports.read` with tenant/facility 404 hiding.

Intentionally out of scope in this PR:
- Template metadata and checklist metadata endpoints that do not fetch/save persisted report resources.
- Pure draft transformation helpers that do not persist or fetch persisted reports.
- Alerts/assistant/rules/narrative utility endpoints.
- Calculator endpoints (remain intentionally unprotected in this phase).
- Health endpoints (remain public).

Facility moves remain blocked on update paths pending a dedicated future workflow.

## PR #191 audit accountability
- Added append-only backend audit events for report create/update/delete, review workflow transitions, export, photo markup create, and authorization denials.
- Health endpoints remain public.
- Calculator endpoints remain intentionally unprotected and are not audited in this phase.
- Audit metadata is sanitized to exclude sensitive values (tokens, authorization headers, passwords, connection strings, raw payloads, markup JSON).
- Audit UI/query/admin tooling remains future work.

- PR #192 hardens backend audit metadata sanitization (recursive sensitive-key stripping, bounded metadata, truncation fallback).
- AuditEvents are append-only at DbContext save boundaries (update/delete blocked).
- Internal tenant-scoped audit query service added for future tooling; no public audit UI/API exposure in this phase.
- Sensitive metadata handling now excludes tokens/auth headers/passwords/connection strings/raw payloads/stack traces/markup JSON.
- Health endpoints remain public.
- Calculator endpoints remain intentionally unprotected and not audited in this phase.
- Audit UI/admin tooling remains future work.

## PR #193 — Persisted report ownership-reference consistency
- Enforces persisted report write-time consistency checks across `ClientOrganizationId`, `FacilityId`, `ProcessUnitId`, and `AssetId`.
- Preserves blocked facility moves; ownership rewrite protections remain in effect.
- Keeps health endpoints public.
- Calculator endpoints remain intentionally unprotected and out of audit scope in this phase.
- Adds no audit/admin/subscription UI or new UI/API surfaces.

## PR #194 Subscription/Entitlement Foundation
- Adds backend-only subscription plan, client subscription, and entitlement data model foundation.
- No billing provider integration in this phase.
- No UI and no public subscription/admin APIs in this phase.
- No endpoint entitlement enforcement in this phase.
- Health endpoints remain public.
- Calculator endpoints remain intentionally unprotected and not entitlement-gated in this phase.

## PR #195 Entitlement Enforcement Guardrails
- Adds feature-flagged backend entitlement enforcement via `Subscriptions:EnforcementEnabled` (default `false`).
- First enforced operations: report create/export, photo markup create, internal audit query service.
- Calculator endpoints remain intentionally ungated in this phase.
- Health endpoints remain public.
- No billing provider integration, UI, or public subscription/admin APIs in this phase.

## PR #196 backend subscription limit hardening
- After existing auth/capability checks and `reports.create` entitlement checks pass, persisted report creation now evaluates `max.activeReports` when subscription enforcement is enabled.
- Limit denials return 403 (`subscription_limit_exceeded`) and emit a safe `EntitlementLimitDenied` audit event with route/reason/activeCount/limitValue only.
- Calculator endpoints remain intentionally unprotected by subscription entitlement checks, and health endpoints remain public.
