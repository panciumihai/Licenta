using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class HostelStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeforeLicenseScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BeforeMasterScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOneMasterScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOneScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearTwoScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeforeLicenseScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BeforeMasterScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "YearOneMasterScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "YearOneScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "YearTwoScore",
                table: "Students");
        }
    }
}
