using CodeLearn.Domain.TestingSessions;

namespace CodeLearn.Application.TestingSessions.Queries.GetAllMyTestingSessions;

/// <summary>
/// Used to get testing sessions for Student.
/// </summary>
public record GetAllMyTestingSessionsQuery(string UserId) : IRequest<OneOf<TestingSession[], ValidationFailed, NotFound>>;

public class GetAllMyTestingSessionsQueryHandler(
    IApplicationDbContext _context,
    IValidator<GetAllMyTestingSessionsQuery> _validator,
    IIdentityService _identityService)
    : IRequestHandler<GetAllMyTestingSessionsQuery, OneOf<TestingSession[], ValidationFailed, NotFound>>
{
    public async Task<OneOf<TestingSession[], ValidationFailed, NotFound>> Handle(GetAllMyTestingSessionsQuery request, CancellationToken cancellationToken)
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

        var testingSessions = await _context.TestingSessions
            .AsNoTracking()
            .Where(x => x.CreatedBy == request.UserId)
            .ToArrayAsync(cancellationToken);

        return testingSessions.Length == 0 ? [] : testingSessions;
    }
}