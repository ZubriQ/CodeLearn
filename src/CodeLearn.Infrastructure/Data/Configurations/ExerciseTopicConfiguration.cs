using CodeLearn.Domain.ExerciseTopics;
using CodeLearn.Domain.ExerciseTopics.ValueObjects;

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
            .ValueGeneratedOnAdd()
            .HasConversion(
                topicId => topicId.Value,
                idValue => ExerciseTopicId.Create(idValue));

        builder
            .Property(et => et.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasIndex(e => e.Name);
    }
}