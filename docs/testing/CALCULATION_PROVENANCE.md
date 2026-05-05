# Calculation Provenance Map

This document captures where current regression baselines come from and what evidence is still pending formal engineering sign-off.

## 1) B31.3 material stress / thickness

### Baseline policy
- Treat ASME **A** and **SA** specs as separate lookups.
- `SA106` must **not** silently map to `A106` when querying B31.3 materials.
- Any alias behavior change requires explicit provenance and test updates.

### Current canonical baseline (A106 Grade B / UNS K03006)
- Allowable stress at **500°F**: **19.0 ksi**.
- Allowable stress at **600°F**: **17.9 ksi**.
- Linear interpolation at **579°F**: approximately **18.131 ksi** (**18,131 psi**).
- Current NPS 2 thickness regression baseline expected result: **0.0130 in**.

### Current source in repository
- B31.3 piping stress records are imported/seeded through backend stress importer content and repository-backed lookup paths in:
  - `iE.Core/MaterialStress/Importers/NewStressDataImporter_Batch002.cs`
  - `iE.Core/Mechanical/B313/Materials/B313MaterialStressRepository.cs`
  - importer utility entrypoint: `iE.Tools.StressImporter/Program.cs`
- Until a fully curated engineering source package is attached, these values are treated as **current regression baseline; reference pending review**.

## 2) Pipe lookup
- Pipe lookup golden cases should capture exact schedule/nominal size lookup expectations, including units.
- If external handbook/table provenance is missing, mark as **current regression baseline; reference pending review**.
- Any later table corrections must include source-note updates and reviewer sign-off.

## 3) Corrosion rate / remaining life
- Corrosion remaining-life cases must include formula context, units, and tolerance explicitly.
- When source worksheet/standard clause is unavailable, use baseline lock with pending-reference marker.
- Do not silently alter expected corrosion outputs without provenance note and reviewer approval.

## 4) Pressure vessel shell/head/nozzle
- Pressure vessel cases should identify geometry class (shell/head/nozzle), applicable standard edition, and calculation assumptions.
- Baseline-only expectations are allowed for scaffolding, but must be labeled pending review.
- Any engineering-logic change requires both test updates and provenance delta documentation.

## 5) API 653 MRT (placeholder/deferred)
- API 653 MRT expected values are intentionally deferred.
- No API 653 MRT golden expected output should be considered authoritative until a dedicated provenance package and reviewer approval exists.
