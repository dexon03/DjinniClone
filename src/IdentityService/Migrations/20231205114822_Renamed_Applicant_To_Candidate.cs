using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Applicant_To_Candidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("280fc4c3-9bf1-4799-98ed-2b776ab1929a"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("397e34fc-8a3a-4f56-9bdd-5e71429884a3"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("7c365820-3447-4147-a839-2d1768a3ae23"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("ccb0ad8f-5080-410a-bf1e-19e97fcd9cbf"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("59a441cf-0b9d-416b-b94a-73bd1e28cf86"), true, "Admin" },
                    { new Guid("a3ea1b0e-ca72-491c-954f-4426769a983b"), true, "Recruiter" },
                    { new Guid("dd8bf902-d9d4-4179-8356-689380a6cdf8"), true, "CompanyOwner" },
                    { new Guid("dee05964-e869-4e9b-80c1-c149ffff2baa"), true, "Candidate" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("59a441cf-0b9d-416b-b94a-73bd1e28cf86"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a3ea1b0e-ca72-491c-954f-4426769a983b"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("dd8bf902-d9d4-4179-8356-689380a6cdf8"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("dee05964-e869-4e9b-80c1-c149ffff2baa"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("280fc4c3-9bf1-4799-98ed-2b776ab1929a"), true, "Applicant" },
                    { new Guid("397e34fc-8a3a-4f56-9bdd-5e71429884a3"), true, "Admin" },
                    { new Guid("7c365820-3447-4147-a839-2d1768a3ae23"), true, "CompanyOwner" },
                    { new Guid("ccb0ad8f-5080-410a-bf1e-19e97fcd9cbf"), true, "Recruiter" }
                });
        }
    }
}
