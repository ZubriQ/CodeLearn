using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLearn.Db.Migrations
{
    public partial class Supabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "06fb23b5-d1b7-4b1a-88ce-de8b3d07b590");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d7b1efe-cd56-48c3-9ab6-7e4d0205851a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "GroupId", "IsTeacher", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "06d7c3b3-f791-4c71-8c4f-1c756282e307", 0, "8b6aeaab-1d38-43ef-87d5-ad6c64a792a7", "Student", "student@example.com", true, "studentFirstName", 1, true, "studentLastName", true, null, "STUDENT@EXAMPLE.COM", "STUDENT", "AQAAAAEAACcQAAAAELGT7MIgBATE557c4P5Ms0iEf8Bh+p+1UF30DabkubdrV7rvA82QBUV3VNamfQGFrQ==", null, null, false, "61b0f8c3-d835-4e75-98cf-7da5ce4909cf", false, "student" },
                    { "4de9a78f-83a2-4c5b-900f-b107ddf6efba", 0, "98ac3684-c718-497e-82f3-e3710a8e048d", "Teacher", "teacher@example.com", true, "teacherFirstName", null, true, "teacherLastName", true, null, "TEACHER@EXAMPLE.COM", "TEACHER", "AQAAAAEAACcQAAAAEGkVjFxxbIE68IMDVlBOsEI7ojGzlKb74bK2kQ+sxNwqR1dp7DuDpJVM7P8Oxb0FGA==", null, null, false, "b9661c70-09fc-4c06-9120-e32a787c791d", false, "teacher" }
                });

            migrationBuilder.UpdateData(
                table: "Testings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TestCreatorId",
                value: "4de9a78f-83a2-4c5b-900f-b107ddf6efba");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "06d7c3b3-f791-4c71-8c4f-1c756282e307");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4de9a78f-83a2-4c5b-900f-b107ddf6efba");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "GroupId", "IsTeacher", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "06fb23b5-d1b7-4b1a-88ce-de8b3d07b590", 0, "6da48604-7c2e-4524-aeb1-8c69809ab409", "Student", "student@example.com", true, "studentFirstName", 1, true, "studentLastName", true, null, "STUDENT@EXAMPLE.COM", "STUDENT", "AQAAAAEAACcQAAAAEG43UdrAxe8NZABigo5Y1obxYM5ofLd0is5whEfpudxLM4RVsFDgO4JqnKHcqhdW0w==", null, null, false, "5bb2d5a8-9d8f-498e-b9aa-f99d940ce3a0", false, "student" },
                    { "6d7b1efe-cd56-48c3-9ab6-7e4d0205851a", 0, "e3e73baa-cc33-46c5-840f-767227db1984", "Teacher", "teacher@example.com", true, "teacherFirstName", null, true, "teacherLastName", true, null, "TEACHER@EXAMPLE.COM", "TEACHER", "AQAAAAEAACcQAAAAELwTchSiLt+9YIun7zirkEREX+EwQRpplg7sjj+GYYZfYMTd5S56zKbuXIe5i/zzTQ==", null, null, false, "d06ed00a-83dd-4b5d-9257-4804c6638c0f", false, "teacher" }
                });

            migrationBuilder.UpdateData(
                table: "Testings",
                keyColumn: "Id",
                keyValue: 1,
                column: "TestCreatorId",
                value: "6d7b1efe-cd56-48c3-9ab6-7e4d0205851a");
        }
    }
}
