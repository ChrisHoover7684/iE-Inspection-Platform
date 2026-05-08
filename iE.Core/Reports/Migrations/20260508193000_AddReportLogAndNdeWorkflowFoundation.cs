using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddReportLogAndNdeWorkflowFoundation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NdeRequestTypeDefinitions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsBuiltIn = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_NdeRequestTypeDefinitions", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "NdeRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FacilityId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ProcessUnitId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    AssetId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ReportId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    RequestNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    NdeType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Priority = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Reason = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RequestedByUserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    RequestedByExternalSubject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    AssignedToName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    VendorName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DueDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScheduledAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResultsReceivedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClosedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CanceledAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResultsSummary = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CancellationReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_NdeRequests", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "ReportLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FacilityId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ReportId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EventType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EventStatus = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ActorUserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ActorExternalSubject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FromStatus = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ToStatus = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    RelatedNdeRequestId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MetadataJson = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ReportLogEntries", x => x.Id); });

            migrationBuilder.CreateIndex(name: "IX_NdeRequestTypeDefinitions_ClientOrganizationId_Code", table: "NdeRequestTypeDefinitions", columns: new[] { "ClientOrganizationId", "Code" });
            migrationBuilder.CreateIndex(name: "IX_NdeRequestTypeDefinitions_IsBuiltIn", table: "NdeRequestTypeDefinitions", column: "IsBuiltIn");
            migrationBuilder.CreateIndex(name: "IX_NdeRequestTypeDefinitions_IsActive", table: "NdeRequestTypeDefinitions", column: "IsActive");

            migrationBuilder.CreateIndex(name: "IX_NdeRequests_ClientOrganizationId_FacilityId_Status", table: "NdeRequests", columns: new[] { "ClientOrganizationId", "FacilityId", "Status" });
            migrationBuilder.CreateIndex(name: "IX_NdeRequests_ClientOrganizationId_ReportId", table: "NdeRequests", columns: new[] { "ClientOrganizationId", "ReportId" });
            migrationBuilder.CreateIndex(name: "IX_NdeRequests_ClientOrganizationId_AssetId", table: "NdeRequests", columns: new[] { "ClientOrganizationId", "AssetId" });
            migrationBuilder.CreateIndex(name: "IX_NdeRequests_DueDateUtc", table: "NdeRequests", column: "DueDateUtc");
            migrationBuilder.CreateIndex(name: "IX_NdeRequests_CreatedAtUtc", table: "NdeRequests", column: "CreatedAtUtc");

            migrationBuilder.CreateIndex(name: "IX_ReportLogEntries_ClientOrganizationId_FacilityId_ReportId_CreatedAtUtc", table: "ReportLogEntries", columns: new[] { "ClientOrganizationId", "FacilityId", "ReportId", "CreatedAtUtc" });
            migrationBuilder.CreateIndex(name: "IX_ReportLogEntries_ClientOrganizationId_ReportId", table: "ReportLogEntries", columns: new[] { "ClientOrganizationId", "ReportId" });
            migrationBuilder.CreateIndex(name: "IX_ReportLogEntries_EventType", table: "ReportLogEntries", column: "EventType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "NdeRequestTypeDefinitions");
            migrationBuilder.DropTable(name: "NdeRequests");
            migrationBuilder.DropTable(name: "ReportLogEntries");
        }
    }
}
