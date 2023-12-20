using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.JunctionTables;
using CodeLearn.Domain.QuestionChoices;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class ChoiceExerciseSubmissionConfiguration : IEntityTypeConfiguration<ChoiceExerciseSubmission>
{
    public void Configure(EntityTypeBuilder<ChoiceExerciseSubmission> builder)
    {
          builder
            .HasMany(e => e.QuestionChoices)
            .WithMany(e => e.ChoiceExerciseSubmission)
            .UsingEntity<SelectedChoices>(
                "ExerciseSubmission2Choice",
                j => j
                    .HasOne<QuestionChoice>()
                    .WithMany()
                    .HasForeignKey("QuestionChoiceId")
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne<ChoiceExerciseSubmission>()
                    .WithMany()
                    .HasForeignKey("ExerciseSubmissionId")
                    .HasPrincipalKey("Id"));
    }
}