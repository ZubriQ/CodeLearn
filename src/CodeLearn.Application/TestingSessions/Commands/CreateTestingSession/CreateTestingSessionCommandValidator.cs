namespace CodeLearn.Application.TestingSessions.Commands.CreateTestingSession;

public class CreateTestingSessionCommandValidator : AbstractValidator<CreateTestingSessionCommand>
{
    public CreateTestingSessionCommandValidator()
    {
        RuleFor(x => x.TestingId)
            .GreaterThan(0);
    }
}