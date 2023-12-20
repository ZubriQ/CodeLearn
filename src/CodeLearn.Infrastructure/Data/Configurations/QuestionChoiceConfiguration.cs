using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.QuestionChoices.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class QuestionChoiceConfiguration : IEntityTypeConfiguration<QuestionChoice>
{
    public void Configure(EntityTypeBuilder<QuestionChoice> builder)
    {
        ConfigureQuestionChoiceTable(builder);
    }

    private static void ConfigureQuestionChoiceTable(EntityTypeBuilder<QuestionChoice> builder)
    {
        builder.ToTable("QuestionChoice", DatabaseSchemes.Test);

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                choiceId => choiceId.Value,
                value => QuestionChoiceId.Create(value));

        builder
            .Property(q => q.Text)
            .HasMaxLength(300)
            .IsRequired();

        builder
            .Property(q => q.IsCorrect)
            .IsRequired();

        builder
            .Property(q => q.Explanation)
            .HasMaxLength(300);
    }
}