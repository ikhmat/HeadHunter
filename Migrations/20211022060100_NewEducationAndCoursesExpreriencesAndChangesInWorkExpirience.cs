using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeadHunter.Migrations
{
    public partial class NewEducationAndCoursesExpreriencesAndChangesInWorkExpirience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "WorkExpiriences");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEnd",
                table: "WorkExpiriences",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfReceiving",
                table: "WorkExpiriences",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfEnd",
                table: "WorkExpiriences");

            migrationBuilder.DropColumn(
                name: "DateOfReceiving",
                table: "WorkExpiriences");

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "WorkExpiriences",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
