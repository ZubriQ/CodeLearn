using CodeLearn.Domain.Exercises;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class MethodCodingExerciseConfiguration : IEntityTypeConfiguration<MethodCodingExercise>
{
    public void Configure(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        ConfigureMethodCodingExercise(builder);
    }

    private static void ConfigureMethodCodingExercise(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        builder
            .HasOne(e => e.MethodReturnType)
            .WithMany()
            .HasForeignKey(t => t.MethodReturnTypeId)
            .IsRequired();
        
        builder
            .Property(e => e.MethodName)
            .HasMaxLength(50)
            .IsRequired();
    }
}