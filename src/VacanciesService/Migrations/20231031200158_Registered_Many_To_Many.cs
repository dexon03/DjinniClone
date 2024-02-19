using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacanciesService.Migrations
{
    /// <inheritdoc />
    public partial class Registered_Many_To_Many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VacancySkill_SkillId",
                table: "VacancySkill");

            migrationBuilder.DropIndex(
                name: "IX_LocationVacancy_LocationId",
                table: "LocationVacancy");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkill_SkillId_VacancyId",
                table: "VacancySkill",
                columns: new[] { "SkillId", "VacancyId" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationVacancy_LocationId_VacancyId",
                table: "LocationVacancy",
                columns: new[] { "LocationId", "VacancyId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VacancySkill_SkillId_VacancyId",
                table: "VacancySkill");

            migrationBuilder.DropIndex(
                name: "IX_LocationVacancy_LocationId_VacancyId",
                table: "LocationVacancy");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkill_SkillId",
                table: "VacancySkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationVacancy_LocationId",
                table: "LocationVacancy",
                column: "LocationId");
        }
    }
}
