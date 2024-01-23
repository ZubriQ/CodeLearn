using CodeLearn.Domain.Common.Result;

namespace CodeLearn.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string userRole);

    Task<(Result Result, string JwtToken)> Login(string email, string password);

    Task<Result> DeleteUserAsync(string userId);
}