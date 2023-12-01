using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProfilesService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Mocked_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("04a6fe97-55af-4ae8-8e31-43810baf2c1e"), "Odesa", "Ukraine" },
                    { new Guid("89df1fcf-e799-4712-8d66-b1c4b9f37d63"), "Lviv", "Ukraine" },
                    { new Guid("9596cf1c-b217-4b8f-aa6f-b7fb581b0a82"), "Zaporizhzhia", "Ukraine" },
                    { new Guid("99c8fe24-cfc1-4203-9d86-d68127cf9abc"), "Dnipro", "Ukraine" },
                    { new Guid("acb32064-9b58-4532-86b1-5e7a12a87a15"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("d22b07e5-63d3-4f4a-bf51-3696f5231d2e"), "Vinnytsia", "Ukraine" },
                    { new Guid("d8a2c680-fd38-46f5-a825-be0dedd9dd9d"), "Kyiv", "Ukraine" },
                    { new Guid("fb119ac9-537b-4221-bfe9-8e77040b9f60"), "Kharkiv", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2315b327-92db-4184-ba38-02f1a877b7f9"), "Swift" },
                    { new Guid("2bb988a0-d438-4007-8700-83d18de5d4ed"), "Python" },
                    { new Guid("2ddef62d-475f-4bbc-b429-75bdbf1e2fc6"), "Kotlin" },
                    { new Guid("9a5062b9-f6f2-4fd1-80d8-60e2d1cc2d21"), "Ruby" },
                    { new Guid("a32a886b-1608-43db-97dc-1fcf104f0f67"), "TypeScript" },
                    { new Guid("a589c792-8f91-4865-b1ed-30f4e8cb2dbd"), "Java" },
                    { new Guid("b7d1aca2-fe9c-4416-9c98-8cb86e72d0ef"), "Scala" },
                    { new Guid("c0f8b1ec-8332-454b-bc29-b1fa90a2e492"), "PHP" },
                    { new Guid("c8d64beb-aa58-45d8-aed7-d43f9f91c935"), "C#" },
                    { new Guid("cbda1b0a-26b3-4d59-90ff-fffce31f1a33"), "Go" },
                    { new Guid("e2e2f95b-c33e-4c41-a5dc-ae41431f0b9c"), "C++" },
                    { new Guid("f91a258a-1603-4cb0-8940-b44110099648"), "JavaScript" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("04a6fe97-55af-4ae8-8e31-43810baf2c1e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("89df1fcf-e799-4712-8d66-b1c4b9f37d63"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("9596cf1c-b217-4b8f-aa6f-b7fb581b0a82"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("99c8fe24-cfc1-4203-9d86-d68127cf9abc"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("acb32064-9b58-4532-86b1-5e7a12a87a15"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("d22b07e5-63d3-4f4a-bf51-3696f5231d2e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("d8a2c680-fd38-46f5-a825-be0dedd9dd9d"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("fb119ac9-537b-4221-bfe9-8e77040b9f60"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2315b327-92db-4184-ba38-02f1a877b7f9"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2bb988a0-d438-4007-8700-83d18de5d4ed"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2ddef62d-475f-4bbc-b429-75bdbf1e2fc6"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("9a5062b9-f6f2-4fd1-80d8-60e2d1cc2d21"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("a32a886b-1608-43db-97dc-1fcf104f0f67"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("a589c792-8f91-4865-b1ed-30f4e8cb2dbd"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b7d1aca2-fe9c-4416-9c98-8cb86e72d0ef"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("c0f8b1ec-8332-454b-bc29-b1fa90a2e492"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("c8d64beb-aa58-45d8-aed7-d43f9f91c935"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("cbda1b0a-26b3-4d59-90ff-fffce31f1a33"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("e2e2f95b-c33e-4c41-a5dc-ae41431f0b9c"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("f91a258a-1603-4cb0-8940-b44110099648"));
        }
    }
}
