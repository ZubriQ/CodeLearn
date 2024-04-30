using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TestingSessionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                schema: "Test",
                table: "TestingSession",
                newName: "SolvedExerecisesCount");

            migrationBuilder.AddColumn<int>(
                name: "CorrectQuestionsCount",
                schema: "Test",
                table: "TestingSession",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentFeedback",
                schema: "Test",
                table: "TestingSession",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectQuestionsCount",
                schema: "Test",
                table: "TestingSession");

            migrationBuilder.DropColumn(
                name: "StudentFeedback",
                schema: "Test",
                table: "TestingSession");

            migrationBuilder.RenameColumn(
                name: "SolvedExerecisesCount",
                schema: "Test",
                table: "TestingSession",
                newName: "Score");
        }
    }
}
