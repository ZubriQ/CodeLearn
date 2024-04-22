using CodeLearn.Application.TestingSessions.Commands.CreateTestingSession;
using CodeLearn.Application.TestingSessions.Queries.GetAllMyTestingSessions;
using CodeLearn.Contracts.TestingSessions;
using CodeLearn.Domain.TestingSessions;

namespace CodeLearn.Api.Common.Mapping;

public class TestingSessionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestingSessionRequest, CreateTestingSessionCommand>();

        config.NewConfig<TestingSession, TestingSessionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.TestingId, src => src.TestingId.Value)
            .Map(dest => dest.Status, src => src.Status.ToString());

        config.NewConfig<StudentTestingSessionDto, StudentTestingSessionResponse>()
            .Map(dest => dest.Status, src => src.Status.ToString());
    }
}