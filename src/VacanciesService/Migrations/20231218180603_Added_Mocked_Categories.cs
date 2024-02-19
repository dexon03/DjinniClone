using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VacanciesService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Mocked_Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("34220b83-9b28-4a76-997a-dd2e49785cf0"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("812238ec-d562-467e-a7f5-1c7443f9885e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("94b1f01d-4f73-425f-9b9a-d8b6e80f07f7"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("9b970b6d-58bf-4c9f-b981-a3dea199fc84"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("a389d4e0-9972-4691-8cdd-8532ea50d780"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("a8568044-4ba2-4e3c-a92b-4f147480ef12"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("d79e993d-6f88-4849-b3f4-6873193f44c2"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("fbcdd586-418c-4753-b517-d658f9975452"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("16384ec8-5d01-402d-985d-868fc7bb29ec"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("17ed2572-d21b-4828-a23b-a7e5a045fb6c"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2a694ee3-7759-48af-a4d6-faee01f8d4f0"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("49bbab77-1424-473f-94aa-40079102a2d3"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("55802add-009f-4d7f-a374-10366dcdae03"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("6c0c5870-39ff-4306-82bb-7a671ddcf4ec"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("99cd5a5c-1001-4c84-9901-6dc93c6958da"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("ab05729e-1c6c-4a42-b4a1-29ad16a80dfd"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b0e5ec0d-35d3-4f3f-99e0-6037024fa46c"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b261cf52-dc0a-4257-aab0-aa740d625d16"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("d62e1084-bc24-4a05-9a20-a7cbe792efac"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("f7c461f9-5781-45d4-b6cc-054e501c4e83"));

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("072709d0-497a-47f7-a7e6-52244d68a8a0"), "Software Development" },
                    { new Guid("3bb99b5a-43f2-4ada-a772-d7948572f583"), "Design" },
                    { new Guid("ec2f3887-cd8b-4d21-aa97-81e71ff54717"), "Marketing" },
                    { new Guid("effa8ab8-aa35-4b4a-a9f6-8c2a0725ee54"), "Finance" },
                    { new Guid("f7ab8b13-46cc-439e-beda-bbb725b343d2"), "Management" },
                    { new Guid("fb3215b8-2f2b-4c78-8d04-98f1000af0f0"), "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("0066c9d0-6a55-4000-89eb-e9e6e79e77d7"), "Zaporizhzhia", "Ukraine" },
                    { new Guid("0af1e92c-823d-4211-924a-1d3c35799634"), "Vinnytsia", "Ukraine" },
                    { new Guid("62293990-ccee-46dd-a1ea-4c5c3c63ffd8"), "Dnipro", "Ukraine" },
                    { new Guid("83f07cf4-5bd9-495f-90ce-267af40d670a"), "Lviv", "Ukraine" },
                    { new Guid("8b7c091b-1ffe-42d9-af56-2c48766a1f10"), "Kharkiv", "Ukraine" },
                    { new Guid("aa3bc596-d3c9-4432-90d6-0fe2a521bdb7"), "Odesa", "Ukraine" },
                    { new Guid("b4790de2-cafa-491a-a405-3e335738237c"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("ec9cfdfc-337e-4685-b09c-8c9eb1cb6653"), "Kyiv", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10e9ec4e-c654-4176-a37e-386347db2b4a"), "Swift" },
                    { new Guid("1afd648d-c2e7-4f38-88df-a215c724e508"), "Python" },
                    { new Guid("1d4c000e-365e-44b3-91e5-1b46985ae154"), "Ruby" },
                    { new Guid("4be68a1e-e0fb-47b7-a03e-b5c068d8e498"), "Go" },
                    { new Guid("54ec1d50-6717-4da3-85dd-46980905946f"), "C++" },
                    { new Guid("650c1066-114a-4a5b-8d80-a6e6a46001d8"), "Java" },
                    { new Guid("6d763a36-e8b3-400f-880e-2557ce96ed7f"), "Kotlin" },
                    { new Guid("8157ecf7-2f67-40e8-bad4-891600c09a66"), "Scala" },
                    { new Guid("99343c5e-51ae-423a-b126-4f05a8e9814e"), "C#" },
                    { new Guid("adfa1621-7b29-4357-b754-5acfd654fd86"), "TypeScript" },
                    { new Guid("f4c839ea-6f61-4edc-9f48-8789ca4637c4"), "JavaScript" },
                    { new Guid("f6dbb5ea-1cb3-466d-811e-f7e7b4274372"), "PHP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("072709d0-497a-47f7-a7e6-52244d68a8a0"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("3bb99b5a-43f2-4ada-a772-d7948572f583"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("ec2f3887-cd8b-4d21-aa97-81e71ff54717"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("effa8ab8-aa35-4b4a-a9f6-8c2a0725ee54"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("f7ab8b13-46cc-439e-beda-bbb725b343d2"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("fb3215b8-2f2b-4c78-8d04-98f1000af0f0"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("0066c9d0-6a55-4000-89eb-e9e6e79e77d7"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("0af1e92c-823d-4211-924a-1d3c35799634"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("62293990-ccee-46dd-a1ea-4c5c3c63ffd8"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("83f07cf4-5bd9-495f-90ce-267af40d670a"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("8b7c091b-1ffe-42d9-af56-2c48766a1f10"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("aa3bc596-d3c9-4432-90d6-0fe2a521bdb7"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("b4790de2-cafa-491a-a405-3e335738237c"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("ec9cfdfc-337e-4685-b09c-8c9eb1cb6653"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("10e9ec4e-c654-4176-a37e-386347db2b4a"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("1afd648d-c2e7-4f38-88df-a215c724e508"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("1d4c000e-365e-44b3-91e5-1b46985ae154"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("4be68a1e-e0fb-47b7-a03e-b5c068d8e498"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("54ec1d50-6717-4da3-85dd-46980905946f"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("650c1066-114a-4a5b-8d80-a6e6a46001d8"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("6d763a36-e8b3-400f-880e-2557ce96ed7f"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("8157ecf7-2f67-40e8-bad4-891600c09a66"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("99343c5e-51ae-423a-b126-4f05a8e9814e"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("adfa1621-7b29-4357-b754-5acfd654fd86"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("f4c839ea-6f61-4edc-9f48-8789ca4637c4"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("f6dbb5ea-1cb3-466d-811e-f7e7b4274372"));

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("34220b83-9b28-4a76-997a-dd2e49785cf0"), "Kyiv", "Ukraine" },
                    { new Guid("812238ec-d562-467e-a7f5-1c7443f9885e"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("94b1f01d-4f73-425f-9b9a-d8b6e80f07f7"), "Zaporizhzhia", "Ukraine" },
                    { new Guid("9b970b6d-58bf-4c9f-b981-a3dea199fc84"), "Dnipro", "Ukraine" },
                    { new Guid("a389d4e0-9972-4691-8cdd-8532ea50d780"), "Lviv", "Ukraine" },
                    { new Guid("a8568044-4ba2-4e3c-a92b-4f147480ef12"), "Odesa", "Ukraine" },
                    { new Guid("d79e993d-6f88-4849-b3f4-6873193f44c2"), "Kharkiv", "Ukraine" },
                    { new Guid("fbcdd586-418c-4753-b517-d658f9975452"), "Vinnytsia", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("16384ec8-5d01-402d-985d-868fc7bb29ec"), "Scala" },
                    { new Guid("17ed2572-d21b-4828-a23b-a7e5a045fb6c"), "JavaScript" },
                    { new Guid("2a694ee3-7759-48af-a4d6-faee01f8d4f0"), "Go" },
                    { new Guid("49bbab77-1424-473f-94aa-40079102a2d3"), "C#" },
                    { new Guid("55802add-009f-4d7f-a374-10366dcdae03"), "Python" },
                    { new Guid("6c0c5870-39ff-4306-82bb-7a671ddcf4ec"), "PHP" },
                    { new Guid("99cd5a5c-1001-4c84-9901-6dc93c6958da"), "Ruby" },
                    { new Guid("ab05729e-1c6c-4a42-b4a1-29ad16a80dfd"), "Kotlin" },
                    { new Guid("b0e5ec0d-35d3-4f3f-99e0-6037024fa46c"), "C++" },
                    { new Guid("b261cf52-dc0a-4257-aab0-aa740d625d16"), "Java" },
                    { new Guid("d62e1084-bc24-4a05-9a20-a7cbe792efac"), "TypeScript" },
                    { new Guid("f7c461f9-5781-45d4-b6cc-054e501c4e83"), "Swift" }
                });
        }
    }
}
