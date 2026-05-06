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
