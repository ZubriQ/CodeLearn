﻿using CodeLearn.Application.Common.Models;
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

    Task<(Result Result, string? UserId)> CreateStudentUserAsync(RegisterStudentDto studentDto);

    Task<(Result Result, string? UserId)> CreateTeacherUserAsync(string firstName, string lastName, string patronymic);

    Task<Result> AddStudentUsersFromDtoAsync(ImportedStudentDto[] students, string studentGroup);

    Task<(Result Result, TokensDto? TokensDto)> Login(string userName, string password);

    Task<(Result Result, TokensDto? TokensDto)> RefreshToken(string jwtToken, string refreshToken);

    Task<string?> GetStudentGroupNameByUsernameAsync(string userName);
}