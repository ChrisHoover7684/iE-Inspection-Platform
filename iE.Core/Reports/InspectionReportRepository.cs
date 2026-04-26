using Microsoft.EntityFrameworkCore;

namespace iE.Core.Reports;

public class InspectionReportRepository(InspectionReportsDbContext dbContext)
{
    public List<InspectionReport> GetAll()
    {
        return dbContext.InspectionReports
            .AsNoTracking()
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
