# Report workflow and NDE request foundation (PR #204)

PR #204 adds backend-only Report Log and NDE Request Log foundation.

- No UI was added.
- No public vendor workflow was added.
- No email sending was added.
- No automatic findings creation was added.
- ReportLogEntry is business workflow history and not a replacement for AuditEvent.
- NDE requests are tenant/facility scoped and may link to reports/assets.
