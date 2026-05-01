using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iE.Core.Reports.Migrations
{
    public partial class AddPhotoMarkups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoMarkups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PhotoId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    MarkupJson = table.Column<string>(type: "character varying(16000)", maxLength: 16000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoMarkups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoMarkups_CreatedAt",
                table: "PhotoMarkups",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoMarkups_PhotoId",
                table: "PhotoMarkups",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoMarkups");
        }
    }
}
