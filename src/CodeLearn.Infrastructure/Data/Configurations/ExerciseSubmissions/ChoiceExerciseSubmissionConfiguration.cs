using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.JunctionTables;
using CodeLearn.Domain.ExerciseSubmissions.ValueObjects;
using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.QuestionChoices.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations.ExerciseSubmissions;

public sealed class ChoiceExerciseSubmissionConfiguration : IEntityTypeConfiguration<ChoiceExerciseSubmission>
{
    public void Configure(EntityTypeBuilder<ChoiceExerciseSubmission> builder)
    {
        ConfigureChoiceExerciseSubmission(builder);
    }

    private static void ConfigureChoiceExerciseSubmission(EntityTypeBuilder<ChoiceExerciseSubmission> builder)
    {
        builder
            .HasMany(e => e.Choices)
            .WithMany(e => e.ExerciseSubmissions)
            .UsingEntity<SelectedChoice>(
                nameof(SelectedChoice),
                j => j
                    .HasOne<QuestionChoice>()
                    .WithMany()
                    .HasForeignKey(nameof(QuestionChoiceId))
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne<ChoiceExerciseSubmission>()
                    .WithMany()
                    .HasForeignKey(nameof(ExerciseSubmissionId))
                    .HasPrincipalKey("Id"));
    }
}