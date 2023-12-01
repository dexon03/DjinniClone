using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VacanciesService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Mpcked_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("1b85f311-d2c4-4d0c-b832-3192e55499b6"), "Dnipro", "Ukraine" },
                    { new Guid("22e3774e-5a3d-47e8-ae0b-189daecf826c"), "Lviv", "Ukraine" },
                    { new Guid("53c9f6c5-7746-4eb5-99fb-8292293b803e"), "Zaporizhzhia", "Ukraine" },
                    { new Guid("921c4b14-65ea-46c0-bf15-9512458946ab"), "Vinnytsia", "Ukraine" },
                    { new Guid("9eb1f622-e6f4-4dc4-a4d4-24fecdfd56e7"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("c880c605-981e-46eb-a410-f0ccb5b4fb12"), "Kharkiv", "Ukraine" },
                    { new Guid("e31affbd-8bfd-4528-aa93-b7ed6ee72f7f"), "Odesa", "Ukraine" },
                    { new Guid("fa9d8524-3eb0-4e6f-9af8-00d3dc1910aa"), "Kyiv", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("14ae68eb-5d01-4dbf-bd27-af55653055e4"), "Kotlin" },
                    { new Guid("23371813-5fa7-4be4-9a88-6f9222c2e1ef"), "C#" },
                    { new Guid("2c5c8af6-a697-4f6d-9fe7-20d223ae3756"), "JavaScript" },
                    { new Guid("3a126c43-065e-429b-a808-ec81e956ec38"), "PHP" },
                    { new Guid("45cfaa73-c324-4104-8212-5c36c33cd504"), "TypeScript" },
                    { new Guid("57d76ee3-6837-4137-93df-9089f0e74a39"), "Go" },
                    { new Guid("640eb86f-ecfd-4917-8dfa-cb6f48066f06"), "Ruby" },
                    { new Guid("8841e9c4-00b0-499b-8a7b-bb609ce4951e"), "Python" },
                    { new Guid("a53aa54d-1f3c-4ddc-b9d7-6da58881183d"), "Java" },
                    { new Guid("b4b26b30-1de6-4cb9-9092-7fb26ffe6cb6"), "Scala" },
                    { new Guid("cefca230-956d-42ae-bf8f-65bb39c25562"), "C++" },
                    { new Guid("ee7cd2f6-fc59-4c03-a5f1-ecd45d54f45d"), "Swift" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("1b85f311-d2c4-4d0c-b832-3192e55499b6"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("22e3774e-5a3d-47e8-ae0b-189daecf826c"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("53c9f6c5-7746-4eb5-99fb-8292293b803e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("921c4b14-65ea-46c0-bf15-9512458946ab"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("9eb1f622-e6f4-4dc4-a4d4-24fecdfd56e7"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("c880c605-981e-46eb-a410-f0ccb5b4fb12"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("e31affbd-8bfd-4528-aa93-b7ed6ee72f7f"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("fa9d8524-3eb0-4e6f-9af8-00d3dc1910aa"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("14ae68eb-5d01-4dbf-bd27-af55653055e4"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("23371813-5fa7-4be4-9a88-6f9222c2e1ef"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2c5c8af6-a697-4f6d-9fe7-20d223ae3756"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("3a126c43-065e-429b-a808-ec81e956ec38"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("45cfaa73-c324-4104-8212-5c36c33cd504"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("57d76ee3-6837-4137-93df-9089f0e74a39"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("640eb86f-ecfd-4917-8dfa-cb6f48066f06"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("8841e9c4-00b0-499b-8a7b-bb609ce4951e"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("a53aa54d-1f3c-4ddc-b9d7-6da58881183d"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b4b26b30-1de6-4cb9-9092-7fb26ffe6cb6"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("cefca230-956d-42ae-bf8f-65bb39c25562"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("ee7cd2f6-fc59-4c03-a5f1-ecd45d54f45d"));
        }
    }
}
