# Golden Test Foundation for Engineering Calculations

This document defines a safe structure for future golden tests without inventing unvalidated engineering expected values.

## Goal
Golden tests should lock verified calculator behavior to trusted references while making units, tolerances, and code editions explicit.

## Golden Case Record Format
Each golden case must include:
- **Case ID**: stable identifier (example: `B313/A106_B_K03006_579F`).
- **Calculator/module**: backend module or service under test.
- **Input JSON**: complete request payload.
- **Expected output JSON**: approved expected result payload.
- **Units**: unit system and per-field units.
- **Source/provenance note**: where expected behavior came from.
- **Tolerance**: absolute/relative tolerance and rounding expectations.
- **Reviewer/approval status**: approver name/date or pending marker.
- **Code/standard edition**: known edition/year or `unknown`.
- **Known limitations**: caveats, pending references, interpolation assumptions, etc.

## File/Folder Guidance
Domain folders under `iE.Tests/GoldenCases/`:
- `PressureVessels/`
- `B313/`
- `Api653/`
- `PipeLookup/`
- `Corrosion/`
- `Nozzles/`

Recommended per-case files:
- `case-name.input.json`
- `case-name.expected.json`
- `case-name.meta.json`

## Governance Rules
- Do **not** add placeholder expected numeric outputs as authoritative values.
- Do **not** merge golden cases without provenance metadata.
- Treat changes to golden expected outputs as high-risk and require engineering review.
- Where provenance is incomplete, label the case as **current regression baseline; reference pending review**.
