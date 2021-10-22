using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeadHunter.Migrations
{
    public partial class AddExpirienciesInDbSer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "WorkExpiriences",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "WorkExpiriences",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CoursesExpiriences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ResumeId = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Speciality = table.Column<string>(type: "text", nullable: true),
                    DateOfReceiving = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesExpiriences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationExpiriences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ResumeId = table.Column<string>(type: "text", nullable: true),
                    InstitutionName = table.Column<string>(type: "text", nullable: true),
                    Speciality = table.Column<string>(type: "text", nullable: true),
                    DateOfReceiving = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationExpiriences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesExpiriences");

            migrationBuilder.DropTable(
                name: "EducationExpiriences");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "WorkExpiriences",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "WorkExpiriences",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
