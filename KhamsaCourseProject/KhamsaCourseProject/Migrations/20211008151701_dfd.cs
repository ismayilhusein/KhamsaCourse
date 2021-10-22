using Microsoft.EntityFrameworkCore.Migrations;

namespace KhamsaCourseProject.Migrations
{
    public partial class dfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeContractId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "EmployeeContractId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");
        }
    }
}
