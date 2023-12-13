using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Enums;
using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                exercise => exercise.Value,
                id => new ExerciseId(id))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.TestingId)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new TestingId(v))
            .HasColumnType("int");

        builder.HasOne<Testing>()
            .WithMany()
            .HasForeignKey(e => e.TestingId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(e => e.Difficulty)
            .IsRequired()
            .HasMaxLength(6)
            .HasConversion(
                d => d.ToString(),
                d => (ExerciseDifficulty)Enum.Parse(typeof(ExerciseDifficulty), d));

        builder.ToTable("Exercise", DatabaseSchemes.Test);
    }
}