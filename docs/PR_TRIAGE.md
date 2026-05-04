# PR Triage Plan

Before adding major calculator features, reconcile existing open pull requests to reduce merge risk and duplicated engineering logic changes.

## Why this comes first
- Open PR drift increases risk of conflicting calculator logic.
- CI/build status should be clear before adding new behavior.
- Early triage prevents rework and unclear ownership.

## Open PR Inventory (as of 2026-05-04)
> Repository access via GitHub API/CLI was not available in this environment during this pass. Fill in actual PR numbers/titles in the table below when network/CLI access is available.

| PR | Title | Purpose | Overlap with `master` | Build/Test Status | Recommendation (merge/rebase/close) | Risk |
|---|---|---|---|---|---|---|
| TBD | TBD | TBD | TBD | TBD | TBD | TBD |

## Per-PR Checklist
For each open PR, document:
1. **Purpose** — What problem is it solving?
2. **Overlap with `master`** — Which files/domains overlap current branch and other PRs?
3. **Build/test status** — Current CI result, local reproducibility, failing tests summary.
4. **Recommendation** — Merge as-is, rebase/update, split, or close.
5. **Risk level** — Low/Medium/High with rationale.
