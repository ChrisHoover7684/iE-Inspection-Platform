# iE Inspection Platform — Repository Agent Guide

## Purpose
This repository hosts an inspection and mechanical integrity platform with a .NET backend/core and React/Vite frontend. It includes engineering calculators, inspection reporting services, and related tooling.

## Main Layout
- `iE.Api/` — ASP.NET Core API host, controllers, DI, EF Core setup.
- `iE.Core/` — core domain logic and engineering/report services.
- `iE.Tests/` — xUnit tests for mechanical and report behavior.
- `iE.Web/` — React + TypeScript + Vite frontend.
- `iE.Tools.StressImporter/` — utility/tooling project for stress data.
- `docs/` — migration notes and project documentation.
- `docker-compose.yml` — local PostgreSQL support for development.

## Build and Test Commands
From repository root:
- `dotnet restore iE.Api/iE.Api.sln`
- `dotnet build iE.Api/iE.Api.sln --configuration Release --no-restore`
- `dotnet test iE.Api/iE.Api.sln --configuration Release --no-build`

## Frontend Commands
From `iE.Web/`:
- `npm ci`
- `npm run dev`
- `npm run build`
- `npm run preview`

## Coding Expectations
- Keep changes small and reviewable.
- Prefer existing project patterns and folder structure over introducing new architecture.
- Do not alter engineering formulas/constants/assumptions unless required to fix a verified failing test or compile/runtime break.
- Preserve readability and explicit naming over clever abstractions.

## Testing Expectations
- Do not delete or bypass tests.
- If tests fail, report failures clearly with context.
- Add tests for bug fixes and behavior changes when feasible.
- For engineering logic changes, include validation evidence and references.

## Engineering Calculation Safety Rules
- Treat all calculator outputs as safety-relevant.
- Never introduce unvalidated “expected” values into calculator tests.
- Reference governing code/standard edition when defining or updating expected behavior.
- Record tolerances and units explicitly for numeric comparisons.

## Configuration and Secrets Rules
- Do not commit production secrets or real credentials.
- Use environment variables for sensitive configuration (for example `ConnectionStrings__InspectionReports`).
- Committed config should be placeholders or local-development defaults only.
- Document required settings in README/docs when adding new configuration.

## Definition of Done (for routine changes)
- Code builds locally (or constraints documented if tooling unavailable).
- Relevant tests are run and outcomes reported.
- Documentation/config updates are included for new behavior.
- No secrets added to source control.
