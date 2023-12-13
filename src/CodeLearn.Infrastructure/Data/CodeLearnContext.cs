using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;

namespace CodeLearn.Infrastructure.Data;

public sealed class CodeLearnContext : DbContext
{
    public DbSet<Testing> Testings => Set<Testing>();

    public DbSet<Exercise> Exercises => Set<Exercise>();

    public DbSet<Teacher> Teachers => Set<Teacher>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=TEST_CodeLearn;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeLearnContext).Assembly);
    }
}