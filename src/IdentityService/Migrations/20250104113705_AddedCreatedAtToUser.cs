using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedAtToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("01943117-fb31-746c-8c46-3e4656c887ba"), true, "Admin" },
                    { new Guid("01943118-4440-7b9e-8960-efa609825355"), true, "Recruiter" },
                    { new Guid("01943118-53b4-7ca3-84fc-43f969fde248"), true, "Candidate" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "RefreshToken", "RoleId" },
                values: new object[] { new Guid("01943117-825a-7c86-b126-2e2839404e47"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mail.com", "Admin", "Admin", "7e490b39c1f46c3db6aa0a5d0150990b29b7e4420fe42a1f1632e4d5c41e480e", "zLKOph3JrlA4WjOpdms/wQ==", "123456789", null, new Guid("01943117-fb31-746c-8c46-3e4656c887ba") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("01943118-4440-7b9e-8960-efa609825355"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("01943118-53b4-7ca3-84fc-43f969fde248"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("01943117-825a-7c86-b126-2e2839404e47"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("01943117-fb31-746c-8c46-3e4656c887ba"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "User");

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
    }
}
