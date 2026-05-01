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

        dbContext.Entry(existing).Collection(r => r.Sections).Load();
        existing.Sections.Clear();
        foreach (var section in report.Sections)
        {
            existing.Sections.Add(section);
        }

        dbContext.Entry(existing).Collection(r => r.Findings).Load();
        existing.Findings.Clear();
        foreach (var finding in report.Findings)
        {
            existing.Findings.Add(finding);
        }

        dbContext.Entry(existing).Collection(r => r.Observations).Load();
        existing.Observations.Clear();
        foreach (var observation in report.Observations)
        {
            existing.Observations.Add(observation);
        }

        dbContext.Entry(existing).Collection(r => r.Photos).Load();
        existing.Photos.Clear();
        foreach (var photo in report.Photos)
        {
            existing.Photos.Add(photo);
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
}
