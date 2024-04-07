using CodeLearn.Domain.TestingSessions;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.TestingSessions.Queries.GetTestingSessionById;

public record GetTestingSessionByIdQuery(int Id) : IRequest<OneOf<TestingSession, NotFound>>;

public class GetTestingSessionByIdQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetTestingSessionByIdQuery, OneOf<TestingSession, NotFound>>
{
    public async Task<OneOf<TestingSession, NotFound>> Handle(GetTestingSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var testingSession = await _context.TestingSessions
            .FirstOrDefaultAsync(t => t.Id == TestingSessionId.Create(request.Id), cancellationToken);

        if (testingSession is null)
        {
            return new NotFound();
        }

        return testingSession;
    }
}