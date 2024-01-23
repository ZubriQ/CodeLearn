using CodeLearn.Domain.Tests;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        ConfigureTestTable(builder);
    }

    private static void ConfigureTestTable(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("Test", DatabaseSchemes.Test);

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                testId => testId.Value,
                id => TestId.Create(id));

        builder
            .Property(t => t.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(t => t.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(t => t.IsPublic)
            .IsRequired();

        builder
            .Property(t => t.DurationInMinutes)
            .IsRequired();
    }
}