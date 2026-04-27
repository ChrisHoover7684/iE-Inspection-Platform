# iE Web (Developer/Test UI)

Minimal React + TypeScript UI for testing the reporting workflow.

## Run

```bash
npm install
npm run dev
```

The UI defaults to API base URL `http://localhost:5229` and can be changed in the dashboard.

## Routes

- `/reports-test` - templates + report instances dashboard
- `/reports-test/:id` - report editor with checklist, findings, save/sync/export actions
