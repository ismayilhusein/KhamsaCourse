using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KhamsaCourseProject.Migrations
{
    public partial class sectorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Exams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Benefit = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    SectorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SectorId",
                table: "Exams",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_SectorId",
                table: "Publications",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Sectors_SectorId",
                table: "Exams",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Sectors_SectorId",
                table: "Exams");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SectorId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Exams");
        }
    }
}
