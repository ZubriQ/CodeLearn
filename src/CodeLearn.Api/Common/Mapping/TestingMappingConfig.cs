using CodeLearn.Application.Testings.Commands.CreateTesting;
using CodeLearn.Contracts.Testings;
using CodeLearn.Domain.Testings;

namespace CodeLearn.Api.Common.Mapping;

public class TestingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestingRequest, CreateTestingCommand>()
            .Map(dest => dest.TeacherId.Value, src => src.TeacherId);

        config.NewConfig<Testing, TestingResponse>()
            .Map(dest => dest.TestingId, src => src.Id.Value)
            .Map(dest => dest.TeacherId, src => src.TeacherId.Value);
    }
}