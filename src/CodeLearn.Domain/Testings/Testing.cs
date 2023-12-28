﻿namespace CodeLearn.Domain.Testings;

public sealed class Testing : BaseEntity<TestingId>, IAggregateRoot
{
    public TeacherId TeacherId { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int DurationInMinutes { get; private set; }
    public bool IsPublic { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime ModifiedDateTime { get; private set; }

    private Testing() { }

    private Testing(
        TestingId testingId,
        TeacherId teacherId,
        string title,
        string description,
        int durationInMinutes,
        bool isPublic,
        DateTime createdDateTime,
        DateTime modifiedDateTime)
        : base(testingId)
    {
        TeacherId = teacherId;
        Title = title;
        Description = description;
        DurationInMinutes = durationInMinutes;
        IsPublic = isPublic;
        CreatedDateTime = createdDateTime;
        ModifiedDateTime = modifiedDateTime;
    }

    public static Testing Create(
        TeacherId teacherId,
        string title,
        string description,
        int durationInMinutes,
        DateTime now)
    {
        return new Testing(
            TestingId.CreateUnique(),
            teacherId,
            title,
            description,
            durationInMinutes,
            true,
            now,
            now);
    }

    public void UpdateDetails(
        string title,
        string description,
        int durationInMinutes,
        bool isPublic,
        DateTime now)
    {
        // TODO: Validate

        Title = title;
        Description = description;
        DurationInMinutes = durationInMinutes;
        IsPublic = isPublic;
        ModifiedDateTime = now;
    }
}