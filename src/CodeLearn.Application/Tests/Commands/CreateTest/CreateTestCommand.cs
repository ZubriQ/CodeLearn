namespace CodeLearn.Application.Tests.Commands.CreateTest;

public record CreateTestCommand(
    string Title,
    string Description)
    : IRequest<OneOf<Guid, ValidationFailed>>;