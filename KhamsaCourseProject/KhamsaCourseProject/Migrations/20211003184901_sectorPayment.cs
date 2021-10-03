using Microsoft.EntityFrameworkCore.Migrations;

namespace KhamsaCourseProject.Migrations
{
    public partial class sectorPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SectorId",
                table: "Payments",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Sectors_SectorId",
                table: "Payments",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Sectors_SectorId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SectorId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Payments");
        }
    }
}
