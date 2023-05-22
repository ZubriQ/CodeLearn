using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeLearn.Db
{
    public partial class CodeLearnContext : IdentityDbContext<ApplicationUser>
    {
        public CodeLearnContext() { }

        public CodeLearnContext(DbContextOptions<CodeLearnContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Database.EnsureCreated();
        }

        public virtual DbSet<DataType> DataTypes { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<ExerciseType> ExerciseTypes { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<TestCase> TestCases { get; set; } = null!;
        public virtual DbSet<TestCaseParameter> TestCaseParameters { get; set; } = null!;
        public virtual DbSet<TestMethodInfo> TestMethodInfos { get; set; } = null!;
        public virtual DbSet<TestMethodParameter> TestMethodParameters { get; set; } = null!;
        public virtual DbSet<Testing> Testings { get; set; } = null!;
        public virtual DbSet<TestingAnswer> TestingAnswers { get; set; } = null!;
        public virtual DbSet<TestingResult> TestingResults { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();

                var config = new System.Xml.XmlDocument();
                config.Load(AppDomain.CurrentDomain.BaseDirectory + "ConnectionStrings.config");
                string connectionString = config.SelectSingleNode("/connectionStrings/add[@name='Supabase']")!
                    .Attributes!["connectionString"]!.Value;

                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seed Mandatory Initial data

            modelBuilder.Entity<DataType>().HasData(
                new DataType { Id = 1, Name = "Void", ShortName = "void" },
                new DataType { Id = 2, Name = "System.Boolean", ShortName = "bool" },
                new DataType { Id = 3, Name = "System.Byte", ShortName = "byte" },
                new DataType { Id = 4, Name = "System.SByte", ShortName = "sbyte" },
                new DataType { Id = 5, Name = "System.Char", ShortName = "char" },
                new DataType { Id = 6, Name = "System.Decimal", ShortName = "decimal" },
                new DataType { Id = 7, Name = "System.Double", ShortName = "double" },
                new DataType { Id = 8, Name = "System.Single", ShortName = "float" },
                new DataType { Id = 9, Name = "System.Int32", ShortName = "int" },
                new DataType { Id = 10, Name = "System.UInt32", ShortName = "uint" },
                new DataType { Id = 11, Name = "System.Int64", ShortName = "long" },
                new DataType { Id = 12, Name = "System.UInt64", ShortName = "ulong" },
                new DataType { Id = 13, Name = "System.Int16", ShortName = "short" },
                new DataType { Id = 14, Name = "System.UInt16", ShortName = "ushort" },
                new DataType { Id = 15, Name = "System.IntPtr", ShortName = "nint" },
                new DataType { Id = 16, Name = "System.Object", ShortName = "object" },
                new DataType { Id = 17, Name = "System.String", ShortName = "string" },
                new DataType { Id = 18, Name = "System.Object", ShortName = "dynamic" }
            );

            modelBuilder.Entity<ExerciseType>().HasData(
                new ExerciseType { Id = 1, Name = "Method coding" },
                new ExerciseType { Id = 2, Name = "Class coding" },
                new ExerciseType { Id = 3, Name = "Question" }
            );

            #endregion

            #region Seed teacher and student tables

            // Test teacher
            var teacherId = Guid.NewGuid().ToString();
            var teacherEmail = "teacher@example.com";
            var teacherUserName = "teacher";
            var teacherPassword = "qwerty123";
            var teacherFirstName = "teacherFirstName";
            var teacherLastName = "teacherLastName";

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacherId,
                FirstName = teacherFirstName,
                LastName = teacherLastName,
                UserName = teacherUserName,
                NormalizedUserName = teacherUserName.ToUpper(),
                Email = teacherEmail,
                NormalizedEmail = teacherEmail.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(null!, teacherPassword),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                IsTeacher = true,
                GroupId = null
            });

            // Test group
            modelBuilder.Entity<Group>().HasData(new Group
            {
                Id = 1,
                Name = "Test group",
                Year = 2023
            });

            // Test student
            var studentId = Guid.NewGuid().ToString();
            var studentEmail = "student@example.com";
            var studentUserName = "student";
            var studentPassword = "qwerty123";
            var studentFirstName = "studentFirstName";
            var studentLastName = "studentLastName";

            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = studentId,
                FirstName = studentFirstName,
                LastName = studentLastName,
                UserName = studentUserName,
                NormalizedUserName = studentUserName.ToUpper(),
                Email = studentEmail,
                NormalizedEmail = studentEmail.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(null!, studentPassword),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                IsTeacher = false,
                GroupId = 1
            });

            #endregion

            #region Seed exercise table

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Description = "Напишите тело метода GetArea так, чтобы оно возвращало площадь прямоугольника. Можете считать, что параметры ''''a'''' и ''''b'''' всегда положительные числа.",
                    Context = "// tests example\nstatic void Main()\n{\n Console.WriteLine(GetArea(6, 6));\n Console.WriteLine(GetArea(7.5, 10));\n Console.WriteLine(GetArea(1, 5));\n}",
                    CodingArea = "public static double GetArea(double a, double b)\n{\n    // Example\n    return a * b;\n}",
                    ExerciseTypeId = 1,
                    ClassName = "TestClass",
                    Score = 1,
                    ShortDescription = "Вычисление площади"
                },
                new Exercise
                {
                    Id = 2,
                    Description = "Для простого примера, выведите число, которой будет на 1 меньше.",
                    Context = "// example\nGetNumber(3);\nGetNumber(75);\nGetNumber(100);",
                    CodingArea = "public static double GetNumber(long a)\n{\n    // example\n    return --a;\n}",
                    ExerciseTypeId = 1,
                    ClassName = "TestClass",
                    Score = 1,
                    ShortDescription = "Простой вывод"
                }
            );

            #endregion

            #region Seed test_cases table

            modelBuilder.Entity<TestCase>().HasData(
                new TestCase
                {
                    Id = 1,
                    Result = "9",
                    TestMethodId = 1
                },
                new TestCase
                {
                    Id = 2,
                    Result = "10",
                    TestMethodId = 1
                },
                new TestCase
                {
                    Id = 3,
                    Result = "5",
                    TestMethodId = 2
                },
                new TestCase
                {
                    Id = 4,
                    Result = "8",
                    TestMethodId = 2
                }
            );

            #endregion

            #region Seed test_case_parameters table

            modelBuilder.Entity<TestCaseParameter>().HasData(
                new TestCaseParameter
                {
                    Id = 1,
                    TestCaseId = 1,
                    Value = "3",
                    Position = 0
                },
                new TestCaseParameter
                {
                    Id = 2,
                    TestCaseId = 1,
                    Value = "3",
                    Position = 1
                },
                new TestCaseParameter
                {
                    Id = 3,
                    TestCaseId = 2,
                    Value = "5",
                    Position = 0
                },
                new TestCaseParameter
                {
                    Id = 4,
                    TestCaseId = 2,
                    Value = "2",
                    Position = 1
                },
                new TestCaseParameter
                {
                    Id = 5,
                    TestCaseId = 3,
                    Value = "6",
                    Position = 0
                },
                new TestCaseParameter
                {
                    Id = 6,
                    TestCaseId = 4,
                    Value = "9",
                    Position = 0
                }
            );

            #endregion

            #region Seed test_method_info table

            modelBuilder.Entity<TestMethodInfo>().HasData(
                new TestMethodInfo
                {
                    Id = 1,
                    Name = "GetArea",
                    ReturnTypeId = 7,
                    ExerciseId = 1
                },
                new TestMethodInfo
                {
                    Id = 2,
                    Name = "GetNumber",
                    ReturnTypeId = 11,
                    ExerciseId = 2
                }
            );

            #endregion

            #region Seed testing and exerciseTesting tables

            modelBuilder.Entity<Testing>().HasData(
                new Testing
                {
                    Id = 1,
                    Name = "Тест",
                    Description = "Простой пример теста по программированию на языке C#",
                    DurationInMinutes = 50,
                    TestCreatorId = teacherId
                }
            );

            modelBuilder.Entity<Testing>()
                .HasMany(t => t.Exercises)
                .WithMany(e => e.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "ExerciseTesting",
                    j => j.HasOne<Exercise>().WithMany().HasForeignKey("ExercisesId"),
                    j => j.HasOne<Testing>().WithMany().HasForeignKey("CoursesId"),
                    j => j.HasData(new object[]
                        {
                            new { CoursesId = 1, ExercisesId = 1 },
                            new { CoursesId = 1, ExercisesId = 2 }
                        }
                    ));

            #endregion

            #region Seed test_method_parameters table

            modelBuilder.Entity<TestMethodParameter>().HasData(
                new TestMethodParameter
                {
                    Id = 1,
                    DataTypeId = 7,
                    TestMethodInfoId = 1,
                    Position = 0
                },
                new TestMethodParameter
                {
                    Id = 2,
                    DataTypeId = 7,
                    TestMethodInfoId = 1,
                    Position = 1
                },
                new TestMethodParameter
                {
                    Id = 3,
                    DataTypeId = 11,
                    TestMethodInfoId = 2,
                    Position = 0
                }
            );

            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
