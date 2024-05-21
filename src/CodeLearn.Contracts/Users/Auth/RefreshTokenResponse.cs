namespace CodeLearn.Contracts.Users.Auth;

public record RefreshTokenResponse(
    string JwtToken,
    string RefreshToken);