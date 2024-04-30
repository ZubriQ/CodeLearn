using CodeLearn.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CodeLearn.Infrastructure.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateTokenString(string userId, string username, string role, string? windowsAccountName = null)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.WindowsAccountName, string.IsNullOrEmpty(windowsAccountName) ? "" : windowsAccountName),
            new Claim(ClaimTypes.Role, role),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"]!,
            audience: _configuration["JwtSettings:Audience"]!,
            expires: DateTime.UtcNow.AddSeconds(10),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public ClaimsPrincipal? GetUsernameFromToken(string token)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = securityKey,
            ValidateLifetime = false,
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
    }

    public string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[64];

        using var numberGenerator = RandomNumberGenerator.Create();

        numberGenerator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}