namespace CodeLearn.Application.TestingSessions.Queries.GetAllMyTestingSessions;

/// <summary>
/// Used to get testing sessions for Student.
/// </summary>
public record GetAllMyTestingSessionsQuery(string UserId) : IRequest<OneOf<StudentTestingSessionDto[], ValidationFailed, NotFound>>;

public class GetAllMyTestingSessionsQueryHandler(
    IApplicationDbContext _context,
    IValidator<GetAllMyTestingSessionsQuery> _validator,
    IIdentityService _identityService)
    : IRequestHandler<GetAllMyTestingSessionsQuery, OneOf<StudentTestingSessionDto[], ValidationFailed, NotFound>>
{
    public async Task<OneOf<StudentTestingSessionDto[], ValidationFailed, NotFound>> Handle(GetAllMyTestingSessionsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var userExists = await _identityService.UserExistsAsync(request.UserId);

        if (!userExists)
        {
            return new NotFound();
        }

        var testingSessionsDto = await _context.TestingSessions
            .AsNoTracking()
            .Where(x => x.CreatedBy == request.UserId)
            .Join(_context.Testings,
                  ts => ts.TestingId,
                  t => t.Id,
                  (ts, t) => new { TestingSession = ts, Testing = t })
            .Join(_context.Tests,
                  ts => ts.Testing.TestId,
                  t => t.Id,
                  (ts, t) => new StudentTestingSessionDto
                  {
                      Id = ts.TestingSession.Id.Value,
                      TestingId = ts.TestingSession.TestingId.Value,
                      TestId = ts.Testing.TestId.Value,
                      TestTitle = t.Title,
                      Status = ts.TestingSession.Status,
                      StartDateTime = ts.TestingSession.StartDateTime,
                      FinishDateTime = ts.TestingSession.FinishDateTime,
                      Score = ts.TestingSession.Score
                  })
            .OrderByDescending(x => x.FinishDateTime)
            .ToArrayAsync(cancellationToken);

        return testingSessionsDto.Length == 0 ? [] : testingSessionsDto;
    }
}