using CodeLearn.Application.Common.IdentityModels;
using CodeLearn.Application.Users;
using CodeLearn.Application.Users.Commands.Login;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Contracts.Users;

namespace CodeLearn.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, StudentResponse>()
            .Map(dest => dest.FullName, src => $"{src.LastName} {src.FirstName} {src.Patronymic}".Trim());

        config.NewConfig<RegisterStudentRequest, RegisterStudentCommand>()
            .ConstructUsing(src => new RegisterStudentCommand(
                UserFullName.Create(src.FirstName, src.LastName, src.Patronymic),
                StudentUserDetails.Create(src.StudentGroupName, src.UserCode)));

        //config.NewConfig<RegisterTeacherRequest, RegisterTeacherCommand>()
        //    .ConstructUsing(src => new RegisterTeacherCommand(
        //        UserFullName.Create(src.FirstName, src.LastName, src.Patronymic)));

        config.NewConfig<LoginRequest, LoginCommand>();
    }
}