# Golden Test Foundation for Engineering Calculations

This document defines a safe structure for future golden tests without inventing unvalidated engineering expected values.

## Goal
Golden tests should lock verified calculator behavior to trusted references while making units, tolerances, and code editions explicit.

## Required Golden Case Structure
Each case should include:
- **Input JSON**: complete request inputs used by calculator/service.
- **Expected output JSON**: approved expected result payload.
- **Units**: explicit unit system and per-field units where relevant.
- **Code/standard edition**: e.g., ASME/API edition/year used for expected value derivation.
- **Source/reference note**: citation to hand calc, approved worksheet, or published reference.
- **Tolerance rules**: absolute/relative tolerance and rounding rules.
- **Review/approval status**: who approved and when.

## File/Folder Guidance
Suggested domain folders under `iE.Tests/GoldenCases/`:
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
- Do **not** add placeholder expected numeric outputs.
- Do **not** merge golden cases without reference metadata.
- Treat changes to golden expected outputs as high-risk and require engineering review.
