using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VacanciesService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Index_To_Rec_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("120a6313-946c-4189-9672-269d5f702e36"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("21b34254-569c-4783-a94a-4a032997807f"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("525660ec-4a96-4059-8217-0c2e438f2e1e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("52faf63e-62a4-40f3-b21e-3118535bff3e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("8c9458ed-cffe-4e6f-a7e7-f449f35c491a"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("97711ed9-52d5-4690-80cb-c93206f3efb2"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("c1bb0135-93d7-4a8e-a34d-6238af0676c8"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("eb6745e9-f29d-4640-a88f-c91057a79e28"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("078efeb8-129a-4c87-a4b2-85a151af7c14"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("1fa78f35-918e-4244-a24f-25029b997c03"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("792299fe-0394-4a52-a0f8-72114a764976"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("8bef3018-9417-424e-970b-4f8029ecadbd"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("8e1b3732-4039-44f5-9431-a128173d4a7f"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("9dd0d536-a120-4f2e-bc37-f65e08f436be"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b5d51f7d-5352-4ded-af8c-484b9b8f3d60"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b713e7e3-ad4e-41d8-8f3a-5401da80f889"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("bf962382-9bd2-4fb8-8afd-4203422abacc"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("c8494c38-55a9-4784-b727-6ed4d3b53ceb"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("df8ed035-ed2e-41eb-907a-2f3d1f1e7abf"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("e026f57a-cbfd-44fb-b868-9d3c58c727dc"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_RecruiterId",
                table: "Vacancy",
                column: "RecruiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vacancy_RecruiterId",
                table: "Vacancy");

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
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("120a6313-946c-4189-9672-269d5f702e36"), "Lviv", "Ukraine" },
                    { new Guid("21b34254-569c-4783-a94a-4a032997807f"), "Zaporizhzhia", "Ukraine" },
                    { new Guid("525660ec-4a96-4059-8217-0c2e438f2e1e"), "Dnipro", "Ukraine" },
                    { new Guid("52faf63e-62a4-40f3-b21e-3118535bff3e"), "Kyiv", "Ukraine" },
                    { new Guid("8c9458ed-cffe-4e6f-a7e7-f449f35c491a"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("97711ed9-52d5-4690-80cb-c93206f3efb2"), "Kharkiv", "Ukraine" },
                    { new Guid("c1bb0135-93d7-4a8e-a34d-6238af0676c8"), "Vinnytsia", "Ukraine" },
                    { new Guid("eb6745e9-f29d-4640-a88f-c91057a79e28"), "Odesa", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("078efeb8-129a-4c87-a4b2-85a151af7c14"), "JavaScript" },
                    { new Guid("1fa78f35-918e-4244-a24f-25029b997c03"), "Kotlin" },
                    { new Guid("792299fe-0394-4a52-a0f8-72114a764976"), "C++" },
                    { new Guid("8bef3018-9417-424e-970b-4f8029ecadbd"), "Python" },
                    { new Guid("8e1b3732-4039-44f5-9431-a128173d4a7f"), "Go" },
                    { new Guid("9dd0d536-a120-4f2e-bc37-f65e08f436be"), "Scala" },
                    { new Guid("b5d51f7d-5352-4ded-af8c-484b9b8f3d60"), "PHP" },
                    { new Guid("b713e7e3-ad4e-41d8-8f3a-5401da80f889"), "Ruby" },
                    { new Guid("bf962382-9bd2-4fb8-8afd-4203422abacc"), "Java" },
                    { new Guid("c8494c38-55a9-4784-b727-6ed4d3b53ceb"), "Swift" },
                    { new Guid("df8ed035-ed2e-41eb-907a-2f3d1f1e7abf"), "TypeScript" },
                    { new Guid("e026f57a-cbfd-44fb-b868-9d3c58c727dc"), "C#" }
                });
        }
    }
}
