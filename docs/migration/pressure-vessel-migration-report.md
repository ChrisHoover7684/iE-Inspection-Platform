# Pressure Vessel Migration Report (intermediate)

Status: **Not ready to merge**. This report captures what is missing before behavior-preserving migration is complete.

## Source received so far
- `PressureVesselCalcs.cs` (WinForms coordinator)
- `CylindricalControl.cs`
- `SphericalControl.cs`
- `ConicalControl.cs`
- `HeadCalculators.cs`
- `NozzleControl.cs`
- `HeadControl.cs` (Part 1 + Part 2)

## What can be extracted with confidence now
From the provided files, only the following calculation logic is explicit:

1. **Displayed shell/head/nozzle formulas in `PressureVesselCalcs.cs`**
   - Cylindrical shell UG-27(c)(1): `t = (P*R)/(S*E - 0.6P)`
   - Cylindrical shell UG-27(c)(2): `t = (P*R)/(2SE + 0.4P)`
   - Spherical shell UG-27(d): `t = (P*R)/(2SE - 0.2P)`
   - Conical UG-32(g): `t = (P*D)/(2*cos(a)*(SE - 0.6P))`
   - Head equations shown for UG-32(d), UG-32(f), UG-32(e), UG-32(g), UG-34
   - Nozzle combination logic shown for `t_a`, `t_b1`, `t_b2`, `t_b3`, `t_b`

2. **Cylindrical control explicit behavior in `CylindricalControl.cs`**
   - Radius derivation preference: use `ID / 2`, else `(OD - 2t)/2`
   - Joint efficiency fallback to `0.85` when non-positive
   - Input validation/auto-compute behavior for ID/OD/thickness
   - Delegation call: `ShellCalculators.CylindricalMinimum(...)`

3. **Spherical control explicit behavior in `SphericalControl.cs`**
   - Inside radius derivation preference: use `ID / 2`, else `(OD - 2t)/2`
   - Joint efficiency fallback to `1.0` when non-positive
   - Input validation/auto-compute behavior for ID/OD/thickness
   - Delegation call: `ShellCalculators.Spherical(...)`

4. **Conical control explicit behavior in `ConicalControl.cs`**

5. **Head calculator explicit behavior in `HeadCalculators.cs`**
   - Formula methods for Ellipsoidal, Hemispherical, Torispherical, Conical/Toriconical, and Flat heads
   - Input validation and denominator/radicand guards
   - Validity check helpers for hemispherical and torispherical heads


6. **Nozzle control explicit behavior in `NozzleControl.cs`**
   - Nozzle selector modes: NPS / LWN / Custom with different auto-population behavior
   - Default geometry source for NPS: schedule-based OD/thickness, with ID computed in library
   - Default geometry source for LWN: rating-based thickness, ID from NPS, OD = ID + 2t
   - `GetGeometryInput()` sets `UseODForTMin=true` and maps nominal thickness to both nominal and original thickness

7. **Head control explicit behavior in `HeadControl.cs`**
   - Geometry resolution requires exactly two of ID/OD/thickness to auto-compute the third
   - Geometry consistency tolerance check: `|OD - (ID + 2t)| <= 0.001`
   - Torispherical ASME F&D defaults: crown radius = OD, knuckle radius = 0.06*OD
   - Flat-head default C factors: integral=0.30, bolted=0.33, loose=0.50

## Referenced methods/types that are still missing
The coordinator references the following calculation-bearing dependencies not yet provided:

- `NozzleControl`
  - `GetGeometryInput()` defaults confirmed (UseODForTMin=true, UseIDForTMin=false; nominal/original thickness mapping)
  - NPS/LWN geometry library behavior confirmed (auto-population from schedule/rating)
- `UG45Table`
  - minimum-thickness lookup source used by `NozzleControl.GetGeometryInput()` still needed for full parity
- `NozzleCalcInput`
  - authoritative full input contract still needed to confirm all fields/defaults
- `NozzleCalcResult`
  - authoritative full result contract and text/warnings fields still needed for parity

## Additional source files required next
To complete a formula-accurate migration, the following are required:

1. `NozzleCalcResult.cs`
2. `NozzleCalcInput.cs` (or equivalent definition)
3. `UG45Table.cs`

## Migration constraints being followed
- No WinForms UI/designer/event-handler code is copied into `iE.Core`.
- No duplicate material-stress lookup logic is introduced.
- Migration is marked incomplete until all required source files are identified and reviewed.
