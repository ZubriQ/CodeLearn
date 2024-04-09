using CodeLearn.Application.Users;
using CodeLearn.Contracts.Users;

namespace CodeLearn.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, StudentResponse>()
            .Map(dest => dest.FullName, src => $"{src.LastName} {src.FirstName} {src.Patronymic}".Trim());
    }
}