using Microsoft.EntityFrameworkCore;

namespace iE.Core.Reports.Persistence;

public class InspectionReportRepository(InspectionReportsDbContext dbContext)
{
    public List<InspectionReport> GetAll(
        string? clientOrganizationId = null,
        string? facilityId = null,
        string? templateId = null,
        string? status = null)
    {
        var query = dbContext.InspectionReports.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(clientOrganizationId))
        {
            query = query.Where(r => r.ClientOrganizationId == clientOrganizationId);
        }

        if (!string.IsNullOrWhiteSpace(facilityId))
        {
            query = query.Where(r => r.FacilityId == facilityId);
        }

        if (!string.IsNullOrWhiteSpace(templateId))
        {
            query = query.Where(r => r.TemplateId == templateId);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r => r.Status == status);
        }

        return query
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }


    public List<InspectionReport> GetReports(
        string? status = null,
        string? facilityId = null,
        string? unit = null)
    {
        var query = dbContext.InspectionReports.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r => r.Status == status);
        }

        if (!string.IsNullOrWhiteSpace(facilityId))
        {
            query = query.Where(r => r.FacilityId == facilityId);
        }

        if (!string.IsNullOrWhiteSpace(unit))
        {
            query = query.Where(r => r.Unit == unit);
        }

        return query
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }

    public InspectionReport? GetById(string id)
    {
        return dbContext.InspectionReports
            .AsNoTracking()
            .FirstOrDefault(r => r.Id == id);
    }

    public InspectionReport Create(InspectionReport report)
    {
        dbContext.InspectionReports.Add(report);
        dbContext.SaveChanges();
        return report;
    }

    public InspectionReport? Update(string id, InspectionReport report)
    {
        var existing = dbContext.InspectionReports.FirstOrDefault(r => r.Id == id);
        if (existing is null)
        {
            return null;
        }

        dbContext.Entry(existing).CurrentValues.SetValues(report);
        var sections = report.Sections.ToList();
        var findings = report.Findings.ToList();
        var observations = report.Observations.ToList();
        var photos = report.Photos.ToList();
        var calculations = (report.Calculations ?? [])
            .Select(CloneCalculationSnapshot)
            .ToList();

        dbContext.Entry(existing).Collection(r => r.Sections).Load();
        existing.Sections.Clear();
        foreach (var section in sections)
        {
            existing.Sections.Add(section);
        }

        dbContext.Entry(existing).Collection(r => r.Findings).Load();
        existing.Findings.Clear();
        foreach (var finding in findings)
        {
            existing.Findings.Add(finding);
        }

        dbContext.Entry(existing).Collection(r => r.Observations).Load();
        existing.Observations.Clear();
        foreach (var observation in observations)
        {
            existing.Observations.Add(observation);
        }

        dbContext.Entry(existing).Collection(r => r.Photos).Load();
        existing.Photos.Clear();
        foreach (var photo in photos)
        {
            existing.Photos.Add(photo);
        }

        dbContext.Entry(existing).Collection(r => r.Calculations).Load();
        existing.Calculations.Clear();
        foreach (var calculation in calculations)
        {
            existing.Calculations.Add(calculation);
        }

        dbContext.SaveChanges();
        return existing;
    }

    public bool Delete(string id)
    {
        var existing = dbContext.InspectionReports.FirstOrDefault(r => r.Id == id);
        if (existing is null)
        {
            return false;
        }

        dbContext.InspectionReports.Remove(existing);
        dbContext.SaveChanges();
        return true;
    }

    private static InspectionCalculationSnapshot CloneCalculationSnapshot(InspectionCalculationSnapshot snapshot)
    {
        return new InspectionCalculationSnapshot
        {
            Id = snapshot.Id,
            CalculationType = snapshot.CalculationType,
            DisplayName = snapshot.DisplayName,
            FormulaVersion = snapshot.FormulaVersion,
            Inputs = snapshot.Inputs,
            Outputs = snapshot.Outputs,
            CalculatedAt = snapshot.CalculatedAt,
            InsertLabel = snapshot.InsertLabel,
            LinkedSectionId = snapshot.LinkedSectionId,
            LinkedFieldId = snapshot.LinkedFieldId,
            LinkedFindingId = snapshot.LinkedFindingId,
            StandardReferences = snapshot.StandardReferences.Select(reference => new InspectionCalculationReference
            {
                Standard = reference.Standard,
                Edition = reference.Edition,
                Paragraph = reference.Paragraph,
                Note = reference.Note
            }).ToList(),
            Warnings = snapshot.Warnings.Select(warning => new InspectionCalculationWarning
            {
                Code = warning.Code,
                Message = warning.Message,
                Severity = warning.Severity
            }).ToList()
        };
    }
}
