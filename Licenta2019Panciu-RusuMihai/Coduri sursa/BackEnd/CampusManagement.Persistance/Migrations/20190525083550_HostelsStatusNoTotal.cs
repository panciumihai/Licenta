using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class HostelsStatusNoTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "HostelsStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "HostelsStatus",
                nullable: false,
                defaultValue: 0);
        }
    }
}
