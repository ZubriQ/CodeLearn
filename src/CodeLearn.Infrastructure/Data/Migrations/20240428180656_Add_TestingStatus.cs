using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_TestingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Test",
                table: "Testing",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Test",
                table: "Testing");
        }
    }
}
