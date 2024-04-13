namespace CodeLearn.Contracts.Users;

public record RefreshTokenRequest(
    string JwtToken,
    string RefreshToken);