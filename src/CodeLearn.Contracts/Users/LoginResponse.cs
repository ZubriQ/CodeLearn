namespace CodeLearn.Contracts.Users;

public record LoginResponse(
    string JwtToken,
    string RefreshToken);