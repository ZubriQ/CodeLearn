namespace CodeLearn.Domain.Testings;

public sealed class Testing : BaseAuditableEntity<TestingId>, IAggregateRoot
{
    public TestId TestId { get; private set; } = null!;
    public StudentGroupId StudentGroupId { get; private set; } = null!;
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public int DurationInMinutes { get; private set; }

    private Testing() { }

    private Testing(
        TestingId id,
        TestId testId,
        StudentGroupId studentGroupId,
        DateTime startDateTime,
        DateTime endDateTime,
        int durationInMinutes)
        : base(id)
    {
        TestId = testId;
        StudentGroupId = studentGroupId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        DurationInMinutes = durationInMinutes;
    }

    public static Testing Create(
        TestId testId,
        StudentGroupId studentGroupId,
        DateTime startDateTime,
        int durationInMinutes)
    {
        var endDateTime = startDateTime.AddMinutes(durationInMinutes);

        return new Testing(
            TestingId.CreateUnique(),
            testId,
            studentGroupId,
            startDateTime,
            endDateTime,
            durationInMinutes);
    }
}