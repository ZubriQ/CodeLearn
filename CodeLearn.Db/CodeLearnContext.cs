using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeLearn.Db
{
    public partial class CodeLearnContext : DbContext
    {
        public CodeLearnContext()
        {
        }

        public CodeLearnContext(DbContextOptions<CodeLearnContext> options)
            : base(options)
        {
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer("Server=AUGUSTA\\SQLEXPRESS;Database=CodeLearn;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username");

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

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username");
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
