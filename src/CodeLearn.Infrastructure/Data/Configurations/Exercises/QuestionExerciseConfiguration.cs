using CodeLearn.Domain.Exercises;

namespace CodeLearn.Infrastructure.Data.Configurations.Exercises;

public sealed class QuestionExerciseConfiguration : IEntityTypeConfiguration<QuestionExercise>
{
    public void Configure(EntityTypeBuilder<QuestionExercise> builder)
    {
        ConfigureQuestionExercise(builder);
    }

    private static void ConfigureQuestionExercise(EntityTypeBuilder<QuestionExercise> builder)
    {
        builder
            .Property(q => q.IsMultipleAnswers)
            .IsRequired();

        builder
            .HasMany(q => q.QuestionChoices)
            .WithOne()
            .HasForeignKey(q => q.ExerciseId)
            .IsRequired();
    }
}