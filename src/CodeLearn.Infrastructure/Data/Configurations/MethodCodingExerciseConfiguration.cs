using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class MethodCodingExerciseConfiguration : IEntityTypeConfiguration<MethodCodingExercise>
{
    public void Configure(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        ConfigureMethodCodingExercise(builder);
        ConfigureMethodCodingExerciseMethodParameterTable(builder);
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
    
    private static void ConfigureMethodCodingExerciseMethodParameterTable(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        builder.OwnsMany(e => e.MethodParameters, methodParameterBuilder =>
        {
            methodParameterBuilder.ToTable("MethodParameter", DatabaseSchemes.Test);

            methodParameterBuilder
                .WithOwner()
                .HasForeignKey(e => e.ExerciseId);

            methodParameterBuilder.HasKey(mp => mp.Id);

            methodParameterBuilder
                .Property(mp => mp.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MethodParameterId.Create(value));

            methodParameterBuilder
                .HasOne(mp => mp.DataType)
                .WithMany()
                .HasForeignKey(t => t.DataTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            methodParameterBuilder
                .Property(mp => mp.Position)
                .IsRequired();

        });

        builder.Metadata
            .FindNavigation(nameof(MethodCodingExercise.MethodParameters))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}