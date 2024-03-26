using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Contracts.Testings;

namespace CodeLearn.Api.Common.Mapping;

public class TestingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestingRequest, CreateTestCommand>();
    }
}