using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("44d04cb4-8c75-4125-b53e-d29fabafe372"), true, "Candidate" },
                    { new Guid("978d3f9e-89d0-4061-8716-a45c3e75d5c0"), true, "Admin" },
                    { new Guid("f9a4f1d5-b7c8-4501-b30f-c1bbb59a3fb6"), true, "Recruiter" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "RefreshToken", "RoleId" },
                values: new object[] { new Guid("a7d84623-a276-443e-8f3a-8009d71f5221"), "admin@mail.com", "Admin", "Admin", "e31600e95eb220a2977ce41943e87acb4ba82e7bb80e30c9f7c9cd256a87c6d0", "2FogXOBmztSYqisKP7F8zg==", "123456789", null, new Guid("978d3f9e-89d0-4061-8716-a45c3e75d5c0") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("44d04cb4-8c75-4125-b53e-d29fabafe372"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f9a4f1d5-b7c8-4501-b30f-c1bbb59a3fb6"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a7d84623-a276-443e-8f3a-8009d71f5221"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("978d3f9e-89d0-4061-8716-a45c3e75d5c0"));

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
    }
}
