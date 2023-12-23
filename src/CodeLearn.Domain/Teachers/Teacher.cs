namespace CodeLearn.Domain.Teachers;

public sealed class Teacher : BaseEntity<TeacherId>, IAggregateRoot
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? Patronymic { get; private set; }

    private Teacher() { }

    private Teacher(
        TeacherId id,
        string firstName,
        string lastName,
        string? patronymic)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static Teacher Create(
        string firstName,
        string lastName,
        string? patronymic = null)
    {
        return new Teacher(
            TeacherId.CreateUnique(),
            firstName,
            lastName,
            patronymic);
    }

    public void UpdateName(string firstName, string lastName, string? patronymic = null)
    {
        // TODO: Validate

        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }
}