namespace CodeLearn.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName, string? patronymic);
}