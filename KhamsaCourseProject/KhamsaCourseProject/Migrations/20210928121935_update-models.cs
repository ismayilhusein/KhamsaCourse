using Microsoft.EntityFrameworkCore.Migrations;

namespace KhamsaCourseProject.Migrations
{
    public partial class updatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SectorId",
                table: "Students",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Sectors_SectorId",
                table: "Students",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Sectors_SectorId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SectorId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Students");
        }
    }
}
