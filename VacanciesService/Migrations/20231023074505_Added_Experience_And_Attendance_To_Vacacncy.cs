using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacanciesService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Experience_And_Attendance_To_Vacacncy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendanceMode",
                table: "Vacancy",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Vacancy",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceMode",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Vacancy");
        }
    }
}
