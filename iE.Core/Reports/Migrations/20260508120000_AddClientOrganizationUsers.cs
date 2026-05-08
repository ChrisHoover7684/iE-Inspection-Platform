using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddClientOrganizationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientOrganizationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ExternalSubject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastSeenAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOrganizationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientOrganizationUsers_ClientOrganizations_ClientOrganizationId",
                        column: x => x.ClientOrganizationId,
                        principalTable: "ClientOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId", table: "ClientOrganizationUsers", column: "ClientOrganizationId");
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_Email", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "Email" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "ExternalSubject" }, unique: true);
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_Status", table: "ClientOrganizationUsers", column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ClientOrganizationUsers");
        }
    }
}
