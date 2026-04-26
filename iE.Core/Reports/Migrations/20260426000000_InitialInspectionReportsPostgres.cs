using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class InitialInspectionReportsPostgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientOrganizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ClientOrganizations", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Location = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_ClientOrganizations_ClientOrganizationId",
                        column: x => x.ClientOrganizationId,
                        principalTable: "ClientOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionReports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FacilityId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ProcessUnitId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    AssetId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CreatedByUserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    TemplateId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ReportNumber = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EquipmentTag = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Unit = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SystemId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CircuitId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Service = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionReports_ClientOrganizations_ClientOrganizationId",
                        column: x => x.ClientOrganizationId,
                        principalTable: "ClientOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InspectionReports_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessUnits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FacilityId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UnitCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessUnits_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFacilityAccesses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FacilityId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Role = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFacilityAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFacilityAccesses_ClientOrganizations_ClientOrganizationId",
                        column: x => x.ClientOrganizationId,
                        principalTable: "ClientOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFacilityAccesses_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FacilityId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ProcessUnitId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    EquipmentTag = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EquipmentType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Service = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_ProcessUnits_ProcessUnitId",
                        column: x => x.ProcessUnitId,
                        principalTable: "ProcessUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InspectionFindings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    InspectionReportId = table.Column<string>(type: "character varying(64)", nullable: false),
                    ComponentLocation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ComponentType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FindingType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AssociatedChecklistItem = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DetailedDescription = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Severity = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    RecommendationRequired = table.Column<bool>(type: "boolean", nullable: false),
                    RecommendationText = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionFindings", x => new { x.InspectionReportId, x.Id });
                    table.ForeignKey(
                        name: "FK_InspectionFindings_InspectionReports_InspectionReportId",
                        column: x => x.InspectionReportId,
                        principalTable: "InspectionReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionPhotos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    InspectionReportId = table.Column<string>(type: "character varying(64)", nullable: false),
                    PhotoNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    RelatedComponent = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RelatedChecklistItem = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PhotoRequired = table.Column<bool>(type: "boolean", nullable: false),
                    PhotoAttached = table.Column<bool>(type: "boolean", nullable: false),
                    FileName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    FileUrl = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionPhotos", x => new { x.InspectionReportId, x.Id });
                    table.ForeignKey(
                        name: "FK_InspectionPhotos_InspectionReports_InspectionReportId",
                        column: x => x.InspectionReportId,
                        principalTable: "InspectionReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionReportSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InspectionReportId = table.Column<string>(type: "character varying(64)", nullable: false),
                    SectionId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SectionTitle = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsRepeatable = table.Column<bool>(type: "boolean", nullable: false),
                    InstanceNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionReportSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionReportSections_InspectionReports_InspectionReportId",
                        column: x => x.InspectionReportId,
                        principalTable: "InspectionReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionFinding_PhotoIds",
                columns: table => new
                {
                    InspectionReportId = table.Column<string>(type: "character varying(64)", nullable: false),
                    InspectionFindingId = table.Column<string>(type: "character varying(64)", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionFinding_PhotoIds", x => new { x.InspectionReportId, x.InspectionFindingId, x.Id });
                    table.ForeignKey(
                        name: "FK_InspectionFinding_PhotoIds_InspectionFindings_InspectionReportId_InspectionFindingId",
                        columns: x => new { x.InspectionReportId, x.InspectionFindingId },
                        principalTable: "InspectionFindings",
                        principalColumns: new[] { "InspectionReportId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionReportAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SectionId = table.Column<int>(type: "integer", nullable: false),
                    FieldId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DataType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Comment = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    PhotoRequired = table.Column<bool>(type: "boolean", nullable: true),
                    TransferToComponentSection = table.Column<bool>(type: "boolean", nullable: true),
                    RecommendationRequired = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionReportAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionReportAnswers_InspectionReportSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "InspectionReportSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionReportAnswer_Values",
                columns: table => new
                {
                    InspectionReportAnswerId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionReportAnswer_Values", x => new { x.InspectionReportAnswerId, x.Id });
                    table.ForeignKey(
                        name: "FK_InspectionReportAnswer_Values_InspectionReportAnswers_InspectionRepo~",
                        column: x => x.InspectionReportAnswerId,
                        principalTable: "InspectionReportAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientOrganizations",
                columns: new[] { "Id", "Name", "IsActive", "CreatedAt" },
                values: new object[] { "client-demo-refining", "Demo Refining Client", true, new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "ClientOrganizationId", "Name", "Location", "IsActive", "CreatedAt" },
                values: new object[] { "facility-demo-gulf-coast", "client-demo-refining", "Demo Gulf Coast Refinery", null, true, new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "ProcessUnits",
                columns: new[] { "Id", "FacilityId", "Name", "UnitCode", "IsActive" },
                values: new object[] { "unit-demo-crude", "facility-demo-gulf-coast", "Crude Unit", "001", true });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "FacilityId", "ProcessUnitId", "EquipmentTag", "EquipmentType", "Service", "IsActive" },
                values: new object[] { "asset-demo-piping-system", "facility-demo-gulf-coast", "unit-demo-crude", "EDR-010-H21", "Piping", "Demo Service", true });

            migrationBuilder.CreateIndex(name: "IX_Assets_FacilityId", table: "Assets", column: "FacilityId");
            migrationBuilder.CreateIndex(name: "IX_Assets_ProcessUnitId", table: "Assets", column: "ProcessUnitId");
            migrationBuilder.CreateIndex(name: "IX_Facilities_ClientOrganizationId", table: "Facilities", column: "ClientOrganizationId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReportAnswers_SectionId", table: "InspectionReportAnswers", column: "SectionId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReportSections_InspectionReportId", table: "InspectionReportSections", column: "InspectionReportId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReports_AssetId", table: "InspectionReports", column: "AssetId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReports_ClientOrganizationId", table: "InspectionReports", column: "ClientOrganizationId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReports_FacilityId", table: "InspectionReports", column: "FacilityId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReports_ProcessUnitId", table: "InspectionReports", column: "ProcessUnitId");
            migrationBuilder.CreateIndex(name: "IX_InspectionReports_Status", table: "InspectionReports", column: "Status");
            migrationBuilder.CreateIndex(name: "IX_InspectionReports_TemplateId", table: "InspectionReports", column: "TemplateId");
            migrationBuilder.CreateIndex(name: "IX_ProcessUnits_FacilityId", table: "ProcessUnits", column: "FacilityId");
            migrationBuilder.CreateIndex(name: "IX_UserFacilityAccesses_ClientOrganizationId", table: "UserFacilityAccesses", column: "ClientOrganizationId");
            migrationBuilder.CreateIndex(name: "IX_UserFacilityAccesses_FacilityId", table: "UserFacilityAccesses", column: "FacilityId");
            migrationBuilder.CreateIndex(name: "IX_UserFacilityAccesses_UserId", table: "UserFacilityAccesses", column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionReports_Assets_AssetId",
                table: "InspectionReports",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionReports_ProcessUnits_ProcessUnitId",
                table: "InspectionReports",
                column: "ProcessUnitId",
                principalTable: "ProcessUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "InspectionFinding_PhotoIds");
            migrationBuilder.DropTable(name: "InspectionPhotos");
            migrationBuilder.DropTable(name: "InspectionReportAnswer_Values");
            migrationBuilder.DropTable(name: "UserFacilityAccesses");
            migrationBuilder.DropTable(name: "InspectionFindings");
            migrationBuilder.DropTable(name: "InspectionReportAnswers");
            migrationBuilder.DropTable(name: "InspectionReportSections");
            migrationBuilder.DropTable(name: "InspectionReports");
            migrationBuilder.DropTable(name: "Assets");
            migrationBuilder.DropTable(name: "ProcessUnits");
            migrationBuilder.DropTable(name: "Facilities");
            migrationBuilder.DropTable(name: "ClientOrganizations");
        }
    }
}
