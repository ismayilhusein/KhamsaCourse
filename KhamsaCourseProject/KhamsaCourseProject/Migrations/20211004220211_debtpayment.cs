using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KhamsaCourseProject.Migrations
{
    public partial class debtpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraDebts",
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
                    table.PrimaryKey("PK_ExtraDebts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraDebts_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Benefit = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BenefitDate = table.Column<DateTime>(nullable: false),
                    SectorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraPayments_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraDebts_SectorId",
                table: "ExtraDebts",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraPayments_SectorId",
                table: "ExtraPayments",
                column: "SectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraDebts");

            migrationBuilder.DropTable(
                name: "ExtraPayments");
        }
    }
}
