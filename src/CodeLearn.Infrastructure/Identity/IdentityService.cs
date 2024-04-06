using CodeLearn.Application.Common.IdentityModels;
using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Common.Result;
using CodeLearn.Domain.Constants;
using CodeLearn.Infrastructure.Identity.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    /// <summary>
    /// Creates a User. If Student group name and Enrolment year are _null_,
    /// We create Teacher, otherwise Student.
    /// </summary>
    /// <param name="credentials">User email and password.</param>
    /// <param name="fullName">User full name.</param>
    /// <param name="studentDetails">Student group name and enrolment year.</param>
    /// <returns>Result information and created user Id.</returns>
    public async Task<(Result Result, string? UserId)> CreateUserAsync(
        UserCredentials credentials, UserFullName fullName, UserStudentDetails? studentDetails = null)
    {
        if (await _userManager.FindByEmailAsync(credentials.Email) is not null)
        {
            return (Result.Failure(InfrastructureErrors.Identity.EmailAlreadyInUse), null);
        }

        var baseUsername = $"{fullName.FirstName}{fullName.LastName}";

        var username = await GenerateUniqueUsernameWithNumber(baseUsername, 1000, 9999);

        var user = new ApplicationUser
        {
            UserName = username,
            Email = credentials.Email,
            FirstName = fullName.FirstName,
            LastName = fullName.LastName,
            Patronymic = fullName.Patronymic,
            StudentGroupName = studentDetails?.StudentGroupName,
            EnrolmentYear = studentDetails?.EnrolmentYear,
        };

        var result = await _userManager.CreateAsync(user, credentials.Password);

        var userRole = (studentDetails is null)
            ? Roles.Teacher
            : Roles.Student;

        await _userManager.AddToRoleAsync(user, userRole);

        return (result.ToApplicationResult(), user.Id);
    }

    private async Task<string> GenerateUniqueUsernameWithNumber(string baseUsername, int min, int max)
    {
        var rng = new Random();
        string username;

        do
        {
            var randomNumber = rng.Next(min, max + 1);
            username = $"{baseUsername}{randomNumber}";
        }
        while (await _userManager.FindByNameAsync(username) != null);

        return username;
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

    public async Task<(Result Result, string JwtToken)> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return (Result.Failure(InfrastructureErrors.Identity.UserNotFoundByEmail), string.Empty);
        }

        var isSuccess = await _userManager.CheckPasswordAsync(user, password);

        if (!isSuccess)
        {
            return (Result.Failure(InfrastructureErrors.Identity.InvalidCredentials), string.Empty);
        }

        var roles = await _userManager.GetRolesAsync(user);

        var tokenString = _jwtTokenGenerator.GenerateTokenString(user.Id, email, roles.FirstOrDefault()!);

        return (Result.Success(), tokenString);
    }
}