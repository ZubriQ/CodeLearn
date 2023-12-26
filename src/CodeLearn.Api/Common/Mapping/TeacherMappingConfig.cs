using CodeLearn.Contracts.Teachers;
using CodeLearn.Domain.Teachers;

namespace CodeLearn.Api.Common.Mapping;

public class TeacherMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Teacher, TeacherResponse>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}