﻿using CodeLearn.Domain.Testings.Enums;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Domain.TestingSessions;

namespace CodeLearn.Application.TestingSessions.Commands.CreateTestingSession;

public record CreateTestingSessionCommand(int TestingId, string UserId) : IRequest<OneOf<int, ValidationFailed, NotFound, Conflict>>;

public class CreateTestingSessionCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateTestingSessionCommand> _validator)
    : IRequestHandler<CreateTestingSessionCommand, OneOf<int, ValidationFailed, NotFound, Conflict>>
{
    public async Task<OneOf<int, ValidationFailed, NotFound, Conflict>> Handle(CreateTestingSessionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var testing = await _context.Testings
            .FirstOrDefaultAsync(x => x.Id == TestingId.Create(request.TestingId), cancellationToken);

        if (testing is null)
        {
            return new NotFound();
        }

        if (testing.Status == TestingStatus.Completed || testing.DeadlineDate < DateTimeOffset.UtcNow)
        {
            return new ValidationFailed(ApplicationErrors.TestingSessions.SessionFinishDateTimeInFuture);
        }

        var testingSessionExists = await _context.TestingSessions
            .AnyAsync(x => x.TestingId == TestingId.Create(request.TestingId) && x.CreatedBy == request.UserId,
                           cancellationToken);

        if (testingSessionExists)
        {
            return new Conflict();
        }

        var currentDateTime = DateTimeOffset.UtcNow;

        var finishDateTime = currentDateTime.AddMinutes(testing.DurationInMinutes);

        var testingSession = TestingSession.Create(TestingId.Create(request.TestingId), currentDateTime, finishDateTime);

        await _context.TestingSessions.AddAsync(testingSession, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return testingSession.Id.Value;
    }
}