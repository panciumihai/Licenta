using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class StudentDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cnp",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnp",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Students");
        }
    }
}
