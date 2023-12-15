using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class ExerciseTopicConfiguration : IEntityTypeConfiguration<ExerciseTopic>
{
    public void Configure(EntityTypeBuilder<ExerciseTopic> builder)
    {
        ConfigureExerciseTopicTable(builder);
    }

    private static void ConfigureExerciseTopicTable(EntityTypeBuilder<ExerciseTopic> builder)
    {
        builder.ToTable("ExerciseTopic", DatabaseSchemes.Test);

        builder.HasKey(et => et.Id);

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                exerciseId => exerciseId.Value,
                idValue => ExerciseTopicId.Create(idValue));
    }
}
