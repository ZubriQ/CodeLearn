using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_ReturnDataTypeFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_DataType_MethodReturnTypeId",
                schema: "Test",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "MethodReturnTypeId",
                schema: "Test",
                table: "Exercise",
                newName: "MethodReturnDataTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_MethodReturnTypeId",
                schema: "Test",
                table: "Exercise",
                newName: "IX_Exercise_MethodReturnDataTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_DataType_MethodReturnDataTypeId",
                schema: "Test",
                table: "Exercise",
                column: "MethodReturnDataTypeId",
                principalSchema: "Test",
                principalTable: "DataType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_DataType_MethodReturnDataTypeId",
                schema: "Test",
                table: "Exercise");

            migrationBuilder.RenameColumn(
                name: "MethodReturnDataTypeId",
                schema: "Test",
                table: "Exercise",
                newName: "MethodReturnTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_MethodReturnDataTypeId",
                schema: "Test",
                table: "Exercise",
                newName: "IX_Exercise_MethodReturnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_DataType_MethodReturnTypeId",
                schema: "Test",
                table: "Exercise",
                column: "MethodReturnTypeId",
                principalSchema: "Test",
                principalTable: "DataType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
