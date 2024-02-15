using CodeLearn.Contracts.StudentGroups;
using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Api.Common.Mapping;

public class StudentGroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StudentGroup, StudentGroupResponse>()
            .Map(dest => dest.id, src => src.Id.Value);
    }
}