using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeLearn.Db
{
    public partial class CodeLearnContext : IdentityDbContext<ApplicationUser>
    {
        public CodeLearnContext()
        {
        }

        public CodeLearnContext(DbContextOptions<CodeLearnContext> options)
            : base(options)
        {
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
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CodeLearn;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Initial data

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

            #region Add test data

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
                GroupId = 0
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
                IsTeacher = true,
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

            #region TestMethodParameters

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

            modelBuilder.Entity<DataType>(entity =>
            {
                entity.ToTable("data_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(50)
                    .HasColumnName("short_name");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("exercise");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(30)
                    .HasColumnName("class_name");

                entity.Property(e => e.CodingArea)
                    .HasMaxLength(2000)
                    .HasColumnName("coding_area");

                entity.Property(e => e.Context)
                    .HasColumnType("text")
                    .HasColumnName("context");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description");

                entity.Property(e => e.ExerciseTypeId).HasColumnName("exercise_type_id");

                entity.Property(e => e.OptionalDlls)
                    .HasMaxLength(200)
                    .HasColumnName("optional_dlls");

                entity.Property(e => e.OptionalUsings)
                    .HasMaxLength(200)
                    .HasColumnName("optional_usings");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(70)
                    .HasColumnName("short_description");

                entity.HasOne(d => d.ExerciseType)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.ExerciseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exercises_ExerciseTypes");
            });

            modelBuilder.Entity<ExerciseType>(entity =>
            {
                entity.ToTable("exercise_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_student_group");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teacher");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic");
            });

            modelBuilder.Entity<TestCase>(entity =>
            {
                entity.ToTable("test_cases");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Result)
                    .HasMaxLength(2000)
                    .HasColumnName("result");

                entity.Property(e => e.TestMethodId).HasColumnName("test_method_id");

                entity.HasOne(d => d.TestMethod)
                    .WithMany(p => p.TestCases)
                    .HasForeignKey(d => d.TestMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_test_case_test_method_info");
            });

            modelBuilder.Entity<TestCaseParameter>(entity =>
            {
                entity.ToTable("test_case_parameters");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.TestCaseId).HasColumnName("test_case_id");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .HasColumnName("value");

                entity.HasOne(d => d.TestCase)
                    .WithMany(p => p.TestCaseParameters)
                    .HasForeignKey(d => d.TestCaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parameters_TestCases");
            });

            modelBuilder.Entity<TestMethodInfo>(entity =>
            {
                entity.ToTable("test_method_info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.ReturnTypeId).HasColumnName("return_type_id");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.TestMethodInfos)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_test_method_info_exercise1");

                entity.HasOne(d => d.ReturnType)
                    .WithMany(p => p.TestMethodInfos)
                    .HasForeignKey(d => d.ReturnTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestMethodsInfo_DataTypes");
            });

            modelBuilder.Entity<TestMethodParameter>(entity =>
            {
                entity.ToTable("test_method_parameters");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataTypeId).HasColumnName("data_type_id");

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.TestMethodInfoId).HasColumnName("test_method_info_id");

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.TestMethodParameters)
                    .HasForeignKey(d => d.DataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParametersTestMethods_DataTypes");

                entity.HasOne(d => d.TestMethodInfo)
                    .WithMany(p => p.TestMethodParameters)
                    .HasForeignKey(d => d.TestMethodInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParametersTestMethods_TestMethodsInfo");
            });

            modelBuilder.Entity<Testing>(entity =>
            {
                entity.ToTable("testing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .HasColumnName("description");

                entity.Property(e => e.DurationInMinutes).HasColumnName("duration_in_minutes");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.TestCreatorId).HasColumnName("test_creator_id");

                entity.HasOne(d => d.TestCreator)
                    .WithMany(p => p.Testings)
                    .HasForeignKey(d => d.TestCreatorId)
                    .HasConstraintName("FK_testing_teacher");

                entity.HasMany(d => d.Exercises)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "TestingExercise",
                        l => l.HasOne<Exercise>().WithMany().HasForeignKey("ExerciseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_testing_exercise_exercise"),
                        r => r.HasOne<Testing>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_testing_exercise_testing"),
                        j =>
                        {
                            j.HasKey("CourseId", "ExerciseId");

                            j.ToTable("testing_exercise");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");

                            j.IndexerProperty<int>("ExerciseId").HasColumnName("exercise_id");
                        });
            });

            modelBuilder.Entity<TestingAnswer>(entity =>
            {
                entity.ToTable("testing_answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .HasColumnType("text")
                    .HasColumnName("answer");

                entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");

                entity.Property(e => e.FailureInfo)
                    .HasMaxLength(200)
                    .HasColumnName("failure_info");

                entity.Property(e => e.IsCorrect).HasColumnName("is_correct");

                entity.Property(e => e.TestingResultId).HasColumnName("testing_result_id");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.TestingAnswers)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_testing_answer_exercise");

                entity.HasOne(d => d.TestingResult)
                    .WithMany(p => p.TestingAnswers)
                    .HasForeignKey(d => d.TestingResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_testing_answer_testing_result");
            });

            modelBuilder.Entity<TestingResult>(entity =>
            {
                entity.ToTable("testing_result");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PassingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("passing_date");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.TestingId).HasColumnName("testing_id");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TestingResults)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_testing_result_student");

                entity.HasOne(d => d.Testing)
                    .WithMany(p => p.TestingResults)
                    .HasForeignKey(d => d.TestingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_testing_result_testing");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
