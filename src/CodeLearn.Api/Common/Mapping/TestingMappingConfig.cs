using CodeLearn.Application.Testings.Queries.GetAllTestings;
using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Contracts.Testings;
using CodeLearn.Domain.Testings;

namespace CodeLearn.Api.Common.Mapping;

public class TestingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestingRequest, CreateTestCommand>();

        config.NewConfig<TestingDto, TestingResponse>();

        config.NewConfig<Testing, TestingResponseForTestingSession>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.TestId, src => src.TestId.Value);
    }
}