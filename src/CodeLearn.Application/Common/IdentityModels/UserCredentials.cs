namespace CodeLearn.Application.Common.IdentityModels;

public class UserCredentials
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    private UserCredentials(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public static UserCredentials Create(string email, string password)
    {
        return new(email, password);
    }
}