# Pressure Vessel Migration Report (intermediate)

Status: **Not ready to merge**. This report captures what is missing before behavior-preserving migration is complete.

## Source received so far
- `PressureVesselCalcs.cs` (WinForms coordinator)
- `ConicalControl.cs`

## What can be extracted with confidence now
From the provided files, only the following calculation logic is explicit:

1. **Displayed shell/head/nozzle formulas in `PressureVesselCalcs.cs`**
   - Cylindrical shell UG-27(c)(1): `t = (P*R)/(S*E - 0.6P)`
   - Cylindrical shell UG-27(c)(2): `t = (P*R)/(2SE + 0.4P)`
   - Spherical shell UG-27(d): `t = (P*R)/(2SE - 0.2P)`
   - Conical UG-32(g): `t = (P*D)/(2*cos(a)*(SE - 0.6P))`
   - Head equations shown for UG-32(d), UG-32(f), UG-32(e), UG-32(g), UG-34
   - Nozzle combination logic shown for `t_a`, `t_b1`, `t_b2`, `t_b3`, `t_b`

2. **Conical control explicit behavior in `ConicalControl.cs`**
   - Diameter derivation preference: use ID, else `OD - 2t`
   - Input validation bounds for angle and efficiency
   - Delegation call: `ShellCalculators.Conical(...)`

## Referenced methods/types that are still missing
The coordinator references the following calculation-bearing dependencies not yet provided:

- `CylindricalControl`
  - `ValidateInputs(...)`
  - `CalculateWithBoth(...)`
  - `GetRadius()`, `GetCorrosionAllowance()`, `GetOriginalThickness()`, `GetJointEfficiency()`
- `SphericalControl`
  - `ValidateInputs(...)`
  - `Calculate(...)`
  - `GetInsideRadius()`, `GetInsideDiameter()`, `GetCorrosionAllowance()`, `GetOriginalThickness()`, `GetJointEfficiency()`
- `ConicalControl`
  - Uses `ShellCalculators.Conical(...)` (calculator implementation still missing)
- `HeadControl`
  - `ValidateInputs(...)`
  - `Calculate(...)`
  - `GetHeadType()`, `GetJointEfficiency()`, `GetEffectiveInsideDiameter()`, `GetEffectiveInsideRadius()`, `GetTorisphericalCrownRadius()`, `GetConicalHalfApexAngle()`, `GetToriconicalHalfApexAngle()`, `GetFlatCFactor()`, `GetFlatEdgeCondition()`, `GetCorrosionAllowance()`, `GetOriginalThickness()`
- `NozzleControl`
  - `GetGeometryInput()` and geometry defaults/normalization behavior
- `NozzleCalculators`
  - `Calculate(NozzleCalcInput)` implementation
- `NozzleCalcResult`
  - authoritative meaning/calculation of fields (especially `RequiredThicknessPlusCA` and acceptability)

## Additional source files required next
To complete a formula-accurate migration, the following are required:

1. `CylindricalControl.cs`
2. `SphericalControl.cs`
3. `HeadControl.cs`
4. `NozzleControl.cs`
5. `NozzleCalculators.cs`
6. `NozzleCalcResult.cs`
7. If referenced by those files: `ShellCalculators.cs` and any helper calculator classes

## Migration constraints being followed
- No WinForms UI/designer/event-handler code is copied into `iE.Core`.
- No duplicate material-stress lookup logic is introduced.
- Migration is marked incomplete until all required source files are identified and reviewed.
