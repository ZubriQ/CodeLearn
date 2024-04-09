namespace CodeLearn.Contracts.Users;

public record LoginRequest(
    string Username,
    string Password);