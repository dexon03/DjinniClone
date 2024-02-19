using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProfilesService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Attendace_To_Candidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "GitHubUrl",
                table: "RecruiterProfile");

            migrationBuilder.AddColumn<int>(
                name: "Attendance",
                table: "CandidateProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City", "Country" },
                values: new object[,]
                {
                    { new Guid("0825654e-5a60-4ffe-aeef-9de14835d837"), "Kharkiv", "Ukraine" },
                    { new Guid("2db784a7-29ea-4737-9248-a6ba7ce29d68"), "Lviv", "Ukraine" },
                    { new Guid("6cefe5b8-c2fc-4d42-ac7e-f834a2378b5d"), "Odesa", "Ukraine" },
                    { new Guid("882fa9e9-125a-4f1a-b437-5c96aedc9285"), "Kyiv", "Ukraine" },
                    { new Guid("8a4db610-f094-4b06-9921-3f548bf76906"), "Vinnytsia", "Ukraine" },
                    { new Guid("e59d3acd-9c6f-4068-9bc6-a0ddecb7860c"), "Dnipro", "Ukraine" },
                    { new Guid("e633e5cc-8fb2-4426-91ae-c16c8257781f"), "Khmelnytskyi", "Ukraine" },
                    { new Guid("f6ebfca2-c3b0-4fbe-b61f-c78f6dba94fe"), "Zaporizhzhia", "Ukraine" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2bcbe678-5ffc-445b-953a-1316d729d8f3"), "Swift" },
                    { new Guid("57770e1d-1e3f-4445-b50c-db8353a60309"), "Java" },
                    { new Guid("60ab2ed7-7168-451b-9d53-d87903afbb05"), "C#" },
                    { new Guid("723f094c-3c53-4291-b45a-7179d52eb46f"), "Python" },
                    { new Guid("896bcbe4-4f3e-42e7-b882-ad67f4a49f66"), "C++" },
                    { new Guid("9e33b3d1-4739-4449-ae79-983b7e9b475a"), "Go" },
                    { new Guid("adbde482-7f84-4473-aa8b-3eff7be40977"), "Kotlin" },
                    { new Guid("b516de12-1042-4c7b-bca3-7278fe5beb12"), "Scala" },
                    { new Guid("b6b2e9fd-9e5c-488d-ad3c-67ab1c60fa90"), "JavaScript" },
                    { new Guid("cf18f2f7-8e66-4afb-885d-6b8698bba8c1"), "Ruby" },
                    { new Guid("d7159815-58c9-4850-af0b-cfd047e2b327"), "PHP" },
                    { new Guid("dd18ae8d-7699-4176-8b9f-29656ff91dd0"), "TypeScript" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("0825654e-5a60-4ffe-aeef-9de14835d837"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("2db784a7-29ea-4737-9248-a6ba7ce29d68"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("6cefe5b8-c2fc-4d42-ac7e-f834a2378b5d"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("882fa9e9-125a-4f1a-b437-5c96aedc9285"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("8a4db610-f094-4b06-9921-3f548bf76906"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("e59d3acd-9c6f-4068-9bc6-a0ddecb7860c"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("e633e5cc-8fb2-4426-91ae-c16c8257781f"));

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: new Guid("f6ebfca2-c3b0-4fbe-b61f-c78f6dba94fe"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("2bcbe678-5ffc-445b-953a-1316d729d8f3"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("57770e1d-1e3f-4445-b50c-db8353a60309"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("60ab2ed7-7168-451b-9d53-d87903afbb05"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("723f094c-3c53-4291-b45a-7179d52eb46f"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("896bcbe4-4f3e-42e7-b882-ad67f4a49f66"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("9e33b3d1-4739-4449-ae79-983b7e9b475a"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("adbde482-7f84-4473-aa8b-3eff7be40977"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b516de12-1042-4c7b-bca3-7278fe5beb12"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("b6b2e9fd-9e5c-488d-ad3c-67ab1c60fa90"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("cf18f2f7-8e66-4afb-885d-6b8698bba8c1"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("d7159815-58c9-4850-af0b-cfd047e2b327"));

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: new Guid("dd18ae8d-7699-4176-8b9f-29656ff91dd0"));

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "CandidateProfile");

            migrationBuilder.AddColumn<string>(
                name: "GitHubUrl",
                table: "RecruiterProfile",
                type: "text",
                nullable: true);

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
    }
}
