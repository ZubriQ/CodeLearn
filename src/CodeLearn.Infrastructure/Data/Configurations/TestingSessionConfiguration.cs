﻿using CodeLearn.Domain.Testings;
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

        builder.HasKey(sg => sg.Id);

        builder
            .Property(sg => sg.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(
                groupId => groupId.Value,
                value => TestingSessionId.Create(value));

        builder
            .HasOne<Testing>()
            .WithMany()
            .HasForeignKey(ts => ts.TestingId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(ts => ts.Status)
            .HasMaxLength(10)
            .IsRequired()
            .HasConversion(
                status => status.ToString(),
                value => (TestingSessionStatus)Enum.Parse(typeof(TestingSessionStatus), value));

        builder
            .Property(ts => ts.StartDateTime)
            .IsRequired(false);

        builder
            .Property(ts => ts.FinishDateTime)
            .IsRequired(false);

        builder
            .Property(ts => ts.Score)
            .IsRequired(false);
    }
}