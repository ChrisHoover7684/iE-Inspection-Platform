# PR Triage Recommendations

This document records recommended disposition for known stale or overlapping PRs after the B31.3 CI stabilization work landed in PR #177.

## Scope and guardrails
- Do not merge stale PR code blindly.
- Preserve current blocking CI posture and current backend calculation baselines.
- Re-open behavior work only through fresh, scoped backend PRs with provenance and golden-case coverage.

## Recommended disposition

| PR | Current assessment | Recommendation | Rationale |
|---|---|---|---|
| #173 | Overlaps the stabilization stream that continued in #174/#176/#177. | **Close** as superseded. | Keeping #173 open increases review noise and merge-conflict risk for no net backend benefit. |
| #157 | Older pressure-vessel line of work; likely partially superseded. | **Defer merge; review for unique migration notes only**. | If migration or rollout notes are unique, extract docs-only content into a new targeted PR; otherwise close. |
| #158 | Older pressure-vessel line of work; likely partially superseded. | **Defer merge; review for unique migration notes only**. | Same as #157; do not merge code as-is into stabilized baseline. |
| #153 | Corrosion/calculator overlap with later backend work. | **Close as-is; reopen missing behavior via fresh scoped PR(s)**. | Avoid importing stale assumptions; reintroduce only validated missing backend behavior with tests/provenance. |
| #154 | Corrosion/calculator overlap with later backend work. | **Close as-is; reopen missing behavior via fresh scoped PR(s)**. | Same disposition as #153. |
| #55 | Sample report JSON contribution, low risk. | **Defer (nice-to-have)**. | Safe candidate later, but not release-critical versus current backend hardening tasks. |
| #4 | API 653 MRT work. | **Defer until provenance/golden process is active**. | High calculation risk area; should enter only after golden-case + provenance workflow is in place. |

## Follow-up actions
1. Use this triage as the canonical stale-PR disposition reference for current hardening work.
2. For any behavior believed missing from #153/#154/#157/#158, open a new backend-only PR with:
   - explicit issue statement,
   - provenance references,
   - golden-case artifact(s),
   - passing blocking CI.
