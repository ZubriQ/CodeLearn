using CodeLearn.Application.Common.IdentityModels;
using CodeLearn.Application.Users;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> UserExistsAsync(string userId);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, UserDto? UserDto)> CreateStudentUserAsync(
        UserFullName fullName, UserStudentDetails studentDetails);

    Task<(Result Result, string JwtToken)> Login(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<UserDto[]> GetUsersInRoleAsync(string role);
}