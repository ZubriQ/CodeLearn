using CodeLearn.Domain.Exercises;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class ClassCodingExerciseConfiguration : IEntityTypeConfiguration<ClassCodingExercise>
{
    public void Configure(EntityTypeBuilder<ClassCodingExercise> builder)
    {
        builder
            .Property(e => e.ClassSolutionCode)
            .HasMaxLength(300)
            .IsRequired();

        builder
            .Property(e => e.ClassTesterCode)
            .HasMaxLength(2000)
            .IsRequired();
    }
}