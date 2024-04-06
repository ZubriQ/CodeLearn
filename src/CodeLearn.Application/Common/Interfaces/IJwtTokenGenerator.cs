namespace CodeLearn.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateTokenString(string userId, string email, string role, string? windowsAccountName = null);
}