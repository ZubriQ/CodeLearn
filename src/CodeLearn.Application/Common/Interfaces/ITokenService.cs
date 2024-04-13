using System.Security.Claims;

namespace CodeLearn.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateTokenString(string userId, string email, string role, string? windowsAccountName = null);

    ClaimsPrincipal? GetUsernameFromToken(string token);

    string GenerateRefreshTokenString();
}