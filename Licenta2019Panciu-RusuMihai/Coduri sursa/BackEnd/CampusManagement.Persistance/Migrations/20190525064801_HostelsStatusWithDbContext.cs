using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class HostelsStatusWithDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentsGroupId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HostelsStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    HostelId = table.Column<Guid>(nullable: false),
                    TotalSeats = table.Column<int>(nullable: false),
                    MaleSeats = table.Column<int>(nullable: false),
                    OccupiedMaleSeats = table.Column<int>(nullable: false),
                    ReservedMaleSeats = table.Column<int>(nullable: false),
                    FemaleSeats = table.Column<int>(nullable: false),
                    OccupiedFemaleSeats = table.Column<int>(nullable: false),
                    ReservedFemaleSeats = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelsStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostelsStatus_Hostels_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    HostelStatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsGroups_HostelsStatus_HostelStatusId",
                        column: x => x.HostelStatusId,
                        principalTable: "HostelsStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentsGroupId",
                table: "Students",
                column: "StudentsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HostelsStatus_HostelId",
                table: "HostelsStatus",
                column: "HostelId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGroups_HostelStatusId",
                table: "StudentsGroups",
                column: "HostelStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentsGroups_StudentsGroupId",
                table: "Students",
                column: "StudentsGroupId",
                principalTable: "StudentsGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentsGroups_StudentsGroupId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentsGroups");

            migrationBuilder.DropTable(
                name: "HostelsStatus");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentsGroupId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentsGroupId",
                table: "Students");
        }
    }
}
