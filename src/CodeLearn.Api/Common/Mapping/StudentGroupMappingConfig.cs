using CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;
using CodeLearn.Application.StudentGroups.Commands.UpdateStudentGroup;
using CodeLearn.Contracts.StudentGroups;
using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Api.Common.Mapping;

public class StudentGroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StudentGroupRequest, CreateStudentGroupCommand>();

        config.NewConfig<(int Id, StudentGroupRequest Request), UpdateStudentGroupCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<StudentGroup, StudentGroupResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}