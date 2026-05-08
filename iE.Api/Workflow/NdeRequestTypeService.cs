using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Workflow;

public interface INdeRequestTypeService
{
    Task<bool> IsValidTypeAsync(string tenantId, string ndeType, CancellationToken ct = default);
}

public sealed class NdeRequestTypeService(InspectionReportsDbContext db) : INdeRequestTypeService
{
    public async Task<bool> IsValidTypeAsync(string tenantId, string ndeType, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(ndeType)) return false;
        if (NdeBuiltInTypes.Codes.Contains(ndeType)) return true;
        return await db.NdeRequestTypeDefinitions.AnyAsync(x => x.IsActive && !x.IsBuiltIn && x.ClientOrganizationId == tenantId && x.Code == ndeType, ct);
    }
}
