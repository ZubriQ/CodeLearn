using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public record CreateTestingCommand(
    TeacherId TeacherId,
    string Title,
    string Description,
    int DurationInMinutes)
    : IRequest<OneOf<Guid, ValidationFailed>>;