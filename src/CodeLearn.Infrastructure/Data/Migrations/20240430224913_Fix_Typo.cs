using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SolvedExerecisesCount",
                schema: "Test",
                table: "TestingSession",
                newName: "SolvedExercisesCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SolvedExercisesCount",
                schema: "Test",
                table: "TestingSession",
                newName: "SolvedExerecisesCount");
        }
    }
}
