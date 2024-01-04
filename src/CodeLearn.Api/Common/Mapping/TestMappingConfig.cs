using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Contracts.Tests;
using CodeLearn.Domain.Tests;

namespace CodeLearn.Api.Common.Mapping;

public class TestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestRequest, CreateTestCommand>()
            .Map(dest => dest.TeacherId.Value, src => src.TeacherId);

        config.NewConfig<Test, TestResponse>()
            .Map(dest => dest.TestId, src => src.Id.Value)
            .Map(dest => dest.TeacherId, src => src.TeacherId.Value)
            .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime.ToISO8601())
            .Map(dest => dest.ModifiedDateTime, src => src.ModifiedDateTime.ToISO8601());
    }
}