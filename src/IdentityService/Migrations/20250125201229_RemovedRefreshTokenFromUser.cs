using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRefreshTokenFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("01943117-825a-7c86-b126-2e2839404e47"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { "8b5c000702373f56e5c02447185bab73c722c67d4aeff2913a5dbd684a8525fe", "a/5Fl1UItotDXvD8sDuaBQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("01943117-825a-7c86-b126-2e2839404e47"),
                columns: new[] { "PasswordHash", "PasswordSalt", "RefreshToken" },
                values: new object[] { "7e490b39c1f46c3db6aa0a5d0150990b29b7e4420fe42a1f1632e4d5c41e480e", "zLKOph3JrlA4WjOpdms/wQ==", null });
        }
    }
}
