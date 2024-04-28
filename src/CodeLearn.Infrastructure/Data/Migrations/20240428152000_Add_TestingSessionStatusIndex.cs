using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TestingSessionStatusIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SentDateTime",
                schema: "Test",
                table: "ExerciseSubmission",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_TestingSession_Status",
                schema: "Test",
                table: "TestingSession",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestingSession_Status",
                schema: "Test",
                table: "TestingSession");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentDateTime",
                schema: "Test",
                table: "ExerciseSubmission",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
