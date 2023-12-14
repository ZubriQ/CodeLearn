using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;
using System.Reflection;

namespace CodeLearn.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private static Assembly ContextAssembly => typeof(ApplicationDbContext).Assembly;

    public DbSet<Testing> Testings => Set<Testing>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Teacher> Teachers => Set<Teacher>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=TEST_CodeLearn;Trusted_Connection=True;TrustServerCertificate=True;",
            b => b.MigrationsAssembly(ContextAssembly.FullName));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(ContextAssembly);

        base.OnModelCreating(modelBuilder);
    }
}