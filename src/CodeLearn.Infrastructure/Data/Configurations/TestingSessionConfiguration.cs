using CodeLearn.Domain.Testings;
using CodeLearn.Domain.TestingSessions;
using CodeLearn.Domain.TestingSessions.Enums;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Infrastructure.Data.Configurations;

public sealed class TestingSessionConfiguration : IEntityTypeConfiguration<TestingSession>
{
    public void Configure(EntityTypeBuilder<TestingSession> builder)
    {
        ConfigureTestingSessionTable(builder);
    }

    private static void ConfigureTestingSessionTable(EntityTypeBuilder<TestingSession> builder)
    {
        builder.ToTable("TestingSession", DatabaseSchemes.Test);

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                sessionId => sessionId.Value,
                id => TestingSessionId.Create(id));

        builder
            .HasOne<Testing>()
            .WithMany()
            .HasForeignKey(x => x.TestingId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.Status)
            .HasMaxLength(10)
            .IsRequired()
            .HasConversion(
                status => status.ToString(),
                value => (TestingSessionStatus)Enum.Parse(typeof(TestingSessionStatus), value));

        builder.HasIndex(x => x.Status);

        builder
            .Property(x => x.StartDateTime)
            .IsRequired();

        builder
            .Property(x => x.FinishDateTime)
            .IsRequired();

        builder
            .Property(x => x.CorrectQuestionsCount)
            .IsRequired();

        builder
            .Property(x => x.SolvedExercisesCount)
            .IsRequired();

        builder
            .Property(x => x.StudentFeedback)
            .HasMaxLength(450)
            .IsRequired();
    }
}