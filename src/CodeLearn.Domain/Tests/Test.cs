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

    public Result UpdateDetails(
        string title,
        string description,
        bool isPublic)
    {
        if (string.IsNullOrEmpty(title) || title.Length > 100)
        {
            return Result.Failure(DomainErrors.Test.InvalidTitleLength);
        }

        if (string.IsNullOrEmpty(description) || description.Length > 1000)
        {
            return Result.Failure(DomainErrors.Test.InvalidDescriptionLength);
        }

        Title = title;
        Description = description;
        IsPublic = isPublic;

        return Result.Success();
    }
}