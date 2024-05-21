namespace CodeLearn.Contracts.Users.Auth;

public record LoginResponse(
    string JwtToken,
    string RefreshToken);