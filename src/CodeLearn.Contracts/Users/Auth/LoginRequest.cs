namespace CodeLearn.Contracts.Users.Auth;

public record LoginRequest(
    string Username,
    string Password);