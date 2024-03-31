using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.Exercises.Enums;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations.Exercises;

public sealed class MethodCodingExerciseConfiguration : IEntityTypeConfiguration<MethodCodingExercise>
{
    public void Configure(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        ConfigureMethodCodingExercise(builder);
        ConfigureMethodCodingExerciseMethodParameterTable(builder);
        ConfigureMethodCodingExerciseTestCaseTable(builder);
        ConfigureMethodCodingExerciseInputOutputExampleTable(builder);
        ConfigureMethodCodingExerciseNoteTable(builder);
    }

    private static void ConfigureMethodCodingExercise(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        builder
            .HasOne(e => e.MethodReturnDataType)
            .WithMany()
            .HasForeignKey(t => t.MethodReturnDataTypeId)
            .IsRequired();

        builder
            .Property(e => e.MethodToExecute)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(e => e.MethodSolutionCode)
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
               .ValueGeneratedOnAdd()
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
                .ValueGeneratedOnAdd()
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
                .ValueGeneratedOnAdd()
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

    private static void ConfigureMethodCodingExerciseInputOutputExampleTable(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        _ = builder.OwnsMany(e => e.InputOutputExamples, exampleBuilder =>
        {
            exampleBuilder.ToTable("InputOutputExample", DatabaseSchemes.Test);

            exampleBuilder
                .WithOwner()
                .HasForeignKey(n => n.ExerciseId);

            exampleBuilder.HasKey(n => n.Id);

            exampleBuilder
                .Property(n => n.Id)
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => InputOutputExampleId.Create(value));

            exampleBuilder
                .Property(n => n.Input)
                .HasMaxLength(100)
                .IsRequired();

            exampleBuilder
               .Property(n => n.Output)
               .HasMaxLength(50)
               .IsRequired();
        });

        builder.Metadata
            .FindNavigation(nameof(MethodCodingExercise.InputOutputExamples))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMethodCodingExerciseNoteTable(EntityTypeBuilder<MethodCodingExercise> builder)
    {
        _ = builder.OwnsMany(e => e.ExerciseNotes, noteBuilder =>
        {
            noteBuilder.ToTable("ExerciseNote", DatabaseSchemes.Test);

            noteBuilder
                .WithOwner()
                .HasForeignKey(n => n.ExerciseId);

            noteBuilder.HasKey(n => n.Id);

            noteBuilder
                .Property(n => n.Id)
                .ValueGeneratedOnAdd()
                .HasConversion(
                    id => id.Value,
                    value => ExerciseNoteId.Create(value));

            noteBuilder
                .Property(n => n.Entry)
                .HasMaxLength(100)
                .IsRequired();

            noteBuilder
                .Property(n => n.Decoration)
                .HasMaxLength(30)
                .IsRequired()
                .HasConversion(
                    decoration => decoration.ToString(),
                    value => (ExerciseNoteDecoration)Enum.Parse(typeof(ExerciseNoteDecoration), value));
        });

        builder.Metadata
            .FindNavigation(nameof(MethodCodingExercise.ExerciseNotes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}