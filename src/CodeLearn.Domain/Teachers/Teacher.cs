namespace CodeLearn.Domain.Teachers;

public sealed class Teacher : BaseEntity<TeacherId>, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Patronymic { get; private set; }

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

    public Result UpdateName(string firstName, string lastName, string? patronymic = null)
    {
        if (patronymic is not null && patronymic.Length > 50)
        {
            return Result.Failure(DomainErrors.Teacher.MaxPatronymicLengthExceeded);
        }

        if (string.IsNullOrEmpty(firstName) || firstName.Length > 50)
        {
            return Result.Failure(DomainErrors.Teacher.InvalidFirstName);
        }

        if (string.IsNullOrEmpty(lastName) || lastName.Length > 50)
        {
            return Result.Failure(DomainErrors.Teacher.InvalidLastName);
        }

        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;

        return Result.Success();
    }
}