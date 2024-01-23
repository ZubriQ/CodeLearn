using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Contracts.Tests;
using CodeLearn.Domain.Tests;

namespace CodeLearn.Api.Common.Mapping;

public class TestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestRequest, CreateTestCommand>();

        config.NewConfig<Test, TestResponse>()
            .Map(dest => dest.TestId, src => src.Id.Value);
    }
}