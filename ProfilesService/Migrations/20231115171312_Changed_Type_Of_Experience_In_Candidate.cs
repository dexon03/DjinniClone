using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfilesService.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Type_Of_Experience_In_Candidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WorkExperience",
                table: "CandidateProfile",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WorkExperience",
                table: "CandidateProfile",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
