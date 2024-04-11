namespace CodeLearn.Application.Common.IdentityModels;

public class UserFullName
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }

    private UserFullName(string firstName, string lastName, string? patronymic = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static UserFullName Create(string firstName, string lastName, string? patronymic = null)
    {
        return new(firstName, lastName, patronymic);
    }
}