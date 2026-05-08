# Role Navigation Architecture and Workflow Hierarchy (Planning)

## Scope and intent
This document defines **planning-level** product/UI architecture for role-aware navigation and workflow visibility.

- This is **not** implementation of new dashboard screens.
- This is **not** implementation of new NDE Request Log, report timeline, or asset activity pages.
- This is **not** a backend status, workflow, or API change.
- This is **not** subscription/admin UI implementation.

The goal is to lock role hierarchy and navigation direction before incremental UI delivery.

## Workflow tracks

### 1) NDE workflow track
1. Client submits NDE request.
2. Owner reviews/approves request.
3. NDE is scheduled/performed.
4. NDE report/results are submitted.
5. Owner reviews/approves NDE report.
6. Client searches, downloads, and bulk exports approved NDE reports.

### 2) API inspection workflow track
1. Inspector creates and maintains ongoing API inspection/report log.
2. Workflow moves through internal report statuses.
3. Finalized reports become searchable/downloadable per role scope.
4. Client request trigger is not required for normal API visual inspections.

## Role hierarchy and access model

### Common security boundaries (all roles)
- Tenant/facility scope applies to every role.
- No cross-tenant visibility.
- No exposure of secrets, raw provider payloads, or internal infra configuration in UI.

### 1) Client / Requestor
**Acts on**
- Submitting NDE requests.
- Tracking own/open requests.
- Downloading approved/final reports.

**Sees first**
- NDE Request Log summary.
- My/Open requests.
- Reports ready to download.
- Approved NDE reports.
- API inspection report library.
- Bulk export actions.

**Can access**
- Submit NDE requests.
- View own NDE request statuses.
- Search/download approved NDE reports.
- Search/download final API inspection reports.
- Filter requests/reports and bulk export approved reports.

**Should not see**
- Draft or unapproved NDE reports.
- Internal owner notes, audit/security details, or entitlement internals.
- Other clients, facilities, or tenants outside assignment.
- Backend readiness/configuration, billing-provider internals, raw payloads, secrets/tokens.
- Internal cancellation notes unless explicitly client-visible.

**Primary dashboard queues**
- My NDE Requests
- Pending Owner Approval
- Approved/Scheduled
- In Progress
- Results Received
- Approved Reports
- Cancelled
- Bulk Export

**Left navigation**
- Dashboard
- NDE Requests
- Reports
- Downloads
- Assets
- Help

### 2) Owner / Approver
**Acts on**
- Request approval decisions.
- NDE report/result approval decisions.
- Priority/overdue bottleneck review.

**Sees first**
- Pending request approvals.
- Reports awaiting approval.
- High priority, overdue, results-received-not-reviewed, cancelled.
- Facility/asset status summary.

**Can access**
- Approve/reject/return NDE requests.
- Approve/return NDE reports.
- Search, download, and export reports.

**Should not see**
- Other tenants/clients.
- System-owner diagnostics.
- Raw secrets, billing/provider payloads, DB config, developer debug internals.
- User/session security details unless combined with Admin role.

**Primary dashboard queues**
- NDE Requests Awaiting Approval
- NDE Reports Awaiting Review
- Urgent/High Priority
- Overdue
- Results Received
- Cancelled
- Facility/Asset Status Summary

**Left navigation**
- Dashboard
- Approval Queue
- NDE Requests
- NDE Reports
- API Inspection Log
- Assets
- Reports
- Exports

### 3) Inspector
**Acts on**
- Authoring/editing API inspection reports.
- Managing running report log.
- Handling returned reports.

**Sees first**
- My drafts.
- Returned reports.
- Reports needing action.
- Recent asset activity.
- Linked NDE requests.

**Can access**
- Create/edit API reports.
- Submit reports (where workflow applies).
- Request NDE from findings (policy-permitted contexts).
- See own drafts/returned reports and scoped logs.

**Should not see**
- User/admin management and subscription settings.
- Out-of-scope facility data.
- Owner-only queues unless dual role assignment exists.

**Primary dashboard queues**
- My Draft Reports
- Returned Reports
- Reports Needing Action
- Recently Updated Assets
- Linked NDE Requests

**Left navigation**
- Dashboard
- My Reports
- API Inspection Log
- NDE Requests
- Assets
- Returned Reports

### 4) NDE Coordinator
**Acts on**
- Scheduling and progressing approved NDE work.
- Recording results receipt and closure/cancellation where permitted.

**Sees first**
- Approved/new requests.
- Scheduled, in-progress, overdue, results-received, cancelled/closed.

**Can access**
- View approved requests.
- Schedule and mark in progress.
- Record results received.
- Close/cancel (policy-dependent).
- Track due dates and priorities.

**Should not see**
- Owner-only decisions except operationally necessary context.
- Subscription/admin settings.
- Cross-tenant or out-of-scope facility data.
- Internal security/audit internals.

**Primary dashboard queues**
- New Approved Requests
- Scheduled
- In Progress
- Results Received
- Overdue
- Cancelled
- By NDE Type

**Left navigation**
- Dashboard
- NDE Requests
- Schedule
- Results Received
- Overdue
- Cancelled
- Assets

### 5) Admin
**Acts on**
- Tenant-level access and configuration governance.

**Sees first**
- Users/seats.
- Facility access.
- Custom NDE disciplines.
- Subscription/stress-test state (future).
- Audit/activity overview.

**Can access**
- Users/access, facilities, custom NDE types.
- Tenant settings and activity/audit views.
- Scoped report/request oversight.

**Should not see**
- Raw secrets/tokens/connection strings.
- Provider payload internals.
- Cross-tenant data unless also Internal/System Owner.

**Primary dashboard queues**
- Active Users
- Seat Usage
- Open Reports
- Open NDE Requests
- Overdue NDE
- Custom NDE Types
- Audit/Activity
- Stress-Test/Subscription Status

**Left navigation**
- Dashboard
- Users & Access
- Facilities
- Assets
- Custom NDE Types
- Reports
- NDE Requests
- Audit Log
- Settings

### 6) Viewer / Read-only
**Acts on**
- Read-only report and asset history consumption.

**Sees first**
- Permitted final/approved report library.
- Permitted asset history.

**Can access**
- Search/view permitted reports.
- Download permitted reports.

**Should not see**
- Draft/edit workflows.
- Approval controls.
- Admin settings.
- Internal audit/security internals.

**Primary dashboard queues**
- Recently Finalized Reports
- Report Search Shortcuts
- Asset History Snapshot

**Left navigation**
- Dashboard
- Reports
- Assets
- Downloads (if tenant policy allows)

### 7) Internal/System Owner (internal only)
**Acts on**
- Internal operations readiness and stress-test setup oversight.

**Sees first**
- Operational readiness summary.
- Stress-test readiness context.

**Can access**
- Internal setup/readiness concepts.
- Stress-test tooling pathways.
- Backend operational documentation references.

**Should not see (in customer-facing persona)**
- Customer tenant data beyond authorized internal support scope.

**Primary dashboard queues**
- Internal Readiness
- Environment Health (non-secret indicators)
- Stress-Test Run Status

**Left navigation**
- Internal Dashboard
- Readiness
- Operations Docs
- Stress-Test Tooling

## NDE Request Log (future UI design contract)

### Columns
- Priority
- Status
- Request #
- NDE Type
- Asset
- Facility / Unit
- Requested By
- Requested Date
- Due Date
- Owner Approval
- NDE Report Status
- Last Activity
- Actions

### Global quick filters
- All
- Pending Approval
- Approved
- Scheduled
- In Progress
- Results Received
- Awaiting Owner Review
- Closed
- Cancelled
- Overdue
- Urgent

### Role quick-filter presets
**Client**
- My Requests
- Needs Clarification
- Approved Reports
- Ready to Download
- Cancelled

**Owner**
- Needs My Approval
- Urgent
- Overdue
- Results Received
- Awaiting NDE Report Approval
- Cancelled

## Report library architecture (future UI design contract)

### Library split
- API Inspection Reports
- NDE Reports
- Final Reports
- Bulk Export
- Draft/Internal reports (internal roles only)

### Search/filter fields
- Asset
- Facility
- Date range
- Report type
- NDE type
- Status
- Report number
- API code

### Visibility rules
- Client: approved/final only.
- Owner: approval-required + finalized.
- Inspector: own drafts/returned + permitted logs.
- Admin: tenant/facility scoped reports.
- All roles: no cross-tenant visibility.

## Status display models (UI mapping only)

### NDE display statuses
- Draft
- Submitted
- Pending Owner Approval
- Approved
- Scheduled
- In Progress
- Results Received
- NDE Report Under Review
- NDE Report Approved
- Closed
- Cancelled
- Returned for Clarification

> Note: This is a UI planning model only; backend status behavior is unchanged by this document.

### API inspection display statuses
- Draft
- Submitted
- In Review
- Returned
- Final

## Implementation boundaries for follow-on PRs
- Build navigation and dashboards incrementally behind role-aware routing.
- Keep approval and workflow enforcement in backend authorization/workflow services.
- Treat this document as the contract for upcoming UI tickets, not as a direct feature release by itself.

## PR #210 Update
- Backend-only role-aware read models were added for future navigation targets defined in #209.
- No UI components were added in this change.
- Client visibility remains final/approved-only for reports; owner/admin visibility remains tenant/facility scoped.

## PR #211 implementation note: frontend app shell foundation

PR #211 implements the first frontend role-aware app shell and left navigation foundation.

- Adds a top bar, left navigation, grouped role-aware menu metadata, and main content region scaffolding.
- Adds minimal placeholder route targets only where needed to support safe navigation.
- Does **not** implement full dashboard feature screens.
- Does **not** implement full NDE Request Log UI.
- Does **not** implement full Report Timeline UI.
- Does **not** implement full Asset Activity UI.

### Security and authorization reminder
Frontend role visibility is a usability convenience and **not** security enforcement.
Authoritative access control remains backend authentication plus tenant/facility authorization.


## PR #212 testing note: role-aware navigation/app shell test foundation

PR #212 adds a frontend test foundation focused on role-aware navigation metadata and app shell rendering.

- Coverage targets role navigation visibility rules per role, temporary role normalization behavior, and shell/navigation rendering contracts.
- No new workflow feature screens are implemented in this PR.
- Frontend role visibility remains UI convenience only; backend authorization remains authoritative.

## Report creation hierarchy
- API 570 Piping
  - External Visual
  - CUI External
- API 510 Pressure Vessel
  - External Visual
  - Internal Visual
  - Shell & Tube Exchanger
  - Hairpin Exchanger
  - Plate & Frame Exchanger
  - Towers
  - Drums
- API 653 Tanks
  - External
  - Internal
  - Floor
  - Shell
  - Roof
- NDE Reports
  - UT Thickness
  - UTT Grid
  - UTSW
  - PAUT
  - PT / WFPT
  - MT / WFMT
  - RT
  - PMI
  - Hardness
  - Boroscope
  - ETC
  - IRIS
  - Guided Wave UT
  - Other / Custom

- Supplemental Inspections
  - Soil-to-Air Interface (SAI)
  - Injection Point Inspection
  - Mix Point Inspection
  - HF Alky Flange Inspection
  - CUI Inspection
  - CUF Inspection
  - Dead Leg Inspection
  - Corrosion Under Pipe Supports (CUPS)
  - Buried Piping / Transition Inspection
  - Small-Bore Connection Inspection
  - Flange / Bolting / Gasket Inspection
  - Coating / External Corrosion Inspection
  - Fireproofing Interface Inspection
  - Insulation Penetration Inspection
  - Vibration / Fatigue Follow-up
  - Weld / HAZ Follow-up
  - Other / Custom Supplemental Inspection

### Supplemental inspection planning note
- These are placeholders/planning categories only in this PR.
- Do not build these report screens yet.
- Do not add backend models for these yet.
- These should appear in future Create Report flow and report-workspace tooling, not as a flat global left-nav list.

## Report-authoring tool panel direction
- Left navigation remains the primary app navigation.
- Report-authoring tools should live inside the report workspace rather than global navigation.
- Tools should be context-sensitive based on report type.
- API 570 report workspace tool examples:
  - B31.3 thickness check
  - Corrosion rate
  - Remaining life
  - NDE request insertion
  - Thickness grid
  - Damage mechanism lookup
- API 510 report workspace tool examples:
  - Pressure vessel calculations
  - PCC-1 criteria
  - Nozzle/head/shell checks
  - Exchanger-specific sections

## Global navigation guidance
- Do not use a global left-nav "Report Actions" group for generic Create/View/Edit links.
- Create/View/Edit should exist as actions within report listing/detail/workspace screens.


## Common Supplemental Report Tools
- Corrosion Rate
- Remaining Life
- Tmin / Required Thickness Check
- Thickness Trend / Grid Review
- Damage Mechanism Lookup
- NDE Request Insertion
- Photo Markup Insertion
- Finding / Recommendation Insertion

### Common supplemental tools planning note
- Corrosion Rate is applicable across equipment types, not only API 570 piping.
- Common supplemental report tools should be available in future report workspaces where relevant.
- These are planning/tooling placeholders only in this PR.
- Do not build these tools/screens in this PR.
- Do not change calculator/formula code in this PR.

## Engineering tools organization direction
- Engineering Tools in the global left nav is only a high-level entry point.
- The full engineering tool library is future work and is not implemented in this PR.
- Tools such as API 653 calculations, corrosion rate, PCC-1 criteria, B31.3, pressure vessel calculations, damage mechanisms, and report-specific insert/check tools should be organized into:
  1. Engineering Tools library
  2. Context-sensitive report workspace tool panel
- Do not add all tools as a flat global left-nav list.
