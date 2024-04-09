using CodeLearn.Application.Common.IdentityModels;
using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Application.Users;
using CodeLearn.Domain.Common.Result;
using CodeLearn.Domain.Constants;
using CodeLearn.Infrastructure.Identity.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(Result Result, UserDto? UserDto)> CreateStudentUserAsync(
        UserFullName fullName, UserStudentDetails studentDetails)
    {
        var username = await GenerateUniqueUsername(fullName.LastName, fullName.FirstName, fullName.Patronymic);

        var temporaryPassword = GenerateTemporaryPassword();

        var user = new ApplicationUser
        {
            UserName = username,
            FirstName = fullName.FirstName,
            LastName = fullName.LastName,
            Patronymic = fullName.Patronymic,
            StudentGroupName = studentDetails?.StudentGroupName,
            UserCode = studentDetails?.UserCode,
            TemporaryPassword = temporaryPassword,
        };

        var result = await _userManager.CreateAsync(user, temporaryPassword);

        if (!result.Succeeded)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidUserFields), null);
        }

        await _userManager.AddToRoleAsync(user, Roles.Student);

        return (Result.Success(), user.ToDto());
    }

    private async Task<string> GenerateUniqueUsername(string lastName, string firstName, string? patronymic)
    {
        var firstNameInitial = char.ToUpper(firstName[0]);
        var patronymicInitial = string.IsNullOrEmpty(patronymic) ? "" : patronymic[..1];

        var baseUsername = $"{lastName}{firstNameInitial}{patronymicInitial}";

        var username = baseUsername;
        int i = 1;

        // Check for existing username and append a number if necessary, but only if i > 1.
        while (await _userManager.FindByNameAsync(username) != null)
        {
            i++;
            username = $"{baseUsername}{i}";
        }

        return username;
    }

    private static string GenerateTemporaryPassword()
    {
        return Guid.NewGuid().ToString()[..8];
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<(Result Result, string JwtToken)> Login(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return (Result.Failure(InfrastructureErrors.Identity.UserNotFoundByUserName), string.Empty);
        }

        var isSuccess = await _userManager.CheckPasswordAsync(user, password);

        if (!isSuccess)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidCredentials), string.Empty);
        }

        var roles = await _userManager.GetRolesAsync(user);

        var tokenString = _jwtTokenGenerator.GenerateTokenString(user.Id, userName, roles.FirstOrDefault()!);

        return (Result.Success(), tokenString);
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null;
    }

    public async Task<UserDto[]> GetUsersInRoleAsync(string role)
    {
        var users = await _userManager.GetUsersInRoleAsync(role);

        return users.Select(u => u.ToDto()).ToArray();
    }
}