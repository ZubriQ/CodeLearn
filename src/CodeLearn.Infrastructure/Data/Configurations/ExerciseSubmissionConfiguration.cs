﻿using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.Enum;
using CodeLearn.Domain.ExerciseSubmissions.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class ExerciseSubmissionConfiguration : IEntityTypeConfiguration<ExerciseSubmission>
{
    public void Configure(EntityTypeBuilder<ExerciseSubmission> builder)
    {
        ConfigureExerciseSubmissionTable(builder);
    }

    private static void ConfigureExerciseSubmissionTable(EntityTypeBuilder<ExerciseSubmission> builder)
    {
        builder.ToTable("ExerciseSubmission", DatabaseSchemes.Test);

        builder.HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion(
                sId => sId.Value,
                id => ExerciseSubmissionId.Create(id));

        builder
            .HasOne<Exercise>()
            .WithMany()
            .HasForeignKey(e => e.ExerciseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(s => s.SentDateTime)
            .IsRequired();

        builder
            .Property(e => e.Status)
            .HasMaxLength(9)
            .IsRequired()
            .HasConversion(
                difficulty => difficulty.ToString(),
                value => (SubmissionTestStatus)Enum.Parse(typeof(SubmissionTestStatus), value));

        builder
            .HasDiscriminator<string>("SubmissionType")
            //.HasValue<ChoiceExerciseSubmission>("Question")
            .HasValue<CodeExerciseSubmission>("Code");
    }
}