namespace CodeLearn.Domain.Testings;

public sealed class Testing : BaseAuditableEntity<TestingId>, IAggregateRoot
{
    public TestId TestId { get; private set; } = null!;
    public StudentGroupId StudentGroupId { get; private set; } = null!;
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public TimeSpan Duration { get; private set; }

    private Testing() { }

    private Testing(
        TestingId id,
        TestId testId,
        StudentGroupId studentGroupId,
        DateTime startDateTime,
        DateTime endDateTime,
        TimeSpan duration)
        : base(id)
    {
        TestId = testId;
        StudentGroupId = studentGroupId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Duration = duration;
    }

    public static Testing Create(
        TestId testId,
        StudentGroupId studentGroupId,
        DateTime startDateTime,
        TimeSpan duration)
    {
        var endDateTime = startDateTime.Add(duration);

        return new Testing(
            TestingId.CreateUnique(),
            testId,
            studentGroupId,
            startDateTime,
            endDateTime,
            duration);
    }
}