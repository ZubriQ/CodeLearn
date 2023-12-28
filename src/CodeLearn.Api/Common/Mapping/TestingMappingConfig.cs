using CodeLearn.Application.Testings.Commands.CreateTesting;
using CodeLearn.Contracts.Testings;

namespace CodeLearn.Api.Common.Mapping;

public class TestingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestingRequest, CreateTestingCommand>()
            .Map(dest => dest.TeacherId.Value, src => src.TeacherId);
    }
}