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
