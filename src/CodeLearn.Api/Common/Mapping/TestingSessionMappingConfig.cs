using CodeLearn.Application.TestingSessions.Commands.CreateTestingSession;
using CodeLearn.Contracts.TestingSessions;

namespace CodeLearn.Api.Common.Mapping;

public class TestingSessionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TestingSessionRequest, CreateTestingSessionCommand>();
    }
}