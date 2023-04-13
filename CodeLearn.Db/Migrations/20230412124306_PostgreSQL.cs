using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeLearn.Db.Migrations
{
    public partial class PostgreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Context = table.Column<string>(type: "text", nullable: false),
                    CodingArea = table.Column<string>(type: "text", nullable: true),
                    ExerciseTypeId = table.Column<int>(type: "integer", nullable: false),
                    OptionalUsings = table.Column<string>(type: "text", nullable: true),
                    OptionalDlls = table.Column<string>(type: "text", nullable: true),
                    ClassName = table.Column<string>(type: "text", nullable: true),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_ExerciseTypes_ExerciseTypeId",
                        column: x => x.ExerciseTypeId,
                        principalTable: "ExerciseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<int>(type: "integer", nullable: true),
                    IsTeacher = table.Column<bool>(type: "boolean", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestMethodInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ReturnTypeId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMethodInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestMethodInfos_DataTypes_ReturnTypeId",
                        column: x => x.ReturnTypeId,
                        principalTable: "DataTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestMethodInfos_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "Testings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    TestCreatorId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testings_AspNetUsers_TestCreatorId",
                        column: x => x.TestCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Result = table.Column<string>(type: "text", nullable: false),
                    TestMethodId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCases_TestMethodInfos_TestMethodId",
                        column: x => x.TestMethodId,
                        principalTable: "TestMethodInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestMethodParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataTypeId = table.Column<int>(type: "integer", nullable: false),
                    TestMethodInfoId = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestMethodParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestMethodParameters_DataTypes_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "DataTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestMethodParameters_TestMethodInfos_TestMethodInfoId",
                        column: x => x.TestMethodInfoId,
                        principalTable: "TestMethodInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTesting",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    ExercisesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTesting", x => new { x.CoursesId, x.ExercisesId });
                    table.ForeignKey(
                        name: "FK_ExerciseTesting_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTesting_Testings_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Testings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestingResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestingId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    PassingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingResults_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestingResults_Testings_TestingId",
                        column: x => x.TestingId,
                        principalTable: "Testings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestCaseId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCaseParameters_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestingAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    TestingResultId = table.Column<int>(type: "integer", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    FailureInfo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestingAnswers_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestingAnswers_TestingResults_TestingResultId",
                        column: x => x.TestingResultId,
                        principalTable: "TestingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "GroupId", "IsTeacher", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6d7b1efe-cd56-48c3-9ab6-7e4d0205851a", 0, "e3e73baa-cc33-46c5-840f-767227db1984", "Teacher", "teacher@example.com", true, "teacherFirstName", null, true, "teacherLastName", true, null, "TEACHER@EXAMPLE.COM", "TEACHER", "AQAAAAEAACcQAAAAELwTchSiLt+9YIun7zirkEREX+EwQRpplg7sjj+GYYZfYMTd5S56zKbuXIe5i/zzTQ==", null, null, false, "d06ed00a-83dd-4b5d-9257-4804c6638c0f", false, "teacher" });

            migrationBuilder.InsertData(
                table: "DataTypes",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Void", "void" },
                    { 2, "System.Boolean", "bool" },
                    { 3, "System.Byte", "byte" },
                    { 4, "System.SByte", "sbyte" },
                    { 5, "System.Char", "char" },
                    { 6, "System.Decimal", "decimal" },
                    { 7, "System.Double", "double" },
                    { 8, "System.Single", "float" },
                    { 9, "System.Int32", "int" },
                    { 10, "System.UInt32", "uint" },
                    { 11, "System.Int64", "long" },
                    { 12, "System.UInt64", "ulong" },
                    { 13, "System.Int16", "short" },
                    { 14, "System.UInt16", "ushort" },
                    { 15, "System.IntPtr", "nint" },
                    { 16, "System.Object", "object" },
                    { 17, "System.String", "string" },
                    { 18, "System.Object", "dynamic" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Method coding" },
                    { 2, "Class coding" },
                    { 3, "Question" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "Year" },
                values: new object[] { 1, "Test group", 2023 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "GroupId", "IsTeacher", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "06fb23b5-d1b7-4b1a-88ce-de8b3d07b590", 0, "6da48604-7c2e-4524-aeb1-8c69809ab409", "Student", "student@example.com", true, "studentFirstName", 1, true, "studentLastName", true, null, "STUDENT@EXAMPLE.COM", "STUDENT", "AQAAAAEAACcQAAAAEG43UdrAxe8NZABigo5Y1obxYM5ofLd0is5whEfpudxLM4RVsFDgO4JqnKHcqhdW0w==", null, null, false, "5bb2d5a8-9d8f-498e-b9aa-f99d940ce3a0", false, "student" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "ClassName", "CodingArea", "Context", "Description", "ExerciseTypeId", "OptionalDlls", "OptionalUsings", "Score", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "TestClass", "public static double GetArea(double a, double b)\n{\n    // Example\n    return a * b;\n}", "// tests example\nstatic void Main()\n{\n Console.WriteLine(GetArea(6, 6));\n Console.WriteLine(GetArea(7.5, 10));\n Console.WriteLine(GetArea(1, 5));\n}", "Напишите тело метода GetArea так, чтобы оно возвращало площадь прямоугольника. Можете считать, что параметры ''''a'''' и ''''b'''' всегда положительные числа.", 1, null, null, 1, "Вычисление площади" },
                    { 2, "TestClass", "public static double GetNumber(long a)\n{\n    // example\n    return --a;\n}", "// example\nGetNumber(3);\nGetNumber(75);\nGetNumber(100);", "Для простого примера, выведите число, которой будет на 1 меньше.", 1, null, null, 1, "Простой вывод" }
                });

            migrationBuilder.InsertData(
                table: "Testings",
                columns: new[] { "Id", "Description", "DurationInMinutes", "Name", "TestCreatorId" },
                values: new object[] { 1, "Простой пример теста по программированию на языке C#", 50, "Тест", "6d7b1efe-cd56-48c3-9ab6-7e4d0205851a" });

            migrationBuilder.InsertData(
                table: "ExerciseTesting",
                columns: new[] { "CoursesId", "ExercisesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "TestMethodInfos",
                columns: new[] { "Id", "ExerciseId", "Name", "ReturnTypeId" },
                values: new object[,]
                {
                    { 1, 1, "GetArea", 7 },
                    { 2, 2, "GetNumber", 11 }
                });

            migrationBuilder.InsertData(
                table: "TestCases",
                columns: new[] { "Id", "Result", "TestMethodId" },
                values: new object[,]
                {
                    { 1, "9", 1 },
                    { 2, "10", 1 },
                    { 3, "5", 2 },
                    { 4, "8", 2 }
                });

            migrationBuilder.InsertData(
                table: "TestMethodParameters",
                columns: new[] { "Id", "DataTypeId", "Position", "TestMethodInfoId" },
                values: new object[,]
                {
                    { 1, 7, 0, 1 },
                    { 2, 7, 1, 1 },
                    { 3, 11, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "TestCaseParameters",
                columns: new[] { "Id", "Position", "TestCaseId", "Value" },
                values: new object[,]
                {
                    { 1, 0, 1, "3" },
                    { 2, 1, 1, "3" },
                    { 3, 0, 2, "5" },
                    { 4, 1, 2, "2" },
                    { 5, 0, 3, "6" },
                    { 6, 0, 4, "9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ExerciseTypeId",
                table: "Exercises",
                column: "ExerciseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTesting_ExercisesId",
                table: "ExerciseTesting",
                column: "ExercisesId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseParameters_TestCaseId",
                table: "TestCaseParameters",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_TestMethodId",
                table: "TestCases",
                column: "TestMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingAnswers_ExerciseId",
                table: "TestingAnswers",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingAnswers_TestingResultId",
                table: "TestingAnswers",
                column: "TestingResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingResults_StudentId",
                table: "TestingResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestingResults_TestingId",
                table: "TestingResults",
                column: "TestingId");

            migrationBuilder.CreateIndex(
                name: "IX_Testings_TestCreatorId",
                table: "Testings",
                column: "TestCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestMethodInfos_ExerciseId",
                table: "TestMethodInfos",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestMethodInfos_ReturnTypeId",
                table: "TestMethodInfos",
                column: "ReturnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestMethodParameters_DataTypeId",
                table: "TestMethodParameters",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestMethodParameters_TestMethodInfoId",
                table: "TestMethodParameters",
                column: "TestMethodInfoId");
        }

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
                name: "ExerciseTesting");

            migrationBuilder.DropTable(
                name: "TestCaseParameters");

            migrationBuilder.DropTable(
                name: "TestingAnswers");

            migrationBuilder.DropTable(
                name: "TestMethodParameters");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "TestingResults");

            migrationBuilder.DropTable(
                name: "TestMethodInfos");

            migrationBuilder.DropTable(
                name: "Testings");

            migrationBuilder.DropTable(
                name: "DataTypes");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExerciseTypes");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
