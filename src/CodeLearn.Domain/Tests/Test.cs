namespace CodeLearn.Domain.Tests;

public sealed class Test : BaseAuditableEntity<TestId>, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsPublic { get; private set; }
    
    /// <summary>
    /// Required by EF Core.
    /// </summary>
    private Test() { }

    private Test(
        string title,
        string description,
        bool isPublic)
        : base(default!)
    {
        Title = title;
        Description = description;
        IsPublic = isPublic;
    }

    public static Test Create(
        string title,
        string description)
    {
        return new Test(
            title,
            description,
            true);
    }

    public void UpdateDetails(
        string title,
        string description,
        bool isPublic)
    {
        // TODO: Validate

        Title = title;
        Description = description;
        IsPublic = isPublic;
    }
}