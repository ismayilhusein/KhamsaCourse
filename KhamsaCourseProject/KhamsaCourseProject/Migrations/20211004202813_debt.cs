using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KhamsaCourseProject.Migrations
{
    public partial class debt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CostDate = table.Column<DateTime>(nullable: false),
                    SectorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debts_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debts_SectorId",
                table: "Debts",
                column: "SectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Debts");
        }
    }
}
