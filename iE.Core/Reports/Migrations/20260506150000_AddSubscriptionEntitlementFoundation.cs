using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddSubscriptionEntitlementFoundation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PlanCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MetadataJson = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSubscriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientOrganizationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PlanId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    StartsAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndsAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TrialEndsAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSubscriptions_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionEntitlements",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PlanId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EntitlementKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LimitValue = table.Column<int>(type: "integer", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionEntitlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionEntitlements_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(name: "IX_ClientSubscriptions_ClientOrganizationId", table: "ClientSubscriptions", column: "ClientOrganizationId");
            migrationBuilder.CreateIndex(name: "IX_ClientSubscriptions_PlanId", table: "ClientSubscriptions", column: "PlanId");
            migrationBuilder.CreateIndex(name: "IX_ClientSubscriptions_Status", table: "ClientSubscriptions", column: "Status");
            migrationBuilder.CreateIndex(name: "IX_SubscriptionEntitlements_EntitlementKey", table: "SubscriptionEntitlements", column: "EntitlementKey");
            migrationBuilder.CreateIndex(name: "IX_SubscriptionEntitlements_PlanId", table: "SubscriptionEntitlements", column: "PlanId");
            migrationBuilder.CreateIndex(name: "IX_SubscriptionEntitlements_PlanId_EntitlementKey", table: "SubscriptionEntitlements", columns: new[] { "PlanId", "EntitlementKey" }, unique: true);
            migrationBuilder.CreateIndex(name: "IX_SubscriptionPlans_PlanCode", table: "SubscriptionPlans", column: "PlanCode", unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ClientSubscriptions");
            migrationBuilder.DropTable(name: "SubscriptionEntitlements");
            migrationBuilder.DropTable(name: "SubscriptionPlans");
        }
    }
}
