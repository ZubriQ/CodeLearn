using CodeLearn.Application.Common.IdentityModels;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> UserExistsAsync(string userId);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string? UserId)> CreateUserAsync(
        UserCredentials credentials, UserFullName fullName, UserStudentDetails? studentDetails = null);

    Task<(Result Result, string JwtToken)> Login(string email, string password);

    Task<Result> DeleteUserAsync(string userId);
}