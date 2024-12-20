﻿using CodeLearn.Application.Common.Models;
using CodeLearn.Application.Users.Commands.Login;
using CodeLearn.Application.Users.Commands.RefreshToken;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Contracts.Users.Auth;
using CodeLearn.Contracts.Users.Students;
using CodeLearn.Contracts.Users.Teachers;

namespace CodeLearn.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, StudentResponse>()
            .Map(dest => dest.FullName, src => $"{src.LastName} {src.FirstName} {src.Patronymic}".Trim());

        config.NewConfig<UserDto, TeacherResponse>()
            .Map(dest => dest.FullName, src => $"{src.LastName} {src.FirstName} {src.Patronymic}".Trim());

        config.NewConfig<RegisterStudentRequest, RegisterStudentCommand>()
            .Map(dest => dest.Student, src => src);

        config.NewConfig<LoginRequest, LoginCommand>();

        config.NewConfig<TokensDto, LoginResponse>();

        config.NewConfig<RefreshTokenRequest, RefreshTokenCommand>();

        config.NewConfig<TokensDto, RefreshTokenResponse>();
    }
}