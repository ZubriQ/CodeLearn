using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Db.Migrations
{
    public partial class AspNet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    IsTeacher = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "data_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    short_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exercise_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
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
                        principalColumn: "id",
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
                        principalColumn: "id",
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
                        principalColumn: "id",
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_AspNetUsers_id",
                        column: x => x.id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "teacher",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher", x => x.id);
                    table.ForeignKey(
                        name: "FK_teacher_AspNetUsers_id",
                        column: x => x.id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "exercise",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    context = table.Column<string>(type: "text", nullable: false),
                    coding_area = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    exercise_type_id = table.Column<int>(type: "int", nullable: false),
                    optional_usings = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    optional_dlls = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    class_name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    score = table.Column<int>(type: "int", nullable: false),
                    short_description = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise", x => x.id);
                    table.ForeignKey(
                        name: "FK_Exercises_ExerciseTypes",
                        column: x => x.exercise_type_id,
                        principalTable: "exercise_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    duration_in_minutes = table.Column<int>(type: "int", nullable: false),
                    test_creator_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testing", x => x.id);
                    table.ForeignKey(
                        name: "FK_testing_teacher",
                        column: x => x.test_creator_id,
                        principalTable: "teacher",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "test_method_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    return_type_id = table.Column<int>(type: "int", nullable: false),
                    exercise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_method_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_test_method_info_exercise1",
                        column: x => x.exercise_id,
                        principalTable: "exercise",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TestMethodsInfo_DataTypes",
                        column: x => x.return_type_id,
                        principalTable: "data_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testing_exercise",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false),
                    exercise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testing_exercise", x => new { x.course_id, x.exercise_id });
                    table.ForeignKey(
                        name: "FK_testing_exercise_exercise",
                        column: x => x.exercise_id,
                        principalTable: "exercise",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_testing_exercise_testing",
                        column: x => x.course_id,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testing_result",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    testing_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    passing_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testing_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_testing_result_student",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_testing_result_testing",
                        column: x => x.testing_id,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "test_cases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    result = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    test_method_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_cases", x => x.id);
                    table.ForeignKey(
                        name: "FK_test_case_test_method_info",
                        column: x => x.test_method_id,
                        principalTable: "test_method_info",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "test_method_parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_type_id = table.Column<int>(type: "int", nullable: false),
                    test_method_info_id = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_method_parameters", x => x.id);
                    table.ForeignKey(
                        name: "FK_ParametersTestMethods_DataTypes",
                        column: x => x.data_type_id,
                        principalTable: "data_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ParametersTestMethods_TestMethodsInfo",
                        column: x => x.test_method_info_id,
                        principalTable: "test_method_info",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testing_answer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exercise_id = table.Column<int>(type: "int", nullable: false),
                    testing_result_id = table.Column<int>(type: "int", nullable: false),
                    answer = table.Column<string>(type: "text", nullable: true),
                    is_correct = table.Column<bool>(type: "bit", nullable: false),
                    failure_info = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testing_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_testing_answer_exercise",
                        column: x => x.exercise_id,
                        principalTable: "exercise",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_testing_answer_testing_result",
                        column: x => x.testing_result_id,
                        principalTable: "testing_result",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "test_case_parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_case_id = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_case_parameters", x => x.id);
                    table.ForeignKey(
                        name: "FK_Parameters_TestCases",
                        column: x => x.test_case_id,
                        principalTable: "test_cases",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "first_name", "group_id", "IsTeacher", "last_name", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a3b34355-c2e0-402d-99e0-7c3d37dcc959", 0, "063920bb-eaa9-4f30-bcc2-d881c90dc354", "student@example.com", true, "studentFirstName", 1, true, "studentLastName", true, null, "STUDENT@EXAMPLE.COM", "STUDENT", "AQAAAAEAACcQAAAAEL7eUCPFYJbI+JuDj5dCq+dt+S7ncZ2i54iPWDB61i3siAN+YgDZ/VFOs1iPdxxGpg==", null, null, false, "ad967813-9c21-4e78-96b4-484289e305cd", false, "student" },
                    { "0f3ee92e-c737-4f35-adff-b13c32f8897b", 0, "5a03f73a-6a32-434b-8233-24fd507b4dd4", "teacher@example.com", true, "teacherFirstName", 0, true, "teacherLastName", true, null, "TEACHER@EXAMPLE.COM", "TEACHER", "AQAAAAEAACcQAAAAEN2w22vIqSGLHwd2vUsJaoo4l4j09ZfN6Ia6dT870LwfBSZGdJF3ZNddnnF3NMB0Dw==", null, null, false, "8a74d66d-ec81-4301-b11a-ea77a4a5dc62", false, "teacher" }
                });

            migrationBuilder.InsertData(
                table: "data_type",
                columns: new[] { "id", "name", "short_name" },
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
                table: "exercise_type",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Method coding" },
                    { 2, "Class coding" },
                    { 3, "Question" }
                });

            migrationBuilder.InsertData(
                table: "group",
                columns: new[] { "id", "name", "year" },
                values: new object[] { 1, "Test group", 2023 });

            migrationBuilder.InsertData(
                table: "exercise",
                columns: new[] { "id", "class_name", "coding_area", "context", "description", "exercise_type_id", "optional_dlls", "optional_usings", "score", "short_description" },
                values: new object[,]
                {
                    { 1, "TestClass", "public static double GetArea(double a, double b)\n{\n    // Example\n    return a * b;\n}", "// tests example\nstatic void Main()\n{\n Console.WriteLine(GetArea(6, 6));\n Console.WriteLine(GetArea(7.5, 10));\n Console.WriteLine(GetArea(1, 5));\n}", "Напишите тело метода GetArea так, чтобы оно возвращало площадь прямоугольника. Можете считать, что параметры ''''a'''' и ''''b'''' всегда положительные числа.", 1, null, null, 1, "Вычисление площади" },
                    { 2, "TestClass", "public static double GetNumber(long a)\n{\n    // example\n    return --a;\n}", "// example\nGetNumber(3);\nGetNumber(75);\nGetNumber(100);", "Для простого примера, выведите число, которой будет на 1 меньше.", 1, null, null, 1, "Простой вывод" }
                });

            migrationBuilder.InsertData(
                table: "student",
                column: "id",
                value: "a3b34355-c2e0-402d-99e0-7c3d37dcc959");

            migrationBuilder.InsertData(
                table: "teacher",
                column: "id",
                value: "0f3ee92e-c737-4f35-adff-b13c32f8897b");

            migrationBuilder.InsertData(
                table: "test_method_info",
                columns: new[] { "id", "exercise_id", "name", "return_type_id" },
                values: new object[] { 1, 1, "GetArea", 7 });

            migrationBuilder.InsertData(
                table: "test_method_info",
                columns: new[] { "id", "exercise_id", "name", "return_type_id" },
                values: new object[] { 2, 2, "GetNumber", 11 });

            migrationBuilder.InsertData(
                table: "test_cases",
                columns: new[] { "id", "result", "test_method_id" },
                values: new object[,]
                {
                    { 1, "9", 1 },
                    { 2, "10", 1 },
                    { 3, "5", 2 },
                    { 4, "8", 2 }
                });

            migrationBuilder.InsertData(
                table: "test_method_parameters",
                columns: new[] { "id", "data_type_id", "position", "test_method_info_id" },
                values: new object[,]
                {
                    { 1, 7, 0, 1 },
                    { 2, 7, 1, 1 },
                    { 3, 11, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "test_case_parameters",
                columns: new[] { "id", "position", "test_case_id", "value" },
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
                name: "IX_exercise_exercise_type_id",
                table: "exercise",
                column: "exercise_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_case_parameters_test_case_id",
                table: "test_case_parameters",
                column: "test_case_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_cases_test_method_id",
                table: "test_cases",
                column: "test_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_method_info_exercise_id",
                table: "test_method_info",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_method_info_return_type_id",
                table: "test_method_info",
                column: "return_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_method_parameters_data_type_id",
                table: "test_method_parameters",
                column: "data_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_method_parameters_test_method_info_id",
                table: "test_method_parameters",
                column: "test_method_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_testing_test_creator_id",
                table: "testing",
                column: "test_creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_testing_answer_exercise_id",
                table: "testing_answer",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_testing_answer_testing_result_id",
                table: "testing_answer",
                column: "testing_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_testing_exercise_exercise_id",
                table: "testing_exercise",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "IX_testing_result_student_id",
                table: "testing_result",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_testing_result_testing_id",
                table: "testing_result",
                column: "testing_id");
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
                name: "group");

            migrationBuilder.DropTable(
                name: "test_case_parameters");

            migrationBuilder.DropTable(
                name: "test_method_parameters");

            migrationBuilder.DropTable(
                name: "testing_answer");

            migrationBuilder.DropTable(
                name: "testing_exercise");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "test_cases");

            migrationBuilder.DropTable(
                name: "testing_result");

            migrationBuilder.DropTable(
                name: "test_method_info");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "testing");

            migrationBuilder.DropTable(
                name: "exercise");

            migrationBuilder.DropTable(
                name: "data_type");

            migrationBuilder.DropTable(
                name: "teacher");

            migrationBuilder.DropTable(
                name: "exercise_type");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
