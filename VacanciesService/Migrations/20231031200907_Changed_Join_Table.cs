using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacanciesService.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Join_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancySkill",
                table: "VacancySkill");

            migrationBuilder.DropIndex(
                name: "IX_VacancySkill_SkillId_VacancyId",
                table: "VacancySkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationVacancy",
                table: "LocationVacancy");

            migrationBuilder.DropIndex(
                name: "IX_LocationVacancy_LocationId_VacancyId",
                table: "LocationVacancy");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VacancySkill");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocationVacancy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancySkill",
                table: "VacancySkill",
                columns: new[] { "SkillId", "VacancyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationVacancy",
                table: "LocationVacancy",
                columns: new[] { "LocationId", "VacancyId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancySkill",
                table: "VacancySkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationVacancy",
                table: "LocationVacancy");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "VacancySkill",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "LocationVacancy",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancySkill",
                table: "VacancySkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationVacancy",
                table: "LocationVacancy",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkill_SkillId_VacancyId",
                table: "VacancySkill",
                columns: new[] { "SkillId", "VacancyId" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationVacancy_LocationId_VacancyId",
                table: "LocationVacancy",
                columns: new[] { "LocationId", "VacancyId" });
        }
    }
}
