using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Testings.ValueObjects;

namespace CodeLearn.Application.Testings.Queries.GetTestingById;

public class GetTestingByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTestingByIdQuery, OneOf<Testing, NotFound>>
{
    public async Task<OneOf<Testing, NotFound>> Handle(GetTestingByIdQuery query, CancellationToken cancellationToken)
    {
        var testing = await context.Testings
            .AsNoTracking()
            .Where(t => t.Id == TestingId.Create(query.TestingId))
            .FirstOrDefaultAsync(cancellationToken);
        if (testing is null)
        {
            return new NotFound();
        }

        return testing;
    }
}