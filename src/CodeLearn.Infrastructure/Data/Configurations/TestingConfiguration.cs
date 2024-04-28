using CodeLearn.Domain.StudentGroups;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Testings.Enums;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Domain.Tests;

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
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Value,
                value => TestingId.Create(value));

        builder
            .HasOne<Test>()
            .WithMany()
            .HasForeignKey(s => s.TestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<StudentGroup>()
            .WithMany()
            .HasForeignKey(s => s.StudentGroupId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(t => t.DeadlineDate)
            .IsRequired();

        builder
            .Property(t => t.DurationInMinutes)
            .IsRequired();

        builder
            .Property(ts => ts.Status)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion(
                status => status.ToString(),
                value => (TestingStatus)Enum.Parse(typeof(TestingStatus), value));
    }
}