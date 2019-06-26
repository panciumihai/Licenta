using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class ApplicationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    ChildOfTeacher = table.Column<bool>(nullable: false),
                    SpecialCases = table.Column<string>(nullable: true),
                    AccommodationRequest = table.Column<bool>(nullable: false),
                    Scholarship = table.Column<bool>(nullable: false),
                    LastYearLocation = table.Column<string>(nullable: true),
                    PostedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hostels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MapLocation = table.Column<string>(nullable: true),
                    TotalCapacity = table.Column<string>(nullable: true),
                    HostelAdminFullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HostelPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    PreferenceNumber = table.Column<int>(nullable: false),
                    HostelId = table.Column<Guid>(nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostelPreferences_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostelPreferences_Hostels_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StudentId",
                table: "Applications",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_HostelPreferences_ApplicationId",
                table: "HostelPreferences",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_HostelPreferences_HostelId",
                table: "HostelPreferences",
                column: "HostelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HostelPreferences");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Hostels");
        }
    }
}
