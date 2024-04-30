using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.TestingSessions.Commands.FinishTestingSession;

public record FinishTestingSessionCommand(int TestingSessionId, string StudentFeedback) : IRequest<OneOf<Success, ValidationFailed, NotFound>>;

public class FinishTestingSessionCommandHandler(IApplicationDbContext _context, IValidator<FinishTestingSessionCommand> _validator)
    : IRequestHandler<FinishTestingSessionCommand, OneOf<Success, ValidationFailed, NotFound>>
{
    public async Task<OneOf<Success, ValidationFailed, NotFound>> Handle(FinishTestingSessionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var testingSession = await _context.TestingSessions
            .FirstOrDefaultAsync(x => x.Id == TestingSessionId.Create(request.TestingSessionId), cancellationToken);

        if (testingSession is null)
        {
            return new NotFound();
        }

        if (!string.IsNullOrEmpty(testingSession.StudentFeedback))
        {
            return new ValidationFailed(ApplicationErrors.TestingSessions.SessionAlreadyHasFeedback);
        }

        testingSession.FinishTestingSession(request.StudentFeedback);

        await _context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}