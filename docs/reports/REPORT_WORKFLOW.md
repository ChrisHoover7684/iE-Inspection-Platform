# Report workflow and NDE request foundation (PR #204)

PR #204 is backend-only.

- No UI was added.
- No email sending was added.
- No public vendor workflow was added.
- No automatic findings creation was added.
- ReportLogEntry is business workflow history, not a replacement for AuditEvent.
- NDE requests are tenant/facility scoped and may link to reports/assets.
- Built-in NDE types include UTSW, PAUT, WFMT, WFPT, ETC, IRIS, Guided Wave UT, and UTT Grid.
- Future UI should support status chips, queues, saved filters, linked report/asset details, and side-panel timelines beyond spreadsheet-style screens.
- Backend foundation now includes tenant-custom NDE type definitions (no public UI/API in this PR).

## PR #205 migration + test hardening update

- PR #205 adds the missing database migration for the PR #204 workflow models (`ReportLogEntries`, `NdeRequests`, `NdeRequestTypeDefinitions`).
- Report Log and NDE Request Log remain backend-only.
- No UI was added.
- No email/vendor workflow was added.
- No automatic findings creation was added.
- Built-in NDE types include UTSW, PAUT, WFMT, WFPT, ETC, IRIS, Guided Wave UT, and UTT Grid.
- Tenant-custom NDE type foundation exists, but public UI/API management remains future work.

- NDE requests support a user-facing "Cancelled" action (internal status code remains `canceled`).
- Cancelled is terminal and kills request workflow transitions; the request is preserved (not deleted) for traceability.
- No UI was added.
- No vendor workflow/email was added.
- No automatic findings creation was added.


## PR #206 report workflow timeline wiring
- Wired `ReportLogEntry` writes into existing report lifecycle backend actions (create, create-from-template, update, submit/start/approve/return review, and status changes).
- `ReportLogEntry` remains business workflow history and does not replace `AuditEvent`.
- No UI, email/vendor workflow, or automatic findings generation was added.
- NDE `Canceled` remains terminal and traceability is preserved.
- Future UI timeline/status history can query report log entries.

## PR #208 frontend layout foundation cleanup
- PR #208 is CSS/layout cleanup only for the existing API 570 report entry page foundation.
- No new UI feature was added.
- Future UI work will build on this foundation for Report Log, NDE Request Log, NDE queues, report timeline, and asset activity views.

## PR #209 role-aware navigation architecture planning reference
- PR #209 defines product/UI planning guidance for role hierarchy, left-navigation structure, role-scoped dashboard queues, and report/request visibility.
- This is documentation/navigation metadata planning only and does not change backend workflow services or status enforcement.
- See `docs/product/ROLE_NAVIGATION_ARCHITECTURE.md` for the role matrix and future UI contracts for NDE Request Log and report library behavior.

## PR #210 Read Model Layer
- Backend read models added for report workflow timeline, report library, NDE request summaries, owner approval queue, client NDE log, and asset activity.
- No frontend workflow screens are introduced in this PR.
- API inspections remain running-log/report-library oriented and NDE workflow remains request/approval/report-review oriented.
