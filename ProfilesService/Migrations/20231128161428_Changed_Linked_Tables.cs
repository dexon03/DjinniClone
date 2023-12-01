using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProfilesService.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Linked_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileSkills",
                table: "ProfileSkills");

            migrationBuilder.DropIndex(
                name: "IX_ProfileSkills_SkillId",
                table: "ProfileSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationProfile",
                table: "LocationProfile");

            migrationBuilder.DropIndex(
                name: "IX_LocationProfile_LocationId",
                table: "LocationProfile");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProfileSkills");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocationProfile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileSkills",
                table: "ProfileSkills",
                columns: new[] { "SkillId", "ProfileId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationProfile",
                table: "LocationProfile",
                columns: new[] { "LocationId", "ProfileId" });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("029ba782-ef06-448d-b6fc-617fd07ee08f"), "Vinnytsia", "Ukraine" },
                    { new Guid("41d836f2-524c-4a71-945a-5059bb1ca4e7"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("781b9ad4-f59e-4aac-8ec8-ef8ade4aa827"), "Zaporizhzhia", "Ukraine" },
                    { new Guid("7acc36ad-846b-4cd7-b940-dc439feb1f97"), "Odesa", "Ukraine" },
                    { new Guid("d404270a-5073-4336-9e49-b4b2d3f7309e"), "Dnipro", "Ukraine" },
                    { new Guid("dfb6a93e-833a-4da0-a539-73d8906426bf"), "Kharkiv", "Ukraine" },
                    { new Guid("e3177bb0-8e9d-4f84-9697-1f038ffe3980"), "Lviv", "Ukraine" },
                    { new Guid("e5f5ccc5-dce9-45b3-8dc1-ea8cc8d5ebd9"), "Kyiv", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("06c84152-dfe1-4271-b160-024196e11b72"), "Go" },
                    { new Guid("0c33a455-90af-4c61-a26c-c95311e3b75d"), "Java" },
                    { new Guid("2f3e2580-13af-4cf6-ad0d-eb8de70b7979"), "JavaScript" },
                    { new Guid("43c90170-2c8d-4c84-b69d-75e93df15601"), "Swift" },
                    { new Guid("8ed1fe43-d2d8-43f6-9d88-2eeddc2651e9"), "C#" },
                    { new Guid("b1dc1406-97bf-41aa-965c-1ef5c27e127e"), "Python" },
                    { new Guid("cc7e00bd-0ddf-4363-b013-8b56677637e2"), "TypeScript" },
                    { new Guid("d22747b6-990e-4cfb-a8e6-c8041d5647f7"), "PHP" },
                    { new Guid("d55e867c-686d-4c88-99bb-4a15dbb51ed6"), "Scala" },
                    { new Guid("d903cce8-111b-4251-8717-998a75ef6e87"), "Kotlin" },
                    { new Guid("e1e89a1a-e699-4d4d-bb2a-7da454b0afe8"), "C++" },
                    { new Guid("e5c8379c-c3e7-4b89-b2e7-f03339de4573"), "Ruby" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileSkills",
                table: "ProfileSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationProfile",
                table: "LocationProfile");

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("029ba782-ef06-448d-b6fc-617fd07ee08f"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("41d836f2-524c-4a71-945a-5059bb1ca4e7"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("781b9ad4-f59e-4aac-8ec8-ef8ade4aa827"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("7acc36ad-846b-4cd7-b940-dc439feb1f97"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("d404270a-5073-4336-9e49-b4b2d3f7309e"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("dfb6a93e-833a-4da0-a539-73d8906426bf"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("e3177bb0-8e9d-4f84-9697-1f038ffe3980"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("e5f5ccc5-dce9-45b3-8dc1-ea8cc8d5ebd9"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("06c84152-dfe1-4271-b160-024196e11b72"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("0c33a455-90af-4c61-a26c-c95311e3b75d"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2f3e2580-13af-4cf6-ad0d-eb8de70b7979"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("43c90170-2c8d-4c84-b69d-75e93df15601"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("8ed1fe43-d2d8-43f6-9d88-2eeddc2651e9"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b1dc1406-97bf-41aa-965c-1ef5c27e127e"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("cc7e00bd-0ddf-4363-b013-8b56677637e2"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("d22747b6-990e-4cfb-a8e6-c8041d5647f7"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("d55e867c-686d-4c88-99bb-4a15dbb51ed6"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("d903cce8-111b-4251-8717-998a75ef6e87"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("e1e89a1a-e699-4d4d-bb2a-7da454b0afe8"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("e5c8379c-c3e7-4b89-b2e7-f03339de4573"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProfileSkills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "LocationProfile",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileSkills",
                table: "ProfileSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationProfile",
                table: "LocationProfile",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSkills_SkillId",
                table: "ProfileSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationProfile_LocationId",
                table: "LocationProfile",
                column: "LocationId");
        }
    }
}
