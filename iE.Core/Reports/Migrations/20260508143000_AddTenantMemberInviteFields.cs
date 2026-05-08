using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddTenantMemberInviteFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "ClientOrganizationUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.Sql("UPDATE \"ClientOrganizationUsers\" SET \"NormalizedEmail\" = lower(trim(\"Email\")) WHERE \"Email\" IS NOT NULL;");

            migrationBuilder.DropIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_Email", table: "ClientOrganizationUsers");
            migrationBuilder.DropIndex(name: "IX_ClientOrganizationUsers_Status", table: "ClientOrganizationUsers");
            migrationBuilder.DropIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalSubject",
                table: "ClientOrganizationUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_NormalizedEmail", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "NormalizedEmail" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_Status", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "Status" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "ExternalSubject" }, unique: true, filter: "\"ExternalSubject\" IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_NormalizedEmail", table: "ClientOrganizationUsers");
            migrationBuilder.DropIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_Status", table: "ClientOrganizationUsers");
            migrationBuilder.DropIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUsers");
            migrationBuilder.DropColumn(name: "NormalizedEmail", table: "ClientOrganizationUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalSubject",
                table: "ClientOrganizationUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_Email", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "Email" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_Status", table: "ClientOrganizationUsers", column: "Status");
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUsers_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUsers", columns: new[] { "ClientOrganizationId", "ExternalSubject" }, unique: true);
        }
    }
}
