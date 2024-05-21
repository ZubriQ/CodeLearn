using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Application.Common.Models;
using CodeLearn.Application.Users.Commands.ImportStudentList;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Domain.Common.Result;
using CodeLearn.Domain.Constants;
using CodeLearn.Infrastructure.Identity.Errors;
using Cyrillic.Convert;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly ITokenService _tokenService;
    private readonly Conversion _conversion;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        ITokenService jwtTokenGenerator)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _tokenService = jwtTokenGenerator;
        _conversion = new();
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
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

    public async Task<bool> UserExistsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null;
    }

    public async Task<bool> UserCodeExistsAsync(string userCode)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserCode == userCode);

        return user != null;
    }

    public async Task<UserDto[]> GetUsersInRoleAsync(string role)
    {
        var users = await _userManager.GetUsersInRoleAsync(role);

        return users.Select(u => u.ToDto()).ToArray();
    }

    public async Task<(Result Result, string? UserId)> CreateStudentUserAsync(RegisterStudentDto studentDto)
    {
        var username = await GenerateUniqueUsername(studentDto.LastName, studentDto.FirstName, studentDto.Patronymic);

        var password = GenerateTemporaryPassword();

        var user = new ApplicationUser
        {
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            Patronymic = studentDto.Patronymic,
            UserCode = studentDto.UserCode,
            StudentGroupName = studentDto.StudentGroupName,
            UserName = username,
            TemporaryPassword = password,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidUserFields), null);
        }

        await _userManager.AddToRoleAsync(user, Roles.Student);

        return (Result.Success(), user.Id);
    }

    private async Task<string> GenerateUniqueUsername(string lastName, string firstName, string? patronymic)
    {
        var firstNameInitial = char.ToUpper(firstName[0]);
        var patronymicInitial = string.IsNullOrEmpty(patronymic) ? "" : patronymic[..1];

        var baseUsername = $"{lastName}{firstNameInitial}{patronymicInitial}";

        var username = _conversion.RussianCyrillicToLatin(baseUsername);
        int i = 1;

        // Check for existing username and append a number if necessary, but only if i > 1.
        while (await _userManager.FindByNameAsync(username) != null)
        {
            i++;
            username = _conversion.RussianCyrillicToLatin($"{baseUsername}{i}");
        }

        return username;
    }

    private static string GenerateTemporaryPassword()
    {
        return Guid.NewGuid().ToString()[..8];
    }

    public async Task<Result> AddStudentUsersFromDtoAsync(ImportedStudentDto[] students, string studentGroup)
    {
        foreach (var student in students)
        {
            var alreadyExists = await UserCodeExistsAsync(student.UserCode);

            if (alreadyExists)
            {
                continue;
            }

            var username = await GenerateUniqueUsername(student.LastName, student.FirstName, student.Patronymic);
            var password = GenerateTemporaryPassword();

            var user = new ApplicationUser
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Patronymic = student.Patronymic,
                UserCode = student.UserCode,
                StudentGroupName = studentGroup,
                UserName = username,
                TemporaryPassword = password,
            };

            var createResult = await _userManager.CreateAsync(user, password);

            if (createResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Student);
            }
            // TODO: handle unadded users?
        }

        return Result.Success();
    }

    public async Task<(Result Result, TokensDto? TokensDto)> Login(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return (Result.Failure(InfrastructureErrors.Identity.UserNotFoundByUserName), null);
        }

        var isSuccess = await _userManager.CheckPasswordAsync(user, password);

        if (!isSuccess)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidCredentials), null);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateTokenString(user.Id, userName, roles.First());
        var refreshToken = _tokenService.GenerateRefreshTokenString();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddHours(24);
        await _userManager.UpdateAsync(user);

        var tokensDto = new TokensDto(token, refreshToken);
        return (Result.Success(), tokensDto);
    }

    public async Task<(Result Result, TokensDto? TokensDto)> RefreshToken(string jwtToken, string refreshToken)
    {
        var principal = _tokenService.GetUsernameFromToken(jwtToken);

        if (principal?.Identity?.Name is null)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidTokenName), null);
        }

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTimeOffset.UtcNow)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidRefreshTokenOrUser), null);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateTokenString(user.Id, principal.Identity.Name, roles.First());
        var newRefreshToken = _tokenService.GenerateRefreshTokenString();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddHours(24);
        await _userManager.UpdateAsync(user);

        var tokensDto = new TokensDto(token, newRefreshToken);
        return (Result.Success(), tokensDto);
    }

    public async Task<string?> GetStudentGroupNameByUsernameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        return user?.StudentGroupName;
    }

    public async Task<(Result Result, string? UserId)> CreateTeacherUserAsync(string firstName, string lastName, string patronymic)
    {
        var username = await GenerateUniqueUsername(lastName, firstName, patronymic);

        var password = GenerateTemporaryPassword();

        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            UserName = username,
            TemporaryPassword = password,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidUserFields), null);
        }

        await _userManager.AddToRoleAsync(user, Roles.Teacher);

        return (Result.Success(), user.Id);
    }
}