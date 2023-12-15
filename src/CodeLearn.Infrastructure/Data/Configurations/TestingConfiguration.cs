using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Testings.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class TestingConfiguration : IEntityTypeConfiguration<Testing>
{
    public void Configure(EntityTypeBuilder<Testing> builder)
    {
        ConfigureTestingTable(builder);
    }

    private static void ConfigureTestingTable(EntityTypeBuilder<Testing> builder)
    {
        builder.ToTable("Testing", DatabaseSchemes.Test);

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                testing => testing.Value,
                id => TestingId.Create(id));

        builder
            .HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(t => t.TeacherId)
            .IsRequired();

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

        builder
            .Property(t => t.CreatedDateTime)
            .IsRequired();

        builder
            .Property(t => t.ModifiedDateTime)
            .IsRequired();
    }
}