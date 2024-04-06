namespace CodeLearn.Domain.Testings;

public sealed class Testing : BaseAuditableEntity<TestingId>, IAggregateRoot
{
    public TestId TestId { get; private set; }
    public StudentGroupId StudentGroupId { get; private set; }
    public DateTimeOffset DeadlineDate { get; private set; }
    public int DurationInMinutes { get; private set; }

    private Testing(
        TestId testId,
        StudentGroupId studentGroupId,
        DateTimeOffset deadlineDate,
        int durationInMinutes)
        : base(default!)
    {
        TestId = testId;
        StudentGroupId = studentGroupId;
        DeadlineDate = deadlineDate;
        DurationInMinutes = durationInMinutes;
    }

    public static Testing Create(
        TestId testId,
        StudentGroupId studentGroupId,
        DateTimeOffset deadlineDate,
        int durationInMinutes)
    {
        return new Testing(
            testId,
            studentGroupId,
            deadlineDate,
            durationInMinutes);
    }
}