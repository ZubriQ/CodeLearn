namespace CodeLearn.Contracts.Users;

public record RefreshTokenResponse(
    string JwtToken,
    string RefreshToken);