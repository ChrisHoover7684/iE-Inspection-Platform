# iE Inspection Platform

iE Inspection Platform is an inspection and mechanical integrity application with a .NET backend and React frontend. The repository includes API/reporting workflows plus engineering-focused services used by integrity and inspection workflows.

> ⚠️ **Engineering caution:** calculator behavior and engineering outputs must be validated against the applicable code/standard edition before production use.

## Current Modules (High Level)
- **`iE.Api`**: ASP.NET Core API, dependency injection, EF Core, controllers.
- **`iE.Core`**: mechanical/engineering services, report logic, shared domain behavior.
- **`iE.Tests`**: xUnit tests for mechanical and reporting behavior.
- **`iE.Web`**: React + TypeScript + Vite UI.
- **`iE.Tools.StressImporter`**: utility tooling for stress data handling.
- **`docs/`**: migration and project documentation.

## Repository Layout
- `iE.Api/`
- `iE.Core/`
- `iE.Tests/`
- `iE.Web/`
- `iE.Tools.StressImporter/`
- `docs/`
- `docker-compose.yml`

## Prerequisites
- .NET SDK 8.x
- Node.js LTS (recommended current LTS)
- npm (ships with Node)
- Docker Desktop (optional, for local PostgreSQL via compose)

## Local Setup
1. Clone repository.
2. Restore backend dependencies:
   ```bash
   dotnet restore iE.Api/iE.Api.sln
   ```
3. Install frontend dependencies:
   ```bash
   cd iE.Web
   npm ci
   ```

## Backend Run Instructions
From repo root:
```bash
dotnet run --project iE.Api/iE.Api.csproj
```

The API uses `ConnectionStrings:InspectionReports` and currently auto-applies EF Core migrations on startup.

## Frontend Run Instructions
From `iE.Web/`:
```bash
npm run dev
```

Build for production checks:
```bash
npm run build
```

## Testing
From repo root:
```bash
dotnet test iE.Api/iE.Api.sln
```

Frontend build validation:
```bash
cd iE.Web
npm run build
```

## Docker / Database Notes
A local PostgreSQL service is provided in `docker-compose.yml`.

Start local DB:
```bash
docker compose up -d postgres
```

Default compose credentials are for local development only and must not be reused for non-local environments.

## Configuration Guidance
- Prefer environment variables for sensitive settings.
- Primary backend DB variable:
  - `ConnectionStrings__InspectionReports`
- Keep committed appsettings values non-secret/local-only.
- See `iE.Api/appsettings.Development.example.json` for a safe local template.

## Project Status
This repository is being stabilized for stronger CI, documentation, safer configuration, and future calculator golden-test coverage.

## CI Status Note
- During this stabilization phase, `.NET` tests are configured as **diagnostic/non-blocking** due to known baseline B31.3 lookup failures.
- Restore/build and frontend build remain blocking.
- See `docs/testing/KNOWN_TEST_FAILURES.md` for details and required follow-up.
