namespace CodeLearn.Contracts.Users.Auth;

public record RefreshTokenRequest(
    string JwtToken,
    string RefreshToken);