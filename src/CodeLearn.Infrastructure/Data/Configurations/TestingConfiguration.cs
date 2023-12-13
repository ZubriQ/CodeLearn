using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class TestingConfiguration : IEntityTypeConfiguration<Testing>
{
    public void Configure(EntityTypeBuilder<Testing> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                testing => testing.Value,
                id => new TestingId(id))
            .ValueGeneratedOnAdd();

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(t => t.TeacherId)
            .IsRequired();

        builder.Property(t => t.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(t => t.IsPublic)
            .IsRequired();

        builder.Property(t => t.DurationInMinutes)
            .IsRequired();

        builder.Property(t => t.CreatedDateTime)
           .IsRequired();

        builder.Property(t => t.ModifiedDateTime)
            .IsRequired();

        builder.ToTable("Testing", DatabaseSchemes.Test);
    }
}