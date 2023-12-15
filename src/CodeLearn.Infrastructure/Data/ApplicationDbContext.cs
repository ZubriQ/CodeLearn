using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;
using System.Reflection;
using CodeLearn.Domain.Exercises.Entities;

namespace CodeLearn.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private static Assembly ContextAssembly => typeof(ApplicationDbContext).Assembly;

    public DbSet<Testing> Testings => Set<Testing>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<MethodCodingExercise> MethodCodingExercises => Set<MethodCodingExercise>();
    public DbSet<DataType> DataTypes => Set<DataType>();
    public DbSet<ExerciseNote> ExerciseNotes => Set<ExerciseNote>();
    public DbSet<ExerciseTopic> ExerciseTopics => Set<ExerciseTopic>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=TEST2_CodeLearn;Trusted_Connection=True;TrustServerCertificate=True;",
            b => b.MigrationsAssembly(ContextAssembly.FullName));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(ContextAssembly);

        base.OnModelCreating(modelBuilder);
    }
}