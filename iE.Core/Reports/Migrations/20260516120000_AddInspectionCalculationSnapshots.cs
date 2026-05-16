using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddInspectionCalculationSnapshots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InspectionCalculations",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InspectionReportId = table.Column<string>(type: "character varying(64)", nullable: false),
                    Id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CalculationType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FormulaVersion = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Inputs = table.Column<string>(type: "character varying(16000)", maxLength: 16000, nullable: false),
                    Outputs = table.Column<string>(type: "character varying(16000)", maxLength: 16000, nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InsertLabel = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    LinkedSectionId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LinkedFieldId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LinkedFindingId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionCalculations", x => x.IdPk);
                    table.ForeignKey(
                        name: "FK_InspectionCalculations_InspectionReports_InspectionReportId",
                        column: x => x.InspectionReportId,
                        principalTable: "InspectionReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionCalculationReferences",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CalculationIdPk = table.Column<int>(type: "integer", nullable: false),
                    Standard = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Edition = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Paragraph = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionCalculationReferences", x => x.IdPk);
                    table.ForeignKey(
                        name: "FK_InspectionCalculationReferences_InspectionCalculations_CalculationIdPk",
                        column: x => x.CalculationIdPk,
                        principalTable: "InspectionCalculations",
                        principalColumn: "IdPk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionCalculationWarnings",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CalculationIdPk = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Severity = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionCalculationWarnings", x => x.IdPk);
                    table.ForeignKey(
                        name: "FK_InspectionCalculationWarnings_InspectionCalculations_CalculationIdPk",
                        column: x => x.CalculationIdPk,
                        principalTable: "InspectionCalculations",
                        principalColumn: "IdPk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(name: "IX_InspectionCalculations_InspectionReportId", table: "InspectionCalculations", column: "InspectionReportId");
            migrationBuilder.CreateIndex(name: "IX_InspectionCalculationReferences_CalculationIdPk", table: "InspectionCalculationReferences", column: "CalculationIdPk");
            migrationBuilder.CreateIndex(name: "IX_InspectionCalculationWarnings_CalculationIdPk", table: "InspectionCalculationWarnings", column: "CalculationIdPk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "InspectionCalculationReferences");
            migrationBuilder.DropTable(name: "InspectionCalculationWarnings");
            migrationBuilder.DropTable(name: "InspectionCalculations");
        }
    }
}
