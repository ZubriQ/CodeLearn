using CodeLearn.Application.Common.IdentityModels;
using CodeLearn.Application.Users.Commands.Login;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Contracts.Users;

namespace CodeLearn.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterStudentRequest, RegisterStudentCommand>()
            .ConstructUsing(src => new RegisterStudentCommand(
                UserFullName.Create(src.FirstName, src.LastName, src.Patronymic),
                UserStudentDetails.Create(src.StudentGroupName, src.UserCode)));

        //config.NewConfig<RegisterTeacherRequest, RegisterTeacherCommand>()
        //    .ConstructUsing(src => new RegisterTeacherCommand(
        //        UserFullName.Create(src.FirstName, src.LastName, src.Patronymic)));

        config.NewConfig<LoginRequest, LoginCommand>();
    }
}