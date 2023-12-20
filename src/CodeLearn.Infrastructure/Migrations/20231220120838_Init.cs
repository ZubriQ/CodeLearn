using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Test");

            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "DataType",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTopic",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTopic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_StudentGroup_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalSchema: "Test",
                        principalTable: "StudentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testing",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testing_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "Person",
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ExerciseType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ClassSolutionCode = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ClassTesterCode = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MethodToExecute = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MethodSolutionCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MethodReturnTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMultipleAnswers = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_DataType_MethodReturnTypeId",
                        column: x => x.MethodReturnTypeId,
                        principalSchema: "Test",
                        principalTable: "DataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Testing_TestingId",
                        column: x => x.TestingId,
                        principalSchema: "Test",
                        principalTable: "Testing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestingSession",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingSession_Student_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "Test",
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestingSession_Testing_TestingId",
                        column: x => x.TestingId,
                        principalSchema: "Test",
                        principalTable: "Testing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercise2ExerciseTopic",
                schema: "Test",
                columns: table => new
                {
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise2ExerciseTopic", x => new { x.ExerciseId, x.ExerciseTopicId });
                    table.ForeignKey(
                        name: "FK_Exercise2ExerciseTopic_ExerciseTopic_ExerciseTopicId",
                        column: x => x.ExerciseTopicId,
                        principalSchema: "Test",
                        principalTable: "ExerciseTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise2ExerciseTopic_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "Test",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseNote",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Entry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Decoration = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseNote_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "Test",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSubmission",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    SubmissionType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    TestingInformation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSubmission_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "Test",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MethodParameter",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodParameter_DataType_DataTypeId",
                        column: x => x.DataTypeId,
                        principalSchema: "Test",
                        principalTable: "DataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MethodParameter_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "Test",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionChoice",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionChoice_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "Test",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCase",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectOutputValue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCase_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "Test",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseParameter",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestCaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCaseParameter_TestCase_TestCaseId",
                        column: x => x.TestCaseId,
                        principalSchema: "Test",
                        principalTable: "TestCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_MethodReturnTypeId",
                schema: "Test",
                table: "Exercise",
                column: "MethodReturnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TestingId",
                schema: "Test",
                table: "Exercise",
                column: "TestingId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise2ExerciseTopic_ExerciseTopicId",
                schema: "Test",
                table: "Exercise2ExerciseTopic",
                column: "ExerciseTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseNote_ExerciseId",
                schema: "Test",
                table: "ExerciseNote",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSubmission_ExerciseId",
                schema: "Test",
                table: "ExerciseSubmission",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodParameter_DataTypeId",
                schema: "Test",
                table: "MethodParameter",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodParameter_ExerciseId",
                schema: "Test",
                table: "MethodParameter",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionChoice_ExerciseId",
                schema: "Test",
                table: "QuestionChoice",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentGroupId",
                schema: "Test",
                table: "Student",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_ExerciseId",
                schema: "Test",
                table: "TestCase",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseParameter_TestCaseId",
                schema: "Test",
                table: "TestCaseParameter",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Testing_TeacherId",
                schema: "Test",
                table: "Testing",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingSession_StudentId",
                schema: "Test",
                table: "TestingSession",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingSession_TestingId",
                schema: "Test",
                table: "TestingSession",
                column: "TestingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercise2ExerciseTopic",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "ExerciseNote",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "ExerciseSubmission",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "MethodParameter",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "QuestionChoice",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "TestCaseParameter",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "TestingSession",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "ExerciseTopic",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "TestCase",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "Student",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "Exercise",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "StudentGroup",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "DataType",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "Testing",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "Teacher",
                schema: "Person");
        }
    }
}
