using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Application.Tests.Commands.UpdateTest;
using CodeLearn.Contracts.Tests;
using CodeLearn.Domain.Tests;

namespace CodeLearn.Api.Common.Mapping;

public class TestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestRequest, CreateTestCommand>();

        config.NewConfig<Test, TestResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<(int Id, UpdateTestDetailsRequest Request), UpdateTestCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest, src => src.Request);
    }
}