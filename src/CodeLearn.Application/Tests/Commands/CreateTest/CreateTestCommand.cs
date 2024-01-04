using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Tests.Commands.CreateTest;

public record CreateTestCommand(
    TeacherId TeacherId,
    string Title,
    string Description,
    int DurationInMinutes)
    : IRequest<OneOf<Guid, ValidationFailed>>;