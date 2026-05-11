import { useMemo, useState } from 'react';
import type { ProjectOption } from '../ndeRequestReferenceData';
import { defaultProjectOptions, getProjectOptions, resetProjectOptions, saveProjectOptions } from '../ndeRequestReferenceData';

const makeId = (name: string) => name.toLowerCase().trim().replace(/[^a-z0-9]+/g, '-').replace(/(^-|-$)/g, '');

export function ReferenceDataProjectsPage() {
  const [projectOptions, setProjectOptions] = useState<ProjectOption[]>(() => getProjectOptions());
  const [newName, setNewName] = useState('');

  const sortedOptions = useMemo(() => [...projectOptions].sort((a, b) => a.name.localeCompare(b.name)), [projectOptions]);

  const updateOptions = (next: ProjectOption[]) => {
    setProjectOptions(next);
    saveProjectOptions(next);
  };

  const addProject = () => {
    const trimmed = newName.trim();
    if (!trimmed) return;
    const idBase = makeId(trimmed);
    const id = projectOptions.some((option) => option.id === idBase) ? `${idBase}-${Date.now()}` : idBase;
    updateOptions([...projectOptions, { id, name: trimmed, isActive: true }]);
    setNewName('');
  };

  return (
    <section className="nde-workspace">
      <div className="card">
        <h2>Reference Data / Projects</h2>
        <p className="muted">Project options are frontend demo reference data until backend persistence is connected.</p>
      </div>

      <div className="card">
        <h3>Manage Projects</h3>
        <div className="row" style={{ gap: 8, alignItems: 'end', flexWrap: 'wrap' }}>
          <label>
            Project Name
            <input value={newName} onChange={(event) => setNewName(event.target.value)} placeholder="Add a project option" />
          </label>
          <button type="button" onClick={addProject}>Add Project</button>
          <button type="button" onClick={() => updateOptions(resetProjectOptions())}>Reset to Demo Defaults</button>
        </div>
      </div>

      <div className="card">
        <table>
          <thead>
            <tr>
              <th>Project</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {sortedOptions.map((option) => (
              <tr key={option.id}>
                <td>
                  <input
                    aria-label={`Project name ${option.name}`}
                    value={option.name}
                    onChange={(event) => {
                      const next = projectOptions.map((current) => current.id === option.id
                        ? { ...current, name: event.target.value }
                        : current);
                      updateOptions(next);
                    }}
                  />
                </td>
                <td>{option.isActive ? 'Active' : 'Inactive'}</td>
                <td>
                  <button
                    type="button"
                    onClick={() => {
                      const next = projectOptions.map((current) => current.id === option.id
                        ? { ...current, isActive: !current.isActive }
                        : current);
                      updateOptions(next);
                    }}
                  >
                    {option.isActive ? 'Deactivate' : 'Reactivate'}
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        {projectOptions.length === 0 && <p className="muted">No project options yet. Add one above.</p>}
        <p className="muted">Default demo projects loaded: {defaultProjectOptions.length}</p>
      </div>
    </section>
  );
}
