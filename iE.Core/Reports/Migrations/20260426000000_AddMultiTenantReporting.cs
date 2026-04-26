using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations;

public partial class AddMultiTenantReporting : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Facilities",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Location = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Facilities", x => x.Id);
                table.ForeignKey(
                    name: "FK_Facilities_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserAccesses",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                Role = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserAccesses", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserAccesses_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.AddColumn<string>(
            name: "ClientId",
            table: "InspectionReports",
            type: "character varying(64)",
            maxLength: 64,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "CreatedByUserId",
            table: "InspectionReports",
            type: "character varying(128)",
            maxLength: 128,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<string>(
            name: "FacilityId",
            table: "InspectionReports",
            type: "character varying(64)",
            maxLength: 64,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.CreateIndex(
            name: "IX_Clients_Code",
            table: "Clients",
            column: "Code",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Facilities_ClientId_Code",
            table: "Facilities",
            columns: new[] { "ClientId", "Code" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_InspectionReports_ClientId_FacilityId_CreatedAt",
            table: "InspectionReports",
            columns: new[] { "ClientId", "FacilityId", "CreatedAt" });

        migrationBuilder.CreateIndex(
            name: "IX_InspectionReports_ClientId_FacilityId_ReportNumber",
            table: "InspectionReports",
            columns: new[] { "ClientId", "FacilityId", "ReportNumber" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_InspectionReports_FacilityId",
            table: "InspectionReports",
            column: "FacilityId");

        migrationBuilder.CreateIndex(
            name: "IX_UserAccesses_ClientId_UserId",
            table: "UserAccesses",
            columns: new[] { "ClientId", "UserId" },
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_InspectionReports_Clients_ClientId",
            table: "InspectionReports",
            column: "ClientId",
            principalTable: "Clients",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_InspectionReports_Facilities_FacilityId",
            table: "InspectionReports",
            column: "FacilityId",
            principalTable: "Facilities",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_InspectionReports_Clients_ClientId",
            table: "InspectionReports");

        migrationBuilder.DropForeignKey(
            name: "FK_InspectionReports_Facilities_FacilityId",
            table: "InspectionReports");

        migrationBuilder.DropTable(
            name: "UserAccesses");

        migrationBuilder.DropTable(
            name: "Facilities");

        migrationBuilder.DropTable(
            name: "Clients");

        migrationBuilder.DropIndex(
            name: "IX_InspectionReports_ClientId_FacilityId_CreatedAt",
            table: "InspectionReports");

        migrationBuilder.DropIndex(
            name: "IX_InspectionReports_ClientId_FacilityId_ReportNumber",
            table: "InspectionReports");

        migrationBuilder.DropIndex(
            name: "IX_InspectionReports_FacilityId",
            table: "InspectionReports");

        migrationBuilder.DropColumn(
            name: "ClientId",
            table: "InspectionReports");

        migrationBuilder.DropColumn(
            name: "CreatedByUserId",
            table: "InspectionReports");

        migrationBuilder.DropColumn(
            name: "FacilityId",
            table: "InspectionReports");
    }
}
