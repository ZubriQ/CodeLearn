namespace CodeLearn.Domain.Tests;

public sealed class Test : BaseAuditableEntity<TestId>, IAggregateRoot
{
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int DurationInMinutes { get; private set; }
    public bool IsPublic { get; private set; }

    private Test() { }

    private Test(
        TestId testId,
        string title,
        string description,
        int durationInMinutes,
        bool isPublic)
        : base(testId)
    {
        Title = title;
        Description = description;
        DurationInMinutes = durationInMinutes;
        IsPublic = isPublic;
    }

    public static Test Create(
        string title,
        string description,
        int durationInMinutes)
    {
        return new Test(
            TestId.CreateUnique(),
            title,
            description,
            durationInMinutes,
            true);
    }

    public void UpdateDetails(
        string title,
        string description,
        int durationInMinutes,
        bool isPublic)
    {
        // TODO: Validate

        Title = title;
        Description = description;
        DurationInMinutes = durationInMinutes;
        IsPublic = isPublic;
    }
}