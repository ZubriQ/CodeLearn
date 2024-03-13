using CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;
using CodeLearn.Contracts.StudentGroups;
using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Api.Common.Mapping;

public class StudentGroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StudentGroupRequest, CreateStudentGroupCommand>();

        config.NewConfig<StudentGroup, StudentGroupResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}