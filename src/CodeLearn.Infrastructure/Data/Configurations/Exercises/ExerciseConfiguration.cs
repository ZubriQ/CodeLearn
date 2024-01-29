using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Enums;
using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseTopics;
using CodeLearn.Domain.Tests;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations.Exercises;

public sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        ConfigureExerciseTable(builder);
        ConfigureExerciseNoteTable(builder);
    }

    private static void ConfigureExerciseTable(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercise", DatabaseSchemes.Test);

        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                exercise => exercise.Value,
                id => ExerciseId.Create(id));

        builder
            .Property(e => e.TestId)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => TestId.Create(v));

        builder
            .HasOne<Test>()
            .WithMany()
            .HasForeignKey(e => e.TestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(e => e.ExerciseTopics)
            .WithMany(e => e.Exercises)
            // Instead of UsingEntity("Exercise2ExerciseTopic); for renaming foreign keys.
            .UsingEntity<Dictionary<string, object>>(
                "Exercise2ExerciseTopic",
                et => et
                    .HasOne<ExerciseTopic>()
                    .WithMany()
                    .HasForeignKey("ExerciseTopicId"),
                e => e
                    .HasOne<Exercise>()
                    .WithMany()
                    .HasForeignKey("ExerciseId"));

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(e => e.Difficulty)
            .HasMaxLength(6)
            .IsRequired()
            .HasConversion(
                difficulty => difficulty.ToString(),
                value => (ExerciseDifficulty)Enum.Parse(typeof(ExerciseDifficulty), value));

        builder
            .HasDiscriminator<string>("ExerciseType")
            .HasValue<MethodCodingExercise>("MethodCoding")
            .HasValue<ClassCodingExercise>("ClassCoding")
            .HasValue<QuestionExercise>("Question");
    }

    private static void ConfigureExerciseNoteTable(EntityTypeBuilder<Exercise> builder)
    {
        _ = builder.OwnsMany(e => e.ExerciseNotes, noteBuilder =>
        {
            noteBuilder.ToTable("ExerciseNote", DatabaseSchemes.Test);

            noteBuilder
                .WithOwner()
                .HasForeignKey(n => n.ExerciseId);

            noteBuilder.HasKey(n => n.Id);

            noteBuilder
                .Property(n => n.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ExerciseNoteId.Create(value));

            noteBuilder
                .Property(n => n.Entry)
                .HasMaxLength(100)
                .IsRequired();

            noteBuilder
                .Property(n => n.Decoration)
                .HasMaxLength(30)
                .IsRequired()
                .HasConversion(
                    decoration => decoration.ToString(),
                    value => (ExerciseNoteDecoration)Enum.Parse(typeof(ExerciseNoteDecoration), value));
        });

        builder.Metadata
            .FindNavigation(nameof(Exercise.ExerciseNotes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}