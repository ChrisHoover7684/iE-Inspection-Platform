using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddAuditEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditEvents",
                columns: table => new
                {
                    AuditEventId = table.Column<string>(type: "text", maxLength: 64, nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<string>(type: "text", maxLength: 64, nullable: true),
                    FacilityId = table.Column<string>(type: "text", maxLength: 64, nullable: true),
                    ActorUserId = table.Column<string>(type: "text", maxLength: 128, nullable: true),
                    Action = table.Column<string>(type: "text", maxLength: 128, nullable: false),
                    ResourceType = table.Column<string>(type: "text", maxLength: 64, nullable: false),
                    ResourceId = table.Column<string>(type: "text", maxLength: 128, nullable: true),
                    Result = table.Column<string>(type: "text", maxLength: 32, nullable: false),
                    CorrelationId = table.Column<string>(type: "text", maxLength: 128, nullable: true),
                    ClientIp = table.Column<string>(type: "text", maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(type: "text", maxLength: 512, nullable: true),
                    MetadataJson = table.Column<string>(type: "text", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEvents", x => x.AuditEventId);
                });

            migrationBuilder.CreateIndex(name: "IX_AuditEvents_ActorUserId", table: "AuditEvents", column: "ActorUserId");
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_OccurredAtUtc", table: "AuditEvents", column: "OccurredAtUtc");
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_ResourceType_ResourceId", table: "AuditEvents", columns: new[] { "ResourceType", "ResourceId" });
            migrationBuilder.CreateIndex(name: "IX_AuditEvents_TenantId", table: "AuditEvents", column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AuditEvents");
        }
    }
}
