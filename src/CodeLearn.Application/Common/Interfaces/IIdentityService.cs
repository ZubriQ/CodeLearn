using CodeLearn.Application.Users;
using CodeLearn.Application.Users.Commands.ImportStudentList;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> UserExistsAsync(string userId);

    Task<bool> UserCodeExistsAsync(string userCode);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<Result> DeleteUserAsync(string userId);

    Task<UserDto[]> GetUsersInRoleAsync(string role);

    Task<(Result Result, string? userId)> CreateStudentUserAsync(RegisterStudentDto studentDto);

    Task<Result> AddStudentUsersFromDtoAsync(ImportedStudentDto[] students, string studentGroup);

    Task<(Result Result, string JwtToken)> Login(string userName, string password);
}