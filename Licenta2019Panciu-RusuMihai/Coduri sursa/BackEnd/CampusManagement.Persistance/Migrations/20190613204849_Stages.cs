using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class Stages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    PostedDate = table.Column<DateTimeOffset>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stages");
        }
    }
}
