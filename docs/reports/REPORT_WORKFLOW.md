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
