using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Test");

            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindowsAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataType",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTopic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnrolmentYear = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ExerciseType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ClassSolutionCode = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ClassTesterCode = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MethodToExecute = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MethodSolutionCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MethodReturnDataTypeId = table.Column<int>(type: "int", nullable: true),
                    IsMultipleAnswers = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_DataType_MethodReturnDataTypeId",
                        column: x => x.MethodReturnDataTypeId,
                        principalSchema: "Test",
                        principalTable: "DataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Test_TestId",
                        column: x => x.TestId,
                        principalSchema: "Test",
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testing",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    StudentGroupId = table.Column<int>(type: "int", nullable: false),
                    DeadlineDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testing_StudentGroup_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalSchema: "Person",
                        principalTable: "StudentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Testing_Test_TestId",
                        column: x => x.TestId,
                        principalSchema: "Test",
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise2ExerciseTopic",
                schema: "Test",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    ExerciseTopicId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
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
                name: "InputOutputExample",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Output = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputOutputExample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputOutputExample_Exercise_ExerciseId",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    DataTypeId = table.Column<int>(type: "int", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
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
                name: "TestingSession",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestingId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FinishDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingSession_Testing_TestingId",
                        column: x => x.TestingId,
                        principalSchema: "Test",
                        principalTable: "Testing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseParameter",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestCaseId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ExerciseSubmission",
                schema: "Test",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    TestingSessionId = table.Column<int>(type: "int", nullable: false),
                    SentDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SubmissionType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    TestingInformation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RuntimeInMilliseconds = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ExerciseSubmission_TestingSession_TestingSessionId",
                        column: x => x.TestingSessionId,
                        principalSchema: "Test",
                        principalTable: "TestingSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelectedChoice",
                schema: "Test",
                columns: table => new
                {
                    ExerciseSubmissionId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionChoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedChoice", x => new { x.ExerciseSubmissionId, x.QuestionChoiceId });
                    table.ForeignKey(
                        name: "FK_SelectedChoice_ExerciseSubmission_ExerciseSubmissionId",
                        column: x => x.ExerciseSubmissionId,
                        principalSchema: "Test",
                        principalTable: "ExerciseSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedChoice_QuestionChoice_QuestionChoiceId",
                        column: x => x.QuestionChoiceId,
                        principalSchema: "Test",
                        principalTable: "QuestionChoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_MethodReturnDataTypeId",
                schema: "Test",
                table: "Exercise",
                column: "MethodReturnDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TestId",
                schema: "Test",
                table: "Exercise",
                column: "TestId");

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
                name: "IX_ExerciseSubmission_TestingSessionId",
                schema: "Test",
                table: "ExerciseSubmission",
                column: "TestingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTopic_Name",
                schema: "Test",
                table: "ExerciseTopic",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InputOutputExample_ExerciseId",
                schema: "Test",
                table: "InputOutputExample",
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
                name: "IX_SelectedChoice_QuestionChoiceId",
                schema: "Test",
                table: "SelectedChoice",
                column: "QuestionChoiceId");

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
                name: "IX_Testing_StudentGroupId",
                schema: "Test",
                table: "Testing",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Testing_TestId",
                schema: "Test",
                table: "Testing",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingSession_Status",
                schema: "Test",
                table: "TestingSession",
                column: "Status");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Exercise2ExerciseTopic",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "ExerciseNote",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "InputOutputExample",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "MethodParameter",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "SelectedChoice",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "TestCaseParameter",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExerciseTopic",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "ExerciseSubmission",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "QuestionChoice",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "TestCase",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "TestingSession",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "Exercise",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "Testing",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "DataType",
                schema: "Test");

            migrationBuilder.DropTable(
                name: "StudentGroup",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Test",
                schema: "Test");
        }
    }
}
