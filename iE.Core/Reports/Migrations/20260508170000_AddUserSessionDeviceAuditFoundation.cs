using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddUserSessionDeviceAuditFoundation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientOrganizationUserDevices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationUserId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ExternalSubject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DeviceFingerprintHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FirstSeenAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSeenAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastIpHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastUserAgentHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    IsTrusted = table.Column<bool>(type: "boolean", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ClientOrganizationUserDevices", x => x.Id); });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserDevices_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUserDevices", columns: new[] { "ClientOrganizationId", "ExternalSubject" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserDevices_ClientOrganizationId_ClientOrganizationUserId", table: "ClientOrganizationUserDevices", columns: new[] { "ClientOrganizationId", "ClientOrganizationUserId" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserDevices_ClientOrganizationId_DeviceFingerprintHash", table: "ClientOrganizationUserDevices", columns: new[] { "ClientOrganizationId", "DeviceFingerprintHash" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserDevices_LastSeenAtUtc", table: "ClientOrganizationUserDevices", column: "LastSeenAtUtc");
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserDevices_IsDisabled", table: "ClientOrganizationUserDevices", column: "IsDisabled");

            migrationBuilder.CreateTable(
                name: "ClientOrganizationUserSessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationUserId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ExternalSubject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    SessionKeyHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    StartedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSeenAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IpHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    UserAgentHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DeviceId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ClientOrganizationUserSessions", x => x.Id); });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserSessions_ClientOrganizationId_ExternalSubject", table: "ClientOrganizationUserSessions", columns: new[] { "ClientOrganizationId", "ExternalSubject" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserSessions_ClientOrganizationId_ClientOrganizationUserId", table: "ClientOrganizationUserSessions", columns: new[] { "ClientOrganizationId", "ClientOrganizationUserId" });
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserSessions_LastSeenAtUtc", table: "ClientOrganizationUserSessions", column: "LastSeenAtUtc");
            migrationBuilder.CreateIndex(name: "IX_ClientOrganizationUserSessions_IsRevoked", table: "ClientOrganizationUserSessions", column: "IsRevoked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ClientOrganizationUserDevices");
            migrationBuilder.DropTable(name: "ClientOrganizationUserSessions");
        }
    }
}
