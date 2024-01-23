namespace CodeLearn.Application.Tests.Commands.CreateTest;

public record CreateTestCommand(
    string Title,
    string Description,
    int DurationInMinutes)
    : IRequest<OneOf<Guid, ValidationFailed>>;