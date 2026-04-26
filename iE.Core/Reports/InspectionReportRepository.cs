namespace iE.Core.Reports;

public class InspectionReportRepository
{
    private static readonly List<InspectionReport> Reports = new();
    private static readonly object Sync = new();

    public List<InspectionReport> GetAll()
    {
        lock (Sync)
        {
            return Reports.ToList();
        }
    }

    public InspectionReport? GetById(string id)
    {
        lock (Sync)
        {
            return Reports.FirstOrDefault(r => string.Equals(r.Id, id, StringComparison.OrdinalIgnoreCase));
        }
    }

    public InspectionReport Create(InspectionReport report)
    {
        lock (Sync)
        {
            Reports.Add(report);
            return report;
        }
    }

    public InspectionReport? Update(string id, InspectionReport report)
    {
        lock (Sync)
        {
            var index = Reports.FindIndex(r => string.Equals(r.Id, id, StringComparison.OrdinalIgnoreCase));
            if (index < 0)
            {
                return null;
            }

            Reports[index] = report;
            return report;
        }
    }

    public bool Delete(string id)
    {
        lock (Sync)
        {
            var existing = Reports.FirstOrDefault(r => string.Equals(r.Id, id, StringComparison.OrdinalIgnoreCase));
            if (existing is null)
            {
                return false;
            }

            Reports.Remove(existing);
            return true;
        }
    }
}
