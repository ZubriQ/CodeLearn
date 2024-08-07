﻿using CodeLearn.Application.Common.Models;

namespace CodeLearn.Infrastructure.Identity;

public static class ApplicationUserExtensions
{
    public static UserDto ToDto(this ApplicationUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Patronymic = user.Patronymic,
            UserName = user.UserName!,
            WindowsAccountName = user.WindowsAccountName,
            StudentGroupName = user.StudentGroupName,
            UserCode = user.UserCode,
            TemporaryPassword = user.TemporaryPassword,
        };
    }
}