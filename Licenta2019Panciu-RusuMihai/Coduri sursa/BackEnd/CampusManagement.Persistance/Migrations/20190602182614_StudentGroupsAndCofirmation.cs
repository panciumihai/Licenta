using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class StudentGroupsAndCofirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Students",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Students");
        }
    }
}
