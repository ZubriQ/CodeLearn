namespace CodeLearn.Application.TestingSessions.Commands.FinishTestingSession;

public class FinishTestingSessionCommandValidator : AbstractValidator<FinishTestingSessionCommand>
{
    public FinishTestingSessionCommandValidator()
    {
        RuleFor(x => x.TestingSessionId)
            .GreaterThan(0);

        RuleFor(x => x.StudentFeedback)
            .NotNull();
    }
}