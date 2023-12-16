using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class MethodCodingExerciseConfiguration : IEntityTypeConfiguration<MethodCodingExercise>
{
    public void Configure(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        ConfigureMethodCodingExercise(builder);
        ConfigureMethodCodingExerciseMethodParameterTable(builder);
        ConfigureMethodCodingExerciseTestCaseTable(builder);
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
            .HasMaxLength(30)
            .IsRequired();
        
        builder
            .Property(e => e.MethodStartingCode)
            .HasMaxLength(150)
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

    private static void ConfigureMethodCodingExerciseTestCaseTable(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        builder.OwnsMany(e => e.TestCases, testCaseBuilder =>
        {
            testCaseBuilder.ToTable("TestCase", DatabaseSchemes.Test);

            testCaseBuilder
                .WithOwner()
                .HasForeignKey(tc => tc.ExerciseId);

            testCaseBuilder.HasKey(tc => tc.Id);

            testCaseBuilder
                .Property(tc => tc.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    tcId => tcId.Value,
                    idValue => TestCaseId.Create(idValue));

            testCaseBuilder
                .Property(tc => tc.CorrectOutputValue)
                .HasMaxLength(250)
                .IsRequired();

            ConfigureMethodCodingExerciseTestCaseTestCaseParameterTable(testCaseBuilder);
        });

        builder.Metadata
            .FindNavigation(nameof(MethodCodingExercise.TestCases))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMethodCodingExerciseTestCaseTestCaseParameterTable(
        OwnedNavigationBuilder<MethodCodingExercise, TestCase> testCaseBuilder)
    {
        testCaseBuilder.OwnsMany(e => e.TestCaseParameters, parameterBuilder =>
        {
            parameterBuilder.ToTable("TestCaseParameter", DatabaseSchemes.Test);

            parameterBuilder
                .WithOwner()
                .HasForeignKey(p => p.TestCaseId);

            parameterBuilder.HasKey(p => p.Id);

            parameterBuilder
                .Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => TestCaseParameterId.Create(value));

            parameterBuilder
                .Property(p => p.Position)
                .IsRequired();

            parameterBuilder
                .Property(p => p.Value)
                .HasMaxLength(250)
                .IsRequired();
        }).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}