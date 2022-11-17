using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<TestCase> TestCases { get; set; } = null!;
        public virtual DbSet<TestCaseParameter> TestCaseParameters { get; set; } = null!;
        public virtual DbSet<TestMethodInfo> TestMethodInfos { get; set; } = null!;
        public virtual DbSet<TestMethodParameter> TestMethodParameters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Server=AUGUSTA\\SQLEXPRESS;Database=CodeLearn;Trusted_Connection=True;");
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

                entity.Property(e => e.ExerciseDescription)
                    .HasMaxLength(2000)
                    .HasColumnName("exercise_description");

                entity.Property(e => e.ExerciseTypeId).HasColumnName("exercise_type_id");

                entity.Property(e => e.OptionalDlls)
                    .HasMaxLength(200)
                    .HasColumnName("optional_dlls");

                entity.Property(e => e.OptionalUsings)
                    .HasMaxLength(200)
                    .HasColumnName("optional_usings");

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

            modelBuilder.Entity<TestCase>(entity =>
            {
                entity.ToTable("test_cases");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Result)
                    .HasMaxLength(200)
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
                    .HasMaxLength(200)
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
