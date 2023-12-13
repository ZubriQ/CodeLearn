namespace CodeLearn.Domain.Teachers;

public sealed class Teacher : BaseEntity<TeacherId>, IAggregateRoot
{
    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string? Patronymic { get; private set; }

    public string? Username { get; private set; }

    private Teacher() { }

    private Teacher(
        string firstName, 
        string lastName, 
        string? patronymic = null, 
        string? username = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Username = username;
    }

    public static Teacher Create(
        string firstName, 
        string lastName, string? 
        patronymic = null, string? 
        username = null)
    {
        return new(firstName, lastName, patronymic, username);
    }
}