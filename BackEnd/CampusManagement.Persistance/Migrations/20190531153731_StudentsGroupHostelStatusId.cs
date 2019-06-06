using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusManagement.Persistance.Migrations
{
    public partial class StudentsGroupHostelStatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsGroups_HostelsStatus_HostelStatusId",
                table: "StudentsGroups");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "StudentsGroups",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HostelStatusId",
                table: "StudentsGroups",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsGroups_HostelsStatus_HostelStatusId",
                table: "StudentsGroups",
                column: "HostelStatusId",
                principalTable: "HostelsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsGroups_HostelsStatus_HostelStatusId",
                table: "StudentsGroups");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "StudentsGroups",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "HostelStatusId",
                table: "StudentsGroups",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsGroups_HostelsStatus_HostelStatusId",
                table: "StudentsGroups",
                column: "HostelStatusId",
                principalTable: "HostelsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
