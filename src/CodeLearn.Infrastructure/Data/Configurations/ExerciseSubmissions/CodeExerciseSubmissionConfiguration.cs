using CodeLearn.Domain.ExerciseSubmissions;

namespace CodeLearn.Infrastructure.Data.Configurations.ExerciseSubmissions;

public sealed class CodeExerciseSubmissionConfiguration : IEntityTypeConfiguration<CodeExerciseSubmission>
{
    public void Configure(EntityTypeBuilder<CodeExerciseSubmission> builder)
    {
        ConfigureCodeExerciseSubmission(builder);
    }

    private static void ConfigureCodeExerciseSubmission(EntityTypeBuilder<CodeExerciseSubmission> builder)
    {
        builder
            .Property(s => s.StudentCode)
            .HasMaxLength(3000)
            .IsRequired();

        builder
            .Property(s => s.TestingInformation)
            .HasMaxLength(200);

        builder.Property(e => e.RuntimeInMilliseconds);
    }
}