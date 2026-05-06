using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddAuditTenantScopedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_TenantId_OccurredAtUtc", table: "AuditEvents", columns: new[] { "TenantId", "OccurredAtUtc" });
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_TenantId_ResourceType_ResourceId", table: "AuditEvents", columns: new[] { "TenantId", "ResourceType", "ResourceId" });
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_TenantId_ActorUserId", table: "AuditEvents", columns: new[] { "TenantId", "ActorUserId" });
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_TenantId_Action", table: "AuditEvents", columns: new[] { "TenantId", "Action" });
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_TenantId_FacilityId", table: "AuditEvents", columns: new[] { "TenantId", "FacilityId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_AuditEvents_TenantId_OccurredAtUtc", table: "AuditEvents");
            migrationBuilder.DropIndex(name: "IX_AuditEvents_TenantId_ResourceType_ResourceId", table: "AuditEvents");
            migrationBuilder.DropIndex(name: "IX_AuditEvents_TenantId_ActorUserId", table: "AuditEvents");
            migrationBuilder.DropIndex(name: "IX_AuditEvents_TenantId_Action", table: "AuditEvents");
            migrationBuilder.DropIndex(name: "IX_AuditEvents_TenantId_FacilityId", table: "AuditEvents");
        }
    }
}
