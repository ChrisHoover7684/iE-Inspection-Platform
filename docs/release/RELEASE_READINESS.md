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
- [ ] Required environment variables documented.
- [ ] Connection strings and secrets resolved via secure config paths.

## 5) Database migrations
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
- [ ] Access checks verified for modified endpoints/services.
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
