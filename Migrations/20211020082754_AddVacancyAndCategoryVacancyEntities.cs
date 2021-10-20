using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeadHunter.Migrations
{
    public partial class AddVacancyAndCategoryVacancyEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryVacancies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryVacancies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Wage = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExperienceFrom = table.Column<int>(type: "integer", nullable: false),
                    ExperienceTo = table.Column<int>(type: "integer", nullable: false),
                    CategoryVacancyId = table.Column<string>(type: "text", nullable: true),
                    DateOfUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryVacancies");

            migrationBuilder.DropTable(
                name: "Vacancies");
        }
    }
}
