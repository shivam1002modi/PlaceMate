using Microsoft.EntityFrameworkCore.Migrations;

namespace PlaceMate.Migrations
{
    public partial class AddStructuredExperienceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "InterviewExperiences");

            migrationBuilder.AlterColumn<string>(
                name: "JobRole",
                table: "InterviewExperiences",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HRQuestions",
                table: "InterviewExperiences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InterviewDifficulty",
                table: "InterviewExperiences",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalQuestions",
                table: "InterviewExperiences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipsForJuniors",
                table: "InterviewExperiences",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HRQuestions",
                table: "InterviewExperiences");

            migrationBuilder.DropColumn(
                name: "InterviewDifficulty",
                table: "InterviewExperiences");

            migrationBuilder.DropColumn(
                name: "TechnicalQuestions",
                table: "InterviewExperiences");

            migrationBuilder.DropColumn(
                name: "TipsForJuniors",
                table: "InterviewExperiences");

            migrationBuilder.AlterColumn<string>(
                name: "JobRole",
                table: "InterviewExperiences",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InterviewExperiences",
                type: "text",
                nullable: true);
        }
    }
}
