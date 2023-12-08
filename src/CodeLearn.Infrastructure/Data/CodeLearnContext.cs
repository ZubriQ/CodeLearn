namespace CodeLearn.Domain.Data;

public partial class CodeLearnContext : DbContext
{
    public CodeLearnContext()
    {
    }

    public CodeLearnContext(DbContextOptions<CodeLearnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataType> DataTypes { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseAnswer> ExerciseAnswers { get; set; }

    public virtual DbSet<ExerciseNote> ExerciseNotes { get; set; }

    public virtual DbSet<ExerciseTopic> ExerciseTopics { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TestCase> TestCases { get; set; }

    public virtual DbSet<TestCaseParameter> TestCaseParameters { get; set; }

    public virtual DbSet<TestMethodParameter> TestMethodParameters { get; set; }

    public virtual DbSet<Testing> Testings { get; set; }

    public virtual DbSet<TestingResult> TestingResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CodeLearn;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DataTypes");

            entity.ToTable("DataType", "Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.SystemName).HasMaxLength(50);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Exercises");

            entity.ToTable("Exercise", "Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Difficulty).HasMaxLength(50);
            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Test).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercise_Test");

            entity.HasMany(d => d.Topics).WithMany(p => p.Exercises)
                .UsingEntity<Dictionary<string, object>>(
                    "ExerciseExerciseTopic",
                    r => r.HasOne<ExerciseTopic>().WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExerciseTopic_Topic"),
                    l => l.HasOne<Exercise>().WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExerciseTopic_Exercise"),
                    j =>
                    {
                        j.HasKey("ExerciseId", "TopicId").HasName("PK_ExerciseTopic");
                        j.ToTable("ExerciseExerciseTopic", "Test");
                        j.IndexerProperty<int>("ExerciseId").HasColumnName("ExerciseID");
                        j.IndexerProperty<int>("TopicId").HasColumnName("TopicID");
                    });
        });

        modelBuilder.Entity<ExerciseAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TestingAnswers");

            entity.ToTable("ExerciseAnswer", "Test");

            entity.HasIndex(e => e.ExerciseId, "IX_TestingAnswers_ExerciseId");

            entity.HasIndex(e => e.TestingResultId, "IX_TestingAnswers_TestingResultId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");
            entity.Property(e => e.TestingResultId).HasColumnName("TestingResultID");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseAnswers)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK_TestingAnswers_Exercises_ExerciseId");

            entity.HasOne(d => d.TestingResult).WithMany(p => p.ExerciseAnswers)
                .HasForeignKey(d => d.TestingResultId)
                .HasConstraintName("FK_TestingAnswers_TestingResults_TestingResultId");
        });

        modelBuilder.Entity<ExerciseNote>(entity =>
        {
            entity.ToTable("ExerciseNote", "Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Entry).HasMaxLength(255);
            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseNotes)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseNote_Exercise");
        });

        modelBuilder.Entity<ExerciseTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Topic");

            entity.ToTable("ExerciseTopic", "Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Students");

            entity.ToTable("Student", "Person");

            entity.HasIndex(e => e.GroupId, "IX_Students_GroupId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Students_Groups_GroupId");
        });

        modelBuilder.Entity<StudentGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Groups");

            entity.ToTable("StudentGroup", "Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Teachers");

            entity.ToTable("Teacher", "Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<TestCase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TestCases");

            entity.ToTable("TestCase", "Test");

            entity.HasIndex(e => e.ExerciseId, "IX_TestCases_TestMethodId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");

            entity.HasOne(d => d.Exercise).WithMany(p => p.TestCases)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestCase_Exercise");
        });

        modelBuilder.Entity<TestCaseParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TestCaseParameters");

            entity.ToTable("TestCaseParameter", "Test");

            entity.HasIndex(e => e.TestCaseId, "IX_TestCaseParameters_TestCaseId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TestCaseId).HasColumnName("TestCaseID");
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.TestCase).WithMany(p => p.TestCaseParameters)
                .HasForeignKey(d => d.TestCaseId)
                .HasConstraintName("FK_TestCaseParameters_TestCases_TestCaseId");
        });

        modelBuilder.Entity<TestMethodParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TestMethodParameters");

            entity.ToTable("TestMethodParameter", "Test");

            entity.HasIndex(e => e.DataTypeId, "IX_TestMethodParameters_DataTypeId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataTypeId).HasColumnName("DataTypeID");
            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");

            entity.HasOne(d => d.DataType).WithMany(p => p.TestMethodParameters)
                .HasForeignKey(d => d.DataTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestMethodParameters_DataTypes_DataTypeId");

            entity.HasOne(d => d.Exercise).WithMany(p => p.TestMethodParameters)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestMethodParameterValue_Exercise");
        });

        modelBuilder.Entity<Testing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Testings");

            entity.ToTable("Testing", "Test");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Feedback).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Creator).WithMany(p => p.Testings)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Test_Teacher1");

            entity.HasMany(d => d.StudentGroups).WithMany(p => p.Testings)
                .UsingEntity<Dictionary<string, object>>(
                    "TestingAccess",
                    r => r.HasOne<StudentGroup>().WithMany()
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TestAccess_StudentGroup"),
                    l => l.HasOne<Testing>().WithMany()
                        .HasForeignKey("TestingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TestAccess_Test"),
                    j =>
                    {
                        j.HasKey("TestingId", "StudentGroupId").HasName("PK_TestAccess");
                        j.ToTable("TestingAccess", "Test");
                        j.IndexerProperty<int>("TestingId").HasColumnName("TestingID");
                        j.IndexerProperty<int>("StudentGroupId").HasColumnName("StudentGroupID");
                    });
        });

        modelBuilder.Entity<TestingResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TestingResults");

            entity.ToTable("TestingResult", "Test");

            entity.HasIndex(e => e.StudentId, "IX_TestingResults_StudentId");

            entity.HasIndex(e => e.TestingId, "IX_TestingResults_TestingId");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TestingId).HasColumnName("TestingID");

            entity.HasOne(d => d.Student).WithMany(p => p.TestingResults)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_TestingResults_Students_StudentId");

            entity.HasOne(d => d.Testing).WithMany(p => p.TestingResults)
                .HasForeignKey(d => d.TestingId)
                .HasConstraintName("FK_TestingResults_Testings_TestingId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}