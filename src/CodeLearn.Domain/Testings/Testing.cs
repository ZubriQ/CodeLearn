namespace CodeLearn.Domain.Testings;

public sealed class Testing : BaseAuditableEntity<TestingId>, IAggregateRoot
{
    public TestId TestId { get; private set; }
    public StudentGroupId StudentGroupId { get; private set; }
    public DateTimeOffset DeadlineDate { get; private set; }
    public int DurationInMinutes { get; private set; }
    public TestingStatus Status { get; private set; }

    private Testing(
        TestId testId,
        StudentGroupId studentGroupId,
        DateTimeOffset deadlineDate,
        int durationInMinutes,
        TestingStatus status)
        : base(default!)
    {
        TestId = testId;
        StudentGroupId = studentGroupId;
        DeadlineDate = deadlineDate;
        DurationInMinutes = durationInMinutes;
        Status = status;
    }

    public static Testing Create(
        TestId testId,
        StudentGroupId studentGroupId,
        DateTimeOffset deadlineDate,
        int durationInMinutes,
        TestingStatus status)
    {
        return new Testing(
            testId,
            studentGroupId,
            deadlineDate,
            durationInMinutes,
            status);
    }
}